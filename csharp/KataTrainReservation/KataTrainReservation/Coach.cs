using System;
using System.Collections.Generic;
using System.Text;

namespace KataTrainReservation
{
    public class Coach : IEquatable<Coach>, IComparable<Coach>
    {
        private readonly string coachId;

        public Coach(string coachId)
        {
            this.coachId = coachId;

        }

        public string CoachId
        {
            get;
        }

        public int CompareTo(Coach other)
        {
            return coachId.CompareTo(other.CoachId); 
        }

        public override bool Equals(object obj)
        {
            var coach = obj as Coach;
            return Equals(coach);
        }

        public bool Equals(Coach other)
        {
            return other != null &&
                  coachId == other.coachId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(coachId);
        }
    }
}
