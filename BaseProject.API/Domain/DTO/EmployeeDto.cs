using System.ComponentModel.DataAnnotations;

namespace BaseProject.API.Domain.DTO
{
    public class EmployeeDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DepartmentDto Department { get; set; }
    }
}
