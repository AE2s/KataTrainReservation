using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataTrainReservation
{
    public class BookingService : IBookingService
    {       
        private const int MAX_LENGTH_BOOKING_ID = 5;
        private const string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        
        public string GetBookingId()
        {
            return RandomString();
        }
        
        private static string RandomString()
        {
            Random random = new Random();
            return new string(Enumerable.Repeat(CHARACTERS, MAX_LENGTH_BOOKING_ID)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
