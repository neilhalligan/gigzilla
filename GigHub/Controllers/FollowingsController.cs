using System.Linq;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var followeeId = dto.FolloweeId;
            var followerId = User.Identity.GetUserId();

            var exists = _context.Follows
                .Any(f => f.FolloweeId == followeeId && f.FollowerId == followerId);

            if (exists)
                return BadRequest("Already following this artist!");

            var following = new Following
            {
                FollowerId = followerId,
                FolloweeId = followeeId
            };

            _context.Follows.Add(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}
