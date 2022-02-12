FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=http://+:5003
WORKDIR /app
EXPOSE 5003
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NotesApp.History.Service/src/NotesApp.History.Service.Api/NotesApp.History.Service.Api.csproj", "src/NotesApp.History.Service.Api/"]
COPY ["NotesApp.History.Service/src/NotesApp.History.Service.Application/NotesApp.History.Service.Application.csproj", "src/NotesApp.History.Service.Application/"]
COPY ["NotesApp.History.Service/src/NotesApp.History.Service.Domain/NotesApp.History.Service.Domain.csproj", "src/NotesApp.History.Service.Domain/"]
COPY ["NotesApp.History.Service/src/NotesApp.History.Service.Infrastructure/NotesApp.History.Service.Infrastructure.csproj", "src/NotesApp.History.Service.Infrastructure/"]
RUN dotnet restore "src/NotesApp.History.Service.Api/NotesApp.History.Service.Api.csproj"
COPY . .
WORKDIR "/src/NotesApp.History.Service/src/NotesApp.History.Service.Api"
RUN dotnet build "NotesApp.History.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotesApp.History.Service.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesApp.History.Service.Api.dll"]
