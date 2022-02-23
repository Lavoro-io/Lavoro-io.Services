using UserService.DTO;

namespace UserService.IServices
{
    public interface IUserService
    {
        UserDTO GetUser(Guid uuid);
    }
}

