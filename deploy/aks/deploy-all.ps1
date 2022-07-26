Param(
    [parameter(Mandatory=$false)][string]$externalDns,
    [parameter(Mandatory=$false)][string]$appName="dav08649",
    [parameter(Mandatory=$false)][bool]$deployCharts=$true,
    [parameter(Mandatory=$false)][bool]$deployGateways=$true,
    [parameter(Mandatory=$false)][bool]$deployData=$true,
    [parameter(Mandatory=$false)][bool]$clean=$true,
    [parameter(Mandatory=$false)][string]$aksName="",
    [parameter(Mandatory=$false)][string]$aksRg="",
    [parameter(Mandatory=$false)][string]$imageTag="latest",
    [parameter(Mandatory=$false)][bool]$useLocal=$true,
    [parameter(Mandatory=$false)][string][ValidateSet('Always','IfNotPresent','Never', IgnoreCase=$false)]$imagePullPolicy="Always",
    [parameter(Mandatory=$false)][string]$chartsToDeploy="*"
    )

Write-Host "Use local: $useLocal" -ForegroundColor Green

function Install-Chart {
    Param([string]$chart,[string]$initialOptions)
    $options=$initialOptions

    $command = "install $appName-$chart $options $chart"
    Write-Host "Helm Command: helm $command" -ForegroundColor Gray
    Invoke-Expression 'cmd /c "helm $command"'
}

$dns = $externalDns

$ingressValuesFile="ingress_values.yaml"

if ($useLocal -eq $true) {
    $ingressValuesFile="ingress_values_dockerk8s.yaml"
    $dns="localhost"
}

if ($externalDns -eq "aks") {
    if  ([string]::IsNullOrEmpty($aksName) -or [string]::IsNullOrEmpty($aksRg)) {
        Write-Host "Error: When using -dns aks, MUST set -aksName and -aksRg too." -ForegroundColor Red
        exit 1
    }
    Write-Host "Getting DNS of AKS of AKS $aksName (in resource group $aksRg)..." -ForegroundColor Green
    $dns = $(az aks show -n $aksName  -g $aksRg --query addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName)
    if ([string]::IsNullOrEmpty($dns)) {
        Write-Host "Error getting DNS of AKS $aksName (in resource group $aksRg). Please ensure AKS has httpRouting enabled AND Azure CLI is logged & in version 2.0.37 or higher" -ForegroundColor Red
        exit 1
    }
    $dns = $dns -replace '[\"]'
    Write-Host "DNS base found is $dns. Will use $appName.$dns for the app!" -ForegroundColor Green
    $dns = "$appName.$dns"
}

# Initialization & check commands
if ([string]::IsNullOrEmpty($dns)) {
    Write-Host "No DNS specified. Ingress resources will be bound to public ip" -ForegroundColor Yellow
}

if ($clean) {
    $listOfReleases=$(helm ls --filter dav08649 -q)
    if ([string]::IsNullOrEmpty($listOfReleases)) {
        Write-Host "No previous releases found!" -ForegroundColor Green
	} else {
        Write-Host "Previous releases found" -ForegroundColor Green
        Write-Host "Cleaning previous helm releases..." -ForegroundColor Green
        hel uninstall $listOfReleases
        Write-Host "Previous releases deleted" -ForegroundColor Green
	}
}

Write-Host "Begin BusAggregator installation using Helm" -ForegroundColor Green

$data = ("catalogsqldb")
$gateways = ("adapterapigw", "catalogapigw")
$charts = ("catalog-api", "adapter-api", "webstatus", "webspa")

if ($deployData) {
    foreach ($dataItem in $data) {
        Write-Host "Installing data: $dataItem" -ForegroundColor Green
        helm install "$appName-$dataItem" --values app.yaml --values $ingressValuesFile --set app.name=$appName --set "ingress.hosts={$dns}" $dataItem
    }
}

if ($deployCharts) {
    foreach ($chart in $charts) {
        if ($chartsToDeploy -eq "*" -or $chartsToDeploy.Contains($chart)) {
            Write-Host "Installing: $chart" -ForegroundColor Green
            Install-Chart $chart "-f app.yaml -f $ingressValuesFile --set app.name=$appName --set ingress.hosts={$dns} --set image.tag=$imageTag --set image.pullPolicy=$imagePullPolicy"
        }
    }
}

if ($deployGateways) {
    foreach ($gateway in $gateways) {
        if ($chartsToDeploy -eq "*" -or $chartsToDeploy.Contains($gateway)) {
            Write-Host "Installing: $gateway" -ForegroundColor Green
            Install-Chart $gateway "-f app.yaml -f $ingressValuesFile --set app.name=$appName --set ingress.hosts={$dns} --set image.tag=$imageTag --set image.pullPolicy=$imagePullPolicy"
        }
    }
}

Write-Host "helm charts installed." -ForegroundColor Green
