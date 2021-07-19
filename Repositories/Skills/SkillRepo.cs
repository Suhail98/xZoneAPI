using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Skills;

namespace xZoneAPI.Repositories.Skills
{
    public class SkillRepo : ISkillRepo
    {
        private readonly ApplicationDBContext _db;
        public SkillRepo(ApplicationDBContext db)
        {
            _db = db;
        }
        public Skill AddSkill(Skill Skill)
        {
            _db.Skills.Add(Skill);
            Save();
            return Skill;
        }

        public bool DeleteSkill(Skill Skill)
        {
            _db.Skills.Remove(Skill);
            return Save();
        }

        public Skill FindSkillById(int Id)
        {
            return _db.Skills.FirstOrDefault(a => a.Id == Id);
        }

        public ICollection<Skill> GetAllSkills()
        {
            return _db.Skills.ToList();
        }
        public bool UpdateSkill(Skill Skill)
        {
            _db.Update(Skill);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }
        public List<Skill> FindSkillByName(string name)
        {
            return _db.Skills.Where(a => a.Name.Contains(name)).ToList();
        }
    }
}
