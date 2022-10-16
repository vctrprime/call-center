using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CallCenter.Entities.Enums;

namespace CallCenter.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmployeePosition Position { get; set; }
        
        [NotMapped]
        public int? WorkingRequestId { get; set; }
        
        public bool IsActive { get; set; } = true;

    }
}