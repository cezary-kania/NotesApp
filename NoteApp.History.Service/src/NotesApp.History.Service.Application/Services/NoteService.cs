using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.History.Service.Application.DTOs;
using NotesApp.History.Service.Domain.Repositories;
using NotesApp.History.Service.Domain.Entities;
using NotesApp.History.Service.Application.Services.Interfaces;

namespace NotesApp.History.Service.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }
        
        public async Task<NoteDto> GetAsync(Guid id)
        {
            var note = await _noteRepository.GetHistoryAsync(id);
            return _mapper.Map<NoteDto>(note);
        }
    }
}