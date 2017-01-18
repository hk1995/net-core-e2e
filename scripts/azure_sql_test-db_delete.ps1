param([string]$resourceGroupName, [string]$sqlServerName,  [string]$databaseName)

Write-Host "Creating database '$databaseName' on SQL Server '$sqlServerName' in Resource Group '$resourceGroupName'."
Remove-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
 -ServerName $sqlServerName -DatabaseName $databaseName -erroraction 'silentlycontinue'

Write-Host "'$databaseName' database deleted."