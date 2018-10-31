using System.Web.Http;
using GigHub.Core;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.APIs
{
    [Authorize]
    public class GigsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null || gig.IsCancelled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();
            
            gig.Cancel();

            _unitOfWork.Complete();
            return Ok();
        }
    }
}
