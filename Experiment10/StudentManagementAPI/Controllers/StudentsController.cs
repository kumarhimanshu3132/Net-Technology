namespace StudentManagementAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _repository;

    public StudentsController(IStudentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetAll([FromQuery] string? branch = null)
    {
        var students = _repository.GetAll(branch);
        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = _repository.GetById(id);
        if (student == null)
        {
            return NotFound(new { message = $"Student with ID {id} was not found." });
        }

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> Create([FromBody] Student student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = _repository.Add(student);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Student student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = _repository.Update(id, student);
        if (!updated)
        {
            return NotFound(new { message = $"Student with ID {id} was not found." });
        }

        return Ok(student);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Student with ID {id} was not found." });
        }

        return Ok(new { message = $"Student with ID {id} successfully deleted." });
    }
}
