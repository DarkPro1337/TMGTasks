using System;
using System.Collections.Generic;
using System.Text;
using TextAPI.Data;

namespace TextAPI.Logic
{
    public static class WordCount
    {
        public static List<Info> GetListTextInfo(List<Text> wordList)
        {
            int wordCount = 0;
            List<string> wordlist = new List<string>();
            int VowelCount = 0;
            List<Info> resultList = new List<Info>();
            for (int i = 0; i < wordList.Count; i++)
            {
                wordlist = new List<string>(wordList[i].text.Split(' '));
                wordCount = wordlist.Count;

                VowelCount = GetVowelCountFromString(wordList[i].text);
                resultList.Add(new Info(wordList[i].text, wordCount, VowelCount));
            }
            return resultList;
        }

        private static int GetVowelCountFromString(string text)
        {

            string words = VowelWords.GetVowelWords();
            int count = System.Text.RegularExpressions.Regex.Matches(text, @"[" + words + "]", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Count;

            return count;
        }
    }
}
