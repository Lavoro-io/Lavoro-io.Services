using UserService.DTO;

namespace UserService.IServices
{
    public interface IUserService
    {
        UserDTO GetUser(Guid uuid);
        bool UpdateUser(UserDTO user);
        bool DeleteUser(Guid uuid);
        bool AddUser(UserDTO user);
    }
}

