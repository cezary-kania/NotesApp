using AutoMapper;
using NotesApp.Application.DTOs;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Mappings
{
    public class AutoMapperConfig
    {
        public static IMapper Init()
            => new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Note, NoteDto>()
                    .ForMember(dest => dest.LastVersion, 
                        opt => opt.MapFrom(src => src.GetLastVersion())
                    );
                cfg.CreateMap<NoteVersion, NoteVersionDto>();
                    
            })
            .CreateMapper();
    }
}