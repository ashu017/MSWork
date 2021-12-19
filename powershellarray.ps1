$loc = "northeurope", "eastus"


foreach ($location in $loc){
	$databaseName = "db-pipelinemanager-prod-$location"
	$server = "sql-pipelinemanager-prod-$location-primary"
	Write-Output "Env variables: Location=$location; DB=$databaseName; Serv=$server"
}