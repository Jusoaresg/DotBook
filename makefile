STARTUP_PROJECT = BulkyWeb
DATA_PROJECT = Bulky.DataAccess

DB_CONTAINER_NAME = bulkyBase

run: run-docker 
	dotnet run --project $(STARTUP_PROJECT)

watch:
	dotnet watch --project $(STARTUP_PROJECT) 

build:
	dotnet build Bulky.sln 

NAME=""
add-migration: build
	dotnet ef migrations add $(NAME) --project $(DATA_PROJECT) --startup-project $(STARTUP_PROJECT)

remove-migration: 
	dotnet ef migrations remove --project $(DATA_PROJECT) --startup-project $(STARTUP_PROJECT)

# DOCKER

run-docker:
	sudo docker start $(DB_CONTAINER_NAME)

