using System.ComponentModel.DataAnnotations;

namespace CallCenter.DTO.Settings
{
    /// <summary>
    /// ДТО настроек (POST)
    /// </summary>
    public class SettingPostDto
    {
        /// <summary>
        /// Время в очереди для взяти работу менеджером (сек)
        /// </summary>
        [Required]
        public int TimeManager { get; set; }
        
        /// <summary>
        /// Время в очереди для взяти работу директором (сек)
        /// </summary>
        [Required]
        public int TimeDirector { get; set; }
        
        /// <summary>
        /// Минимальная длительность звонка (сек)
        /// </summary>
        [Required]
        public int ExecuteTimeLimitLeft { get; set; }
        
        /// <summary>
        /// Максимальная длительность звонка (сек)
        /// </summary>
        [Required]
        public int ExecuteTimeLimitRight { get; set; }
    }
}