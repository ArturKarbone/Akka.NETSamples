using Voting.Domain;

namespace Voting.Actors.Messages
{
    class BeginVoting
    {
        public BeginVoting(Talk talk)   
        {
            this.Talk = talk;
        }

        public Talk Talk { get; private set; }
    }
}
