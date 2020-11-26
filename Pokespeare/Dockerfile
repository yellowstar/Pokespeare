FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Pokespeare/Pokespeare.csproj", "Pokespeare/"]

RUN dotnet restore "Pokespeare/Pokespeare.csproj"
COPY . .
WORKDIR "/src/Pokespeare"
RUN dotnet build "Pokespeare.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokespeare.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokespeare.dll"]