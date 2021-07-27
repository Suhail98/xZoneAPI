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
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace xZoneAPI.Repositories.ProjectRepo
{
    public class ProjectRepository : IProjectRepository
    {
        ApplicationDBContext db;
        private readonly AppSettings appSettings;
        ISectionRepository SectionRepo;
        IMapper mapper;
        public ProjectRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings, ISectionRepository SectionRepo, IMapper mapper)
        {
            db = _db;
            appSettings = _appSettings.Value;
            this.SectionRepo = SectionRepo;
            this.mapper = mapper;
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

        public Project addProject(int roadmapId, int userId)
        {
            Project project = db.Roadmaps
                .Include(u => u.Project)
                .ThenInclude(u => u.Sections)
                .ThenInclude(u => u.ProjectTasks)
                .SingleOrDefault(u => u.Id == roadmapId).Project;
            //project.OwnerID = userId;
            ProjectWithNoId project2 = new ProjectWithNoId(project.Name, project.Sections, project.OwnerID);
            Project project3 = mapper.Map<Project>(project2);
            project3.OwnerID = userId;
            db.Projects.Add(project3);
            Save();
            return project3;
        }

    }
}
