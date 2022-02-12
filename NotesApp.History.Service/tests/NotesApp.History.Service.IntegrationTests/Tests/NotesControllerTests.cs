using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NotesApp.History.Service.Api;
using NotesApp.History.Service.Application.DTOs;
using Xunit;

namespace NotesApp.History.Service.IntegrationTests.Tests
{
    public class NotesControllerTestsBase
    {
        private readonly HttpClient Client;
        
        public NotesControllerTestsBase()
        {
            Client = new WebAppFactory().CreateClient();
        }

        [Fact]
        public async Task Given_valid_noteId_history_should_exist()
        {
            var noteId = "0fbbfa74-c69f-4a7e-9a97-af78876727a9";
            var response = await Client.GetAsync($"/NoteHistory/{noteId}");
            response.StatusCode
                    .Should()
                    .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Given_valid_noteId_history_should_be_valid()
        {
            var noteId = "d4240b8c-75c3-4486-a861-fd04317611c8";
            var response = await Client.GetAsync($"NoteHistory/{noteId}");
            var responseString = await response.Content.ReadAsStringAsync();
            var note = JsonConvert.DeserializeObject<NoteDto>(responseString);
            var noteVersions = note.Versions.ToList();
            note.Id
                .ToString()
                .Should()
                .BeEquivalentTo(noteId);
            noteVersions.Count
                .Should()
                .BeGreaterOrEqualTo(1);
            for (int i = 0; i < noteVersions.Count; ++i)
            {
                noteVersions[i].VersionNo
                    .Should()
                    .Be(i + 1);
                noteVersions[i].Title
                    .Should()
                    .NotBeNullOrWhiteSpace();
                noteVersions[i].Content
                    .Should()
                    .NotBeNullOrWhiteSpace();
            }
        }

        [Fact]
        public async Task Given_invalid_noteId_history_should_not_exist()
        {
            var noteId = "00000000-c69f-4a7e-9a97-af78876727a9";
            var response = await Client.GetAsync($"NoteHistory/{noteId}");
            response.StatusCode
                    .Should()
                    .Be(HttpStatusCode.NotFound);
        } 
    }
}