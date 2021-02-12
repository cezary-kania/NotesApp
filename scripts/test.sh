#!/bin/bash
projects=(NotesApp.CRUD.Service/tests/NotesApp.IntegrationTests/NotesApp.IntegrationTests.csproj
        NotesApp.History.Service/tests/NotesApp.History.Service.IntegrationTests/NotesApp.History.Service.IntegrationTests.csproj)
for project in ${projects[@]}
do
    echo Testing project: $project
    dotnet test $project
done