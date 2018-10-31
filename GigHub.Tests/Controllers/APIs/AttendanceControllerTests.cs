using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.APIs;
using GigHub.Core;
using GigHub.Core.DTOs;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.IntegrationTests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.APIs
{
    [TestClass]
    public class AttendanceControllerTests
    {
        private string _userId;
        private AttendancesController _controller;
        private Mock<IAttendancesRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendancesRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(m => m.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

   
        [TestMethod]
        public void Attend_UserAttendsGigAlreadyAttending_ShouldReturnBadRequest()
        {
            var attendance = new Attendance()
            {
                AttendeeId = "1",
                GigId = 1
            };
            _mockRepository.Setup(r => r.GetAttendance("1", 1)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto() {GigId = 1});

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_UserAttendsValidGig_ShouldReturnOk()
        {
            _mockRepository.Setup(r => r.GetAttendance("1", 1)).Returns((Attendance) null);
            var result = _controller.Attend(new AttendanceDto() { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }
    }
}
