using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Core;

public  class RoomBookingRequestProcessorTest
{
    /// <summary>
    /// Tests if BookRoom result is not null
    /// </summary>
    [Fact]
    public void ShouldReturnRoomBookingResponseWithRequestValues()
    {
        // Arrange
        var request = new RoomBookingRequest
        {
            FullName = "Test name",
            Email = "test@request.com",
            Date = new DateTime(2024, 05, 08)
        };

        var processor = new RoomBookingRequestProcessor();

        // Act
        RoomBookingResult result = processor.BookRoom(request);

        // Assert
        // Assert.NotNull(result);
        //Assert.Equal(request.FullName, result.FullName);
        //Assert.Equal(request.Email, result.Email);
        //Assert.Equal(request.Date, result.Date);
        result.ShouldNotBeNull();
        result.FullName.ShouldBe(request.FullName);
        result.Email.ShouldBe(request.Email);
        result.Date.ShouldBe(request.Date);
    }
}
