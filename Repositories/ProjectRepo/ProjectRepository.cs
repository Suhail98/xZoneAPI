using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.ProjectModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using xZoneAPI.Repositories.SectionRepo;

namespace xZoneAPI.Repositories.ProjectRepo
{
    public class ProjectRepository : IProjectRepository
    {
        ApplicationDBContext db;
        private readonly AppSettings appSettings;
        ISectionRepository SectionRepo;
        public ProjectRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings, ISectionRepository SectionRepo)
        {
            db = _db;
            appSettings = _appSettings.Value;
            this.SectionRepo = SectionRepo;
        }
        public Project addProject(Project Project)
        {
            db.Projects.Add(Project);
            Save();
            return Project;
        }

        public Project FindProjectById(int id)
        {
            Project Project = db.Projects.SingleOrDefault(x => x.Id == id);
            return Project;
        }
        public ICollection<Project> GetAllProjects()
        {

            return db.Projects.ToList();
        }
        public bool DeleteProject(Project Project)
        {
            db.Projects.Remove(Project);
            return Save();
        }
        public bool UpdateProject(Project project)
        {
            db.Projects.Update(project);
            return Save();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }


    }
}
