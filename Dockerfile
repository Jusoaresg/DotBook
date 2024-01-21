FROM ubuntu

WORKDIR /app
COPY ./dir /app 
RUN apt get install dotnet-sdk-8.0


EXPOSE 5171

