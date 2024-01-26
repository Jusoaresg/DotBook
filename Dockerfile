# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
#RUN make restore
RUN dotnet restore "./DotBook.sln" 
RUN dotnet publish "./DotBook.sln" -c release -o /app --no-restore

# Server Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5171

ENTRYPOINT [ "dotnet", "BookWeb.dll"]