﻿namespace PruebaTecnica.Models.DTOs
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public List<int> DepartmentIds { get; set; }
    }

}
