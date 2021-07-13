using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Data;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Repositories.ZoneRepo
{
    public class ZoneRepository : IZoneRepository
    {
        ApplicationDBContext db;
        private readonly AppSettings appSettings;

        public ZoneRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }
        public bool AddZone(Zone Zone)
        {
            db.Zones.Add(Zone);
            return Save();
        }

        public bool DeleteZone(Zone Zone)
        {
            db.Zones.Remove(Zone);
            return Save();
        }

        public Zone FindZoneById(int Id)
        {
            Zone zone = db.Zones.SingleOrDefault(z => z.Id == Id);
            return zone;
        }

        public ICollection<Zone> GetAllZones()
        {
            return db.Zones.ToList();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }

        public bool UpdateZone(Zone Zone)
        {
            db.Zones.Update(Zone);
            return Save();
        }
    }
}
