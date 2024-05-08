using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.Core;

public class RoomBookingRequestProcessorTest
{
    private RoomBookingRequestProcessor _processor;

    public RoomBookingRequestProcessorTest()
    {
        _processor = new RoomBookingRequestProcessor();
    }

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

        // Act
        RoomBookingResult result = _processor.BookRoom(request);

        // Assert
        // Assert.NotNull(result);
        // Assert.Equal(request.FullName, result.FullName);
        // Assert.Equal(request.Email, result.Email);
        // Assert.Equal(request.Date, result.Date);
        result.ShouldNotBeNull();
        result.FullName.ShouldBe(request.FullName);
        result.Email.ShouldBe(request.Email);
        result.Date.ShouldBe(request.Date);
    }

    /// <summary>
    /// Tests if an exception will be thrown when request is null
    /// </summary>
    [Fact]
    public void ShouldThrowExceptionForNullRequest()
    {
        var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));

        // After checking if an ArgumentNullException was thrown, checks if the argument name is request
        exception.ParamName.ShouldBe("bookingRequest");
    }
}
