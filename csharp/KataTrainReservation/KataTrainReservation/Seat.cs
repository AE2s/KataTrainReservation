using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataTrainReservation
{
    public class Seat : IEquatable<Seat>
    {
        public Coach Coach { get; private set; }
        public int SeatNumber { get; private set; }

        public Seat(Coach coach, int seatNumber)
        {
            this.Coach = coach;
            this.SeatNumber = seatNumber;
        }

        public override bool Equals(object obj)
        {
            var seat = obj as Seat;
            return Equals(seat);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coach, SeatNumber);
        }

        public bool Equals(Seat seat)
        {
            return seat != null &&
                  Coach.Equals(seat.Coach) &&
                  SeatNumber == seat.SeatNumber;
        }
    }
}
