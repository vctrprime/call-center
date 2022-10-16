using System.ComponentModel.DataAnnotations;

namespace CallCenter.DTO.Calls
{
    /// <summary>
    /// ДТО заявки (POST)
    /// </summary>
    public class CallPostDto
    {
        /// <summary>
        /// Кем создан
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }
    }
}