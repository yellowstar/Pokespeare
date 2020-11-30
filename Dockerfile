FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Pokespeare/Pokespeare.csproj", "Pokespeare/"]
COPY ["Pokespeare.Models/*.csproj", "Pokespeare.Models/"]
COPY ["Pokespeare.Providers.Interfaces/*.csproj", "Pokespeare.Providers.Interfaces/"]
COPY ["Pokespeare.Providers.Pokemon/*.csproj", "Pokespeare.Providers.Pokemon/"]
COPY ["Pokespeare.Providers.Translation/*.csproj", "Pokespeare.Providers.Translation/"]
COPY ["Pokespeare.Service/*.csproj", "Pokespeare.Service/"]

RUN dotnet restore "Pokespeare/Pokespeare.csproj"
#COPY . .
COPY Pokespeare/ Pokespeare/
COPY Pokespeare.Models/ Pokespeare.Models/
COPY Pokespeare.Providers.Interfaces/ Pokespeare.Providers.Interfaces/
COPY Pokespeare.Providers.Pokemon/ Pokespeare.Providers.Pokemon/
COPY Pokespeare.Providers.Translation/ Pokespeare.Providers.Translation/
COPY Pokespeare.Service/ Pokespeare.Service/
COPY ["Solution Files/", "Solution Files/"]

WORKDIR "/src/Pokespeare"
RUN dotnet build "Pokespeare.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokespeare.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokespeare.dll"]