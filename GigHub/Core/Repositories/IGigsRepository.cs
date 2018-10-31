using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigsRepository
    {
        Gig GetGig(int id);
        Gig GetGigWithArtist(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        void Add(Gig gig);
        IEnumerable<Gig> GetUpcomingGigs();
    }
}