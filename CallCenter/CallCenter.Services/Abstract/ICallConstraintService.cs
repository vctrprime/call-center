using CallCenter.Entities;

namespace CallCenter.Services.Abstract
{
    public interface ICallConstraintService
    {
        bool EmployeeCanTakeCall(Employee employee, Call call, Setting setting);
    }
}