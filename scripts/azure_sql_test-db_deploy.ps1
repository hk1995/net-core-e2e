Write-Host "Setting credentials"
$creds = new-object System.Management.Automation.PSCredential("trevor-admin", ("U{%k?aQ'J23/FBhM" | ConvertTo-SecureString -asPlainText -Force))

Write-Host "Setting db server context"
$context = New-AzureSqlDatabaseServerContext -ServerName val -Credential $creds

Write-Host "Creating new test database 'bing-bong'"
$currentDatabse = New-AzureSqlDatabase -Context $context -DatabaseName bing-bong -Edition Basic

Write-Host "'bing-bong' deployed. Done"