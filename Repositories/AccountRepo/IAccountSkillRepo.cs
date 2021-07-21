using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
namespace xZoneAPI.Repositories.AccountRepo
{
    public interface IAccountSkillRepo
    {
        bool AddAccountSkill(AccountSkill AccountSkill);
        ICollection<AccountSkill> GetAllAccountSkills();
        public ICollection<AccountSkill> GetAllSkillsForAccount(int Id);
        public AccountSkill GetAccountSkill(int accountId, int skillId);
        bool DeleteAccountSkill(AccountSkill AccountSkill);
        bool UpdateAccountSkill(AccountSkill AccountSkill);
        public bool Save();
        public ICollection<int> GetAccountSkillsId(int accountId);

    }
}
