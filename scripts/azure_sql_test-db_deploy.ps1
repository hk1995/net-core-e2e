param([string]$resourceGroupName, [string]$sqlServerName,  [string]$databaseName, [string]$databaseEdition)

Write-Host "Determining if database already exists.."
$currentDatabase = Get-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
 -ServerName $sqlServerName -DatabaseName $databaseName -erroraction 'silentlycontinue'
if(!$currentDatabase)
{
	Write-Host "Database $databaseName does not exist"
	Write-Host "Creating a '$databaseEdition' edition database, '$databaseName' on SQL Server '$sqlServerName' in Resource Group '$resourceGroupName'."
	$currentDatabase = New-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
	-ServerName $sqlServerName -DatabaseName $databaseName `
	-Edition $databaseEdition 
	Write-Host "'$databaseName' database created."
}
else 
{
	Write-Host "Database $databaseName already exists. Exiting..."
}