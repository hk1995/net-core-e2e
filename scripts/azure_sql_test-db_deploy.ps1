param([string]$resourceGroupName, [string]$sqlServerName,  [string]$databaseName, [string]$databaseEdition)

Write-Host "Creating a '$databaseEdition' edition database, '$databaseName' on SQL Server '$sqlServerName' in Resource Group '$resourceGroupName'."
$currentDatabase = New-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
 -ServerName $sqlServerName -DatabaseName $databaseName `
 -Edition $databaseEdition 

Write-Host "'$databaseName' database created."