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
        private readonly IBookingService _bookingService;
        private const double MAX_PERCENT_SEAT_FILL = 0.7;

        public TicketOffice(ISeatService seatService, IBookingService bookingService)
        {
            _seatService = seatService;
            _bookingService = bookingService;
        }
        
        public Reservation MakeReservation(ReservationRequest request)
        {
            var freeSeats = _seatService.GetAvailableSeats(request.TrainId);

            if (request.SeatCount > freeSeats.Count*MAX_PERCENT_SEAT_FILL)
                return null;

            var reservedSeats = freeSeats.GetRange(0, request.SeatCount);            
            
            return new Reservation(request.TrainId, _bookingService.GetBookingId(), reservedSeats);
        }
    }
}
