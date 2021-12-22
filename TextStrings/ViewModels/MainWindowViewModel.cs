using System.Collections.Generic;
using System.ComponentModel;
using TextStrings.Models;

namespace TextStrings.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        public string Text { get; set; }
        public int WordsCount { get; set; }
        public int VowelCount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel(string text, int wordsCount, int vowelCount)
        {
            Text = text;
            WordsCount = wordsCount;
            VowelCount = vowelCount;
        }

        public static string GetVowelWords()
        {
            string AllWords = @"aeiouyауоыиэяюёеàáâãäåæāăąèéêëēĕėęěìíîïĨĩĪīĬĭĮįİıòóôõöøōŏőœùúûüũūŭůűųýÿŷAEIOUYАУОЫИЭЯЮЁЕÀÁÂÃÄÅÆĀĂĄÈÉÊËĒĔĖĘĚÌÍÎÏĨĨĪĪĬĬĮĮİIÒÓÔÕÖØŌŎŐŒÙÚÛÜŨŪŬŮŰŲÝŸŶ";
            return AllWords;
        }

        public static List<MainWindowViewModel> GetListTextInfo(List<Text> wordList)
        {
            int wordsCount = 0;
            List<string> wordlist = new List<string>();
            int VowelCount = 0;
            List<MainWindowViewModel> resultList = new List<MainWindowViewModel>();
            for (int i = 0; i < wordList.Count; i++)
            {
                wordlist = new List<string>(wordList[i].text.Split(' '));
                wordsCount = wordlist.Count;

                VowelCount = GetVowelCountFromString(wordList[i].text);
                resultList.Add(new MainWindowViewModel(wordList[i].text, wordsCount, VowelCount));
            }
            return resultList;
        }

        private static int GetVowelCountFromString(string text)
        {
            string words = GetVowelWords();
            int count = System.Text.RegularExpressions.Regex.Matches(text, @"[" + words + "]", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Count;
            return count;
        }
    }
}
