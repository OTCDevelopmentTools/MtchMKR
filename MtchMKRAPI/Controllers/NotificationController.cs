using Microsoft.AspNetCore.Mvc;
using MtchMKRAPI.Data.Entities;
using MtchMKRAPI.Services;

namespace MtchMKRAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("SendNotification")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        //[Route("sendApple")]
        //[HttpPost]
        //public async Task<IActionResult> SendAppleNotification(NotificationModel notificationModel)
        //{
        //    var result = await _notificationService.SendAppleNotification(notificationModel);
        //    return Ok(result);
        //}
    }
}


