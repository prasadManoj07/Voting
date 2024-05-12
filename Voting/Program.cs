using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Voting.Model;

namespace Voting
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var voters = new Voters();
            var candidates = new Candidate();


            Console.WriteLine("Voting App");

            bool isExit = false;

            while (!isExit)
            {
                Console.WriteLine("Add Voters select [V]");
                Console.WriteLine("Add candidates select [C]");
                Console.WriteLine("To cast a vote select [D]");
                Console.WriteLine("To exit select [E] \n\n");

                var selection = Console.ReadLine(); ;

                switch (selection)
                {
                    case "V":
                    case "v":
                        AddVoters();
                        break;
                    case "C":
                    case "c":
                        AddCandidates();
                        
                        break;
                    case "D":
                    case "d":
                        CastVote();
                        
                        break;
                    case "E":
                    case "e":
                        isExit = true;
                        break;
                    default:
                        break;


                }

            }
            Console.WriteLine("Add Voters");

            void AddVoters()
            {
                bool isValidVoter = false;

                while (!isValidVoter)
                {
                    Console.WriteLine("Enter Voter Name");
                    var voterName = Console.ReadLine();

                    var voterData = new VoterModel { VoterName = voterName };

                    if (string.IsNullOrEmpty(voterData.VoterName))
                    {
                        Console.WriteLine("Voter name Cannot be empty");
                    }
                    else if (!voters.CheckVoter(voterData))
                    {
                        Console.WriteLine("Voter must be unique");
                    }
                    else
                    {
                        voters.AddVoter(voterData);
                        isValidVoter = true;
                        ShowCandidateAndVoters();
                    }

                }

            }

            void AddCandidates()
            {
                bool isValidCandidate = false;
                Console.WriteLine("Enter Candidate Name");
                while (!isValidCandidate)
                {
                    var candidateName = Console.ReadLine();

                    var candidateDate = new CandidateModel { CandidateName = candidateName };

                    if (string.IsNullOrEmpty(candidateDate.CandidateName))
                    {
                        Console.WriteLine("Candidate name Cannot be empty");
                    }
                    else if (!candidates.CheckVoter(candidateDate))
                    {
                        Console.WriteLine("Candidate must be unique");
                    }
                    else
                    {
                        candidates.AddCandidate(candidateDate);
                        isValidCandidate = true;
                        ShowCandidateAndVoters();

                    }

                }
            }

            void CastVote()
            {
                if(candidates.GetCandidates().Count() == 0)
                {
                    Console.WriteLine("No Candidates available for voting.\n\n");
                    return;
                }
               
                var voterName = IsValidVoter();
               
                Console.WriteLine();
                var candidateName = IsValidCandidate();

                UpdateVotingData(voterName, candidateName);
                ShowCandidateAndVoters();
                Console.WriteLine("Thank you for voting");

            }

            void ShowVoters()
            {
                var voter = voters.GetVoterList();
               

                for(int i = 0; i< voter.Count; i++)
                {
                    Console.WriteLine($"\t{i +1}. {voter[i].VoterName} \n\n");
                }

                Console.WriteLine();
            }

            void ShowCandidates()
            {
                var candidate = candidates.GetCandidates();
                for (int i = 0; i < candidate.Count; i++)
                {
                    Console.WriteLine($"\t{i +1}. {candidate[i].CandidateName} \n\n");
                }

                Console.WriteLine();

            }


            string IsValidVoter()
            {
                bool IsValidIndex = false;
                Console.WriteLine("Voters list.\n\n");
                while (!IsValidIndex)
                {
                    ShowVoters();
                    Console.WriteLine("Select voter name");

                    var index = Console.ReadLine();

                    if (string.IsNullOrEmpty(index))
                    {
                        Console.WriteLine("Selected index cannot be empty.");
                    }

                    if (int.TryParse(index, out int number))
                    {
                        var voterList = voters.GetVoterList();
                        var voterName = voterList[number - 1].VoterName;
                        if (CheckVotersVotingStatus(voterName))
                        {
                            Console.WriteLine($"{voterName} has already votted.");
                        }
                        else
                        {
                            IsValidIndex = true;
                            return voterName;
                        }
                        
                    }
                }

                return string.Empty;
            } 
            string IsValidCandidate()
            {
                bool IsValidIndex = false;
                Console.WriteLine("Candidate list.\n\n");
                while (!IsValidIndex)
                {
                    ShowCandidates();
                    Console.WriteLine("Select candidate name you are voting for.");

                    var index = Console.ReadLine();

                    if (string.IsNullOrEmpty(index))
                    {
                        Console.WriteLine("Selected index cannot be empty.");
                    }

                    if (int.TryParse(index, out int number))
                    {
                        var camdidateList = candidates.GetCandidates();
                        var candidate = camdidateList[number - 1].CandidateName;
                        IsValidIndex = true;
                        return candidate;
                    }
                }

                return string.Empty;
            }


            void UpdateVotingData(string voterName, string candidateName)
            {
                voters.UpdateVotingStatus(voterName);
                candidates.UpdateVotesCount(candidateName);
            }


            bool CheckVotersVotingStatus(string voterName)
            {
                return voters.GetVoterList().FirstOrDefault(x => x.VoterName == voterName).HasVoted;
            }




            void ShowCandidateAndVoters()
            {
                ShowVoterList();
                ShowCandidateList();
            }

            void ShowVoterList()
            {

                Console.WriteLine("\n\n Voters! \n");
                var voter = voters.GetVoterList();
                Console.Write("Name  \t\t\t\t  HasVotted\n\n");

                foreach (var item in voter)
                {
                    Console.Write($"{item.VoterName} \t\t\t\t   {item.HasVoted} \n");
                }

                Console.WriteLine();
                Console.WriteLine();

            }

            void ShowCandidateList()
            {
                Console.WriteLine("\n\n Candidates \n");
                var candidate = candidates.GetCandidates();
                if(candidate.Count() == 0)
                {
                    Console.WriteLine("No Candidates added. \n\n");
                    return;
                }
                   

                Console.Write("Name  \t\t\t\t  Votes \n");

                foreach (var item in candidate)
                {
                    Console.Write($"{item.CandidateName} \t\t\t\t   {item.votes} \n");
                }

                Console.WriteLine();
                Console.WriteLine();
            }



        }
    }
}
