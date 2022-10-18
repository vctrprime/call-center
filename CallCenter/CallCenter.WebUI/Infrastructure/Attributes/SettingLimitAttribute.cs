using System.Net;
using CallCenter.DTO.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CallCenter.WebUI.Infrastructure.Attributes
{
    /// <summary>
    /// Проверка на корректность ввода данных настроек
    /// </summary>
    public class SettingLimitAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dto = context.ActionArguments["dto"] as SettingPostDto;

            var error = CheckDto(dto);
            
            if (string.IsNullOrEmpty(error)) return;
            
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            
            context.Result = new ContentResult
            {
                Content = error
            };
        }

        private string CheckDto(SettingPostDto dto)
        {
            if (dto.ExecuteTimeLimitLeft >= dto.ExecuteTimeLimitRight) return "Некорректно введен диапазон лимитов!";
            
            if (dto.ExecuteTimeLimitLeft is < 10 or > 90) 
                return "Левый лимит должнен быть в диапазоне от 10 до 90!";
            if (dto.ExecuteTimeLimitRight is < 30 or > 120) 
                return "Левый лимит должнен быть в диапазоне от 30 до 120!";;
            
            if (dto.TimeManager is < 20 or > 80)  
                return "Время реакции директора должно быть в диапазоне от 20 до 80!";
            if (dto.TimeDirector is < 40 or > 100)  
                return "Время реакции директора должно быть в диапазоне от 40 до 100!";
            if (dto.TimeManager >= dto.TimeDirector)
                return "Время реакции директора не может быть меньше или равно времени реакции менеджера!";
            
            
            return string.Empty;
        }
    }
}