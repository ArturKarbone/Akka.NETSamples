using Akka.Actor;
using System;

namespace TerminatorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().Wait();
        }

        private static async System.Threading.Tasks.Task AsyncMain()
        {
            using (var myActorSystem = ActorSystem.Create("myActorSystem"))
            {
                var terminatorActor = myActorSystem.ActorOf(Props.Create(() => new TerminatorActor()), "terminatorActor");

                //Emulate unhandled message
                terminatorActor.Tell("Terminate this system");

                terminatorActor.Tell(new TerminatorActor.Terminate());
                //myActorSystem.AwaitTermination();
                await myActorSystem.WhenTerminated;

                Console.WriteLine("Actor System has been terminated");
                Console.ReadLine();
            }
        }
    }
}
