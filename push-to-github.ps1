# DomumBackend - Push to GitHub Script (PowerShell)
# Usage: .\push-to-github.ps1 -Username YOUR_GITHUB_USERNAME
# Example: .\push-to-github.ps1 -Username waqar9282

param(
    [Parameter(Mandatory=$true)]
    [string]$Username
)

$RepoName = "DomumBackend"
$RepoUrl = "https://github.com/$Username/$RepoName.git"

Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "DomumBackend GitHub Push Script" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Repository URL: $RepoUrl" -ForegroundColor Yellow
Write-Host ""

# Check if git is initialized
if (!(Test-Path ".git")) {
    Write-Host "❌ Git not initialized in current directory" -ForegroundColor Red
    exit 1
}

# Remove old remote if exists
Write-Host "Updating remote configuration..." -ForegroundColor Cyan
git remote remove origin 2>$null | Out-Null

# Add new remote
git remote add origin "$RepoUrl"

Write-Host "✅ Remote updated" -ForegroundColor Green
Write-Host ""

# Display current remote
Write-Host "Current remote:" -ForegroundColor Cyan
git remote -v
Write-Host ""

# Ask about branch rename
$rename = Read-Host "Rename 'master' to 'main'? (y/n)"
if ($rename -eq 'y' -or $rename -eq 'Y') {
    git branch -M main
    Write-Host "✅ Branch renamed to main" -ForegroundColor Green
    $branch = "main"
} else {
    Write-Host "ℹ️ Keeping 'master' branch" -ForegroundColor Yellow
    $branch = "master"
}

Write-Host ""
Write-Host "Pushing to GitHub..." -ForegroundColor Cyan
Write-Host ""

# Push to GitHub
git push -u origin $branch

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "==========================================" -ForegroundColor Green
    Write-Host "✅ SUCCESS! Repository pushed to GitHub" -ForegroundColor Green
    Write-Host "==========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Repository URL: $RepoUrl" -ForegroundColor Cyan
    Write-Host "Branch: $branch" -ForegroundColor Cyan
    Write-Host ""
} else {
    Write-Host "❌ Push failed. Please check your credentials and try again." -ForegroundColor Red
    exit 1
}
