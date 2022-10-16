using System.Collections.Generic;
using System.Threading.Tasks;
using CallCenter.Entities;

namespace CallCenter.DataAccess.Repositories.Abstract
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAsync();
        
        Task InsertAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(int employeeId);
    }
}