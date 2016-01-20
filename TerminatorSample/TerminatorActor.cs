using Akka.Actor;

namespace TerminatorSample
{
    class TerminatorActor : ReceiveActor
    {
        #region Messages

        public class Terminate
        {

        }

        #endregion

        public TerminatorActor()
        {   
            this.Receive<Terminate>(m =>
            {
                Context.System.Terminate();
            }, m =>
            {               
                //Use Consesus pattern here to make sure that the sender can actually shutdown the system
                return this.Sender.Path.ToString().Contains("SuperHero");
                //return this.Sender.Path.ToString().Contains("akka://all-systems/");
            });
        }
    }
}
