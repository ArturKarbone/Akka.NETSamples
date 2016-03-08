using Akka.Actor;
using System;
using System.Threading;
using Voting.Actors.Messages;
using Voting.Domain;

namespace Voting.Actors
{
    public class VotingActor : ReceiveActor
    {
        #region Internal State

        Talk Talk { get; set; }      

        #endregion

        public VotingActor()
        {
            Become(ClosedForVoting);    
        }

        #region Behaviours

        public void ClosedForVoting()
        {
            Receive<BeginVoting>(message =>
            {                
                this.Talk = message.Talk;                
                Become(OpenForVoting);
            });
        }

        public void OpenForVoting()
        {
            Receive<Vote>(message =>
            {
                EmulateLatency();
                Talk.Votes.Add(message);
                Console.WriteLine($"Satisfacton: {message.SatisfactionLevel}; Timestamp: {message.TimeStamp}");
            });
        }

        #endregion

        #region Privat Helpers

        private void EmulateLatency()
        {
            Thread.Sleep(new Random().Next(100));
        }

        #endregion
    }
}
