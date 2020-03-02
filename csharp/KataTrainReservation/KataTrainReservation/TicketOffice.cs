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

        public TicketOffice(ISeatService seatService)
        {
            _seatService = seatService;
        }
        
        public Reservation MakeReservation(ReservationRequest request)
        {
            var seats = _seatService.GetSeats(request.TrainId);
            var reservedSeats = seats.GetRange(0, request.SeatCount);            

            return new Reservation(request.TrainId, string.Empty, reservedSeats);
        }
    }
}
