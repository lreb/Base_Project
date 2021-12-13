using BaseProject.API.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.API.Domain.Models.Account
{
    public class UserProperty: BaseAuditFielModel
    {
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Value { get; set; }
        public PropertyType ValueType { get; set; }
        public User User { get; set; }
    }
}
