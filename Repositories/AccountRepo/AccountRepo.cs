using System;
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
        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }
    }
}
