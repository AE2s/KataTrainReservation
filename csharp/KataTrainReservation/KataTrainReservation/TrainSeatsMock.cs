using System;
using System.Collections.Generic;
using System.Text;

namespace KataTrainReservation
{
    public class TrainSeatsMock : ITrainSeats
    {
        public Dictionary<string, List<Seat>> GetSeatsByTrain()
        {
            return  new Dictionary<string, List<Seat>>()
            {
                {"train1",   new List<Seat>() { new Seat("01", 1), new Seat("01", 2)
                , new Seat("01", 3), new Seat("01", 4), new Seat("02", 5), new Seat("02", 6)
                , new Seat("02", 7), new Seat("02", 8), new Seat("01", 9), new Seat("01", 10)
                  } },
                {"train2",   new List<Seat>() { new Seat("02", 1), new Seat("02", 2)
                , new Seat("02", 3), new Seat("02", 4), new Seat("02", 5), new Seat("02", 6)
                , new Seat("02", 7), new Seat("02", 8), new Seat("02", 9), new Seat("02", 10)
                  } }
            };
        }
    }
}
