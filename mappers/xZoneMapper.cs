using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.TaskModel;

namespace xZoneAPI.mappers
{
    public class xZoneMapper: Profile
    {
        public xZoneMapper()
        {
            CreateMap<Account, AccountRegisterInDto>().ReverseMap();
            CreateMap<AppTask, TaskDto>().ReverseMap();
        }
    }
}
