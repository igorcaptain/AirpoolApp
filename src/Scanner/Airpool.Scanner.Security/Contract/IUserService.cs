using Airpool.Scanner.Security.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airpool.Scanner.Security.Contract
{
    public interface IUserService
    {
        IList<User> GetUsers();
        Task<User> RegisterUser(User user);
    }
}
