using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /**
       https://github.com/mission-peace/interview/blob/master/src/com/interview/graph/MaximumBiparteMatching.java   
       http://www.cdn.geeksforgeeks.org/maximum-bipartite-matching/
     */

    public partial class GeneralSamples
    {        
        public int BiparateMatching(Dictionary<int, List<int>> jobApplications, List<int> allJobs)
        {

            Dictionary<int, int> match = new Dictionary<int, int>();
            int maxMatch = 0;

            foreach (int candidate in jobApplications.Keys)
            {
                HashSet<int> jobsSeen = new HashSet<int>();
                maxMatch += MatchJobs(candidate, jobApplications, match, jobsSeen) == true ? 1 : 0;
            }
            return maxMatch;
        }

        private bool MatchJobs(int candidate, Dictionary<int, List<int>> jobApplications, Dictionary<int, int> match, HashSet<int> jobsSeen)
        {
            foreach(int job in jobApplications[candidate])
            {
                if (jobsSeen.Contains(job))
                    continue;

                jobsSeen.Add(job);

                if (match.ContainsKey(job))
                {
                    match[job] = candidate;
                    return true;
                }

                bool flag = MatchJobs(match[job], jobApplications, match, jobsSeen);
                if (flag == true)
                {
                    match[job] = candidate;
                    return true;
                }
            }
            return false;
        }

        public void BiparateMatchingTest()
        {
            List<int> app0 = new List<int>();
            app0.Add(10);
            app0.Add(11);
            app0.Add(13);

            List<int> app1 = new List<int>();
            app1.Add(10);

            List<int> app2 = new List<int>();
            app2.Add(12);

            List<int> app3 = new List<int>();
            app3.Add(12);
            app3.Add(10);
            app3.Add(11);

            Dictionary<int, List<int>> jobApplications = new Dictionary<int, List<int>>();

            jobApplications[0] = app0;
            jobApplications[1] = app1;
            jobApplications[2] = app2;
            jobApplications[3] = app3;

            List<int> allJobs = new List<int>();
            allJobs.Add(10);
            allJobs.Add(11);
            allJobs.Add(12);
            allJobs.Add(13);

            Console.WriteLine("Biparate Matching");
            Console.WriteLine(BiparateMatching(jobApplications, allJobs));
        }
    }
}