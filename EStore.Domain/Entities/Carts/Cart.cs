using EStore.Domain.Entities.Commons;
using EStore.Domain.Entities.Products;
using EStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Carts
{
    public class Cart:BaseEntity
    {
        public User User { get; set; }
        public int? UserId { get; set; }

        public Guid BrowserId { get; set; }
        public bool Finished { get; set; }

        public virtual ICollection<CartItem> CartItem { get; set; }

    }
    public class CartItem:BaseEntity
    {
        public Product  Product { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public virtual Cart Cart { get; set; }
        public int CartId { get; set; }
    }
}
