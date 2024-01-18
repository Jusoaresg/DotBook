STARTUP_PROJECT = BulkyWeb
DATA_PROJECT = Bulky.DataAccess
SOLUTION = Bulky.sln

DB_CONTAINER_NAME = bulkyBase

run: run-docker 
	dotnet run --project $(STARTUP_PROJECT)

watch:
	dotnet watch --project $(STARTUP_PROJECT) 

build:
	dotnet build $(SOLUTION)

NAME=""
add-migration: build
	dotnet ef migrations add $(NAME) --project $(DATA_PROJECT) --startup-project $(STARTUP_PROJECT)

remove-migration: 
	dotnet ef migrations remove --project $(DATA_PROJECT) --startup-project $(STARTUP_PROJECT)

restore:
	dotnet restore

update-database:
	dotnet ef database update --project $(DATA_PROJECT) --startup-project $(STARTUP_PROJECT)
# DOCKER

create-container:
	sudo docker run -e "ACCEPT_EULA=Y" -e 'MSSQL_SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 --name $(DB_CONTAINER_NAME) -d mcr.microsoft.com/mssql/server:2022-latest

run-container:
	sudo docker start $(DB_CONTAINER_NAME)

