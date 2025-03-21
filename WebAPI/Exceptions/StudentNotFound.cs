using System;

namespace WebAPI.Exceptions
{
    public class StudentNotFound : Exception
    {
        public StudentNotFound() { }

        public StudentNotFound(string message)
            : base(message) 
        { }

        public StudentNotFound(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
