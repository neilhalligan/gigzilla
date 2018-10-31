﻿using System.Data.Entity;

namespace GigHub.Core.Models
{
    public interface IApplicationDbContext
    {
        DbSet<Gig> Gigs { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}