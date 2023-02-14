# Deploy ASP.NET 7 to fly.io

## Requirements

## Configuration

`docker run --name pg-aspnet7-deploy-flyio -e POSTGRES_PASSWORD=mysecretpassword -d postgres`

`flyctl secrets set -a rastrainj-aspnet7-demo ConnectionStrings__PostgresDatabase="Host=XXXXXXXXXXX.internal; Port=5432; Database=aspnet7-deploy-flyio; Username=postgres; Password=XXXXXXXXXX"`