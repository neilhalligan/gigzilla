﻿using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistance;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
            
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();

            var gig = new Gig()
            {
                Artist = user,
                Genre = genre,
                Venue = "TestVenue",
                DateTime = DateTime.Now.AddDays(1)
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            var result = _controller.Mine();
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(2);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateGivenGig()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);

            var gig = new Gig()
            {
                Artist = user,
                Genre = genre,
                Venue = "TestVenue",
                DateTime = DateTime.Now.AddMonths(1)
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            var result = _controller.Update(new GigFormViewModel
            {
                Id = gig.Id,
                Date = DateTime.Today.AddMonths(2).ToString("d MMM yyyy"),
                Time = "20:00",
                Genre = 2,
                Venue = "A Venue"
            });

            _context.Entry(gig).Reload();

            gig.DateTime.Should().Be(DateTime.Today.AddMonths(2).AddHours(20));
            gig.Venue.Should().Be("A Venue");
            gig.GenreId.Should().Be(2);
        }
    }
}
