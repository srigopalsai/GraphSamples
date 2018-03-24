using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    public partial class GeneralSamples
    {

        /*Given two words(beginWord and endWord), and a dictionary's word list.
          Find all shortest transformation sequence(s) from beginWord to endWord, such that:
          Only one letter can be changed at a time
          Each intermediate word must exist in the word list

          Solution -
          Since we have to find all paths we need care about below extra stuff on top of regular BFS
          1) Maintain parent of every word to recreate the path
          2) Maintain 2 visited.First is for level and once the entire level is done then move it to visited.
          3) Also since we are looking result from beginWord to endWord switch them initially and go from endWord towards beginWord.
          https://leetcode.com/problems/word-ladder-ii/
          https://github.com/mission-peace/interview/blob/master/src/com/interview/graph/WordLadder.java
           */
        public List<List<String>> FindLadders(String beginWord, String endWord, HashSet<String> wordList)
        {
            if (wordList == null || wordList.Count == 0)
                return new List<List<string>>();

            String temp = endWord;
            endWord = beginWord;
            beginWord = temp;

            Dictionary<String, List<String>> parent = new Dictionary<string, List<string>>();
            Queue<String> queue1 = new Queue<string>();
            HashSet<String> visited = new HashSet<string>();
            HashSet<String> levelVisited = new HashSet<string>();
            List<List<String>> result = new List<List<string>>();

            parent[beginWord] = null;

            queue1.Enqueue(beginWord);
            visited.Add(beginWord);

            bool foundDestination = false;

            while (queue1.Count > 0)
            {
                while (queue1.Count > 0)
                {

                    String word = queue1.Dequeue();

                    for (int i = 0; i < word.Length; i++)
                    {
                        char[] wordArray = word.ToCharArray();

                        for (char ch = 'a'; ch <= 'z'; ch++)
                        {
                            wordArray[i] = ch;

                            String newWord = new String(wordArray);

                            if (!endWord.Equals(newWord) && (!wordList.Contains(newWord) || visited.Contains(newWord)))
                                continue;

                            List<String> parents = parent.ContainsKey(newWord) ? parent[newWord] : null;

                            if (parents == null)
                            {
                                parents = new List<string>();
                                parent[newWord] = parents;
                            }
                            parents.Add(word);

                            levelVisited.Add(newWord);
                            if (endWord.Equals(newWord))
                            {
                                foundDestination = true;
                                break;
                            }
                        }
                    }
                }
                if (foundDestination == true)
                    break;

                foreach (String word1 in levelVisited)
                {
                    queue1.Enqueue(word1);
                    visited.Add(word1);
                }

                levelVisited.Clear();
            }

            if (!foundDestination)
            {
                return new List<List<string>>();
            }
            else
            {
                SetParent(parent, beginWord, new List<string>(), endWord, result);
            }
            return result;
        }

        private void SetParent(Dictionary<String, List<String>> parent, String startWord, List<String> path, String currentWord, List<List<String>> result)
        {
            path.Add(currentWord);
            if (startWord.Equals(currentWord))
            {
                result.Add(new List<string>(path));
                path.RemoveAt(path.Count - 1);
                return;
            }
            foreach (String p in parent[currentWord])
            {
                SetParent(parent, startWord, path, p, result);
            }

            path.RemoveAt(path.Count - 1);
        }

        public void FindLaddersTest()
        {
            String[] wordList = { "hot", "dot", "dog", "lot", "log" };
            HashSet<String> wordSet = new HashSet<string>(wordList);
            List<List<String>> result = FindLadders("hit", "cog", wordSet);

            Console.WriteLine("Word Ladder Demo");

            foreach (List<string> items in result)
            {
                foreach (string str in items)
                {
                    Console.WriteLine(str);
                }
            }
        }
    }
}