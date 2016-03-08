using System;

namespace Voting.Domain
{
    enum SatisfactionLevel
    {
        Red,
        Green,
        Yellow
    }

    class Vote
    {
        public SatisfactionLevel SatisfactionLevel { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
