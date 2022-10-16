namespace CallCenter.DTO.Employees
{
    /// <summary>
    /// ДТО сотрудника (GET)
    /// </summary>
    public class EmployeeGetDto
    {
        /// <summary>
        /// Id 
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        
        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }
    }
}