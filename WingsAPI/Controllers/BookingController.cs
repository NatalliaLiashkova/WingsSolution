using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsAPI.Model;
using WingsOn.DTO;
using WingsOnServices.Interfaces;

namespace WingsAPI.Controllers
{
    [ApiController]
    public class BookingController : Controller
    {
        #region Properties
        private readonly IBookingService bookingService;
        private readonly IMapper mapper;
        #endregion

        #region Ctors
        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            this.bookingService = bookingService;
            this.mapper = mapper;
        }
        #endregion  

        [HttpPost]
        [Route("api/booking/create/{flightNumber}")]
        public async Task<IActionResult> CreateBooking([FromBody]PersonModel person, string flightNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await bookingService.CreateBooking(flightNumber, mapper.Map<PersonDTO>(person));
            return Ok();
        }
    }
}
