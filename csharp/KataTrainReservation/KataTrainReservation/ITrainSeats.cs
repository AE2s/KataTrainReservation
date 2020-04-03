using System.Collections.Generic;

namespace KataTrainReservation
{
    public interface ITrainSeats
    {
        List<Train> GetSeatsByTrain();
    }
}
