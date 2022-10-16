using System.Threading.Tasks;

namespace CallCenter.Services.Abstract
{
    public interface ICallDistributorService
    {
        Task<bool> Distribute();
    }
}