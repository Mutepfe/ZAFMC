# Open-ZafmcReport.ps1
#
# Handler for the zafmc-pbi:<PageKey> links on Reports.aspx. Register-ZafmcReportLinks.ps1 registers it
# for the current Windows user. It opens a page of ZAFMCReports.pbip in Power BI Desktop on this PC.
#
# Any web page can start a custom protocol, so the argument is untrusted. Only an exact page name from
# $Pages is accepted (case-insensitive, letters, digits, '_' and '-' only). Anything else exits silently
# before any file is read or written.
#
# The repo project is never changed. The launcher keeps a viewing copy in %LOCALAPPDATA%\ZAFMC\PowerBI,
# copies the repo project over it when the repo project has changed (the copy's .pbi folders, which hold
# the loaded data, are kept), sets the copy's start page and opens the copy in Power BI Desktop.
# If Power BI Desktop already has the copy open, the launcher only brings that window to the front and
# says which page tab to click. It never closes Power BI Desktop.

Set-StrictMode -Version 2.0
$ErrorActionPreference = 'Stop'

$Pages = @(
    'Home', 'Congregation', 'Male', 'Female', 'Children', 'Leadership', 'Administration', 'Deceased',
    'UserLogIn', 'Churches', 'ChurchProvince', 'ChurchDistricts', 'ChurchZones', 'ChurchSection',
    'ChurchPastors', 'ChurchEvangelist', 'ChurchPreachers', 'ChurchProphets', 'ChurchDecons',
    'ChurchMembers', 'Passovers', 'PassBigSunday', 'PassGeneralMeeting', 'PassGraduation', 'PassMemorial',
    'PassWedding', 'PassWomen', 'PassYouth', 'Events', 'MediaNews', 'MediaSermon', 'MediaTestimony',
    'MediaVideo', 'Projects', 'ProjectsCompleted', 'ProjectsInProgress', 'ProjectsOverdue'
)

