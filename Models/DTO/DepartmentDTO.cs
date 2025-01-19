namespace PruebaTecnica.Models.DTO
{
    public class DepartmentDTO
    {
        public required string Name { get; set; }
        public ICollection<UserDepartment> UserDepartments { get; set; }
    }
}
