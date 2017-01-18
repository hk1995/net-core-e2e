Write-Host "Setting credentials"
$resourceGroupName = "VAL"
$sqlServerName = "val"
$databaseName = "bing-bongg"
$databaseEdition = "Basic"

Write-Host "Creating a $databaseEdition database $databaseName on SQL Server $databaseName"
$currentDatabase = New-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
 -ServerName $sqlServerName -DatabaseName $databaseName `
 -Edition $databaseEdition 

Write-Host "databaseName database created"