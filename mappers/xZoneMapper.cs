using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.CommentModel;
using xZoneAPI.Models.Posts;
using xZoneAPI.Models.ProjectModel;
using xZoneAPI.Models.ProjectTaskModel;
using xZoneAPI.Models.RoadmapModel;
using xZoneAPI.Models.SectionModel;
using xZoneAPI.Models.Skills;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.mappers
{
    public class xZoneMapper: Profile
    {
        public xZoneMapper()
        {
            CreateMap<Account, AccountRegisterInDto>().ReverseMap();
            CreateMap<Account, ProfileDto>().ReverseMap();
            CreateMap<AppTask, TaskDto>().ReverseMap();
            CreateMap<ZoneTask, TaskDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<ProjectTask, ProjectTaskDto>().ReverseMap();
            CreateMap<Roadmap, RoadmapDto>().ReverseMap();
            CreateMap<Zone, ZoneDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<ZoneTask, ZoneTaskDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();            
            CreateMap<Project, ProjectWithNoId>().ReverseMap();
        }
    }
}
