namespace TextAPI.Logic
{
    public class Info
    {
        public string Text { get; set; }
        public int WordsCount { get; set; }
        public int VowelsCount { get; set; }

        public Info(string text, int wordsCount, int vowelsCount)
        {
            Text = text;
            WordsCount = wordsCount;
            VowelsCount = vowelsCount;
        }
    }
}
