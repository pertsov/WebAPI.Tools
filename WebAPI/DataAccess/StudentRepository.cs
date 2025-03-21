using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public class StudentRepository : IStudentRepository
    {
        public Task<List<Student>> GetAsync() =>
            Task.FromResult(Database.Students);

        public Task<Student> GetAsync(int id) => 
            Task.FromResult(Database.Students.FirstOrDefault(s => s.Id == id));

        public Task<int> AddAsync(Student student)
        {
            var latestIds = Database.Students.Select(s => s.Id).Max() + 1;
            student.Id = latestIds;

            Database.Students.Add(student);
            return Task.FromResult(latestIds);
        }

        public Task UpdateAsync(Student student)
        {
            var dbStudent = Database.Students.First(s => s.Id == student.Id);

            dbStudent.FirstName = student.FirstName;
            dbStudent.LastName = student.LastName;
            dbStudent.GroupCode = student.GroupCode;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var dbStudent = Database.Students.First(s => s.Id == id);

            Database.Students.Remove(dbStudent);

            return Task.CompletedTask;
        }
    }
}
