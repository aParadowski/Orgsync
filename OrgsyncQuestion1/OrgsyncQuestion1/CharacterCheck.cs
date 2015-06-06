using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgsyncQuestion1
{
    /// <summary>
    /// This application takes in a string as an argument and prints out the most used letter, the 
    /// least used letter, and if there is a tie prints the lowest coded character
    /// 
    /// Assumptions: 
    /// 
    /// (1) The string itself is relatively small. Iterating through the dictionary is
    /// approximately O(n), so as long as the string isn't extremely large (1000's of characters) it 
    /// should function appropriately.
    /// 
    /// (2) Being a 'string' means numerical values are also accepted as input, they will be treated
    /// as any other alphabetic character or special character would
    /// 
    /// (3) Space would also be treated as a character if the previous assumption holds, however I feel that
    /// it would not be imperative to know how many spaces are in a given string (if you wanted to parse a book
    /// it would almost always mean the null character was the most used)
    /// </summary>
    class CharacterCheck
    {
        private static Dictionary<string, int> characterCounts = new Dictionary<string, int>(); 
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a string: ");
            String inputString = Console.ReadLine();

            ParseString(inputString);
            GetMostUsedCharacter();
            GetLeastUsedCharacter();
        }
        
        /// <summary>
        /// Parses a given string and counts the number of times a letter is found
        /// </summary>
        /// <param name="input">The string to parse</param>
        static void ParseString(string input)
        {
            //remove all whitespaces in the string (assumption 3)
            input = new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
            foreach(var character in input)
            {
                    if (characterCounts.ContainsKey(character.ToString()))
                    {
                        characterCounts[character.ToString()] = characterCounts[character.ToString()] + 1;
                    }
                    else
                    {
                        characterCounts.Add(character.ToString(), 1);
                    }
            }
        }

        static void GetMostUsedCharacter()
        {
            //integer for the most used character(s)
            int mostUsed = characterCounts.Values.ToList().Max();
            string finalChar;
            //find the most used character(s)
            var mostUsedChar = characterCounts.Where(x => x.Value == mostUsed).ToList();
            //there is a tie
            if(mostUsedChar.Count > 1)
            {
                finalChar = FindLowestCodePoint(mostUsed);
            }
            else
            {
                finalChar =  characterCounts.FirstOrDefault(x => x.Value == mostUsed).Key;//.ToString();
            }
            Console.WriteLine("Most Used: {0}, count {1}", finalChar, characterCounts[finalChar] );
        }

        static void GetLeastUsedCharacter()
        {
            //integer for the most used character(s)
            int leastUsed = characterCounts.Values.ToList().Min();
            //character to be dispayed
            string finalChar;
            //find the most used character(s)
            var leastUsedChar = characterCounts.Where(x => x.Value == leastUsed).ToList();
            //there is a tie
            if (leastUsedChar.Count > 1)
            {
                finalChar = FindLowestCodePoint(leastUsed);
            }
            else
            {
                finalChar = characterCounts.FirstOrDefault(x => x.Value == leastUsed).Key;//.ToString();
            }
            Console.WriteLine("Least Used: {0}, count {1}", finalChar, characterCounts[finalChar]);
        }

        /// <summary>
        /// Finds the lowest unicode value of the given characters
        /// that have chosenValue as their value in the dictionary
        /// </summary>
        /// <param name="chosenValue">Value of Key in Dictionary</param>
        /// <returns></returns>
        static string FindLowestCodePoint(int chosenValue)
        {
            //grab a list of letters that match chosen value
            var keys = characterCounts.Where(x => x.Value == chosenValue).Select(pair => pair.Key);
            string lowestKey = "a";
            int lowestValue = 999;
            int temp=0;

            foreach(var key in keys)
            {
                temp = Char.ConvertToUtf32(key, 0);
                if(temp < lowestValue)
                {
                    lowestValue = temp;
                    lowestKey = key;
                }
            }
            return lowestKey;
        }
    }
}
