using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Domain;
using RoomBookingApp.Persistence.Repositories;

namespace RoomBookingApp.Persistence.Tests;

public class RoomBookingServiceTest
{
    private DbContextOptions<RoomBookingAppDbContext> DbContextSetup(string inMemoryDatabase)
    {
        return new DbContextOptionsBuilder<RoomBookingAppDbContext>()
        .UseInMemoryDatabase(inMemoryDatabase).Options;
    }

    /// <summary>
    /// Tests if available rooms are being returned
    /// </summary>
    [Fact]
    public void ShoulReturnAvailableRooms()
    {
        // Arrange
        var date = new DateTime(2024, 05, 09);

        var dbOptions = DbContextSetup("AvailableRoomTest");

        // Creates new context and seeds it with data
        var context = new RoomBookingAppDbContext(dbOptions);

        context.Add(new Room { Id = 1, Name = "Room 1" });
        context.Add(new Room { Id = 2, Name = "Room 2" });
        context.Add(new Room { Id = 3, Name = "Room 3" });

        context.Add(new RoomBooking { RoomId = 1, Date = date });
        context.Add(new RoomBooking { RoomId = 2, Date = date.AddDays(-1) });

        context.SaveChanges();

        var roomBookingService = new RoomBookingService(context);

        // Act
        var availableRooms = roomBookingService.GetAvailableRooms(date);

        // Assert
        Assert.Equal(2, availableRooms.Count());
        Assert.Contains(availableRooms, q => q.Id == 2);
        Assert.Contains(availableRooms, q => q.Id == 3);
        Assert.DoesNotContain(availableRooms, q => q.Id == 1);
    }

    /// <summary>
    /// Tests if romm booking is being saved
    /// </summary>
    [Fact]
    public void ShouldSaveRoomBooking()
    {
        var dbOptions = DbContextSetup("ShouldSaveTest");

        var roomBooking = new RoomBooking { RoomId = 1, Date = new DateTime(2024, 05, 09) };

        var context = new RoomBookingAppDbContext(dbOptions);
        var roomBookingService = new RoomBookingService(context);
        roomBookingService.Save(roomBooking);

        var bookings = context.RoomBookings.ToList();
        var booking = Assert.Single(bookings);

        Assert.Equal(roomBooking.Date, booking.Date);
        Assert.Equal(roomBooking.RoomId, booking.RoomId);
    }
}
