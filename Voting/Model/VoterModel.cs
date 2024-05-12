using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Model
{
    public class VoterModel
    {
        public string VoterName { get; set; }
        public bool HasVoted { get; set; }

    }

    public class CandidateModel
    {
        public string CandidateName { get; set; }
        public int votes { get; set; }
    }
}
