using System;
using System.Collections;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistance.Repositories;
using static GigHub.IntegrationTests.Extensions.MockDbExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class GigsRepositoryTests
    {
        private GigsRepository _gigsRepository;
        private Mock<IApplicationDbContext> _mockContext;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _userId = "1";
            _mockContext = new Mock<IApplicationDbContext>();
            _gigsRepository = new GigsRepository(_mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig {DateTime = DateTime.Now.AddDays(-1), ArtistId = _userId};
            var gigs = GetQueryableMockDbSet(gig);
            _mockContext.SetupGet(c => c.Gigs).Returns(gigs);

            var result = _gigsRepository.GetUpcomingGigsByArtist(_userId);

            result.Should().BeEmpty();
        }
    }
}
