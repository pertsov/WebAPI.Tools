using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAsync();
        Task<Student> GetAsync(int id);
        Task<int> AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}
