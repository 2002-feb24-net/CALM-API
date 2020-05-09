FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY Calm.App/*.csproj Calm.App/
COPY Calm.Lib/*.csproj Calm.Lib/
COPY Calm.Dtb/*.csproj Calm.Dtb/
# COPY Calm.UnitTests/*.csproj Calm.UnitTests/
RUN dotnet restore

COPY .config ./
RUN dotnet tool restore

# Copy everything else and generate SQL script from migrations
COPY . ./
RUN dotnet ef migrations script -p Calm.Dtb -s Calm.App -o init-db.sql

# Build runtime image
FROM postgres:12.0
WORKDIR /docker-entrypoint-initdb.d

# ENV POSTGRES_USER "postgres"
# ENV POSTGRES_PASSWORD "Password123"
# ENV POSTGRES_DB "calmdb"

COPY --from=build-env /app/init-db.sql .