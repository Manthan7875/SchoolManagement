using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.UseCases.Command;
using SchoolManagement.Application.UseCases.Query;

namespace schoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUpdateStudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data is null.");
            }

            try
            {
                var command = new CreateStudentCommand(studentDto);
                var createdStudent = await _mediator.Send(command); // Ensure _mediator is properly injected
                return Ok(createdStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent(int id)
        {
            var result = await _mediator.Send(new RemoveStudentCommand(id));
            if (result)
            {
                return Ok("Student deleted successfully.");
            }
            return NotFound("Student not found.");
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());

            if (result == null || !result.Any())
            {
                return NotFound("No students found.");
            }

            return Ok(result);
        }


    }
}
