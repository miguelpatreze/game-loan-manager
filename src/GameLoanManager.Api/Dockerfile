# Stage 1 (Copy and restore layers from application)
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build 
WORKDIR /GameLoanManager.Api
COPY *.sln ./
COPY GameLoanManager.Api/GameLoanManager.Api.csproj/ GameLoanManager.Api/
COPY GameLoanManager.CrossCutting/GameLoanManager.CrossCutting.csproj/ GameLoanManager.CrossCutting/
COPY GameLoanManager.Domain/GameLoanManager.Domain.csproj/ GameLoanManager.Domain/
COPY GameLoanManager.MongoDB/GameLoanManager.MongoDB.csproj/ GameLoanManager.MongoDB/
COPY GameLoanManager.Domain.Test/GameLoanManager.Domain.Test.csproj/ GameLoanManager.Domain.Test/
RUN dotnet restore
COPY . ./

# Stage 2 (Run Unit Tests and Code Coverage)
LABEL test=true
RUN dotnet test -c Release 

# Stage 3 (Publish application into /app directory)
WORKDIR /GameLoanManager.Api/GameLoanManager.Api
RUN dotnet publish -c Release -o /app

# Stage 4 (Generate runtime image from previous processes)
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "GameLoanManager.Api.dll"]