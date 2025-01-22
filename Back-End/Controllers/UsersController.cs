using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Models;
using PruebaTecnica.Models.Services;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userDAO;

        public UsersController(UserService userDAO)
        {
            _userDAO = userDAO;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userDAO.GetUsers();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userDAO.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/users/departments
        [HttpGet("departments")]
        public async Task<IActionResult> GetUsersWithDepartment()
        {
            var users = await _userDAO.GetUsersWithDepartment();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/users/{id}/departments
        [HttpGet("{id}/departments")]
        public async Task<IActionResult> GetUserWithDepartment(int id)
        {
            var user = await _userDAO.GetUserWithDepartment(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser(User request)
        {
            var user = await _userDAO.CreateUser(request);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id}, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var user = await _userDAO.UpdateUser(request);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userDAO.DeleteUser(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}