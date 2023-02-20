FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tictactoe.API/Tictactoe.API.csproj", "Tictactoe.API/"]
RUN dotnet restore "Tictactoe.API/Tictactoe.API.csproj"
COPY . .
WORKDIR "/src/Tictactoe.API"
RUN dotnet build "Tictactoe.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tictactoe.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tictactoe.API.dll"]
