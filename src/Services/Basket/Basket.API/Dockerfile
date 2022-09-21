FROM mcr.microsoft.com/dotnet/aspnet:6.0 as run
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src
COPY [ "Services/Basket/Basket.API/Basket.API.csproj" , "Services/Basket/Basket.API/" ]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj" ,"BuildingBlocks/Common.Logging/"]
COPY ["BuildingBlocks/Infrastructure/Infrastructure.csproj" ,"BuildingBlocks/Infrastructure/"]
COPY ["BuildingBlocks/Shared/Shared.csproj" ,"BuildingBlocks/Shared/"]
COPY ["BuildingBlocks/Contracts/Contracts.csproj" ,"BuildingBlocks/Contracts/"]
RUN dotnet restore "Services/Basket/Basket.API/Basket.API.csproj"
COPY . .
RUN dotnet build "Services/Basket/Basket.API/Basket.API.csproj" -c Realease -o /app/build
RUN dotnet publish "Services/Basket/Basket.API/Basket.API.csproj" -c Realease -o /app/publish

FROM run
COPY --from=build /app/publish . 
ENTRYPOINT [ "dotnet" , "Basket.API.dll" ]
