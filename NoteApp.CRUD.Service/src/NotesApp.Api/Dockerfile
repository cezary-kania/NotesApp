FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NoteApp.CRUD.Service/src/NotesApp.Api/NotesApp.Api.csproj", "NoteApp.CRUD.Service/src/NotesApp.Api/"]
RUN dotnet restore "NoteApp.CRUD.Service/src/NotesApp.Api/NotesApp.Api.csproj"
COPY . .
WORKDIR "/src/NoteApp.CRUD.Service/src/NotesApp.Api"
RUN dotnet build "NotesApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotesApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesApp.Api.dll"]
