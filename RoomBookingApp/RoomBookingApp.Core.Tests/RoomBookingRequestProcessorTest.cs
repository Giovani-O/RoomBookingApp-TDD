using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.Core;

public class RoomBookingRequestProcessorTest
{
    private RoomBookingRequestProcessor _processor;
    private RoomBookingRequest _request;
    private Mock<IRoomBookingService> _roomBookingServiceMock;
    private List<Room> _availableRooms;

    public RoomBookingRequestProcessorTest()
    {
        _request = new RoomBookingRequest
        {
            FullName = "Test name",
            Email = "test@request.com",
            Date = new DateTime(2024, 05, 08)
        };

        _availableRooms = new List<Room>() { new Room() };

        // Creates mock for IRoomBookingService
        _roomBookingServiceMock = new Mock<IRoomBookingService>();
        _roomBookingServiceMock.Setup(q => q.GetAvailableRooms(_request.Date))
            .Returns(_availableRooms);
        _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
    }

    /// <summary>
    /// Tests if BookRoom result is not null
    /// </summary>
    [Fact]
    public void ShouldReturnRoomBookingResponseWithRequestValues()
    {
        // Act
        RoomBookingResult result = _processor.BookRoom(_request);

        // Assert
        // Assert.NotNull(result);
        // Assert.Equal(request.FullName, result.FullName);
        // Assert.Equal(request.Email, result.Email);
        // Assert.Equal(request.Date, result.Date);
        result.ShouldNotBeNull();
        result.FullName.ShouldBe(_request.FullName);
        result.Email.ShouldBe(_request.Email);
        result.Date.ShouldBe(_request.Date);
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

    /// <summary>
    /// Tests if we can save a new room booking
    /// </summary>
    [Fact]
    public void ShouldSaveRoomBookingRequest()
    {
        // Setup Save method in the mock object
        RoomBooking savedBooking = null;
        _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
            .Callback<RoomBooking>(booking =>
            {
                savedBooking = booking;
            });

        _processor.BookRoom(_request);

        // Verify if method was called once
        _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);

        savedBooking.ShouldNotBeNull();
        savedBooking.FullName.ShouldBe(_request.FullName);
        savedBooking.Email.ShouldBe(_request.Email);
        savedBooking.Date.ShouldBe(_request.Date);
    }

    /// <summary>
    /// Tests if room boking will not be saved when there are no rooms available
    /// </summary>
    [Fact]
    public void ShouldNotSaveRoomBookingIfNoneAvailable()
    {
        // Make sure there are no available rooms at the start of the method
        _availableRooms.Clear();
        // We call the method to book a room
        _processor.BookRoom(_request);
        // Verify if save was never called
        _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Never);
    }
}
