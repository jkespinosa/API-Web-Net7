using ExampleCode.Models;
using ExampleCode.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCode.DataAccess.Data.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserList();
        Task<User> GetUserById(int Id);
        Task<bool> AddUser(User model);
        Task<bool> ModifyUser(User model);
        bool DeleteUser(User model);

    }
}
