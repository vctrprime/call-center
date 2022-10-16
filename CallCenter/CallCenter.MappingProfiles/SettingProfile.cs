using AutoMapper;
using CallCenter.DTO.Settings;
using CallCenter.Entities;

namespace CallCenter.MappingProfiles
{
    public class SettingProfile : Profile
    {
        public SettingProfile()
        {
            CreateMap<Setting, SettingGetDto>();
            CreateMap<SettingPostDto, Setting>();
        }
    }
}