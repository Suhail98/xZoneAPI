using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Skills;
namespace xZoneAPI.Repositories.Skills
{
    public interface IAccountSkillRepo
    {
        bool AddSkill(Skill Skill);
        Skill FindSkillById(int Id);
        ICollection<Skill> GetAllSkills();
        bool DeleteSkill(Skill Skill);
        bool UpdateSkill(Skill Skill);
        public bool Save();
        Skill FindSkillByName(string name);
    }
}
