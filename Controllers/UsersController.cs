using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Models;
using PruebaTecnica.Models.DTO;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManagementContext _context;

        public UsersController(UserManagementContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.Include(u => u.UserDepartments).ThenInclude(ud => ud.Department).ToListAsync();

            return Ok(users);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO request)
        {
            // Creamos al usuario que vamos a almacenar en la BBDD con los datos recibidos en la solicitud
            var user = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                Password = request.Password,
                Phone = request.Phone,
                UserDepartments = request.UserDepartments,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO request)
        {
            // Recogemos el usuario cuyo ID es el recibido como parametro
            var user = await _context.Users.FindAsync(id);

            // Verificamos que exista un usuario con ese ID registrado en la BBDD
            if (user == null)
            {
                // Si el usuario no existe en nuestra BBDD devolvemos mensaje de error
                return NotFound();
            }

            user.Name = request.Name;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Password = request.Password;
            user.Phone = request.Phone;
            user.UserDepartments = request.UserDepartments;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, UserDTO request)
        {
            // Recogemos el usuario cuyo ID es el recibido como parametro
            var user = await _context.Users.FindAsync(id);

            // Verificamos que exista un usuario con ese ID registrado en la BBDD
            if (user == null)
            {
                // Si el usuario no existe en nuestra BBDD devolvemos mensaje de error
                return NotFound();
            } else
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
