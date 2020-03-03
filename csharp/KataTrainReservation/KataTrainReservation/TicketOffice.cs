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

            if (!CanFillTrainWith(request.SeatCount, freeSeats.Count))
                return null;

            var orderedSeats = freeSeats.OrderBy(x => x.Coach).ThenBy(x => x.SeatNumber).ToList();
            var reservedSeats = new List<Seat>();
            foreach (var seat in orderedSeats.GroupBy(x=>x.Coach))
            {
                if (request.SeatCount <= seat.Count())
                {
                    reservedSeats = seat.ToList().GetRange(0, request.SeatCount);
                    _seatService.ConfirmReservation(reservedSeats);
                    return new Reservation(request.TrainId, _bookingService.GetBookingId(), reservedSeats);
                }
            }

            return null;
        }

        private bool CanFillTrainWith(int requestSeatCount, int freeTrainSeat)
        {
            return requestSeatCount <= freeTrainSeat * MAX_PERCENT_SEAT_FILL;
        }
    }
}
