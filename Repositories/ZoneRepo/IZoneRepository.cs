using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Repositories.ZoneRepo
{
    public interface IZoneRepository
    {
        bool AddZone(Zone Zone);
        Zone FindZoneById(int Id);
        ICollection<Zone> GetAllZones();
        bool DeleteZone(Zone Zone);
        bool UpdateZone(Zone Zone);
        public bool Save();
    }
}
