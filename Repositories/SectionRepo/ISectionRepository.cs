using System.Collections.Generic;
using xZoneAPI.Models.SectionModel;

namespace xZoneAPI.Repositories.SectionRepo
{
    public interface ISectionRepository
    {
        Section addSection(Section Section);
        Section FindSectionById(int Id);
        ICollection<Section> GetAllSections();
        bool DeleteSection(Section Section);

        
        bool UpdateSection(Section Section);
        public bool Save();
    }
}
