# Stage 1 (Copy and restore layers from application)
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build 
WORKDIR /IdentityServer.MVC
COPY *.sln ./
COPY IdentityServer.MVC/IdentityServer.MVC.csproj/ IdentityServer.MVC/
RUN dotnet restore
COPY . ./

# Stage 2 (Publish application into /app directory)
WORKDIR /IdentityServer.MVC/IdentityServer.MVC
RUN dotnet publish -c Release -o /app

# Stage 3 (Generate runtime image from previous processes)
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "IdentityServer.MVC.dll"]