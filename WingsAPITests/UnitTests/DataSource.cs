using System;
using System.Collections.Generic;
using System.Globalization;
using WingsOn.Domain;

namespace WingsAPITests.UnitTests
{
    public class DataSource
    {
        #region Properties
        public List<Booking> Bookings { get; }
        public List<Person> Persons { get; }
        public List<Flight> Flights { get; }
        #endregion

        CultureInfo cultureInfo = new CultureInfo("nl-NL");

        public DataSource()
        {
            Persons = new List<Person>()
            {
                new Person
                {
                    Id = 91,
                    Address = "805-1408 Mi Rd.",
                    DateBirth = DateTime.Parse("24/09/1980", new CultureInfo("nl-NL")),
                    Email = "egestas.a.dui@aliquet.ca",
                    Gender = GenderType.Male,
                    Name = "Kendall Velazquez"
                },
                new Person
                {
                    Id = 69,
                    Address = "P.O. Box 344, 5822 Curabitur Rd.",
                    DateBirth = DateTime.Parse("27/11/1948", new CultureInfo("nl-NL")),
                    Email = "non.cursus.non@turpisIncondimentum.co.uk",
                    Gender = GenderType.Female,
                    Name = "Claire Stephens"
                }
            };

            Bookings = new List<Booking>() {
                new Booking
                {
                    Id = 55,
                    Number = "WO-291470",
                    Customer = Persons[0],
                    DateBooking = DateTime.Parse("03/03/2006 14:30", cultureInfo),
                    Flight = new Flight(){
                        Id = 30,
                        Number = "BB124"
                    },
                    Passengers = Persons
                },
            
                new Booking
                {
                    Id = 83,
                    Number = "WO-151277",
                    Customer = Persons[1],
                    DateBooking = DateTime.Parse("12/02/2000 12:55", cultureInfo),
                    Flight = new Flight(){
                        Id = 81,
                        Number = "PZ696"
                    },
                    Passengers = Persons
                },
            };

            Flights = new List<Flight>() {
                new Flight
                {
                    Id = 81,
                    Number = "PZ696"
                },

                 new Flight
                {
                    Id = 30,
                    Number = "BB124",
                },
            };
        }
    }
}
