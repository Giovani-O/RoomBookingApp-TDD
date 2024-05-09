using RoomBookingApp.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace RoomBookingApp.Domain;

public class RoomBooking : RoomBookingBase
{
    public int Id { get; set; }

    [JsonIgnore]
    public Room Room { get; set; }

    public int RoomId { get; set; }
}
