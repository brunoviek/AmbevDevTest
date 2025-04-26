$databaseHost = "localhost"
$databasePort = "5433"

$certificatePath = "./aspnetapp.pfx"
$certificatePassword = "projetinho"

if (-Not (Test-Path $certificatePath)) {
    dotnet dev-certs https --export-path $certificatePath --password $certificatePassword
}

$dotnetEfInstalled = dotnet tool list -g | Select-String "dotnet-ef"
if (-Not $dotnetEfInstalled) {
    dotnet tool install --global dotnet-ef
}

docker-compose up --build -d

dotnet ef database update --startup-project src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj --project src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj --context DefaultContext

Write-Host "Environment is ready."
