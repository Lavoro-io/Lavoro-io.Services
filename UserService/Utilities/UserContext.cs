using Microsoft.EntityFrameworkCore;
using UserService.DAL;

namespace UserService.Utilities
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            
        }

        public DbSet<UserDAL> Users { get; set; }
        public DbSet<RoleDAL> Roles { get; set; }
    }

    public static class UserContextInitializer
    {
        public static void InitDbContext(DbContextOptions<UserContext>  options)
        {
            using (var context = new UserContext(options))
            {
                var roles = new List<RoleDAL>();
                foreach(Roles role in Enum.GetValues(typeof(Roles)))
                {
                    roles.Add(new RoleDAL(){Name = role.ToString(), RoleEnum = role});
                }

                context.AddRange(roles);
                context.SaveChanges();

                var users = new List<UserDAL>
                {
                    new UserDAL()
                    {
                        UserId = new Guid("fc0bb1cc-0a93-406f-bf39-9be7ab81438b"),
                        Username = "Dev",
                        Name = "Developer",
                        Surname = "Fullstack",
                        Email = "dev@example.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("12345678"),
                        RoleId = roles.Where(x => x.RoleEnum == Roles.Admin).First().RoleId,
                        Enabled = true
                    },
                };

                context.AddRange(users);
                context.SaveChanges();

            }
        }
    }
}

