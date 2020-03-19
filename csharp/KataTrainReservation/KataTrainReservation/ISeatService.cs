using System.Collections.Generic;

namespace KataTrainReservation
{
    public interface ISeatService
    {
        List<Seat> GetAvailableSeats(string trainId);

        void ConfirmReservation(List<Seat> seats);
    }
}
