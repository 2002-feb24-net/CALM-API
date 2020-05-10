# FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
# WORKDIR /app

# # Copy csproj and restore as distinct layers
# COPY *.sln ./
# COPY Calm.App/*.csproj Calm.App/
# COPY Calm.Dtb/*.csproj Calm.Dtb/
# COPY Calm.Lib/*.csproj Calm.Lib/
# COPY Calm.Test/*.csproj Calm.Test/
# RUN dotnet restore

# # Copy everything else and build
# COPY . ./
# RUN dotnet publish Calm.App -c Release -o out

# # Build runtime image
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
# WORKDIR /app
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "Calm.App.dll"]