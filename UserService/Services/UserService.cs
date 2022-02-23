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

        private DAL.UserDAL GetUserDb(Guid uuid)
        {
            return _dbContext.Users.Where(x => x.UserId == uuid).FirstOrDefault();
        }

        public UserDTO GetUser(Guid uuid)
        {
            var user = GetUserDb(uuid);

            if (user == null) return null;

            return new UserDTO()
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname
            };
        }

        public bool UpdateUser(UserDTO user)
        {
            var userToUpdate = GetUserDb(user.UserId);

            userToUpdate.Username = user.Username; 
            userToUpdate.Email = user.Email;  
            userToUpdate.Surname = user.Surname;
            userToUpdate.Name = user.Name;

            var newUser = _dbContext.Users.Update(userToUpdate);
            _dbContext.SaveChanges();

            return newUser != null;
        }

        public bool DeleteUser(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public bool AddUser(UserDTO user)
        {
            var newUser = _dbContext.Users.Add(new DAL.UserDAL()
            {
                UserId = user.UserId,
                Email = user.Email,
                Surname = user.Surname,
                Name = user.Name,
                Username = user.Username
            });

            _dbContext.SaveChanges();

            return newUser != null;
        }
    }

}
