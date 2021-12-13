# Persistence

## Docker MSSQl
docker pull mcr.microsoft.com/mssql/server

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pa$$w0rd' -p 1433:1433 -d mcr.microsoft.com/mssql/server

docker container ls


server: localhost
port: 1433
user: sa
password: Pa$$w0rd




some commands

docker images

docker container ps -l

docker start 09d6bc2c2915

## Code Firts EntityFramework

Add-Migration InitialDatabaseCreate

Add-Migration AddEmailInCustomers

Update-Database