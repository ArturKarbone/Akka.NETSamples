using Akka.Actor;
using Akka.Routing;
using System;
using System.Threading;
using Voting.Actors.Messages;
using Voting.Domain;

namespace Voting.Actors
{
    class VotingCoordinatorActor : ReceiveActor
    {
        #region Internal State

        IActorRef VotingActor { get; set; }

        #endregion

        public VotingCoordinatorActor()
        {
            Become(ClosedForVoting);
        }

        #region Behaviours

        public void ClosedForVoting()
        {
            Receive<BeginVoting>(message =>
            {
                var votingProps = Props.Create<VotingActor>(() => new VotingActor(message.Talk)).WithRouter(new RoundRobinPool(8));              
                VotingActor = Context.ActorOf(votingProps, "votingActor");
                Become(OpenForVoting);
            });
        }

        public void OpenForVoting()
        {
            Receive<Vote>(message =>
            {               
                VotingActor.Tell(message);              
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
