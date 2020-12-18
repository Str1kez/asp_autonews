using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_autonews.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace asp_autonews.Domain
{
    public class DbContext : IdentityDbContext<IdentityUser>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options) {}

        public DbSet<InfoField> InfoFields { get; set; }
        public DbSet<Entities.Article> Articles { get; set; }

        // создаем по умолчанию админа
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "A6B112B3-2D1F-4338-A99B-FAED9C47B7AD",
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            
            // добавлям пока только админа
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "4E3F1DE6-D027-4A7E-BC33-102C23E14970",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "adminnews@auto.com",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "password")
            });

            // связываем админку с пользователем
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "A6B112B3-2D1F-4338-A99B-FAED9C47B7AD",
                UserId = "4E3F1DE6-D027-4A7E-BC33-102C23E14970"
            });

            modelBuilder.Entity<InfoField>().HasData(new InfoField
            {
                Id = new Guid("399F4583-55C0-483F-BCEC-E856F7A369DF"),
                Key = "Index",
                Title = "Главная",
            });
            
            modelBuilder.Entity<InfoField>().HasData(new InfoField
            {
                Id = new Guid("E608A37B-88F6-4D46-9FAA-F43E1D031782"),
                Key = "Articles",
                Title = "Новости",
            });
            
            modelBuilder.Entity<InfoField>().HasData(new InfoField
            {
                Id = new Guid("239C63ED-9DA2-439B-9C45-ACB774DAFA54"),
                Key = "Contacts",
                Title = "Контакты",
            });
        }
    }
}
