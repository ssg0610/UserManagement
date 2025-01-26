using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class Department
    {
        public Department()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
