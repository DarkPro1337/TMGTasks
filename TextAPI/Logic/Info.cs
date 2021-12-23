namespace TextAPI.Logic
{
    public class Info
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the words count.
        /// </summary>
        /// <value>
        /// The words count.
        /// </value>
        public int WordsCount { get; set; }
        /// <summary>
        /// Gets or sets the vowels words count.
        /// </summary>
        /// <value>
        /// The vowels count.
        /// </value>
        public int VowelsCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Info"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="wordsCount">The words count.</param>
        /// <param name="vowelsCount">The vowels count.</param>
        public Info(string text, int wordsCount, int vowelsCount)
        {
            Text = text;
            WordsCount = wordsCount;
            VowelsCount = vowelsCount;
        }
    }
}
