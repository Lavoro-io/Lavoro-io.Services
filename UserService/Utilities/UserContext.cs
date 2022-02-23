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
    }

    public static class UserContextInitializer
    {
        public static void InitDbContext(DbContextOptions<UserContext>  options)
        {
            using (var context = new UserContext(options))
            {
                var users = new List<UserDAL>
                {
                    new UserDAL()
                    {
                        UserId = new Guid("fc0bb1cc-0a93-406f-bf39-9be7ab81438b"),
                        Username = "dev",
                        Name = "Developer",
                        Surname = "Fullstack",
                        Email = "dev@example.com"
                    },
                };

                context.Users.AddRange(users);
                context.SaveChanges();

            }
        }
    }
}

