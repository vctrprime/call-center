using System;
using CallCenter.DataAccess.Repositories.Abstract;
using CallCenter.DataAccess.Repositories.Concrete;
using CallCenter.MappingProfiles;
using CallCenter.Services.Abstract;
using CallCenter.Services.Concrete;
using CallCenter.WebUI.Infrastructure.Hubs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CallCenter.WebUI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрация прикладных модулей
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterModules(this IServiceCollection services)
        {
            services
                .RegisterRepositories()
                .RegisterMapperProfiles()
                .RegisterServices();
            
            RegisterLogger();
            
            return services;
        }
        
        /// <summary>
        /// Регистрация репозиториев
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<ICallRepository, CallRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
        
        /// <summary>
        /// Регистрация профилей маппинга
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(SettingProfile),
                typeof(CallProfile),
                typeof(EmployeeProfile)
            );
            return services;
        }
        
        /// <summary>
        /// Регистрация логгера
        /// </summary>
        private static void RegisterLogger()
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"Logging Process Error: {msg}"));
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
        
        /// <summary>
        /// Регистрация сервисов
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ICallConstraintService, CallConstraintService>();
            
            services.AddSingleton<ICallDistributorService, CallDistributorService>();
            //services.AddHostedService<CallDistributorHostingService>();
            
            services.AddSignalR();
            services.AddScoped<CallHub>();

            return services;
        }
    }
}