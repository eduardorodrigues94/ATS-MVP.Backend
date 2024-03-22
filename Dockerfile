#BUILD
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore ".\ATS.MVP.Api\ATS.MVP.Api.csproj" --disable-parallel
RUN dotnet publish ".\ATS.MVP.Api\ATS.MVP.Api.csproj" -c release -o /app --no-restore

#SERVE
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "ATS.MVP.Api.dll"]