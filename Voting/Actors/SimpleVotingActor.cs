using Akka.Actor;
using System;
using System.Threading;
using Voting.Actors.Messages;
using Voting.Domain;

namespace Voting.Actors
{
    public class SimpleVotingActor : ReceiveActor
    {
        #region Internal State

        Talk Talk { get; set; }
        public bool IsVoteOpen { get; set; }

        #endregion

        public SimpleVotingActor()
        {
            Receive<BeginVoting>(message =>
            {
                this.IsVoteOpen = true;
                this.Talk = message.Talk;
            });
            Receive<Vote>(message =>
            {
                if (IsVoteOpen)
                {
                    EmulateLatency();
                    Talk.Votes.Add(message);
                    Console.WriteLine($"Satisfacton: {message.SatisfactionLevel}; Timestamp: {message.TimeStamp}; Processed: {DateTime.Now}");
                }
            });
        }

        #region Privat Helpers

        private void EmulateLatency()
        {
            Thread.Sleep(new Random().Next(100));
        }

        #endregion
    }
}
