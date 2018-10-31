using System.Linq;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.DTOs;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.APIs
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var gigId = dto.GigId;
            var userId = User.Identity.GetUserId();

            var existingAttendance = _unitOfWork.Attendances.GetAttendance(userId, gigId);

            if (existingAttendance != null)
                return BadRequest("The attendance already exists");

            var attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(userId, id);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
