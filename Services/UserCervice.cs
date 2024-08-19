using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserCervice
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Services/users.json");

        public void SaveUsers(List<User> users)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(users, options);
             File.WriteAllText(_filePath, json);
        }

        public List<User> GetUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var json =  File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json);
        }
        public void AddUser(User user) {
            var users = GetUsers();
            users.Add(user);
            SaveUsers(users);
        }
    }
}
