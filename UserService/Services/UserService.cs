using UserService.IServices;
using UserService.Utilities;
using UserService.DTO;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _dbContext;

        public UserService(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserDTO GetUser(Guid uuid)
        {
            var user = _dbContext.Users.Where(x => x.UserId == uuid).FirstOrDefault();

            if(user == null) return null;

            return new UserDTO()
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }

}
