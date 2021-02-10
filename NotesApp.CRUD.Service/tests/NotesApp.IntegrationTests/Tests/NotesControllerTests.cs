using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NotesApp.Api;
using NotesApp.Application.DTOs;
using Xunit;

namespace NotesApp.IntegrationTests.Tests
{
    public class NotesControllerTestsBase
    {
        private readonly HttpClient Client;
        
        public NotesControllerTestsBase()
        {
            Client = new CustomWebAppFactory<Startup>()
                .CreateClient();
        }

        [Fact]
        public async Task Valid_list_of_notes_should_be_returned()
        {
            
            var response = await Client.GetAsync("Notes");

            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.OK);

            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<IEnumerable<NoteDto>>(responseString).ToList();
            
            notes.Count
                .Should()
                .BeGreaterOrEqualTo(10);
            
            foreach(var note in notes)
            {
                note.Created
                    .Should()
                    .BeBefore(DateTime.UtcNow);
                note.Modified
                    .Should()
                    .BeBefore(DateTime.UtcNow);
                note.Title
                    .Should()
                    .NotBeNullOrWhiteSpace();
            }
        }

        [Fact]
        public async Task Given_valid_data_note_should_be_created()
        {
            var newNote = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(newNote);
            
            var response = await Client.PostAsync("Notes", serializedNote);

            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.Created);

            var newNoteLoc = response.Headers.Location.ToString();
            response = await Client.GetAsync(newNoteLoc);
            var responseString = await response.Content.ReadAsStringAsync();
            var createdNote = JsonConvert.DeserializeObject<NoteDetailsDto>(responseString);
            
            createdNote.LastVersion.Title
                .Should()
                .BeEquivalentTo(newNote.Title);
            
            createdNote.LastVersion.Content
                .Should()
                .BeEquivalentTo(newNote.Content);

