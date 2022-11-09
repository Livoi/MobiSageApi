using MobiSageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiSageApi.IRepository
{
    public interface IUserRepository
    {
        Task<User> Save(User obj);

        Task<User> Get(int obj);

        Task<List<User>> Gets();

        Task<User> GetByUsernamePassword(User user);

        Task<string> Delete(User obj);
    }
}
