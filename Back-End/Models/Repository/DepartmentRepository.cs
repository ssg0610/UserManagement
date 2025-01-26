using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Models.Services;

namespace PruebaTecnica.Models.DTO
{
    public class DepartmentRepository : DepartmentService
    {
        private readonly ApplicationContext _context;
        public DepartmentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Departments.Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<IEnumerable<Department>> GetDepartmentsWithUsers()
        {
            return await _context.Departments.Where(x => x.IsDeleted == false).Include(x => x.Users).ToListAsync();
        }
        public async Task<Department> GetDepartment(int id)
        {
            return await _context.Departments.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Department> GetDepartmentWithUsers(int id)
        {
            return await _context.Departments.Where(x => x.IsDeleted == false).Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Department> CreateDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }
        public async Task<Department> UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }
        public async Task<bool> DeleteDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);

            if (dept == null)
            {
                return false;
            }

            dept.IsDeleted = true;
            _context.Departments.Update(dept);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