            createdNote.LastVersion.VersionNo
                .Should()
                .Be(1);
        }

        [Fact]
        public async Task Given_valid_noteId_note_should_exists()
        {
            var newNote = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(newNote);
            
            var response = await Client.PostAsync("Notes", serializedNote);
            var newNoteLoc = response.Headers.Location.ToString();
            response = await Client.GetAsync(newNoteLoc);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Given_valid_noteId_note_should_be_deleted()
        {
            var newNote = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(newNote);
            
            var response = await Client.PostAsync("Notes", serializedNote);
            var newNoteLoc = response.Headers.Location.ToString();
            //Deleting note
            response = await Client.DeleteAsync(newNoteLoc);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.NoContent);
            //Searching for deleted note should fail
            response = await Client.GetAsync(newNoteLoc);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_valid_data_new_version_should_be_created()
        {
            var note = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(note);
            
            var response = await Client.PostAsync("Notes", serializedNote);
            var newNoteLoc = response.Headers.Location.ToString();
           
            var noteWithUpdatedTitle = new UpdateNoteDto {
                Title = "Updated title",
                Content = "New content"
            };
            serializedNote = SerializeData(noteWithUpdatedTitle); 
            response = await Client.PutAsync(newNoteLoc,serializedNote);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.NoContent);
            
            response = await Client.GetAsync(newNoteLoc);
            
            var responseString = await response.Content.ReadAsStringAsync();
            var updatedNote = JsonConvert.DeserializeObject<NoteDetailsDto>(responseString);

            updatedNote.LastVersion.VersionNo
                .Should()
                .Be(2);
        }

        [Fact]
        public async Task Given_valid_data_title_should_change()
        {
            var note = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(note);
            
            var response = await Client.PostAsync("Notes", serializedNote);
            var newNoteLoc = response.Headers.Location.ToString();
           
            var noteWithUpdatedTitle = new UpdateNoteDto {
                Title = "Updated title",
                Content = "New content"
            };
            serializedNote = SerializeData(noteWithUpdatedTitle); 
            response = await Client.PutAsync(newNoteLoc,serializedNote);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.NoContent);
            
            response = await Client.GetAsync(newNoteLoc);
            
            var responseString = await response.Content.ReadAsStringAsync();
            var updatedNote = JsonConvert.DeserializeObject<NoteDetailsDto>(responseString);
            
            updatedNote.LastVersion.Title
                .Should()
                .BeEquivalentTo(noteWithUpdatedTitle.Title);

            updatedNote.LastVersion.Content
                .Should()
                .BeEquivalentTo(note.Content);
        }

        [Fact]
        public async Task Given_valid_data_content_should_change()
        {
            var note = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(note);
            
            var response = await Client.PostAsync("Notes", serializedNote);
            var newNoteLoc = response.Headers.Location.ToString();
           
            var noteWithUpdatedTitle = new UpdateNoteDto {
                Title = "New title",
                Content = "Updated content"
            };
            serializedNote = SerializeData(noteWithUpdatedTitle); 
            response = await Client.PutAsync(newNoteLoc,serializedNote);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.NoContent);
            
            response = await Client.GetAsync(newNoteLoc);
            
            var responseString = await response.Content.ReadAsStringAsync();
            var updatedNote = JsonConvert.DeserializeObject<NoteDetailsDto>(responseString);
            
            updatedNote.LastVersion.Title
                .Should()
                .BeEquivalentTo(note.Title);

            updatedNote.LastVersion.Content
                .Should()
                .BeEquivalentTo(noteWithUpdatedTitle.Content);
        }
        
        [Fact]
        public async Task Given_invalid_noteId_note_should_not_exist()
        {
            var invalidUrl = $"Notes/{Guid.NewGuid()}";
           
            var response = await Client.GetAsync(invalidUrl);

            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_invalid_noteId_delete_should_fail()
        {
            var invalidUrl = $"Notes/{Guid.NewGuid()}";
           
            var response = await Client.DeleteAsync(invalidUrl);

            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_invalid_noteId_update_should_fail()
        {
            var note = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(note);
            
            var response = await Client.PostAsync("Notes", serializedNote);
            var newNoteLoc = response.Headers.Location.ToString();
           
            var noteWithUpdatedTitle = new UpdateNoteDto {
                Title = "New title",
                Content = "Updated content"
            };

            serializedNote = SerializeData(noteWithUpdatedTitle); 
            var invalidUrl = $"Notes/{Guid.NewGuid()}";
            response = await Client.PutAsync(invalidUrl,serializedNote);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_invalid_title_update_should_fail()
        {
            var note = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(note);
            
            var response = await Client.PostAsync("Notes", serializedNote);
            var newNoteLoc = response.Headers.Location.ToString();
           
            var noteWithUpdatedTitle = new UpdateNoteDto {
                Title = "",
                Content = "Updated content"
            };
            serializedNote = SerializeData(noteWithUpdatedTitle); 
            
            response = await Client.PutAsync(newNoteLoc,serializedNote);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Given_invalid_content_update_should_fail()
        {
            var note = new CreateNoteDto {
                Title = "New title",
                Content = "New content"
            };
            var serializedNote = SerializeData(note);
            
            var response = await Client.PostAsync("Notes", serializedNote);
            var newNoteLoc = response.Headers.Location.ToString();
           
            var noteWithUpdatedTitle = new UpdateNoteDto {
                Title = "",
                Content = "Updated content"
            };
            serializedNote = SerializeData(noteWithUpdatedTitle); 
            
            response = await Client.PutAsync(newNoteLoc,serializedNote);
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Given_invalid_content_create_should_fail()
        {
            var note = new CreateNoteDto {
                Title = "New title",
                Content = ""
            };
            var serializedNote = SerializeData(note);
            
            var response = await Client.PostAsync("Notes", serializedNote);
           
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Given_invalid_title_create_should_fail()
        {
            var note = new CreateNoteDto {
                Title = "",
                Content = "New content"
            };
            var serializedNote = SerializeData(note);
            
            var response = await Client.PostAsync("Notes", serializedNote);
           
            response.StatusCode
                .Should()
                .BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        private static StringContent SerializeData(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        } 
    }
}