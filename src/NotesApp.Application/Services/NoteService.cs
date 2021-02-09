using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Application.DTOs;
using NotesApp.Domain.Repositories;
using NotesApp.Domain.Entities;
using NotesApp.Application.Services.Interfaces;

namespace NotesApp.Application.Services
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
        
        public async Task<NoteDetailsDto> GetAsync(Guid id)
        {
            var note = await _noteRepository.GetAsync(id);
            return _mapper.Map<NoteDetailsDto>(note);
        }

        public async Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            var notes = await _noteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        public async Task CreateAsync(Guid noteId, string title, string content)
        {
            var note = await _noteRepository.GetAsync(title);
            if(note != null) throw new Exception($"Note with tite: '{title}' already exists.");
            note = new Note(noteId);
            note.AddVersion(title, content);
            await _noteRepository.CreateAsync(note);
        }

        public async Task DeleteAsync(Guid noteId)
        {
            var note = await _noteRepository.GetAsync(noteId);
            if(note == null) throw new Exception($"Note with id: '{noteId}' does not exist.");
            note.MarkAsDeleted();
            await _noteRepository.UpdateAsync(note);
        }

        public async Task UpdateAsync(Guid noteId, string title, string content)
        {
            var note = await _noteRepository.GetAsync(noteId);
            if(note == null) throw new Exception($"Note with id: '{noteId}' does not exist.");
            note.AddVersion(title, content);
            await _noteRepository.UpdateAsync(note);
        }
    }
}