FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ApiDarioJoanaProjetoFinal.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /src
COPY --from=build /src/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ApiDarioJoanaProjetoFinal.dll"]