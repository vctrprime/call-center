using System;
using CallCenter.Entities;
using CallCenter.Entities.Enums;
using CallCenter.Services.Abstract;

namespace CallCenter.Services.Concrete
{
    public class CallConstraintService : ICallConstraintService
    {
        public bool EmployeeCanTakeCall(Employee employee, Call call, Setting setting)
        {
            return employee.Position switch
            {
                EmployeePosition.Operator => true,
                EmployeePosition.Manager => DateTime.Now > call.CreatedDate.AddSeconds(setting.TimeManager),
                EmployeePosition.Director => DateTime.Now > call.CreatedDate.AddSeconds(setting.TimeDirector),
                _ => false
            };
        }
    }
}