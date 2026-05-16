# Usa a imagem oficial do .NET 8 para compilar o código
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Usa a imagem mais leve do .NET 8 apenas para rodar a API
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
# A porta padrão do .NET 8 em contêineres é 8080
EXPOSE 8080 
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FinScan.API.dll"]