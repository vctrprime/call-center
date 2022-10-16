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
        public string Name { get; set; }
        
        /// <summary>
        /// Должность
        /// </summary>
        public int Position { get; set; }

    }
}