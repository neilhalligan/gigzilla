using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigsRepository
    {
        Gig GetGig(int id);
        Gig GetGigWithArtist(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigsWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtistWithGenre(string userId);
        void Add(Gig gig);
    }
}