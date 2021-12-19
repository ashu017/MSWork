$loc = "ukwest", "japaneast", "germanynorth", "southafricawest", "swedencentral", "westus3"

foreach ($location in $loc){
	$server = "sql-pipelinemanager-prod-$location-primary"
	$databaseName = "db-pipelinemanager-prod-$location"
	
	# Write-Output "Env variables: Location=$location; DB=$databaseName; Serv=$server"
	if ([string]::IsNullOrEmpty($location))
	{
		Write-Output "Location is empty. Script will exit."
		exit
	}
	# Connecting to Pipelinemanager database

	$dbConnString = "Server=tcp:$server.database.windows.net,1433;Initial Catalog=$databaseName;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
	Write-Output "Connecting to Server=$server, Database=$databaseName"

	$sqlConnection = New-Object System.Data.SqlClient.SqlConnection

	$sqlConnection.ConnectionString = $dbConnString

	$rawTokenResponse = Get-AzAccessToken -Resource "https://database.windows.net"

	# $jsonTokenResponse = $rawTokenResponse.Content | ConvertFrom-Json

	$sqlConnection.AccessToken = $rawTokenResponse.Token

	$try_count = 0;

	do {
		Write-Output "Connecting to Server=$server, Database=$databaseName"
		try {
			$sqlConnection.Open();
			Write-Output "Successfully connected to Server=$server, Database=$databaseName"
			$sqlCmd = New-Object System.Data.SqlClient.SqlCommand
			$sqlCmd.Connection = $sqlConnection
			$sqlCmd.CommandText = $("SELECT * FROM [dbo].[entity_azure_app_gateway]")	
			$SqlAdapter = New-Object System.Data.SqlClient.SqlDataAdapter
			$SqlAdapter.SelectCommand = $SqlCmd
			$DataSet = New-Object System.Data.DataSet 
			$SqlAdapter.Fill($DataSet) 
			$dataset.Tables[0]
			if($dataset.Tables[0].Rows.Count -eq 0)
			{
				Write-Output "Insert Needed \n \n" 
			}
			break;
		}
		catch {
			Write-Host $_
		}
		Start-Sleep -Seconds 15
		$try_count++;
	} while ($try_count -lt 5)

	if ($sqlConnection.State -eq [System.Data.ConnectionState]::Closed)
	{    
		Write-Output "Unable to connect to Server=$server, Database=$databaseName after 5 tries."
		exit
	}
}