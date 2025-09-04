using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public class CaseStudy {
    public class VowelSubsequence // Helper class to store word, its longest vowel subsequence, and the length of that subsequence
    {
        public string Word { get; set; }
        public string Sequence { get; set; }
        public int Length { get; set; }

        public VowelSubsequence(string word, string sequence, int length)
        {
            Word = word;
            Sequence = sequence;
            Length = length;
        }
    }
    public static string LongestVowelSubSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0) // Handle empty input
            return "[]";

        List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
        List<VowelSubsequence> vowelSubsequence = new List<VowelSubsequence>();
        foreach (string word in words)
        {
            string currentSequence = ""; // Initialize variables to track the current and longest vowel subsequences
            string longestSequence = ""; // Initialize variables to track the current and longest vowel subsequences          

            foreach (char c in word)
            {
                if (vowels.Contains(c))
                {
                    currentSequence += c;
                    if (currentSequence.Length > longestSequence.Length) { // Update longestSequence if currentSequence is longer
                        longestSequence = currentSequence;
                    }
                }
                else
                {
                    currentSequence = ""; // Reset currentSequence if a non-vowel character is encountered
                }
            }
            vowelSubsequence.Add(new VowelSubsequence(word, longestSequence, longestSequence.Length)); // Add the word and its longest vowel subsequence to the list
        }
        return JsonSerializer.Serialize(vowelSubsequence); // Serialize the list of VowelSubsequence objects to JSON
    }

}
