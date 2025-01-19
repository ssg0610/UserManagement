using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<UserDepartment> UserDepartments { get; set; }
    }
}
