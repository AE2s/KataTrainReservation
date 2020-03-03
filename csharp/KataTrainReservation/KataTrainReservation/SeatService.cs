using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KataTrainReservation
{
    public class SeatService : ISeatService
    {
        private readonly ITrainSeats _trainSeats;
        private readonly Dictionary<string, List<Seat>> seatsByTrain;
        List<Seat> seatFilled;

        public SeatService(ITrainSeats trainSeats)
        {          
            seatFilled = new List<Seat>();
            _trainSeats = trainSeats;
            seatsByTrain = _trainSeats.GetSeatsByTrain();
        }

        public void ConfirmReservation(List<Seat> seats)
        {
            seatFilled.AddRange(seats);
        }

        public List<Seat> GetAvailableSeats(string trainId)
        {            
            if (!seatsByTrain.ContainsKey(trainId))
                return new List<Seat>();

            return seatsByTrain[trainId].Except(seatFilled).ToList();
        }
    }
}
