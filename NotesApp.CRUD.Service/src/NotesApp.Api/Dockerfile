FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app
EXPOSE 5000
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NotesApp.CRUD.Service/src/NotesApp.Api/NotesApp.Api.csproj", "src/NotesApp.Api/"]
COPY ["NotesApp.CRUD.Service/src/NotesApp.Application/NotesApp.Application.csproj", "src/NotesApp.Application/"]
COPY ["NotesApp.CRUD.Service/src/NotesApp.Domain/NotesApp.Domain.csproj", "src/NotesApp.Domain/"]
COPY ["NotesApp.CRUD.Service/src/NotesApp.Infrastructure/NotesApp.Infrastructure.csproj", "src/NotesApp.Infrastructure/"]
RUN dotnet restore "src/NotesApp.Api/NotesApp.Api.csproj"
COPY . .
WORKDIR "/src/NotesApp.CRUD.Service/src/NotesApp.Api"
RUN dotnet build "NotesApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotesApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesApp.Api.dll"]
