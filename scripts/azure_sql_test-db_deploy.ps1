$creds = new-object System.Management.Automation.PSCredential("trevor-admin",("U{%k?aQ'J23/FBhM" | ConvertTo-SecureString -asPlainText -Force))
$context = New-AzureSqlDatabaseServerContext -ServerName val -Credential $creds
New-AzureSqlDatabase -Context $context -DatabaseName bing-bong