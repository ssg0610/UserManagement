using PruebaTecnica.Context;

namespace PruebaTecnica.Models.Services
{
    public interface DepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<IEnumerable<Department>> GetDepartmentsWithUsers();
        Task<Department> GetDepartment(int id);
        Task<Department> GetDepartmentWithUsers(int id);
        Task<Department> CreateDepartment(Department department);
        Task<Department> UpdateDepartment(Department department);
        Task<bool> DeleteDepartment(int id);
    }
}
