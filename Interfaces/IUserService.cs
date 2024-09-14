using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IUserService
    {
        void SaveUsers(List<User> users);
        List<User> GetUsers();
        void AddUser(User user);
        void RemoveUser(User user);
        User GetUserById(Guid id);
        void UpdateUser(User user);
    }
}
