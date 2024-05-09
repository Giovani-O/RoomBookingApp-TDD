using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        private RoomBookingAppDbContext _context;

        public RoomBookingService(RoomBookingAppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            var availableRooms = _context.Rooms
                .Where(q => !q.RoomBookings
                .Any(x => x.Date == date))
                .ToList();

            return availableRooms;
        }

        public void Save(RoomBooking roomBooking)
        {
            throw new NotImplementedException();
        }
    }
}
