using AutoMapper;
using CallCenter.DTO.Employees;
using CallCenter.Entities;
using CallCenter.Entities.Enums;

namespace CallCenter.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeGetDto>()
                .ForMember(dest => dest.Position, act => act.MapFrom(s => s.Position.ToString()))
                .ForMember(dest => dest.Status,
                    act => act.MapFrom(s => s.WorkingRequestId.HasValue ? $"Исполняет запрос {s.WorkingRequestId}" : "Свободен"));

            CreateMap<EmployeePostDto, Employee>()
                .ForMember(dest => dest.Position, act => act.MapFrom(s => (EmployeePosition)s.Position));
            
            CreateMap<EmployeePutDto, Employee>()
                .ForMember(dest => dest.Position, act => act.MapFrom(s => (EmployeePosition)s.Position));
        }
    }
}