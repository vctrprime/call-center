using System.Threading.Tasks;
using CallCenter.Entities;

namespace CallCenter.DataAccess.Repositories.Abstract
{
    public interface ISettingRepository
    {
        Task<Setting> GetAsync();

        Task InsertAsync(Setting setting);
    }
}