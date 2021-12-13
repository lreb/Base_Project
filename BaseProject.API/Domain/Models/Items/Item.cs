using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProject.API.Domain.Models.Items
{
    public class Item: BaseAuditFielModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Rate { get; set; }
    }
}
