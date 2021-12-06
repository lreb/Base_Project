using System.ComponentModel.DataAnnotations;

namespace BaseProject.API.Domain.DTO
{
    public class DepartmentDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
