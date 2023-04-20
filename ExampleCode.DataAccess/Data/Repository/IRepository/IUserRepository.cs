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
        Task<IEnumerable<UserModel>> GetAllUserList();
        Task<UserModel> GetUserById(int Id);
        Task<bool> AddUser(UserModel model);
        Task<bool> ModifyUser(UserModel model);
        bool DeleteUser(UserModel model);

    }
}
