using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Consts;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync(Ordering ordering);
        Task<Student> GetByIdAsync(int id);
        Task<int> AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}
