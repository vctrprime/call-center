using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallCenter.DataAccess.Contexts;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.Entities;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.DataAccess.Repositories.Concrete
{
    public class CallRepository : ICallRepository
    {
        private readonly MainDbContext _context;

        public CallRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Call>> GetAsync()
        {
            return await GetDataAsync();
        }

        public async Task<IEnumerable<Call>> GetAsync(bool isComplete)
        {
            return await GetDataAsync(isComplete);
        }
        
        private async Task<IEnumerable<Call>> GetDataAsync(bool? isComplete = null)
        {
            var calls = isComplete.HasValue ? _context.Calls.Where(x => x.IsComplete == isComplete) : _context.Calls;
            return await calls.SelectMany(
                o => _context.Employees.Where(i => i.Id == o.EmployeeId).DefaultIfEmpty(),
                (o, i) => new Call
                {
                    Id = o.Id,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    TakenDate = o.TakenDate,
                    FinishedDate = o.FinishedDate,
                    EmployeeId = o.EmployeeId,
                    EmployeeName = $"{i.Position} {i.Name}",
                    IsComplete = o.IsComplete
                }).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task InsertAsync(Call call)
        {
            var lastCall = await _context.Calls.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            var id = lastCall is null ? 1 : lastCall.Id + 1;
            call.Id = id;
            call.CreatedDate = DateTime.Now;

            await _context.Calls.AddAsync(call);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Call call)
        {
            var entry = await _context.Calls.FirstOrDefaultAsync(x => x.Id == call.Id);
            entry.TakenDate = call.TakenDate ?? entry.TakenDate;
            entry.EmployeeId = call.EmployeeId ?? entry.EmployeeId;
            entry.FinishedDate = call.FinishedDate  ?? entry.FinishedDate;
            entry.IsComplete = call.IsComplete;
            
            await _context.SaveChangesAsync();
        }
    }
}