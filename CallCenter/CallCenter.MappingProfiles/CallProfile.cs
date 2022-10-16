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
                    act => act.MapFrom(s => GetWaitingTime(s)))
                .ForMember(dest => dest.ExecutingTime,
                    act => act.MapFrom(s => GetExecutingTime(s)))
                .ForMember(dest => dest.Status,
                    act => act.MapFrom(s => s.IsComplete ? "Запрос выполнен" : 
                        s.EmployeeId.HasValue ? "Запрос выполняется" :  "Запрос в очереди"));

            CreateMap<CallPostDto, Call>();
        }

        private int? GetWaitingTime(Call s)
        {
            int? result = s.TakenDate.HasValue ? (int)(s.TakenDate.Value - s.CreatedDate).TotalSeconds : null;
            return result;
        }
        
        private int? GetExecutingTime(Call s)
        {
            if (!s.IsComplete) return null;
            
            int? result = s.FinishedDate.HasValue && s.TakenDate.HasValue ? (int)(s.FinishedDate.Value - s.TakenDate.Value).TotalSeconds : null;
            return result;
        }
    }
}