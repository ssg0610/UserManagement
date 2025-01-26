using PruebaTecnica.Models.DTOs;

namespace PruebaTecnica.Models.Services
{
    public interface UserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetUsersWithDepartment();
        Task<User> GetUser(int id);
        Task<User> GetUserWithDepartment(int id);
        Task<User> CreateUser(CreateUserDTO user);
        Task<User> UpdateUser(UpdateUserDTO user);
        Task<bool> DeleteUser(int id);
    }
}
