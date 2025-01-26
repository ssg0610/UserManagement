using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Models;
using PruebaTecnica.Models.DTOs;
using PruebaTecnica.Models.Services;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();

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
            var user = await _userService.GetUser(id);

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
            var users = await _userService.GetUsersWithDepartment();

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
            var user = await _userService.GetUserWithDepartment(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            try
            {
                var createdUser = await _userService.CreateUser(user);

                if (createdUser == null)
                {
                    // Devuelve un conflicto si el userName ya existe
                    return Conflict(new { Message = $"El nombre de usuario '{user.UserName}' ya está en uso." });
                }

                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error inesperado.", Details = ex.Message });
            }
        }



        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var user = await _userService.UpdateUser(request);

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
            var result = await _userService.DeleteUser(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}