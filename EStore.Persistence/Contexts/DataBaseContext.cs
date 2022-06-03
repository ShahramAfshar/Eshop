using EStore.Application.Interfaces.Contexts;
using EStore.Common.UserRoles;
using EStore.Domain.Entities.HomePages;
using EStore.Domain.Entities.Products;
using EStore.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistence.Contexts
{
   public class DataBaseContext:DbContext,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserInRole> UserInRoles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductFeature> ProductFeatures { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            SeedData(modelBuilder);
            
            //اعمال ایندکس و اعمال عدم تکراری
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            ApplyQueryFilter(modelBuilder);


        
        }

        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            //فقط کاربری رو بارگذاری کن که فیلد فعال باشند
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsRemove);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsRemove);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(ur => !ur.IsRemove);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsRemove);
            modelBuilder.Entity<Slider>().HasQueryFilter(c => !c.IsRemove);
            modelBuilder.Entity<Product>().HasQueryFilter(c => !c.IsRemove);
            modelBuilder.Entity<ProductFeature>().HasQueryFilter(c => !c.IsRemove);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(c => !c.IsRemove);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role() { Id = 1, Name = nameof(UserRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role() { Id = 2, Name = nameof(UserRoles.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role() { Id = 3, Name = nameof(UserRoles.Customer) });

        }


        

    }
}
