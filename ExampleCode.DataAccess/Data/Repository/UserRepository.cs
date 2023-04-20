using ExampleCode.DataAccess.Data.Repository.IRepository;
using ExampleCode.Models;
using ExampleCode.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCode.DataAccess.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<UserModel>> GetAllUserList()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserById(int Id)
        {
            //return await _dbContext.Users.FindAsync(Id);

            return await _dbContext.Users.Where(s => s.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddUser(UserModel model)
        {
            try
            {
                await _dbContext.Users.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch { return false; }


        }

        public async Task<bool> ModifyUser(UserModel model)
        {
            try
            {
                //_dbContext.Entry(model).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();


                _dbContext.Users.Update(model);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch { return false; }
        }

        public bool DeleteUser(UserModel model)
        {
            var exist = _dbContext.Users.Find(model.Id);

            try
            {
                if (exist != null)
                {
                    exist.IsActive = false;
                    _dbContext.Users.Update(exist);
                    _dbContext.SaveChangesAsync();


                    //_dbContext.Entry(exist).State = EntityState.Deleted;
                    //_dbContext.SaveChanges();



                    return true;
                }
            }
            catch { return false; }

            return false;
        }

      
    }
}
