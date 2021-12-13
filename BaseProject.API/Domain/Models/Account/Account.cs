using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.API.Domain.Models.Account
{
    [Index(nameof(Name))]
    public class Account: BaseAuditFielModel
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public virtual ICollection<AccountUser> AccountUsers { get; set; }
    }
}
