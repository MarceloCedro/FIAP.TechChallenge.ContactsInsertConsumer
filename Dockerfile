FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 6060

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContactsInsertConsumer.Api/FIAP.TechChallenge.ContactsInsertConsumer.Api.csproj", "ContactsInsertConsumer.Api/"]
COPY ["ContactsInsertConsumer.Application/FIAP.TechChallenge.ContactsInsertConsumer.Application.csproj", "ContactsInsertConsumer.Application/"]
COPY ["ContactsInsertConsumer.Domain/FIAP.TechChallenge.ContactsInsertConsumer.Domain.csproj", "ContactsInsertConsumer.Domain/"]
COPY ["ContactsInsertConsumer.Infrastructure/FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.csproj", "ContactsInsertConsumer.Infrastructure/"]
RUN dotnet restore "./ContactsInsertConsumer.Api/FIAP.TechChallenge.ContactsInsertConsumer.Api.csproj"
COPY . .
WORKDIR "/src/ContactsInsertConsumer.Api"
RUN dotnet build "./FIAP.TechChallenge.ContactsInsertConsumer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FIAP.TechChallenge.ContactsInsertConsumer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.TechChallenge.ContactsInsertConsumer.Api.dll"]
