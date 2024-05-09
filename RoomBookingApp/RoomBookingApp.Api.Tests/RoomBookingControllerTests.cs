﻿using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBookingApp.Api.Controllers;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Api.Tests
{
    public class RoomBookingControllerTests
    {
        private Mock<IRoomBookingRequestProcessor> _roomBookingProcessor;
        private RoomBookingController _controller;
        private RoomBookingRequest _request;
        private RoomBookingResult _result;

        public RoomBookingControllerTests()
        {
            _roomBookingProcessor = new Mock<IRoomBookingRequestProcessor>();
            _controller = new RoomBookingController(_roomBookingProcessor.Object);
            _request = new RoomBookingRequest();
            _result = new RoomBookingResult();

            _roomBookingProcessor.Setup(x => x.BookRoom(_request)).Returns(_result);
        }

        /// <summary>
        /// Tests if BookRoom is called when valid, and not called when invalid
        /// </summary>
        /// <param name="expectedMethodCalls"></param>
        /// <param name="isModelValid"></param>
        /// <param name="expectedActionResultType"></param>
        [Theory]
        [InlineData(1, true, typeof(OkObjectResult))]
        [InlineData(0, false, typeof(BadRequestObjectResult))]
        public async Task ShouldCallBookingMethodWhenValid(
            int expectedMethodCalls, 
            bool isModelValid, 
            Type expectedActionResultType
        )
        {
            // Arrange
            if (!isModelValid)
            {
                _controller.ModelState.AddModelError("Key", "ErrorMessage");
            }

            // Act
            var result = await _controller.BookRoom(_request);

            // Assert
            result.ShouldBeOfType(expectedActionResultType);
            _roomBookingProcessor.Verify(x => x.BookRoom(_request), Times.Exactly(expectedMethodCalls));
        }
    }
}
