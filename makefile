STARTUP_PROJECT = BookWeb
DATA_PROJECT = Book.DataAccess
SOLUTION = DotBook.sln

DB_CONTAINER_NAME = bulkyBase

run: run-container
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
	sudo docker run -e 'POSTGRES_USER=admin' -e 'POSTGRES_DB=dotbookbase' -e 'POSTGRES_PASSWORD=admin' -p 5432:5432 --name $(DB_CONTAINER_NAME) -d postgres

run-container:
	sudo docker start $(DB_CONTAINER_NAME)

image-build:
	sudo docker build --rm -t dotbook:latest .

remove-docker-containers:
	sudo docker rm dotbookservices_dotbook_1
	sudo docker rm dotbookservices_dotbookBase_1

remove-docker-image:
	sudo docker image rm dotbookservices_dotbook
