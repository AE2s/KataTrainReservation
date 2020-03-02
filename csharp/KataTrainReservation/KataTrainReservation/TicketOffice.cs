using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataTrainReservation
{
    public class TicketOffice
    {
        private readonly ISeatService _seatService;
        private const double MAX_PERCENT_SEAT_FILL = 0.7;

        public TicketOffice(ISeatService seatService)
        {
            _seatService = seatService;
        }
        
        public Reservation MakeReservation(ReservationRequest request)
        {
            var freeSeats = _seatService.GetAvailableSeats(request.TrainId);

            if (request.SeatCount > freeSeats.Count*MAX_PERCENT_SEAT_FILL)
                return null;

            var reservedSeats = freeSeats.GetRange(0, request.SeatCount);            

            return new Reservation(request.TrainId, string.Empty, reservedSeats);
        }
    }
}
