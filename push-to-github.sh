#!/bin/bash
# Push DomumBackend to GitHub
# Usage: ./push-to-github.sh YOUR_USERNAME

if [ -z "$1" ]; then
    echo "Usage: ./push-to-github.sh YOUR_GITHUB_USERNAME"
    echo "Example: ./push-to-github.sh waqar9282"
    exit 1
fi

USERNAME=$1
REPO_NAME="DomumBackend"
REPO_URL="https://github.com/$USERNAME/$REPO_NAME.git"

echo "=========================================="
echo "DomumBackend GitHub Push Script"
echo "=========================================="
echo ""
echo "Repository URL: $REPO_URL"
echo ""

# Check if git is initialized
if [ ! -d ".git" ]; then
    echo "❌ Git not initialized in current directory"
    exit 1
fi

# Remove old remote if exists
echo "Updating remote configuration..."
git remote remove origin 2>/dev/null || true

# Add new remote
git remote add origin "$REPO_URL"

echo "✅ Remote updated"
echo ""

# Display current remote
echo "Current remote:"
git remote -v
echo ""

# Rename branch to main (optional)
read -p "Rename 'master' to 'main'? (y/n) " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    git branch -M main
    echo "✅ Branch renamed to main"
    BRANCH="main"
else
    BRANCH="master"
    echo "ℹ️ Keeping 'master' branch"
fi

echo ""
echo "Pushing to GitHub..."
echo ""

# Push to GitHub
git push -u origin "$BRANCH"

if [ $? -eq 0 ]; then
    echo ""
    echo "=========================================="
    echo "✅ SUCCESS! Repository pushed to GitHub"
    echo "=========================================="
    echo ""
    echo "Repository URL: $REPO_URL"
    echo "Branch: $BRANCH"
    echo ""
else
    echo "❌ Push failed. Please check your credentials and try again."
    exit 1
fi
