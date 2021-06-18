using Airpool.Scanner.Security.Contract;
using Airpool.Scanner.Security.Data;
using Airpool.Scanner.Security.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airpool.Scanner.Security.Impl
{
    public class UserService : IUserService
    {
        private readonly SecurityContext _dbContext;

        public UserService(SecurityContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public async Task<User> RegisterUser(User user)
        {
            user.Login = user.Email?.Split("@")?[0];
            user.Role = "internal";
            user.Password = HashHelper.Encrypt(user.Password);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            user.Password = "";

            return user;
        }
    }
}
