using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.ProjectModel;
using xZoneAPI.Models.ProjectTaskModel;
using xZoneAPI.Models.RoadmapModel;
using xZoneAPI.Models.SectionModel;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.mappers
{
    public class xZoneMapper: Profile
    {
        public xZoneMapper()
        {
            CreateMap<Account, AccountRegisterInDto>().ReverseMap();
            CreateMap<AppTask, TaskDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<ProjectTask, ProjectTaskDto>().ReverseMap();
            CreateMap<Roadmap, RoadmapDto>().ReverseMap();
            CreateMap<Zone, ZoneDto>().ReverseMap();

        }
    }
}
