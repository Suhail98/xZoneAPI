using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.ProjectModel;

namespace xZoneAPI.Repositories.ProjectRepo
{
    public interface IProjectRepository
    {
        Project addProject(Project Project);
        Project FindProjectById(int Id);
        ICollection<Project> GetAllProjects();
        bool DeleteProject(Project Project);
        bool UpdateProject(Project Project);
        public bool Save();
        public Project addProject(int roadmapId, int userId);
    }
}