# ---- 1. Validate the URL (nothing is touched before this passes) ----
if ($args.Count -ne 1 -or $args[0] -isnot [string]) { exit 1 }
$match = [regex]::Match($args[0], '\Azafmc-pbi:(?://)?([A-Za-z0-9_-]{1,64})/?\z',
    [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
if (-not $match.Success) { exit 1 }
$requested = $match.Groups[1].Value
$page = $null
foreach ($p in $Pages) {
    if ([string]::Equals($p, $requested, [StringComparison]::OrdinalIgnoreCase)) { $page = $p; break }
}
if ($null -eq $page) { exit 1 }

# ---- 2. Paths ----
$ProjectItems = @('ZAFMCReports.pbip', 'ZAFMCReports.Report', 'ZAFMCReports.SemanticModel')
$repoDir = $PSScriptRoot
$localAppData = [Environment]::GetFolderPath([Environment+SpecialFolder]::LocalApplicationData)
if ([string]::IsNullOrEmpty($repoDir) -or [string]::IsNullOrEmpty($localAppData)) { exit 2 }
$viewDir = Join-Path $localAppData 'ZAFMC\PowerBI'
$viewPbip = Join-Path $viewDir 'ZAFMCReports.pbip'
$markerFile = Join-Path $viewDir '.zafmc-sync'
$pagesDir = Join-Path $viewDir 'ZAFMCReports.Report\definition\pages'

function Show-Message([string]$text, [System.Windows.Forms.MessageBoxIcon]$icon) {
    $owner = New-Object System.Windows.Forms.Form
    $owner.TopMost = $true
    $owner.ShowInTaskbar = $false
    try {
        [void][System.Windows.Forms.MessageBox]::Show($owner, $text, 'ZAFMC Reports',
            [System.Windows.Forms.MessageBoxButtons]::OK, $icon)
    } finally { $owner.Dispose() }
}

# Power BI Desktop process that has the viewing copy open (from its command line), or $null.
function Get-ViewerProcess {
    if (-not (Get-Process -Name 'PBIDesktop' -ErrorAction SilentlyContinue)) { return $null }
    $procs = @(Get-CimInstance -ClassName Win32_Process -Filter "Name='PBIDesktop.exe'" -OperationTimeoutSec 15 -ErrorAction SilentlyContinue)
    foreach ($proc in $procs) {
        if ($proc.CommandLine -and $proc.CommandLine.IndexOf($viewPbip, [StringComparison]::OrdinalIgnoreCase) -ge 0) {
            return $proc
        }
    }
    return $null
}

# SHA-256 over the relative path and content of every repo project file (not .pbi folders, not .pbix).
function Get-RepoFingerprint {
    $sha = [System.Security.Cryptography.SHA256]::Create()
    try {
        $files = foreach ($item in $ProjectItems) {
            $path = Join-Path $repoDir $item
            if (Test-Path -LiteralPath $path -PathType Leaf) { Get-Item -LiteralPath $path -Force }
            elseif (Test-Path -LiteralPath $path -PathType Container) {
                Get-ChildItem -LiteralPath $path -Recurse -File -Force |
                    Where-Object { $_.FullName -notmatch '\\\.pbi\\' -and $_.Extension -ne '.pbix' }
            }
            else { throw "Missing $path" }
        }
        foreach ($file in ($files | Sort-Object FullName)) {
            $name = [Text.Encoding]::UTF8.GetBytes($file.FullName.Substring($repoDir.Length) + '|')
            [void]$sha.TransformBlock($name, 0, $name.Length, $null, 0)
            $bytes = [IO.File]::ReadAllBytes($file.FullName)
            [void]$sha.TransformBlock($bytes, 0, $bytes.Length, $null, 0)
        }
        [void]$sha.TransformFinalBlock((New-Object byte[] 0), 0, 0)
        return ([BitConverter]::ToString($sha.Hash) -replace '-', '')
    } finally { $sha.Dispose() }
}

function Sync-ViewingCopy {
    $fingerprint = Get-RepoFingerprint
    $current = $null
    if (Test-Path -LiteralPath $markerFile -PathType Leaf) { $current = ([IO.File]::ReadAllText($markerFile)).Trim() }
    if ($current -eq $fingerprint -and (Test-Path -LiteralPath $viewPbip -PathType Leaf) -and
        (Test-Path -LiteralPath (Join-Path $pagesDir 'pages.json') -PathType Leaf)) { return }

    if (-not (Test-Path -LiteralPath $viewDir)) { [void](New-Item -ItemType Directory -Path $viewDir -Force) }
    $common = @('/R:1', '/W:1', '/NFL', '/NDL', '/NJH', '/NJS', '/NP')
    foreach ($item in $ProjectItems) {
        $src = Join-Path $repoDir $item
        if (Test-Path -LiteralPath $src -PathType Leaf) {
            & robocopy.exe $repoDir $viewDir $item @common | Out-Null
        } else {
            # /MIR mirrors the folder; /XD .pbi keeps (and never copies) the .pbi folders with the loaded data
            & robocopy.exe $src (Join-Path $viewDir $item) '/MIR' '/XD' '.pbi' '/XF' '*.pbix' @common | Out-Null
        }
        if ($LASTEXITCODE -ge 8) { throw "Copying $item to $viewDir failed (robocopy exit code $LASTEXITCODE)." }
    }
    [IO.File]::WriteAllText($markerFile, $fingerprint, (New-Object System.Text.UTF8Encoding($false)))
}

function Set-StartPage([string]$name) {
    $pagesJson = Join-Path $pagesDir 'pages.json'
    if (-not (Test-Path -LiteralPath (Join-Path $pagesDir "$name\page.json") -PathType Leaf)) { throw "Page $name is not in the project." }
    $text = [IO.File]::ReadAllText($pagesJson)
    $rx = New-Object System.Text.RegularExpressions.Regex('("activePageName"\s*:\s*")[^"]*(")')
    if (-not $rx.IsMatch($text)) { throw "activePageName not found in $pagesJson" }
    # $name is from $Pages (letters and digits only), so it is safe in JSON and in the replacement pattern
    $updated = $rx.Replace($text, '${1}' + $name + '${2}', 1)
    if ($updated -ne $text) { [IO.File]::WriteAllText($pagesJson, $updated, (New-Object System.Text.UTF8Encoding($false))) }
}

function Find-PowerBIDesktop {
    $programFiles = $env:ProgramW6432
    if ([string]::IsNullOrEmpty($programFiles)) { $programFiles = $env:ProgramFiles }
    $candidates = @(
        (Join-Path $programFiles 'Microsoft Power BI Desktop\bin\PBIDesktop.exe'),
        (Join-Path $localAppData 'Microsoft\WindowsApps\PBIDesktopStore.exe')
    )
    foreach ($c in $candidates) { if (Test-Path -LiteralPath $c -PathType Leaf) { return $c } }
    return $null
}

# ---- 3. Open the page ----
Add-Type -AssemblyName System.Windows.Forms
$mutex = New-Object System.Threading.Mutex($false, 'Local\ZAFMC-PowerBI-Report-Launcher')
$locked = $false
try {
    try { $locked = $mutex.WaitOne(60000) } catch [System.Threading.AbandonedMutexException] { $locked = $true }
    if (-not $locked) { exit 2 }

    $viewer = Get-ViewerProcess
    if ($null -ne $viewer) {
        Add-Type -Namespace ZafmcPbi -Name Win32 -MemberDefinition @'
[DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
[DllImport("user32.dll")] public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
[DllImport("user32.dll")] public static extern bool IsIconic(IntPtr hWnd);
'@
        $handle = (Get-Process -Id $viewer.ProcessId -ErrorAction SilentlyContinue).MainWindowHandle
        if ($handle -and $handle -ne [IntPtr]::Zero) {
            if ([ZafmcPbi.Win32]::IsIconic($handle)) { [void][ZafmcPbi.Win32]::ShowWindowAsync($handle, 9) }
            [void][ZafmcPbi.Win32]::SetForegroundWindow($handle)
        }
        Show-Message ("ZAFMC Reports is already open in Power BI Desktop.`r`n`r`n" +
            "Click the '$page' tab at the bottom of the Power BI Desktop window to see this report.`r`n`r`n" +
            "To have a link open straight on its page, close Power BI Desktop and click the link again.") ([System.Windows.Forms.MessageBoxIcon]::Information)
        exit 0
    }

    $exe = Find-PowerBIDesktop
    if ($null -eq $exe) {
        Show-Message 'Power BI Desktop is not installed on this PC, so the report cannot be opened.' ([System.Windows.Forms.MessageBoxIcon]::Warning)
        exit 2
    }

    Sync-ViewingCopy
    Set-StartPage $page
    Start-Process -FilePath $exe -ArgumentList ('"' + $viewPbip + '"') -WorkingDirectory (Split-Path -Parent $exe)
    exit 0
}
catch {
    Show-Message ("The report could not be opened.`r`n`r`n" + $_.Exception.Message) ([System.Windows.Forms.MessageBoxIcon]::Error)
    exit 2
}
finally {
    if ($locked) { $mutex.ReleaseMutex() }
    $mutex.Dispose()
}
