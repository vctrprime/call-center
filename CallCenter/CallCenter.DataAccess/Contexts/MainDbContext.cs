using System.Collections.Generic;
using System.Text;
using CallCenter.Entities;
using CallCenter.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.DataAccess.Contexts
{
    public class MainDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Setting> Settings { get; set; }
        
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Call>(entity => { entity.HasKey(x => x.Id); });
            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasData(new List<Setting>
                {
                    new Setting
                    {
                        Id = 1,
                        TimeManager = 20,
                        TimeDirector = 40,
                        ExecuteTimeLimitLeft = 10,
                        ExecuteTimeLimitRight = 50
                    }
                });
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasData(new List<Employee>
                {
                    new Employee
                    {
                        Id = 1,
                        Name = "Alisa",
                        Position = EmployeePosition.Director,
                    },
                    new Employee
                    {
                        Id = 2,
                        Name = "Bob",
                        Position = EmployeePosition.Manager,
                    },
                    new Employee
                    {
                        Id = 3,
                        Name = "John",
                        Position = EmployeePosition.Operator,
                    }
                    
                });
            });
        }
       
    }
}