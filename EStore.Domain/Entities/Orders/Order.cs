using EStore.Domain.Entities.Commons;
using EStore.Domain.Entities.Finance;
using EStore.Domain.Entities.Products;
using EStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RequestPayId { get; set; }
        public virtual RequestPay RequestPay { get; set; }

        public OrderState OrderState { get; set; }
        public string Address { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails{get; set;}
    }

    public class OrderDetail : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }


        public int Price { get; set; }
        public int Count { get; set; }
    }


    public enum OrderState
    {
        [Display(Name="درحال پردازش")]
        Processing=0,

        [Display(Name = "لغو سفارش")]
        Canceled =1 ,

        [Display(Name = "تحویل سفارش")]
        Deliverd =2
    }
}
