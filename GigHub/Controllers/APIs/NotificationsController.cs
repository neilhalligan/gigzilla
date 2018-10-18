using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using GigHub.DTOs;
using GigHub.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.APIs
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications =  _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ProjectTo<NotificationDto>()
                .ToList();

            return notifications;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var userNotifications = _context.UserNotifications
                .Where(un => !un.IsRead && un.UserId == userId);
            
            userNotifications.ForEach(un => un.Read());
            _context.SaveChanges();

            return Ok();
        }
    }
}
