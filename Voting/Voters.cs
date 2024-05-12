using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Model;

namespace Voting
{
    public class Voters
    {
        List<VoterModel> voterList;
        public Voters() 
        {
            voterList = new List<VoterModel>();

        }

        public List<VoterModel> GetVoterList()
        {
            return voterList;
        }

        public void AddVoter(VoterModel voter)
        {
            voterList.Add(voter); 
        }

        public bool CheckVoter(VoterModel voter)
        {
            if (voterList.Any(x => x.VoterName == voter.VoterName))
            {
                Console.WriteLine($"Voter {voter.VoterName} already exist");
                return false;
            }

            return true;
        }

        public void UpdateVotingStatus(string voter)
        {
             voterList.FirstOrDefault(x => x.VoterName == voter).HasVoted = true;

            //voterList.Remove(voterData);
            //voterList.Add(voter); 
        }
    }
}
