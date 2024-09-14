using System.Text.Json;
using WebApplication1.Models;
using WebApplication1.Interfaces;
using System.IO;

namespace WebApplication1.Services
{
    public class UserCervice: IUserService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),"Services" , "users.json");

        public UserCervice()
        {

           // string _filePath = Path.Combine(Directory.GetCurrentDirectory(), path);

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public void SaveUsers(List<User> users)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(users, options);
             File.WriteAllText(_filePath, json);
        }

        public List<User> GetUsers()
        {
            try
            {
                if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0)
                {
                    return new List<User>();
                }

                var json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<User>();
                }

                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework or simply write to console)
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                return new List<User>();
            }
        }
        public void AddUser(User user) {
            var users = GetUsers();
            users.Add(user);
            SaveUsers(users);
        }

        public void RemoveUser(User user)
        {
            var users = GetUsers();
           var us = users.Where(u => u.Id.ToString() != user.Id.ToString()).ToList();
            SaveUsers(us);

        }

        public User GetUserById(Guid id)
        {
            var user = GetUsers().FirstOrDefault(u => u.Id.ToString() == id.ToString());
            return user;
        }

        public void UpdateUser(User user)
        {
            var users = GetUsers();
            var index = users.FindIndex(x => x.Id == user.Id);
            users[index] = user;
            SaveUsers(users);
        }
    }
}
