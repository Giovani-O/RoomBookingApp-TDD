using RoomBookingApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Core.DataServices
{
    /// <summary>
    /// Interface for room booking services
    /// </summary>
    public interface IRoomBookingService
    {
        void Save(RoomBooking roomBooking);
    }
}
