using System.ComponentModel.DataAnnotations;

namespace CallCenter.DTO.Employees
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeePutDto : EmployeePostDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}