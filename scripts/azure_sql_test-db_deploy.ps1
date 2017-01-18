Select-AzureSubscription -Default Pay-As-You-Go

Write-Host "Setting credentials"
$creds = new-object System.Management.Automation.PSCredential("trevor-admin", ("U{%k?aQ'J23/FBhM"  | ConvertTo-SecureString -asPlainText -Force))

Write-Host "Setting db context"
$context = New-AzureSqlDatabaseServerContext -ServerName "val" -Credential $creds

Get-AzureSqlDatabaseServer

Write-Host "Creating new test database 'bing-bong'"
New-AzureSqlDatabase $context -DatabaseName bing-bong -Edition Basic

Write-Host "'bing-bong' deployed. Done"