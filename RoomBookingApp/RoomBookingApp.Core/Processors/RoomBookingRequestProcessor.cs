using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Enums;
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

    /// <summary>
    /// Book a room
    /// </summary>
    /// <param name="bookingRequest"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
    {
        if (bookingRequest is null)
            throw new ArgumentNullException(nameof(bookingRequest));

        var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);
        var result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

        // Save room booking if there are available rooms
        if (availableRooms.Any())
        {
            var room = availableRooms.First();
            var roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);
            roomBooking.RoomId = room.Id;
            _roomBookingService.Save(roomBooking);

            result.Flag = BookingResultFlag.Success;
        }
        else
        {     
            result.Flag = BookingResultFlag.Failure;
        }

        return result;
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