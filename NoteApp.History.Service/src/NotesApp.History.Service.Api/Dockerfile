FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NoteApp.History.Service/src/NotesApp.History.Service.Api/NotesApp.History.Service.Api.csproj", "NoteApp.History.Service/src/NotesApp.History.Service.Api/"]
RUN dotnet restore "NoteApp.History.Service/src/NotesApp.History.Service.Api/NotesApp.History.Service.Api.csproj"
COPY . .
WORKDIR "/src/NoteApp.History.Service/src/NotesApp.History.Service.Api"
RUN dotnet build "NotesApp.History.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotesApp.History.Service.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesApp.History.Service.Api.dll"]
