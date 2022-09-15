using EStore.Domain.Entities.Commons;
using EStore.Domain.Entities.Orders;
using EStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Finance
{
   public class RequestPay: BaseEntity
    {
        public Guid Guid  { get; set; }

        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDateTime { get; set; }

        public string Authority { get; set; }
        public long RefId { get; set; } = 0;

        public User User { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
