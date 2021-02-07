using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public class AccountRepo : IAccountRepo
    {
        ApplicationDBContext db;
        public AccountRepo(ApplicationDBContext _db)
        {
            db = _db;
        }
        public Account register(Account account)
        {
            db.Accounts.Add(account);
            Save();
            return account;
        }
        public Account FindAccountByEmail(string email)
        {
           Account account =db.Accounts.SingleOrDefault(x => x.Email == email);
            return account;
        }
        public Account FindAccountById(int id)
        {
            Account account = db.Accounts.SingleOrDefault(x => x.Id == id);
            return account;
        }
        public ICollection<Account> GetAllAccounts()
        {
            
            return db.Accounts.ToList();
        }
        public bool DeleteAccount(Account account)
        {
            db.Accounts.Remove(account);
            return Save();
        }
        public bool UpdateAccount(Account acount)
        {
            db.Accounts.Update(acount);
            return Save();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }


    }
}
