using System;
using System.Collections.Generic;

namespace Voting.Domain
{
    class Talk
    {       
        public string Speaker { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public List<Vote> Votes { get; set; } = new List<Vote>();
    }
}
