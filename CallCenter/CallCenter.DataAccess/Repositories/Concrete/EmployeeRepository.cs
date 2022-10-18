using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallCenter.DataAccess.Contexts;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.Entities;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.DataAccess.Repositories.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MainDbContext _context;

        public EmployeeRepository(MainDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetAsync()
        {
            return await _context.Employees.Where(e => e.IsActive).SelectMany(
                o => _context.Calls.Where(i => i.EmployeeId == o.Id && !i.IsComplete).DefaultIfEmpty(),
                (o, i) => new Employee
                {
                    Id = o.Id,
                    IsActive = o.IsActive,
                    Name = o.Name,
                    Position = o.Position,
                    WorkingRequestId = i == null ? null : i.Id

                }).ToListAsync();
        }

        public async Task InsertAsync(Employee employee)
        {
            var id = (await _context.Employees.OrderByDescending(x => x.Id).FirstAsync()).Id + 1;
           
            employee.Id = id;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            var entry = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
            
            entry.Position = employee.Position;
            entry.Name = employee.Name;
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int employeeId)
        {
            var entry = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            entry.IsActive = false;
            
            await _context.SaveChangesAsync();
        }
    }
}