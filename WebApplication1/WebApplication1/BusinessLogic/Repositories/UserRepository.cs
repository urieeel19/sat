using WebApplication1.BusinessLogic.Provider;
using WebApplication1.DAL.Domain;

namespace WebApplication1.BusinessLogic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserProvider _userProvider;

        public UserRepository(IUserProvider userProvider) { 
            _userProvider = userProvider; 
        }
        public IEnumerable<User> Get()
        {
            var _users = new List<User>();
            var reader = _userProvider.ReadUsersFromFile();
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                if (!string.IsNullOrEmpty(line))
                {
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = Convert.ToDouble(line.Split(',')[5])
                    };
                    _users.Add(user); 
                }
            }
            reader.Close();
            return _users;
        }

        User IUserRepository.CreateAsync(User entity)
        {
            Random rnd = new();
            entity.Id = rnd.Next(1, 1000);
            return entity;
        }

        bool IUserRepository.IsDuplicated(User entity)
        {
            var allUsers = this.Get();
            bool isDuplicated = allUsers.Any(u => u.Email == entity.Email ||
                u.Phone == entity.Phone ||
                (u.Name == entity.Name && u.Address == entity.Address));
            return isDuplicated;
        }
    }
}
