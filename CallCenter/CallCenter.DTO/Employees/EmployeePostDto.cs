using System.ComponentModel.DataAnnotations;

namespace CallCenter.DTO.Employees
{
    /// <summary>
    /// ДТо сотрудника (POST)
    /// </summary>
    public class EmployeePostDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Должность
        /// </summary>
        [Required]
        public int Position { get; set; }

    }
}