using DemoWcf.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DemoWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AccountService.svc or AccountService.svc.cs at the Solution Explorer and start debugging.
    public class AccountService : IAccountService
    {
        DemoWcfEntities ctx = new DemoWcfEntities();
        public async Task<List<AccountContract>> FindAll()
        {
            return await ctx.Accounts.Select(a=>new AccountContract { Id = a.Id,
                Email = a.Email,
                Password = a.Password,
                Fullname = a.Fullname}).ToListAsync();
        }
        public async Task<AccountContract> FindById(string id)
        {
            int nId = Convert.ToInt32(id);
            var acc = await ctx.Accounts.SingleOrDefaultAsync(a => a.Id == nId);
            return new AccountContract
            {
                Id = acc.Id,
                Email = acc.Email,
                Fullname = acc.Fullname
            };
        }
        public async Task<AccountContract> Login(string email, string password)
        {
            var acc = await ctx.Accounts.SingleOrDefaultAsync(a => a.Email == email && a.Password == password);
            if (acc != null)
            {
                return new AccountContract { 
                    Id = acc.Id,
                    Email = acc.Email,
                    Fullname = acc.Fullname };
            }
            return null;
        }
        public async Task<int> Create(AccountContract account)
        {
            Account acc = new Account
            {
                Email = account.Email,
                Password = account.Password,
                Fullname = account.Fullname
            };
            ctx.Accounts.Add(acc);
            await ctx.SaveChangesAsync();
            return acc.Id;
        }
        public async Task Edit(AccountContract account)
        {
            Account a = new Account
            {
                Email = account.Email,
                Password = account.Password,
                Fullname = account.Fullname
            };
            ctx.Entry(a).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Account a = await ctx.Accounts.SingleOrDefaultAsync(b=> b.Id == id);
            if(a != null)
            {
                ctx.Accounts.Remove(a);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
