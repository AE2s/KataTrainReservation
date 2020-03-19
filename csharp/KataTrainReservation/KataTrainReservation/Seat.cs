using System;

namespace KataTrainReservation
{
    public class Seat : IEquatable<Seat>
    {
        public Coach Coach { get; }
        public int SeatNumber { get;}

        public Seat(Coach coach, int seatNumber)
        {
            Coach = coach;
            SeatNumber = seatNumber;
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
