$resourceGroupName = "VAL"
$sqlServerName = "val"

$databaseName = "bing-bong"
$databaseEdition = "Basic"

$currentDatabase = New-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
 -ServerName $sqlServerName -DatabaseName $databaseName `
 -Edition $databaseEdition 