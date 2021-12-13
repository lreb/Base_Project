using System;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.API.Domain.Models
{
    /// <summary>
    /// Base model, all models must inheritance from it to get identifier
    /// </summary>
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }

    /// <summary>
    /// Base model, just models who need to have audit filed must inheritance from it to get identifier and audit fields
    /// </summary>
    public class BaseAuditFielModel : BaseModel
    {
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
