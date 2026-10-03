<#
    Starts the ZAFMC web app in IIS Express (in its own window) and opens the
    Login page in your normal default browser.

    Usage:
        .\run-zafmc.ps1                # start IIS Express and open the default browser
        .\run-zafmc.ps1 -Port 8091     # use a different port
        .\run-zafmc.ps1 -NoBrowser     # start IIS Express only (used for automated testing)
#>
param(
    [int]$Port = 0,
    [switch]$NoBrowser
)

$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'
$siteRoot = $PSScriptRoot

# Port: -Port, else the port in the project's IISUrl, else 8091
if ($Port -le 0) {
    $Port = 8091
    $csproj = Join-Path $siteRoot 'ZAFMC.csproj'
    if (Test-Path $csproj) {
        $m = Select-String -Path $csproj -Pattern '<IISUrl>\s*https?://[^:/<]+:(\d+)' | Select-Object -First 1
        if ($m) { $Port = [int]$m.Matches[0].Groups[1].Value }
    }
}

$url = "http://localhost:$Port"
$startPage = "$url/Default.aspx"

function Test-Site {
    try {
        $r = Invoke-WebRequest -Uri $startPage -UseBasicParsing -TimeoutSec 5
        return ($r.StatusCode -eq 200)
    } catch { return $false }
}

if (Test-Site) {
    Write-Host "ZAFMC is already running at $url"
} else {
    $iisExpress = Join-Path ${env:ProgramFiles} 'IIS Express\iisexpress.exe'
    if (-not (Test-Path $iisExpress)) { $iisExpress = Join-Path ${env:ProgramFiles(x86)} 'IIS Express\iisexpress.exe' }
    if (-not (Test-Path $iisExpress)) { throw 'IIS Express was not found. Install it or run the project from Visual Studio.' }

    # Own console window, so it keeps running after this script ends (close that window or press Q in it to stop)
    $proc = Start-Process -FilePath $iisExpress -ArgumentList "/path:`"$siteRoot`"", "/port:$Port" -PassThru
    Write-Host "IIS Express started (PID $($proc.Id)) at $url"

    # Wait until the Login page answers (first request compiles the site)
    $deadline = (Get-Date).AddSeconds(90)
    while (-not (Test-Site)) {
        if ($proc.HasExited) { throw "IIS Express exited (code $($proc.ExitCode)). Is port $Port already in use?" }
        if ((Get-Date) -gt $deadline) { throw "ZAFMC did not respond at $startPage" }
        Start-Sleep -Seconds 1
    }
}

if ($NoBrowser) {
    Write-Host "Ready: $startPage"
} else {
    # Opening the URL directly uses the system default browser (a normal, visible window)
    Start-Process $startPage
}
