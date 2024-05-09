using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private IRoomBookingRequestProcessor _roomBookingProcessor;

        public RoomBookingController(IRoomBookingRequestProcessor roomBookingProcessor)
        {
            this._roomBookingProcessor = roomBookingProcessor;
        }

        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
