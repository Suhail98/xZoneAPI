﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Data;
using xZoneAPI.Models.Posts;
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
        public Zone AddZone(Zone Zone)
        {
            db.Zones.Add(Zone);
            Save();
            return Zone;
        }

        public bool DeleteZone(Zone Zone)
        {
            db.Zones.Remove(Zone);
            return Save();
        }

        public Zone FindZonePreviewById(int Id)
        {
            Zone zone = db.Zones.SingleOrDefault(z => z.Id == Id);
            return zone;
        }
        public Zone FindZoneById(int Id)
        {
            Zone zone = db.Zones.Include(u => u.Posts)
                .Include(u => u.ZoneMembers)
                .ThenInclude(u => u.Account)
                .Include(u=>u.Posts)
                .ThenInclude(u=>u.Writer)
                .Include(u=>u.Tasks)
                .SingleOrDefault(z => z.Id == Id);
            foreach (ZoneMember zoneMember in zone.ZoneMembers)
                zoneMember.Account.Password = "";
            foreach (Post post in zone.Posts)
               post.Writer.Password = "";
            return zone;
        }

        public ICollection<Zone> GetAllZones()
        {
            return db.Zones.ToList();
        }
        public ICollection<Zone> GetAllPublicZones()
        {
            return db.Zones.Where(u=>u.Privacy == Zone.PrivacyType.Public).ToList();
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
        public List<Zone> FindZonesByName(string name)
        {
            return db.Zones.Where(a => a.Name.Contains(name)).ToList();
        }
    }
}
