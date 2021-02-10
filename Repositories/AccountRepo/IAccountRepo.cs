using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public interface IAccountRepo
    {
        Account register(Account account);
        public bool Save();
    }
}
