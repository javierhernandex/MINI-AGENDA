FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ["MINI-AGENDA/MINI-AGENDA.csproj", "MINI-AGENDA/"]
RUN dotnet restore "MINI-AGENDA/MINI-AGENDA.csproj"

COPY . .
WORKDIR "/src/MINI-AGENDA"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MINI-AGENDA.dll"]