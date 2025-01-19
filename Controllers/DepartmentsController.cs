using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Models;
using PruebaTecnica.Models.DTO;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly UserManagementContext _context;

        public DepartmentsController(UserManagementContext context)
        {
            _context = context;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments.Include(u => u.UserDepartments).ThenInclude(us => us.User).ToListAsync();

            return Ok(departments);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentDTO request)
        {
            // Creamos el nuevo departamento a almacenar en la BBDD con los datos recibidos mediante la solicitud
            var department = new Department
            {
                Name = request.Name,
                UserDepartments = request.UserDepartments
            };

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentDTO request)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            department.Name = request.Name;
            department.UserDepartments = request.UserDepartments;

            _context.Update(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }
    }
}
