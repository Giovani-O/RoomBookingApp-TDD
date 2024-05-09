using RoomBookingApp.Domain;

namespace RoomBookingApp.Core.DataServices
{
    /// <summary>
    /// Interface for room booking services
    /// </summary>
    public interface IRoomBookingService
    {
        void Save(RoomBooking roomBooking);

        IEnumerable<Room> GetAvailableRooms(DateTime date);
    }
}
