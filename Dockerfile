# Use the official .NET SDK image to build the app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuestionBank.Api/QuestionBank.Api.csproj", "QuestionBank.Api/"]
COPY ["QuestionBank.DataManagement/QuestionBank.DataManagement.csproj", "QuestionBank.DataManagement/"]
COPY ["QuestionBank.Common/QuestionBank.Common.csproj", "QuestionBank.Common/"]
COPY ["QuestionBank.Pessistence.Entity/QuestionBank.Persistence.Entity.csproj", "QuestionBank.Pessistence.Entity/"]
COPY ["QuestionBank.Mapper/QuestionBank.Mapper.csproj", "QuestionBank.Mapper/"]
COPY ["QuestionBank.Model.Api/QuestionBank.Model.Api.csproj", "QuestionBank.Model.Api/"]
COPY ["QuestionBank.Model.Domain/QuestionBank.Model.Domain.csproj", "QuestionBank.Model.Domain/"]
COPY ["QuestionBank.Utility/QuestionBank.Utility.csproj", "QuestionBank.Utility/"]
COPY ["QuestionBank.Repository.Impl/QuestionBank.Repository.Impl.csproj", "QuestionBank.Repository.Impl/"]
COPY ["QuestionBank.Repository.Interface/QuestionBank.Repository.Interface.csproj", "QuestionBank.Repository.Interface/"]
COPY ["QuestionBank.Service.Impl/QuestionBank.Service.Impl.csproj", "QuestionBank.Service.Impl/"]
COPY ["QuestionBank.Service.Interface/QuestionBank.Service.Interface.csproj", "QuestionBank.Service.Interface/"]
COPY ["QuestionBank.Validator/QuestionBank.Validator.csproj", "QuestionBank.Validator/"]
RUN dotnet restore "./QuestionBank.Api/QuestionBank.Api.csproj"
COPY . .
WORKDIR "/src/QuestionBank.Api"
RUN dotnet build "./QuestionBank.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuestionBank.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 443
EXPOSE 8080
EXPOSE 8081


ENTRYPOINT ["dotnet", "QuestionBank.Api.dll"]








