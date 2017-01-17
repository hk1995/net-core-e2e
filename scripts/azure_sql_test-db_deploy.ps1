echo "Setting credentials"
$creds = new-object System.Management.Automation.PSCredential("trevor-admin", ("U{%k?aQ'J23/FBhM" | ConvertTo-SecureString -asPlainText -Force))

echo "Setting db server context"
$context = New-AzureSqlDatabaseServerContext -ServerName val -Credential $creds

echo "Creating new test database 'bing-bong'"
New-AzureSqlDatabase -Context $context -DatabaseName bing-bong

echo "'bing-bong' deployed. Done"