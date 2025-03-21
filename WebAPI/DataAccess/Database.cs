using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public static class Database
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student()
            {
                Id = 1, 
                FirstName = "Layton",
                LastName = "Thompson",
                GroupCode = "ASP.NET-101"
            },
            new Student()
            {
                Id = 2,
                FirstName = "Omar",
                LastName = "Grant",
                GroupCode = "ASP.NET-201"
            },
            new Student()
            {
                Id = 3,
                FirstName = "Ayden",
                LastName = "Terry",
                GroupCode = "ASP.NET-301"
            }
        };
    }
}
