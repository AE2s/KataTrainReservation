using System;
using System.Collections.Generic;
using System.Linq;

namespace KataTrainReservation
{
    public class Train : IEquatable<Train>
    {
        public string TraindId { get; }
        public List<Seat> Seats { get; }


        public Train(string trainId, List<Seat> seats)
        {
            TraindId = trainId;
            Seats = seats;
        }

        public override bool Equals(object obj)
        {
            var train = obj as Train;
            return Equals(train);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TraindId, Seats);
        }

        public bool Equals(Train train)
        {
            return train != null &&
                   TraindId == train.TraindId &&
                   Seats.SequenceEqual(train.Seats);
        }
    }


}
