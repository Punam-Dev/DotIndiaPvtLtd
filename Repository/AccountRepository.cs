using DotIndiaPvtLtd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext appDbContext;
        public AccountRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Users> GetUserByUserNameAndPassword(string userName, string password)
        {
            return await appDbContext.users.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
        }

        public async Task<Users> CreateUser(Users users)
        {
            var result = await appDbContext.users.AddAsync(users);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteUser(string userID)
        {
            var userForms = await appDbContext.Forms.Where(x => x.UserCreatedByUserID == userID).ToListAsync();
            var userQuery = await appDbContext.FormQuery.Where(x => x.FormQueryCreatedByUserID == userID).ToListAsync();
            var user = await appDbContext.users.FindAsync(userID);

            if (user != null)
            {
                appDbContext.Forms.RemoveRange(userForms);
                appDbContext.FormQuery.RemoveRange(userQuery);
                appDbContext.users.Remove(user);

                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Users> GetUserByID(string userID)
        {
            return await appDbContext.users.FindAsync(userID);
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await appDbContext.users.ToListAsync();
        }

        public async Task<Users> UpdateUser(Users users)
        {
            var user = await appDbContext.users.FindAsync(users.UserID);

            if(user != null)
            {
                user.UserName = users.UserName;
                user.Password = users.Password;
                user.FirstName = users.FirstName;
                user.LastName = users.LastName;
                user.Email = users.Email;
                user.Phone = users.Phone;
                user.Address = users.Address;
                user.DOB = users.DOB;
                user.CallerName = users.CallerName;
                user.Status = users.Status;
                user.WorkStatus = users.WorkStatus;

                appDbContext.Entry(user).State = EntityState.Modified;
                await appDbContext.SaveChangesAsync();
            }

            return null;
        }

        public async Task<Users> GetUserByUserName(string userName)
        {
            return await appDbContext.users.FirstOrDefaultAsync(user => user.UserName == userName);
        }
    }
}
