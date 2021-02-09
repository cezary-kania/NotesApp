using AutoMapper;
using NotesApp.History.Service.Domain.Entities;
using NotesApp.History.Service.Application.DTOs;


namespace NotesApp.History.Service.Application.Mappings
{
    public class AutoMapperConfig
    {
        public static IMapper Init()
            => new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Note, NoteDto>();
                cfg.CreateMap<NoteVersion, NoteVersionDto>();
                    
            })
            .CreateMapper();
    }
}