using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistance.Repositories;

namespace GigHub.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigsRepository Gigs { get; private set; }
        public IAttendancesRepository Attendances { get; private set; }
        public IFollowingsRepository Followings { get; private set; }
        public IGenresRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigsRepository(context);
            Attendances = new AttendancesRepository(context);
            Followings = new FollowingsRepository(context);
            Genres = new GenresRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}