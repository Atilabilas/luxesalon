param([switch]$NoBuild)

$repoPath = Split-Path -Parent $PSScriptRoot
$previewPath = Join-Path $repoPath '.preview'
$previewDatabase = Join-Path $previewPath 'LuxeSalon.preview.db'
New-Item -ItemType Directory -Path $previewPath -Force | Out-Null
if (-not (Test-Path -LiteralPath $previewDatabase)) {
    Copy-Item -LiteralPath (Join-Path $repoPath 'LuxeSalon.Web\App_Data\LuxeSalon.db') -Destination $previewDatabase
}

$previewVariables = @{
    'ConnectionStrings__DefaultConnection' = "Data Source=$previewDatabase"
    'ASPNETCORE_ENVIRONMENT' = 'Development'
    'Preview__EphemeralKeys' = 'true'
    'Logging__EventLog__LogLevel__Default' = 'None'
    'Logging__LogLevel__Microsoft.AspNetCore.DataProtection' = 'None'
    'Logging__LogLevel__Default' = 'Warning'
}
$previousVariables = @{}
foreach ($name in $previewVariables.Keys) {
    $previousVariables[$name] = [Environment]::GetEnvironmentVariable($name, 'Process')
    [Environment]::SetEnvironmentVariable($name, $previewVariables[$name], 'Process')
}

try {
    $previewArguments = @('run', '--no-launch-profile', '--project', (Join-Path $repoPath 'LuxeSalon.Web'), '--urls', 'http://localhost:5100')
    if ($NoBuild) { $previewArguments += '--no-build' }
    & dotnet @previewArguments
}
finally {
    foreach ($name in $previousVariables.Keys) {
        [Environment]::SetEnvironmentVariable($name, $previousVariables[$name], 'Process')
    }
}
