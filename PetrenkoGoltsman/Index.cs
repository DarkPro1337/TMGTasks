using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PetrenkoGoltsman
{
    /// <summary>
    /// Petrenko-Goltsman index calculation and utility class.
    /// </summary>
    public class Index
    {
        /// <summary>
        /// Gets the string of any kind characters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Output <c>string</c></returns>
        private static string GetCharactersOnly(string input)
        {
            Regex regEx = new Regex(@"\p{L}"); // RegEx of any characters from any language.
            MatchCollection matches = regEx.Matches(input); // Match the RegEx.
            IEnumerable<string> result = matches.Cast<Match>().Select(m => m.Value); // Cast matches to string type.
            string output = string.Join("", result); // Join result string.

            return output; // Return result.
        }

        /// <summary>
        /// Trims input string ater "|" symbol.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>output <c>string</c></returns>
        public static string CommentAfterTrim(string input)
        {
            string output = input; // Declare output and set input to it.
            int index = output.IndexOf("|"); // Declare index and set index of "|" to it.

            if (index >= 0)
            {
                output = output.Substring(0, index);
            }

            return output;
        }

        /// <summary>
        /// Trims input string before "|" symbol.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>output <c>string</c></returns>
        public static string CommentBeforeTrim(string input)
        {
            string output = input; // Declare output and set the input to it.
            int index = output.IndexOf("|"); // Declare index and set index of "|" symbol.

            if (index >= 0)
            {
                output = output.Substring(index + 1);
            }
            else
            {
                return string.Empty;
            }

            return output;
        }

        /// <summary>
        /// Petrenko-Goltsman index calculation from the input string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>index <c>float</c></returns>
        public static float Calculate(string input)
        {
            float index = 0f; // Declare index.
            int counter = 1; // Declare counter.
            string clean = GetCharactersOnly(input); // Declare clean string.

            foreach (char c in clean)
            {
                index += 0.5f * counter; // Calculate index.
                counter += 2; // Calculate counter.
            }

            index *= clean.Length; // Calculate index by the clean string length.
            return index; // Return float index.
        }

        /// <summary>
        /// Petrenko-Goltsman index from input string including the comment.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>index <c>float</c></returns>
        public static float CalculateWithComment(string input)
        {
            string before = (input); // Declare string comment trim;
            string after = CommentBeforeTrim(input); // Declare string comment itself;

            float index = Calculate(before) + Calculate(after); // Calculate indexes on before and after;

            return index; // Return index;
        }
    }
}
