using System;

namespace NotesApp.Application.Services.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(string message = null) : base(message)
        {
        }
    }
}