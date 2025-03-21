using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Consts;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/public/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all students (ordered).
        /// </summary>
        /// <param name="ordering">Ordering direction.</param>
        /// <param name="version">API version (for testing only).</param>
        /// <returns>Returns an ordered collection of students.</returns>
        [HttpGet] // GET localhost:<port>/api/public/students
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Student>))]
        public async Task<IActionResult> Get(Ordering ordering, int version = 1) // version - optional query parameter
        {
            Console.WriteLine($"Query parameter version: {version}");
            return Ok(await _service.GetAllAsync(ordering));
        }

        /// <summary>
        /// Get a student by id.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>Returns a student by the id specified.</returns>
        [HttpGet("{id:int}")] // GET localhost:<port>/api/public/students/123
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1) return StatusCode(StatusCodes.Status400BadRequest, "Invalid user id");

            try
            {
                var student = await _service.GetByIdAsync(id);
                return Ok(student);
            }
            catch (StudentNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Add a student.
        /// </summary>
        /// <param name="student">New student.</param>
        /// <returns>Returns the new student, including the assigned id and location route.</returns>
        [HttpPost] // POST localhost:<port>/api/public/students
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Student))]
        public async Task<IActionResult> Add([FromBody] Student student)
        {
            // Just an example on how we can work with model validation via ModelState if you ever implement custom validation middleware.
            // In .Net8 WebAPI invalid request are getting rejected automatically with a 400 BabRequest error
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var latestId = await _service.AddAsync(student);

            // Adds a Location response header that contains a URL that can be used to retrieve the newly created object.
            // Example: http://localhost:7028/api/public/students/{newId}
            return CreatedAtAction(actionName: nameof(GetById),
                routeValues: new { id = latestId },
                value: student);
        }

        /// <summary>
        /// Update a student.
        /// </summary>
        /// <param name="student">Updated student.</param>
        [HttpPut] // PUT localhost:<port>/api/public/students
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Update([FromBody] Student student)
        {
            if (student.Id < 1) return BadRequest("Invalid user id");

            try
            {
                await _service.UpdateAsync(student);
                return NoContent();
            }
            catch (StudentNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete a student by id.
        /// </summary>
        /// <param name="id">Student id.</param>
        [HttpDelete("{id:int}")] // DELETE localhost:<port>/api/public/students/123
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0) return BadRequest("Invalid user id");

            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (StudentNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
