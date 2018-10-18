using System.Linq;
using System.Web.Http;
using GigHub.DTOs;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.APIs
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

            var follow = _context.Follows
                .Any(f => f.FolloweeId == followeeId && f.FollowerId == followerId);

            if (follow)
            {
                return BadRequest("Follow already exists");
            }

            var following = new Following(followerId, followeeId);

            _context.Follows.Add(following);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var userId = User.Identity.GetUserId();
            var follow = _context.Follows
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (follow == null)
                return BadRequest("Followee Id not found");

            _context.Follows.Remove(follow);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public bool isFollowing(string followeeId)
        {
            var userId = User.Identity.GetUserId();
            var isFollowing = _context.Follows
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == followeeId);

            if (isFollowing == null)
                return false;

            return true;
        }
    }
}
