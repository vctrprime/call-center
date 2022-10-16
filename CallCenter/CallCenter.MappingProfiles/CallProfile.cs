using System;
using AutoMapper;
using CallCenter.DTO.Calls;
using CallCenter.Entities;

namespace CallCenter.MappingProfiles
{
    public class CallProfile : Profile
    {
        public CallProfile()
        {
            CreateMap<Call, CallGetDto>()
                .ForMember(dest => dest.WaitingTime,
                    act => act.MapFrom(s => (int)((s.TakenDate ?? DateTime.Now) - s.CreatedDate).TotalSeconds))
                .ForMember(dest => dest.ExecutingTime,
                    act => act.MapFrom(s => (int)((s.FinishedDate ?? DateTime.Now) - (s.TakenDate ?? DateTime.Now)).TotalSeconds))
                .ForMember(dest => dest.Status,
                    act => act.MapFrom(s => s.IsComplete ? "Запрос выполнен" : 
                        s.EmployeeId.HasValue ? "Запрос выполняется" :  "Запрос в очереди"));

            CreateMap<CallPostDto, Call>();
        }
    }
}