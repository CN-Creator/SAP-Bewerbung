# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /API

# copy csproj and restore
COPY *.csproj ./
RUN dotnet restore
# copy rest of project
COPY . ./
RUN dotnet publish -c Release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /API
COPY --from=build /API/out .
ENTRYPOINT ["dotnet", "API.dll"]