using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Data;
using xZoneAPI.Models.SectionModel;
using Microsoft.Extensions.Options;


namespace xZoneAPI.Repositories.SectionRepo
{
    public class SectionRepository : ISectionRepository
    {
        ApplicationDBContext db;
        private readonly AppSettings appSettings;
        public SectionRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;

        }
        public Section addSection(Section Section)
        {
            db.Sections.Add(Section);
            Save();
            return Section;
        }

        public Section FindSectionById(int id)
        {
            Section Section = db.Sections.SingleOrDefault(x => x.Id == id);
            return Section;
        }
        public ICollection<Section> GetAllSections()
        {
            
            return db.Sections.ToList();
        }
        public bool DeleteSection(Section Section)
        {
            db.Sections.Remove(Section);
            return Save();
        }
        public bool UpdateSection(Section section)
        {
            db.Sections.Update(section);
            return Save();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }


    }
}
