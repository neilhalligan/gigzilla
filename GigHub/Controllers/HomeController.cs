using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using static System.String;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private readonly GigsRepository _gigsRepository;
        private readonly AttendancesRepository _attendancesRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _gigsRepository = new GigsRepository(_context);
            _attendancesRepository = new AttendancesRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();

            var upcomingGigs = _gigsRepository.GetGigsUserAttending(userId);

            if (!IsNullOrEmpty(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }


            var attendances = _attendancesRepository.GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);

            var gigsViewModel = new GigsViewModel
            {
                SearchTerm = query,
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                Attendances = attendances
            };

            return View("Gigs", gigsViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}