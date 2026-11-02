# Register-ZafmcReportLinks.ps1
#
# Registers the zafmc-pbi: URL protocol for the current Windows user, so the report links on Reports.aspx
# open the matching page in Power BI Desktop on this PC (handled by Open-ZafmcReport.ps1 in this folder).
# Only HKCU\Software\Classes\zafmc-pbi is written: no admin rights, no other users affected.
# Safe to run again (it rewrites the same values). Run again after moving the repo folder.
#
#   powershell -NoProfile -ExecutionPolicy Bypass -File .\Register-ZafmcReportLinks.ps1             # install
#   powershell -NoProfile -ExecutionPolicy Bypass -File .\Register-ZafmcReportLinks.ps1 -Unregister # remove

[CmdletBinding()]
param([switch]$Unregister)

Set-StrictMode -Version 2.0
$ErrorActionPreference = 'Stop'

$key = 'HKCU:\Software\Classes\zafmc-pbi'

if ($Unregister) {
    if (Test-Path -LiteralPath $key) {
        Remove-Item -LiteralPath $key -Recurse -Force
        Write-Output "Removed $key. The report links no longer open Power BI Desktop."
    } else {
        Write-Output "$key does not exist. Nothing to remove."
    }
    return
}

$launcher = Join-Path $PSScriptRoot 'Open-ZafmcReport.ps1'
if (-not (Test-Path -LiteralPath $launcher -PathType Leaf)) { throw "Launcher not found: $launcher" }
$launcher = (Resolve-Path -LiteralPath $launcher).ProviderPath
$powershell = Join-Path $env:SystemRoot 'System32\WindowsPowerShell\v1.0\powershell.exe'
if (-not (Test-Path -LiteralPath $powershell -PathType Leaf)) { throw "Windows PowerShell not found: $powershell" }

# The URL is passed as one quoted argument to -File; the launcher validates it before doing anything.
$command = '"{0}" -NoProfile -NonInteractive -ExecutionPolicy Bypass -WindowStyle Hidden -File "{1}" "%1"' -f $powershell, $launcher

foreach ($path in @($key, "$key\shell", "$key\shell\open", "$key\shell\open\command")) {
    if (-not (Test-Path -LiteralPath $path)) { [void](New-Item -Path $path) }
}
Set-ItemProperty -LiteralPath $key -Name '(default)' -Value 'URL:ZAFMC Power BI report'
[void](New-ItemProperty -LiteralPath $key -Name 'URL Protocol' -Value '' -PropertyType String -Force)
Set-ItemProperty -LiteralPath "$key\shell\open\command" -Name '(default)' -Value $command

Write-Output "Registered zafmc-pbi: links for $env:USERNAME."
Write-Output "Command: $command"
