using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.mappers
{
    public class xZoneMapper: Profile
    {
        public xZoneMapper()
        {
            CreateMap<Account, AccountRegisterInDto>().ReverseMap();
            CreateMap<Account, AccountLoginDto>().ReverseMap();
        }
    }
}
