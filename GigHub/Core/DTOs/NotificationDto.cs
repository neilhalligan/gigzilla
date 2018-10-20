using System;
using GigHub.Core.Models;

namespace GigHub.Core.DTOs
{
    public class NotificationDto
    {
        public int id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public GigDto Gig { get; set; }
    }
}