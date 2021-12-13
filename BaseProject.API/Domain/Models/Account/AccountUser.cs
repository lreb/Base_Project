using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProject.API.Domain.Models.Account
{
    [Index(nameof(AccountId))]
    [Index(nameof(UserId))]
    public class AccountUser: BaseAuditFielModel
    {
        [Key, Column(Order = 0)]
        public int AccountId { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public virtual Account Account { get; set; }
        public virtual User User { get; set; }

        public bool IsOwner { get; set; } = false;
    }
}
