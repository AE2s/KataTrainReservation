using System.Collections.Generic;
using System.Linq;

namespace KataTrainReservation
{
    public class SeatService : ISeatService
    {
        private readonly ITrainSeats _trainSeats;
        private readonly List<Train> trains;
        List<Seat> seatFilled;

        public SeatService(ITrainSeats trainSeats)
        {          
            seatFilled = new List<Seat>();
            _trainSeats = trainSeats;
            trains = _trainSeats.GetSeatsByTrain();
        }

        public void ConfirmReservation(List<Seat> seats)
        {
            seatFilled.AddRange(seats);
        }

        public List<Seat> GetAvailableSeats(string trainId)
        {
            var train = trains.SingleOrDefault(x => x.TraindId == trainId);
            if (train == null)
                return new List<Seat>();

            return train.Seats.Except(seatFilled).ToList();
        }
    }
}
