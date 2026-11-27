param(
    [switch]$Hard,
    [string]$Root = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'
$rootPath = (Resolve-Path $Root).Path

if (-not (Test-Path (Join-Path $rootPath 'START-HERE.txt'))) {
    throw "START-HERE.txt not found under '$rootPath'. Run from the demo repository."
}

$removed = 0

$rehearsal = Join-Path $rootPath 'rehearsal'
if (Test-Path $rehearsal) {
    Remove-Item -LiteralPath $rehearsal -Recurse -Force
    $removed++
}

$generatedSarif = Get-ChildItem -Path $rootPath -Filter 'demo-results.sarif' -Recurse -File -ErrorAction SilentlyContinue
foreach ($file in $generatedSarif) {
    Remove-Item -LiteralPath $file.FullName -Force
    $removed++
}

$pythonArtifacts = Get-ChildItem -Path $rootPath -Recurse -Force -Directory -ErrorAction SilentlyContinue |
    Where-Object { $_.Name -eq '__pycache__' }
foreach ($dir in $pythonArtifacts) {
    Remove-Item -LiteralPath $dir.FullName -Recurse -Force
    $removed++
}

$pycFiles = Get-ChildItem -Path $rootPath -Recurse -Force -File -ErrorAction SilentlyContinue |
    Where-Object { $_.Extension -eq '.pyc' }
foreach ($file in $pycFiles) {
    Remove-Item -LiteralPath $file.FullName -Force
    $removed++
}

$gitPath = $null
$gitCmd = Get-Command git -ErrorAction SilentlyContinue
if ($null -ne $gitCmd) {
    $gitPath = $gitCmd.Source
}
elseif (Test-Path 'C:\Program Files\Git\cmd\git.exe') {
    $gitPath = 'C:\Program Files\Git\cmd\git.exe'
}

if ($null -ne $gitPath) {
    $restoreTargets = @('sample', 'overlays')
    $existingTargets = @()
    foreach ($target in $restoreTargets) {
        $treeHit = & $gitPath -C $rootPath ls-tree --name-only HEAD -- $target
        if ($LASTEXITCODE -eq 0 -and -not [string]::IsNullOrWhiteSpace(($treeHit | Out-String))) {
            $existingTargets += $target
        }
    }
    if ($existingTargets.Count -gt 0) {
        & $gitPath -C $rootPath restore --worktree --source=HEAD -- @existingTargets
        if ($LASTEXITCODE -ne 0) {
            throw "git restore failed."
        }
    }

    if ($Hard) {
        & $gitPath -C $rootPath reset --hard HEAD
        if ($LASTEXITCODE -ne 0) {
            throw "git reset --hard failed."
        }
        & $gitPath -C $rootPath clean -fd
        if ($LASTEXITCODE -ne 0) {
            throw "git clean -fd failed."
        }
    }
}
else {
    Write-Warning "git not found; skipped tracked-file reset."
}

if ($Hard) {
    Write-Host "Demo workspace reset complete (hard mode). Removed items: $removed"
}
else {
    Write-Host "Demo workspace reset complete (safe mode). Removed items: $removed"
}
