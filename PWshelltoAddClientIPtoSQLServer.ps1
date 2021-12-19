Enable-AzureRmAlias -Scope CurrentUser

$loc = "germanynorth", "southafricawest", "southindia", "swedencentral", "swedensouth", "westindia", "westus3"

foreach($location in $loc){
	Set-AzContext -SubscriptionId "aub-ms-prod-$location-pipeline-sql"

	$time = [DateTime]::UtcNow.ToString('u').Replace(' ','_').Replace('Z', '')
	$firewallrulename = "ClientIPAddress_$time"
	$clientIp = Invoke-WebRequest 'https://api.ipify.org' | Select-Object -ExpandProperty Content
	$server = "sql-pipelinemanager-prod-$location-primary"
	$rgname = "rg-db-prod-$location"
	Write-Output "$time $firewallrulename $clientIp$server $rgname"
	New-AzureRmSqlServerFirewallRule -ResourceGroupName $rgname -ServerName $server -FirewallRuleName $firewallrulename -StartIpAddress $clientIp -EndIpAddress $clientIp
}