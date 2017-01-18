Write-Host "Setting credentials"
$serverAdmin = "trevor-admin"
$serverPassword = "U{%k?aQ'J23/FBhM"
$securePassword = ConvertTo-SecureString -String $serverPassword -asPlainText -Force
$creds = new-object System.Management.Automation.PSCredential($serverAdmin, $securePassword)

Write-Host "Setting db context"
$context = New-AzureSqlDatabaseServerContext -ServerName "val" -Credential $creds

Write-Host "Creating new test database 'bing-bong'"
$currentDatabase = New-AzureSqlDatabase -DatabaseName "bing-bong" -Edition "Basic" -ConnectionContext $context

Write-Host "'bing-bong' deployed. Done"