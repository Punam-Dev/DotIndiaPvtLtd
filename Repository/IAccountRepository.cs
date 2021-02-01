using DotIndiaPvtLtd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Repository
{
    public interface IAccountRepository
    {
        Task<Users> GetUserByID(string userID);
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> CreateUser(Users users);
        Task<Users> UpdateUser(Users users);
        void DeleteUser(string userID);
        Task<Users> GetUserByUserNameAndPassword(string userName, string password);
        Task<Users> GetUserByUserName(string userName);
    }
}
