using System.Collections.Generic;

namespace KataTrainReservation
{
    public class TrainSeatsMock : ITrainSeats
    {
        public List<Train> GetSeatsByTrain()
        {
            List<Train> trains = new List<Train>();
            var seatsForFirstTrain = new List<Seat>() { new Seat(new Coach("01"), 1), new Seat(new Coach("01"), 2)
                , new Seat(new Coach("01"), 3), new Seat(new Coach("01"), 4), new Seat(new Coach("02"), 5), new Seat(new Coach("02"), 6)
                , new Seat(new Coach("02"), 7), new Seat(new Coach("02"), 8), new Seat(new Coach("01"), 9), new Seat(new Coach("01"), 10)
                  };
            var seatsForSecondTrain = new List<Seat>() { new Seat(new Coach("02"), 1), new Seat(new Coach("02"), 2)
                , new Seat(new Coach("02"), 3), new Seat(new Coach("02"), 4), new Seat(new Coach("02"), 5), new Seat(new Coach("02"), 6)
                , new Seat(new Coach("02"), 7), new Seat(new Coach("02"), 8), new Seat(new Coach("02"), 9), new Seat(new Coach("02"), 10),
                new Seat(new Coach("03"), 1), new Seat(new Coach("03"), 2)
                , new Seat(new Coach("03"), 3), new Seat(new Coach("03"), 4), new Seat(new Coach("03"), 5), new Seat(new Coach("03"), 6)
                , new Seat(new Coach("03"), 7), new Seat(new Coach("03"), 8), new Seat(new Coach("03"), 9), new Seat(new Coach("03"), 10)
                  };

            var firstTrain = new Train("train1",seatsForFirstTrain);
            var secondTrain = new Train("train2", seatsForSecondTrain);

            trains.Add(firstTrain);
            trains.Add(secondTrain);
            return trains;
        }
    }
}
