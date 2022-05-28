FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY CategoriesService.sln ./
COPY Domain/*.csproj ./Domain/
COPY WebApi/*.csproj ./WebApi/
COPY Application/*.csproj ./Application/
COPY Infrastructure.Data/*.csproj ./Infrastructure.Data/

RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","WebApi.dll"]
###############################################################################


