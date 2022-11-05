FROM mcr.microsoft.com/dotnet/sdk:6.0 as build 
WORKDIR /src 
COPY . . 
RUN dotnet build "Services/Ordering/Ordering.API/Ordering.API.csproj" 
RUN dotnet publish "Services/Ordering/Ordering.API/Ordering.API.csproj" -c Realease -o /app/publish 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as publish 
WORKDIR /app 
EXPOSE 80 
COPY --from=build /app/publish . 
ENTRYPOINT [ "dotnet" , "Ordering.API.dll" ]