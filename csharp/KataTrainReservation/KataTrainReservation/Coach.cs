using System;

namespace KataTrainReservation
{
    public class Coach : IEquatable<Coach>, IComparable<Coach>
    {        

        public Coach(string coachId)
        {
            CoachId = coachId;
        }

        public string CoachId { get; }

        public int CompareTo(Coach other)
        {
            return CoachId.CompareTo(other.CoachId);
        }

        public override bool Equals(object obj)
        {
            var coach = obj as Coach;
            return Equals(coach);
        }

        public bool Equals(Coach other)
        {
            return other != null &&
                  CoachId == other.CoachId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CoachId);
        }
    }
}
