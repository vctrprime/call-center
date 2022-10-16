using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.Entities;
using CallCenter.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CallCenter.Services.Concrete
{
    public class CallDistributorService : ICallDistributorService
    {
        private readonly ICallConstraintService _callConstraintService;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CallDistributorService( 
            IServiceScopeFactory serviceScopeFactory,
            ILogger<CallDistributorService> logger,
            ICallConstraintService callConstraintService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _callConstraintService = callConstraintService;
        }

        private ICallRepository _callRepository;
        private IEmployeeRepository _employeeRepository;

        public async Task<bool> Distribute()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            
            _employeeRepository = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
            _callRepository = scope.ServiceProvider.GetRequiredService<ICallRepository>();
            var settingRepository = scope.ServiceProvider.GetRequiredService<ISettingRepository>();
                
            var employees = (await _employeeRepository.GetAsync()).ToArray();
            var calls = (await _callRepository.GetAsync()).ToArray();
            var setting = await settingRepository.GetAsync();

            var hadChangesTakenCalls = await CheckTakenCalls(calls.Where(c => !c.IsComplete && c.EmployeeId.HasValue), employees);
            var hadChangesDistributeCalls =
                await DistributeCallsForFreeEmployees(employees.Where(x => !x.WorkingRequestId.HasValue),
                    calls.Where(c => !c.EmployeeId.HasValue).ToArray(),
                    setting);

            return hadChangesTakenCalls || hadChangesDistributeCalls;
        }
        
        private async Task<bool> CheckTakenCalls(IEnumerable<Call> takenCalls, Employee[] employees)
        {
            var hadChanges = false;
            
            foreach (var call in takenCalls)
            {
                if (!(DateTime.Now >= call.FinishedDate)) continue;
                
                call.IsComplete = true;
                await _callRepository.UpdateAsync(call);
                
                hadChanges = true;
                    
                _logger.LogInformation($"{call.EmployeeName} закончил с запросом {call.Id}!");
            }

            return hadChanges;
        }

        private async Task<bool> DistributeCallsForFreeEmployees(IEnumerable<Employee> freeEmployees,
            Call[] notTakenCalls,
            Setting setting)
        {
            var hadChanges = false;
            
            foreach (var employee in freeEmployees)
            {
                var call = notTakenCalls.FirstOrDefault();
                if (call is null)
                    break;
                
                if (!_callConstraintService.EmployeeCanTakeCall(employee, call, setting)) continue;
                
                _logger.LogInformation($"Запрос {call.Id} подходит {employee.Name}!");
                Random random = new Random();
                call.TakenDate = DateTime.Now;
                call.EmployeeId = employee.Id;
                call.FinishedDate = DateTime.Now.AddSeconds(random.Next(setting.ExecuteTimeLimitLeft, setting.ExecuteTimeLimitRight));
                
                await _callRepository.UpdateAsync(call);

                hadChanges = true;
            }

            return hadChanges;
        }
        
    }
}