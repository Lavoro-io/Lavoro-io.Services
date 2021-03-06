using Microsoft.EntityFrameworkCore;
using GlobalService.DAL;
using System.Linq;
using System.Collections.Generic;
using System;

namespace GlobalService.Utilities
{
    public class GloabalContext : DbContext
    {
        public GloabalContext(DbContextOptions<GloabalContext> options) : base(options)
        {
            this.SavingChanges += DatabaseContext_SavingChanges;
        }

        private void DatabaseContext_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            var now = System.DateTime.UtcNow;
            foreach (var entry in this.ChangeTracker.Entries<BaseDAL>())
            {
                var entity = entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = now;
                        entity.LastUpdate = now;
                        break;
                    case EntityState.Modified:
                        entity.LastUpdate = now;
                        break;
                }
            }
            this.ChangeTracker.DetectChanges();
        }

        public DbSet<UserDAL> Users { get; set; }
        public DbSet<RoleDAL> Roles { get; set; }
        public DbSet<ChatDAL> Chats { get; set; }
        public DbSet<MessageDAL> Messages { get; set; }
        public DbSet<ChatUsersDAL> ChatUsers { get; set; }
    }

    public static class UserContextInitializer
    {
        public static void InitDbContext(DbContextOptions<GloabalContext>  options)
        {
            using (var context = new GloabalContext(options))
            {
                if(!context.Roles.Any())
                {
                    var roles = new List<RoleDAL>();
                    foreach(Roles role in Enum.GetValues(typeof(Roles)))
                    {
                        roles.Add(new RoleDAL(){Name = role.ToString(), RoleEnum = role});
                    }
                    
                    context.AddRange(roles);
                    context.SaveChanges();
                }

                if(!context.Users.Any())
                {
                    var users = new List<UserDAL>
                    {
                        new UserDAL()
                        {
                            UserId = new Guid(),
                            Username = "FullDev",
                            Name = "Developer",
                            Surname = "Fullstack",
                            Email = "fulldev@example.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("12345678"),
                            RoleId = context.Roles.Where(x => x.RoleEnum == Roles.Admin).First().RoleId,
                            IsActive = true
                        },
                        new UserDAL()
                        {
                            UserId = new Guid(),
                            Username = "BackDev",
                            Name = "Developer",
                            Surname = "Backend",
                            Email = "backdev@example.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("12345678"),
                            RoleId = context.Roles.Where(x => x.RoleEnum == Roles.Admin).First().RoleId,
                            IsActive = true
                        },
                        new UserDAL()
                        {
                            UserId = new Guid(),
                            Username = "FrontDev",
                            Name = "Developer",
                            Surname = "Frontend",
                            Email = "frontdev@example.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("12345678"),
                            RoleId = context.Roles.Where(x => x.RoleEnum == Roles.Admin).First().RoleId,
                            IsActive = true
                        },
                    };

                    context.AddRange(users);
                    context.SaveChanges();
                }
            }
        }
    }
}

