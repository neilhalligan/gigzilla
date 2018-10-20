using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingsRepository
    {
        IEnumerable<ApplicationUser> GetFollowees(string userId);
    }
}