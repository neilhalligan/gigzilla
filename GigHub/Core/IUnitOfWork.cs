using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigsRepository Gigs { get; }
        IAttendancesRepository Attendances { get; }
        IFollowingsRepository Followings { get; }
        IGenresRepository Genres { get; }
        void Complete();
    }
}