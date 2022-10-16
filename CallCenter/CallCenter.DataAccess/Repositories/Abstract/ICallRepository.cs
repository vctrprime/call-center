using System.Collections.Generic;
using System.Threading.Tasks;
using CallCenter.Entities;

namespace CallCenter.DataAccess.Repositories.Abstract
{
    public interface ICallRepository
    {
        Task<IEnumerable<Call>> GetAsync();
        
        Task<IEnumerable<Call>> GetAsync(bool isComplete);

        Task InsertAsync(Call call);

        Task UpdateAsync(Call call);
    }
}