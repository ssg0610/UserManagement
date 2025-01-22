using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
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
            return await _context.Users.ToListAsync();
        }
        public async Task<IEnumerable<User>> GetUsersWithDepartment()
        {
            return await _context.Users.Include(u => u.Departments).ToListAsync();
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> GetUserWithDepartment(int id)
        {

            return await _context.Users.Include(x => x.Departments).FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
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
