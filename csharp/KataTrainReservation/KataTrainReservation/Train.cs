using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataTrainReservation
{
    public class Train : IEquatable<Train>
    {
        private readonly string trainId;
        private List<Coach> coachs;

        public Train(string trainId)
        {
            this.trainId = trainId;
            coachs = new List<Coach>();
        }

        public void AddCoachs(IEnumerable<Coach> coachsToAdd)
        {
            coachs.AddRange(coachsToAdd);
        }

        public void AddCoach(Coach coachToAdd)
        {
            coachs.Add(coachToAdd);
        }

        public override bool Equals(object obj)
        {
            var train = obj as Train;
            return Equals(train);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(trainId, coachs);
        }

        public bool Equals(Train train)
        {
            return train != null &&
                   trainId == train.trainId &&
                   coachs.SequenceEqual(train.coachs);
        }
    }

   
}
