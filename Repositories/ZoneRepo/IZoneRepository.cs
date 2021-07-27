using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Repositories.ZoneRepo
{
    public interface IZoneRepository
    {
        Zone AddZone(Zone Zone);
        Zone FindZonePreviewById(int Id);
        ICollection<Zone> GetAllZones();
        ICollection<Zone> GetAllPublicZones(int userId);
        bool DeleteZone(Zone Zone);
        bool UpdateZone(Zone Zone);
        public bool Save();
        public Zone FindZoneById(int Id);
        public List<Zone> FindZonesByName(string name);
    }
}
