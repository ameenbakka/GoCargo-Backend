using GoCargo.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoCargo.Application.Dto;

namespace GoCargo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [HttpGet("my")]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _notificationService.GetUserNotifications(userId);
            return Ok(result);
        }


        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> Send([FromBody] SendNotificationDto dto)
        {
            await _notificationService.SendNotification(dto.RecipientId, dto.Title, dto.Message);
            return Ok(new { Message = "Notification sent." });
        }


        [HttpPut("read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsRead(id);
            return Ok(new { Message = "Marked as read." });
        }
    }
}
