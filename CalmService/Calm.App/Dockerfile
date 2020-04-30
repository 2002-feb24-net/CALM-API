FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Calm.App/Calm.App.csproj", "Calm.App/"]
RUN dotnet restore "Calm.App/Calm.App.csproj"
COPY . .
WORKDIR "/src/Calm.App"
RUN dotnet build "Calm.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calm.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calm.App.dll"]
