using System;
using System.Collections.Generic;
using System.Text;
using TextAPI.Data;

namespace TextAPI.Logic
{
    /// <summary>
    /// Word count service.
    /// </summary>
    public static class WordCount
    {
        /// <summary>
        /// Gets the list with text, it's words count and vowels count.
        /// </summary>
        /// <param name="wordList">The text list.</param>
        /// <returns><c>List</c></returns>
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

        /// <summary>
        /// Gets the vowel count from string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>int</c></returns>
        private static int GetVowelCountFromString(string text)
        {
            string words = VowelWords.GetVowelWords();
            int count = System.Text.RegularExpressions.Regex.Matches(text, @"[" + words + "]", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Count;

            return count;
        }
    }
}
