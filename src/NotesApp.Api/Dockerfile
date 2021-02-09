FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /dotnetapp
COPY ./bin/Release/net5.0 .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT dotnet NotesApp.Api.dll