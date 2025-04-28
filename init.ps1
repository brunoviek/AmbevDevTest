$certificatePath = "./aspnetapp.pfx"
$certificatePassword = "projetinho"

if (-Not (Test-Path $certificatePath)) {
    dotnet dev-certs https --export-path $certificatePath --password $certificatePassword
}

docker-compose up --build -d

Write-Host "Environment is ready."
