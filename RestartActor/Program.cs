using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestartActor
{
    class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            var value = Context.System.Settings.LogDeadLetters;
            Receive<string>(x => Console.WriteLine("Hello, {0}", x));
            Receive<RestartMessage>(x => ((IInternalActorRef)Self).Restart(new Exception("Some Reason")));

            Receive<RestartMessageDoNotUseThisApproach>(x =>
            {
                //Do not use this approach. All messages received after stop will be lost?
                var internalRef = ((IInternalActorRef)Self);
                internalRef.Stop();
                internalRef.Start();
            });
        }
        public static Props Props = Props.Create(() => new GreetingActor());
        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
        }
    }

    class RestartMessage
    {

    }

    class RestartMessageDoNotUseThisApproach
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("system");
            var greetingActor = system.ActorOf(GreetingActor.Props);

            greetingActor.Tell("John");
            greetingActor.Tell(new RestartMessage());

            Thread.Sleep(1000);

            20.Times(() => greetingActor.Tell("Jack"));        

            Console.ReadLine();
        }
    }
}



