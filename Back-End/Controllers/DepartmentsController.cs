using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Models;
using PruebaTecnica.Models.DTO;
using PruebaTecnica.Models.Services;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService _departmentDAO;

        public DepartmentsController(DepartmentService departmentDAO)
        {
            _departmentDAO = departmentDAO;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentDAO.GetDepartments();

            if (departments == null)
            {
                return NotFound();
            }

            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _departmentDAO.GetDepartment(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // GET: api/departments/users
        [HttpGet("users")]
        public async Task<IActionResult> GetDepartmentsWithUsers()
        {
            var departments = await _departmentDAO.GetDepartmentsWithUsers();

            if (departments == null)
            {
                return NotFound();
            }

            return Ok(departments);
        }

        // GET: api/departments/{id}/users
        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetDepartmentWithUsers(int id)
        {
            var department = await _departmentDAO.GetDepartmentWithUsers(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department request)
        {
            var dept = await _departmentDAO.CreateDepartment(request);

            return CreatedAtAction(nameof(GetDepartment), new { id = dept.Id}, dept);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var dept = await _departmentDAO.UpdateDepartment(request);

            if (dept == null)
            {
                return NotFound();
            }

            return Ok(dept);
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentDAO.DeleteDepartment(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
