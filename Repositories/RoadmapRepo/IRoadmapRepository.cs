using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.RoadmapModel;

namespace xZoneAPI.Repositories.RoadmapRepo
{
    public interface IRoadmapRepository
    {
        Roadmap addRoadmap(Roadmap Roadmap);
        Roadmap FindRoadmapById(int Id);
        ICollection<Roadmap> GetAllRoadmaps();
        bool DeleteRoadmap(Roadmap Roadmap);
        bool UpdateRoadmap(Roadmap Roadmap);
        public bool Save();
        public List<Roadmap> FindRoadmapsByName(string name);
       
    }
}
