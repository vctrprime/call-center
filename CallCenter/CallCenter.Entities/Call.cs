using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Entities
{
    public class Call
    {
        public int Id { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? TakenDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public int? EmployeeId { get; set; }
        
        [NotMapped]
        public string EmployeeName { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}