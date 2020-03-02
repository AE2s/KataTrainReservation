using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KataTrainReservation
{
    public class SeatService : ISeatService
    {
        Dictionary<string, List<Seat>> seatByTrains;
        List<Seat> seatFilled;

        public SeatService()
        {
            seatByTrains = new Dictionary<string, List<Seat>>()
            {
                {"train1",   new List<Seat>() { new Seat("01", 1), new Seat("01", 2)
                , new Seat("01", 3), new Seat("01", 4), new Seat("01", 5), new Seat("01", 6)
                , new Seat("01", 7), new Seat("01", 8), new Seat("01", 9), new Seat("01", 10)
                  } },
                {"train2",   new List<Seat>() { new Seat("02", 1), new Seat("02", 2)
                , new Seat("02", 3), new Seat("02", 4), new Seat("02", 5), new Seat("02", 6)
                , new Seat("02", 7), new Seat("02", 8), new Seat("02", 9), new Seat("02", 10)
                  } }
            };

            seatFilled = new List<Seat>();
        }

        public void ConfirmReservation(string trainId, List<Seat> seats)
        {
            seatFilled.AddRange(seats);
        }

        public List<Seat> GetAvailableSeats(string trainId)
        {
            if (!seatByTrains.ContainsKey(trainId))
                return new List<Seat>();

            return seatByTrains[trainId].Except(seatFilled).ToList();
        }
    }
}
