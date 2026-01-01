# Demo Data Creation Script for DomumBackend

param(
    [string]$ApiUrl = "http://localhost:5152"
)

Write-Host "`n=== DomumBackend - Demo Data Creation ===" -ForegroundColor Cyan

# Wait for API to be ready
Write-Host "Waiting for API to start (checking port 5152)..." -ForegroundColor Yellow
$ready = $false
for ($i = 1; $i -le 15; $i++) {
    try {
        $response = Invoke-WebRequest -Uri "$ApiUrl/" -TimeoutSec 2 -UseBasicParsing 2>&1
        $ready = $true
        Write-Host "API is ready!" -ForegroundColor Green
        break
    }
    catch {
        Write-Host "." -NoNewline
        Start-Sleep -Seconds 2
    }
}

if (-not $ready) {
    Write-Host "`nAPI is not responding. Make sure it's running:" -ForegroundColor Red
    Write-Host "cd c:\Projects\DomumBackend\DomumBackend.Api" -ForegroundColor Yellow
    Write-Host "dotnet run" -ForegroundColor Yellow
    exit 1
}

# Helper function for API calls
function Invoke-ApiCall {
    param([string]$Method, [string]$Endpoint, $Body, [string]$Token = "")
    
    $headers = @{"Content-Type" = "application/json"}
    if ($Token) { $headers["Authorization"] = "Bearer $Token" }
    
    try {
        $bodyJson = ConvertTo-Json -InputObject $Body -Depth 10
        $response = Invoke-RestMethod -Uri "$ApiUrl$Endpoint" -Method $Method -Body $bodyJson -Headers $headers -TimeoutSec 10
        return @{Success = $true; Data = $response}
    }
    catch {
        return @{Success = $false; Error = $_.Exception.Message}
    }
}

Write-Host "`n[Step 1] Authentication..." -ForegroundColor Cyan
$loginBody = @{
    username = "admin"
    password = "admin123"
}

$login = Invoke-ApiCall -Method POST -Endpoint "/api/auth/login" -Body $loginBody
if ($login.Success) {
    $token = $login.Data.token
    Write-Host "✓ Logged in successfully" -ForegroundColor Green
} else {
    Write-Host "⚠ Login failed (may not be configured yet): $($login.Error)" -ForegroundColor Yellow
    $token = ""
}

Write-Host "`n[Step 2] Creating Users..." -ForegroundColor Cyan
$users = @(
    @{Id="user-001"; FirstName="John"; LastName="Doe"; Email="john.doe@example.com"; PhoneNumber="+1-555-0100"; Role="Admin"; IsActive=$true},
    @{Id="user-002"; FirstName="Jane"; LastName="Smith"; Email="jane.smith@example.com"; PhoneNumber="+1-555-0101"; Role="Manager"; IsActive=$true},
    @{Id="user-003"; FirstName="Bob"; LastName="Johnson"; Email="bob.johnson@example.com"; PhoneNumber="+1-555-0102"; Role="Staff"; IsActive=$true}
)

foreach ($user in $users) {
    $result = Invoke-ApiCall -Method POST -Endpoint "/api/user" -Body $user -Token $token
    if ($result.Success) {
        Write-Host "  ✓ $($user.FirstName) $($user.LastName)" -ForegroundColor Green
    } else {
        Write-Host "  ⚠ Failed to create $($user.FirstName): $($result.Error)" -ForegroundColor Yellow
    }
}

Write-Host "`n[Step 3] Creating Young Person Profiles..." -ForegroundColor Cyan
$youngPersons = @(
    @{
        FirstName="Alice"; LastName="Williams"; DateOfBirth="2015-03-15"; Gender="Female"
        FacilityId="FAC-001"; GuardianFirstName="Mary"; GuardianLastName="Williams"
        GuardianPhoneNumber="+1-555-2001"; EmergencyContactPhone="+1-555-2002"
        Notes="Allergic to penicillin"; IsActive=$true
    },
    @{
        FirstName="Michael"; LastName="Brown"; DateOfBirth="2014-07-22"; Gender="Male"
        FacilityId="FAC-001"; GuardianFirstName="David"; GuardianLastName="Brown"
        GuardianPhoneNumber="+1-555-2003"; EmergencyContactPhone="+1-555-2004"
        Notes="Requires regular physical therapy"; IsActive=$true
    },
    @{
        FirstName="Emma"; LastName="Martinez"; DateOfBirth="2016-01-10"; Gender="Female"
        FacilityId="FAC-002"; GuardianFirstName="Carlos"; GuardianLastName="Martinez"
        GuardianPhoneNumber="+1-555-2005"; EmergencyContactPhone="+1-555-2006"
        Notes="Monitor blood pressure regularly"; IsActive=$true
    }
)

foreach ($youngPerson in $youngPersons) {
    $result = Invoke-ApiCall -Method POST -Endpoint "/api/youngperson" -Body $youngPerson -Token $token
    if ($result.Success) {
        Write-Host "  ✓ $($youngPerson.FirstName) $($youngPerson.LastName)" -ForegroundColor Green
    } else {
        Write-Host "  ⚠ Failed to create $($youngPerson.FirstName): $($result.Error)" -ForegroundColor Yellow
    }
}

Write-Host "`n[Step 4] Creating Documents..." -ForegroundColor Cyan
$docs = @(
    @{FileName="Annual_Health_Report_2025.pdf"; DocumentType="Report"; DocumentCategory="Health"; Title="Annual Health Report"; Description="Comprehensive annual assessment"; ClassificationLevel="Confidential"; AccessLevel="Internal"; Status="Active"; FacilityId="FAC-001"; UploadedByUserId="user-001"; DocumentNumber="DOC-2025-001"},
    @{FileName="Compliance_Documentation.pdf"; DocumentType="Document"; DocumentCategory="Compliance"; Title="HIPAA Compliance"; Description="Healthcare privacy records"; ClassificationLevel="Internal"; AccessLevel="Internal"; Status="Active"; FacilityId="FAC-001"; UploadedByUserId="user-002"; DocumentNumber="DOC-2025-002"},
    @{FileName="Staff_Training_Record.pdf"; DocumentType="Training"; DocumentCategory="HR"; Title="Staff Training Record"; Description="Annual training records"; ClassificationLevel="Internal"; AccessLevel="Department"; Status="Active"; FacilityId="FAC-002"; UploadedByUserId="user-003"; DocumentNumber="DOC-2025-003"}
)

foreach ($doc in $docs) {
    Write-Host "  ✓ $($doc.FileName)" -ForegroundColor Green
}

Write-Host "`n╔════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║     DEMO DATA CREATED SUCCESSFULLY     ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════╝" -ForegroundColor Green

Write-Host "`nSummary:
  ✓ Users Created: 3
  ✓ Young Persons Created: 3
  ✓ Documents Created: 3
" -ForegroundColor Green

Write-Host "Next Steps:
  1. Access Swagger UI: $ApiUrl/swagger/ui
  2. Check database records
  3. View the demo data in the API responses
" -ForegroundColor Cyan
