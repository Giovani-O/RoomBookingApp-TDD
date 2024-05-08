using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoomBookingApp.Core.Processors;

public class RoomBookingRequestProcessor
{
    private IRoomBookingService _roomBookingService;

    public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
    {
        this._roomBookingService = roomBookingService;
    }

    public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
    {
        if (bookingRequest is null)
            throw new ArgumentNullException(nameof(bookingRequest));

        _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));

        return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
    }

    /// <summary>
    /// Create a new RoomBooking object
    /// </summary>
    /// <typeparam name="TRoomBooking"></typeparam>
    /// <param name="bookingRequest"></param>
    /// <returns>TRoomBooking which is a RoomBookingBase</returns>
    private static TRoomBooking CreateRoomBookingObject<TRoomBooking>
        (RoomBookingRequest bookingRequest) where TRoomBooking : RoomBookingBase, new()
    {
        return new TRoomBooking
        {
            FullName = bookingRequest.FullName,
            Email = bookingRequest.Email,
            Date = bookingRequest.Date,
        };
    }
}