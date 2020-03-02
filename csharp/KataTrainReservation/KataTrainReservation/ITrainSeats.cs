using System;
using System.Collections.Generic;
using System.Text;

namespace KataTrainReservation
{
    public interface ITrainSeats
    {
        Dictionary<string, List<Seat>> GetSeatsByTrain();
    }
}
