using DotIndiaPvtLtd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Repository
{
    public class FormsRepository : IFormsRepository
    {
        private readonly AppDbContext appDbContext;
        public FormsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Forms> CreateForm(Forms Forms)
        {
            var result = await appDbContext.Forms.AddAsync(Forms);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public void DeleteForm(Int64 FormID)
        {
            throw new NotImplementedException();
        }

        public async Task<Forms> GetFormByID(Int64 FormID)
        {
            return await appDbContext.Forms.FindAsync(FormID);
        }

        public async Task<IEnumerable<Forms>> GetForms(bool? isSubmit, string LoggedInUserID)
        {
            if (isSubmit == null)
            {
                if (string.IsNullOrEmpty(LoggedInUserID))
                {
                    return await appDbContext.Forms.ToListAsync();
                }
                return await appDbContext.Forms.Where(x => x.UserCreatedByUserID == LoggedInUserID).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(LoggedInUserID))
            {
                return await appDbContext.Forms.Where(x => x.FormIsSubmit == isSubmit && x.UserCreatedByUserID == LoggedInUserID).ToListAsync();
            }
            return await appDbContext.Forms.Where(x => x.FormIsSubmit == isSubmit).ToListAsync();
        }

        public Task<Forms> UpdateForm(Forms Forms)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FormQuery>> GetFormQueries(string LoggedInUserID)
        {
            if (!string.IsNullOrEmpty(LoggedInUserID))
            {
                return await appDbContext.FormQuery.Where(x => x.FormQueryCreatedByUserID == LoggedInUserID).ToListAsync();
            }
            return await appDbContext.FormQuery.ToListAsync();
        }

        public void CreateFormQuery(List<FormQuery> formQueries)
        {
            appDbContext.FormQuery.AddRange(formQueries);
            appDbContext.SaveChanges();
        }
    }
}
