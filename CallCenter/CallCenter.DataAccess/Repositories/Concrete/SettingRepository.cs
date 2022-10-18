using System;
using System.Linq;
using System.Threading.Tasks;
using CallCenter.DataAccess.Contexts;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.Entities;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.DataAccess.Repositories.Concrete
{
    public class SettingRepository : ISettingRepository
    {
        private readonly MainDbContext _context;

        public SettingRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<Setting> GetAsync()
        {
            var setting = await _context.Settings.OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
            
            return setting;
        }

        public async Task InsertAsync(Setting setting)
        {
            var id = (await GetAsync()).Id + 1;
            setting.Id = id;
            
            await _context.Settings.AddAsync(setting);
            await _context.SaveChangesAsync();
        }
    }
}