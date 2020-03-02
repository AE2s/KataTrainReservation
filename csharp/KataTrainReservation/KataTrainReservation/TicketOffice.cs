using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataTrainReservation
{
    public class TicketOffice
    {
        
        public Reservation MakeReservation(ReservationRequest request)
        {
            var seats = new List<Seat>() { new Seat("01", 1) };
            return new Reservation(request.TrainId, string.Empty, seats);
        }
    }
}
