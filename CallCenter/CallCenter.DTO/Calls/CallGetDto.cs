namespace CallCenter.DTO.Calls
{
    /// <summary>
    /// ДТО заявки (GET)
    /// </summary>
    public class CallGetDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Кем создан
        /// </summary>
        public string CreatedBy { get; set; }
        
        /// <summary>
        /// Время ожидания
        /// </summary>
        public int? WaitingTime { get; set; }
        
        /// <summary>
        /// Время исполнения
        /// </summary>
        public int? ExecutingTime { get; set; }
        
        /// <summary>
        /// Исполнитель
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Общее время
        /// </summary>
        public int? SummaryTime => !WaitingTime.HasValue || !ExecutingTime.HasValue ? null : WaitingTime + ExecutingTime;
    }
}