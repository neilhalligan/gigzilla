using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendancesRepository
    {
        Attendance GetAttendance(string userId, Gig gig);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}