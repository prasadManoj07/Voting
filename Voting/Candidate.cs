using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Model;

namespace Voting
{
    public class Candidate
    {
        List<CandidateModel> candidateList;

        public Candidate()
        {
            candidateList = new List<CandidateModel>();
        }

        public List<CandidateModel> GetCandidates()
        {
            return candidateList;
        }

        public void  AddCandidate(CandidateModel candidate)
        {
            

            candidateList.Add(candidate);
           
        }

        public bool CheckVoter(CandidateModel candidate)
        {
            if (candidateList.Any(c => c.CandidateName == candidate.CandidateName))
            {
                Console.WriteLine($"Candidate {candidate.CandidateName} already exist");
                return false;
            }

            return true;
        }

        public void UpdateVotesCount(string candidate) 
        {
            candidateList.FirstOrDefault(c => c.CandidateName == candidate).votes++;
        }


    }
}
