using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    
    public class AccountSkillRepo : IAccountSkillRepo
    {
        private readonly ApplicationDBContext _db;
        public AccountSkillRepo(ApplicationDBContext db)
        {
            _db = db;
        }
        public bool AddAccountSkill(AccountSkill AccountSkill)
        {
            _db.AccountSkills.Add(AccountSkill);
            return Save();
        }
        public bool DeleteAccountSkill(AccountSkill AccountSkill)
        {
            _db.AccountSkills.Remove(AccountSkill);
            return Save();
        }
        public ICollection<AccountSkill> GetAllAccountSkills()
        {
            return _db.AccountSkills.ToList();
        }
        public ICollection<AccountSkill> GetAllSkillsForAccount(int Id)
        {
            return _db.AccountSkills.OrderBy(a => a.AccountID == Id).ToList();
        }
        public AccountSkill GetAccountSkill(int accountId, int skillId)
        {
            return _db.AccountSkills.SingleOrDefault(a => a.AccountID == accountId && a.SkillID == skillId);
        }
        public ICollection<int> GetAccountSkillsId(int accountId)
        {
            return _db.AccountSkills.Where(a => a.AccountID == accountId).Select(a=>a.SkillID).ToList();
        }
        public bool UpdateAccountSkill(AccountSkill AccountSkill)
        {
            _db.Update(AccountSkill);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

    }

    
}
