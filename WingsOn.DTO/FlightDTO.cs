using System;

namespace WingsOn.DTO
{
    public class FlightDTO
    {
        public string Number { get; set; }
        public AirlineDTO Carrier { get; set; }
        public AirportDTO DepartureAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public AirportDTO ArrivalAirport { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }
    }
}
