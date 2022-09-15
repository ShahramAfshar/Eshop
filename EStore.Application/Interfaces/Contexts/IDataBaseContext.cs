using EStore.Domain.Entities.Carts;
using EStore.Domain.Entities.Finance;
using EStore.Domain.Entities.HomePages;
using EStore.Domain.Entities.Orders;
using EStore.Domain.Entities.Products;
using EStore.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces.Contexts
{
   public interface IDataBaseContext
    {

         DbSet<User> Users { get; set; }
         DbSet<Role> Roles { get; set; }
         DbSet<UserInRole> UserInRoles { get; set; }
         DbSet<Category> Categories { get; set; }
         DbSet<Product> Products { get; set; }
         DbSet<ProductFeature> ProductFeatures { get; set; }
         DbSet<ProductImage> ProductImages { get; set; }
         DbSet<HomePageImages> HomePageImages { get; set; }     
         DbSet<Slider> Sliders { get; set; }
         DbSet<Cart> Carts { get; set; }
         DbSet<CartItem>  CartItems { get; set; }
         DbSet<RequestPay> RequestPays { get; set; }
         DbSet<OrderDetail>  OrderDetails { get; set; }
         DbSet<Order> Orders { get; set; }
        

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,CancellationToken cancellationToken= new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
