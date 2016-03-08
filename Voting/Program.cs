using Akka.Actor;
using Akka.Routing;
using System;
using System.Threading;
using Voting.Actors;
using Voting.Actors.Messages;
using Voting.Domain;
using Voting.Extensions;

namespace Voting
{   
    class Program
    {
        static void Main(string[] args)
        {
            var votingActorSystem = ActorSystem.Create("votingActorSystem");

            //var votingActor = votingActorSystem.ActorOf<VotingActor>("votingActor");

            var votingProps = Props.Create<VotingActor>();//.WithRouter(new RoundRobinPool(8));
            var votingActor = votingActorSystem.ActorOf(votingProps, "votingActor");


            votingActor.Tell(new BeginVoting(new Talk
            {
                EventDate = DateTime.Now,
                Speaker = "Ruslan Zavacky",
                Title = "You Know, for Search"
            }));

            100.Times(() =>
            {
                votingActor.Tell(new Vote { SatisfactionLevel = SatisfactionLevel.Green, TimeStamp = DateTime.Now });
            });

            Console.ReadLine();
        }
    }
}
