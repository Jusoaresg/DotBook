FROM ubuntu

WORKDIR /app
COPY ./dir /app 
RUN apt get install dotnet-sdk-8.0

CMD 
EXPOSE 5171

