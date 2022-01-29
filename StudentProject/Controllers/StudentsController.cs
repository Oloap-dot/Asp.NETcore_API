using Students.Models;
using Students.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Students.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDBContext _context;

        public StudentsController(StudentDBContext context){_context = context;}


        protected async Task<List<Student>> GetStudentListAsync() => await _context.Students.ToListAsync(); //it returns the list waiting for the async operations to be done
        protected async Task<Student> GetStudentAsync(int id) => await _context.Students.FindAsync(id); //waiting for the async search of student in database
        protected async Task SaveAsync() => await _context.SaveChangesAsync(); //i save the change asyncronally into the database
        protected async Task AddStudentAsync (Student student)
        {
            await _context.Students.AddAsync(student);
            await SaveAsync();
        }
        protected async Task<Boolean> UpdateStudentAsync(Student request)
        {
            Student student = await GetStudentAsync(request.Id);
            if (student is null)
                return false;
            
            student.Name = request.Name;
            student.Surname = request.Surname;
            student.Age = request.Age;
            student.SchoolMail = request.SchoolMail;
            
            await SaveAsync();
            return true;
        }
        protected async Task<Boolean> DeleteStudentAsync(int id)
        {
            Student student = await GetStudentAsync(id);
            if(student is null)
                return false;

            _context.Students.Remove(student);

            await SaveAsync();
            return true;
        }



        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return Ok(await GetStudentListAsync()); //it return the list waiting for the async operations to be done
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            Student student = await GetStudentAsync(id); //waiting for the async search
            if (student is null)
                return BadRequest("Student not found");

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Student student)
        {
            await AddStudentAsync(student);
            
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Student request)
        {
            if(!await UpdateStudentAsync(request))
                return BadRequest("Probable wrong id");
            
            return Ok("Student updated");        
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await DeleteStudentAsync(id))
                return BadRequest("Probable wrong id");
            
            return Ok("Student eliminated");
        }
        
        
    }

}