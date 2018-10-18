using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Models;

namespace GigHub.Repositories
{
    public class GigsRepository
    {
        private readonly ApplicationDbContext _context;

        public GigsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGig(int id)
        {
            return _context.Gigs
                .Single(g => g.Id == id);
        }

        public Gig GetGigWithArtist(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigsWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);

        }

        public IEnumerable<Gig> GetUpcomingGigsByArtistWithGenre(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId &&
                            g.DateTime > DateTime.Now &&
                            !g.IsCancelled)
                .Include(g => g.Genre)
                .ToList();
        }
    }

}