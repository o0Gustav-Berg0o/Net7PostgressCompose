# Net7PostgressCompose

#Composefile

version: "3.4"

networks:
  dev:
    driver: bridge

services:
  demo-app:
    image: docker.io/library/demoapp
    depends_on:
      - "app_db"
    container_name: demoapp-services
    ports:
      - "8088:80"
    build:
      context: .
    environment:
      - ConnectionStrings__DefaultConnection=User ID=postgres;Password=postgres;Server=app_db;Port=5432;Database=SampleDbDriver;IntegratedSecurity=true;Pooling=true;
      - ASPNETCORE_URLS=http://+:80
    networks:
      - dev

  app_db:
    image: postgres:latest
    container_name: app_db
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
      - POSTGRES_DB=SampleDbDriver
    ports:
      - "5433:5432"
    restart: always
    volumes:
      - app_data:/var/lib/postgressql/data
    networks:
      - dev

volumes:
  app_data:

#Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY Net7PostgressCompose.csproj ./
RUN dotnet restore

COPY . ./ 
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final-env
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Net7PostgressCompose.dll"] 

#Connectionstring for update database to postgress container

"DefaultConnection": "User ID=postgres;Password=postgres;Server=localhost;Port=5433;Database=SampleDbDriver; IntegratedSecurity=true;Pooling=true;"
