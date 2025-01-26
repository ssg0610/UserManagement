using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Models.DTOs;
using PruebaTecnica.Models.Services;

namespace PruebaTecnica.Models.DTO
{
    public class UserRepository : UserService
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<IEnumerable<User>> GetUsersWithDepartment()
        {
            return await _context.Users.Where(x => x.IsDeleted == false).Include(u => u.Departments).ToListAsync();
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User> GetUserWithDepartment(int id)
        {

            return await _context.Users.Where(x => x.IsDeleted == false).Include(x => x.Departments).FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> CreateUser(CreateUserDTO user)
        {
            var newUser = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                UserName = user.UserName,
                Password = user.Password,
                IsDeleted = user.IsDeleted,
                Departments = new List<Department>()
            };

            if (user.DepartmentIds != null && user.DepartmentIds.Any())
            {
                foreach (var id in user.DepartmentIds)
                {
                    var dept = await _context.Departments.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);
                    dept.Users.Add(newUser);

                    _context.Departments.Update(dept);

                    newUser.Departments.Add(dept);
                }
            }

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

            return newUser;
        }
        public async Task<User> UpdateUser(UpdateUserDTO userDTO)
        {
            // 1. Buscar al usuario existente (con sus departamentos).
            var existingUser = await _context.Users
                .Include(u => u.Departments)
                .FirstOrDefaultAsync(u => u.Id == userDTO.Id);

            if (existingUser == null)
            {
                // Maneja el caso en que el usuario no exista:
                // Por ejemplo, puedes retornar null o lanzar excepción.
                throw new Exception($"No se encontró un usuario con Id = {userDTO.Id}");
            }

            // 2. Actualizar campos básicos
            existingUser.Name = userDTO.Name;
            existingUser.LastName = userDTO.LastName;
            existingUser.Phone = userDTO.Phone;
            existingUser.UserName = userDTO.UserName;
            existingUser.Password = userDTO.Password;
            existingUser.IsDeleted = userDTO.IsDeleted;

            // 3. Sincronizar la relación con los departamentos
            //    a) Obtener la lista de IDs que el cliente envió:
            var newDeptIds = userDTO.DepartmentIds ?? new List<int>();

            //    b) Quitar los departamentos que ya no estén en la lista:
            //       (los que están en existingUser.Departments pero no en newDeptIds)
            var toRemove = existingUser.Departments
                .Where(d => !newDeptIds.Contains(d.Id))
                .ToList(); // usar ToList() para evitar modificar la colección mientras se itera
            foreach (var dept in toRemove)
            {
                existingUser.Departments.Remove(dept);
            }

            //    c) Agregar los departamentos nuevos que no estén ya en la colección:
            //       (los que están en newDeptIds pero no en existingUser.Departments)
            var currentDeptIds = existingUser.Departments.Select(d => d.Id).ToList();
            var toAdd = newDeptIds.Where(id => !currentDeptIds.Contains(id));

            foreach (var deptId in toAdd)
            {
                var dept = await _context.Departments.FindAsync(deptId);

                existingUser.Departments.Add(dept);
            }

            // 4. Guardar los cambios
            await _context.SaveChangesAsync();

            return existingUser;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            user.IsDeleted = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
