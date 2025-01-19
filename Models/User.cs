using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public ICollection<UserDepartment> UserDepartments { get; set; }
    }
}
