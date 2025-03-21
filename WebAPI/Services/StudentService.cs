using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Consts;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Student>> GetAllAsync(Ordering ordering)
        {
            var students = await _repository.GetAsync();

            var orderedStudents = ordering switch
            {
                Ordering.Ascending => students.OrderBy(s => s.Id),
                Ordering.Descending => students.OrderByDescending(s => s.Id),
                _ => throw new ArgumentOutOfRangeException(nameof(ordering),
                    $"Not expected ordering direction: {ordering}")
            };

            return orderedStudents.ToList();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _repository.GetAsync(id);
            if (student is null)
            {
                throw new StudentNotFound($"User by the id of {id} not found");
            }

            return student;
        }

        public async Task<int> AddAsync(Student student) => 
            await _repository.AddAsync(student);

        public async Task UpdateAsync(Student student)
        {
            var existingStudent = await _repository.GetAsync(student.Id);
            if (existingStudent is null)
            {
                throw new StudentNotFound($"User by the id of {student.Id} not found");
            }

            await _repository.UpdateAsync(student);
        }

        public async Task DeleteAsync(int id)
        {
            var existingStudent = await _repository.GetAsync(id);
            if (existingStudent is null)
            {
                throw new StudentNotFound($"User by the id of {id} not found");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
