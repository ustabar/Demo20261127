[CmdletBinding()]
param(
    [switch]$Force,
    [switch]$Preview
)

$ErrorActionPreference = "Stop"

function Resolve-GitPath {
    $gitCmd = Get-Command git -ErrorAction SilentlyContinue
    if ($gitCmd) {
        return $gitCmd.Source
    }

    $candidates = @(
        "C:\Users\barisu\AppData\Local\github-copilot-git-2.56.0-1\cmd\git.exe",
        "C:\Users\barisu\AppData\Local\github-copilot-git-2.53.0-4\cmd\git.exe",
        "C:\Users\barisu\AppData\Local\github-copilot-git-2.53.0-3\cmd\git.exe",
        "C:\Program Files\Git\cmd\git.exe"
    )

    foreach ($candidate in $candidates) {
        if (Test-Path $candidate) {
            return $candidate
        }
    }

    throw "git executable not found. Install Git or add git.exe to PATH."
}

$git = Resolve-GitPath
$repoRoot = (& $git rev-parse --show-toplevel 2>$null).Trim()
if (-not $repoRoot) {
    throw "Current directory is not inside a git repository."
}

$repoName = Split-Path -Leaf $repoRoot
if (Test-Path (Join-Path $repoRoot "Demo-Repo")) {
    $targetRel = "Demo-Repo"
    $targetAbs = Join-Path $repoRoot $targetRel
    $preserve1 = "Demo-Repo/scripts/Reset-DemoWorkspace.ps1"
    $preserve2 = "Demo-Repo/scripts/Reset-DemoWorkspace.bat"
}
elseif ($repoName -ieq "Demo-Repo") {
    $targetRel = "."
    $targetAbs = $repoRoot
    $preserve1 = "scripts/Reset-DemoWorkspace.ps1"
    $preserve2 = "scripts/Reset-DemoWorkspace.bat"
}
else {
    throw "Could not locate Demo-Repo path from repository root: $repoRoot"
}

Write-Host "Repository root: $repoRoot"
Write-Host "Reset target:    $targetAbs"
Write-Host ""

if ($Preview) {
    Write-Host "Tracked changes under target:"
    & $git -C $repoRoot status --short -- $targetRel
    Write-Host ""
    Write-Host "Untracked/ignored items that would be removed:"
    & $git -C $repoRoot clean -ndx -e $preserve1 -e $preserve2 -- $targetRel
    exit 0
}

if (-not $Force) {
    Write-Host "This will discard local changes and delete untracked files under:"
    Write-Host "  $targetAbs"
    $answer = Read-Host "Type RESET to continue"
    if ($answer -cne "RESET") {
        Write-Host "Cancelled."
        exit 1
    }
}

Write-Host "Resetting tracked files..."
& $git -C $repoRoot restore --source=HEAD --staged --worktree -- $targetRel

Write-Host "Cleaning untracked/ignored files..."
& $git -C $repoRoot clean -fdx -e $preserve1 -e $preserve2 -- $targetRel

Write-Host ""
Write-Host "Demo workspace reset complete."
Write-Host "Tip: Run with -Preview to see what would be cleaned before applying."
