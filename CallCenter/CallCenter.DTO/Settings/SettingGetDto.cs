namespace CallCenter.DTO.Settings
{
    /// <summary>
    /// ДТО настроек (GET)
    /// </summary>
    public class SettingGetDto
    {
        /// <summary>
        /// Id записи
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Время в очереди для взяти работу менеджером (сек)
        /// </summary>
        public int TimeManager { get; set; }
        
        /// <summary>
        /// Время в очереди для взяти работу директором (сек)
        /// </summary>
        public int TimeDirector { get; set; }
        
        /// <summary>
        /// Минимальная длительность звонка (сек)
        /// </summary>
        public int ExecuteTimeLimitLeft { get; set; }
        
        /// <summary>
        /// Максимальная длительность звонка (сек)
        /// </summary>
        public int ExecuteTimeLimitRight { get; set; }
    }
}