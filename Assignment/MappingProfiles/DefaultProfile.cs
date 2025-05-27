using AutoMapper;
using Assignment.Models;
using Assignment.DTOs;

namespace Assignment.MapperConfigs
{
    public class DefaultProfile:Profile
    {
        public DefaultProfile()
        {
            CreateMap<ContentData, ReadContentDataDTO>().ReverseMap();
            CreateMap<ContentData,ModifyContentDataDTO>()
                .ForMember(dest=>dest.ImagePath,option=>option.Ignore())
                .ReverseMap();
        }
    }
}
