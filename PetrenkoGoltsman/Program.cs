using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PetrenkoGoltsman
{
    public class Program
    {
        static void Main(string[] args)
        {
            string testString = "Не выходи из комнаты, не совершай ошибку."; // Test the calulation of index on a string from task.
            Console.WriteLine("[" + Index.Calculate(testString) + "] - " + testString); // Output test strin and it's index.
            Dictionary<string, float> en = new Dictionary<string, float>(); // Declare English sentences dictionary.
            Dictionary<string, float> ru = new Dictionary<string, float>(); // Declare Russian sentences dictionary.

            var file = File.ReadAllLines(@".\locales\en.txt"); // Declare file variable and read the English sentences.
            foreach (var line in file)
            {
                en.Add(line, Index.CalculateWithComment(line)); // Add each sentence from file to the English dictionary and count it's index with comment.
            }

            file = File.ReadAllLines(@".\locales\ru.txt"); // Read the Russan sentences and write them into file variable.
            foreach (var line in file)
            {
                ru.Add(line, Index.Calculate(line)); // Add each sentence from file to the Russian dictionary and count it's index.
            }

            var query = from ruDict in ru.AsParallel()
                        join enDict in en.AsParallel() on ruDict.Value equals enDict.Value
                        select new
                        {
                            index = ruDict.Value,
                            russian = ruDict.Key,
                            english = enDict.Key
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"[{item.index}] - {item.russian} - {item.english}"); // Output the result.
            }
        }
    }
}
