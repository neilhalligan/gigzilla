using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GigsRepository _gigsRepository;
        private readonly AttendancesRepository _attendancesRepository;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _gigsRepository = new GigsRepository(_context);
            _attendancesRepository = new AttendancesRepository(_context);
        }

        [Authorize]
        public ActionResult Attending()
        {
            ViewBag.Message = "Gigs your attending";

            var userId = User.Identity.GetUserId();

            var gigsViewModel = new GigsViewModel
            {
                UpcomingGigs = _gigsRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "View Gigs I'm Attending",
                Attendances = _attendancesRepository.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)
            };

            return View("Gigs", gigsViewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _gigsRepository.GetUpcomingGigsByArtistWithGenre(userId);

            return View(gigs);
        }

        [Authorize]
        public ActionResult Following()
        {
            ViewBag.Message = "Who You're Following";

            var userId = User.Identity.GetUserId();

            // GetArtistsFollowing
            var followees = _context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            return View(followees);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _gigsRepository.GetGig(id);

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Genre = gig.GenreId,
                Genres = _context.Genres.ToList(),
                Heading = "Edit Your Gig"
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                viewModel.Heading = "Add a Gig";
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                Venue = viewModel.Venue,
                GenreId = viewModel.Genre
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                viewModel.Heading = "Edit a Gig";
                return View("GigForm", viewModel);
            }

            var gig = _gigsRepository.GetGigsWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm});
        }

        public ActionResult Detail(int gigId)
        {
            var gig = _gigsRepository.GetGigWithArtist(gigId);

            if (gig == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            bool userIsAttending = 
                _attendancesRepository.GetAttendance(userId, gig) != null;

            var viewModel = new GigDetailViewModel
            {
                Gig = gig,
                IsLoggedIn = User.Identity.IsAuthenticated,
                UserIsAttending = userIsAttending
            };

            return View(viewModel);
        }
    }
}