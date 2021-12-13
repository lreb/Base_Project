using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.API.Domain.Models.Account
{
    public class User: BaseAuditFielModel
    {
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }

        public ICollection<UserProperty> UserProperties { get; set; }
        public virtual ICollection<AccountUser> AccountUsers { get; set; }
    }
}
