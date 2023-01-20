using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortAndReverse.BusinessLogic.Utils.Interfaces;

namespace SortAndReverse.BusinessLogic.Utils
{
    internal class FileUtil : IUtilities
    {
        public string[] ReadFile(string path)
        {

            string[] words = File.ReadAllLines(path);
            return words;

        }
        public char[][] Reverse(string[] words)
        {
            string[] reverse = new string[words.Length];
            char[][] finalReverse = new char[reverse.Length][];
            for (int i = 0, j = words.Length - 1; j >= 0 && i < words.Length; i++, j--)
            {
                reverse[i] = words[j];
                finalReverse[i] = new char[words[j].Length];
                for (int count = 0; count < finalReverse[i].Length && words[j].Length - 1 - count >= 0; count++)
                {
                    finalReverse[i][count] = words[j][words[j].Length - 1 - count];
                }
            }
            return finalReverse;
        }
        public string[] Sort(string[] words)
        {
            string[] sorted = new string[words.Length]; //declaring sorted array to store elements in sorted order
            int lastIndex = 0;//index upto which sorted array is filled (0 in the beginning)
            sorted[lastIndex] = words[0];// to begin with, filling up sorted array with first element from words to be sorted

            for (int i = 1, n = 0; i < words.Length; i++, n = 0) //i - to loop in words(starting the loop letter at 1st index as we already copied 0th index letter to sorted array)
            {
                //n holds the index of the character being compared in sorted array 
                //L1: if (sorted[n] > words[i]) //if the character from original words is smaller than the character at nth index in sorted array
                //count- count of letters in words or sorted array element - whichever smaller
                for (int count = 0; count < words[i].Length && count < sorted[n].Length; count++)
                {
                L1: if (count < words[i].Length && count < sorted[n].Length && sorted[n][count] > words[i][count])
                    {
                        for (int j = n, k = lastIndex; j < sorted.Length && k >= n; j++, k--)
                        {

                            sorted[k + 1] = sorted[k];// move each of the bigger words to next index in sorted array 

                        }
                        sorted[n] = words[i]; // add the word from words array prior to all bigger words in sorted array
                        lastIndex++;
                        break;
                    }
                    else if (count < words[i].Length && count < sorted[n].Length && sorted[n][count] == words[i][count])
                    {
                        count++;
                        goto L1; //repeat logic labelled as L1
                    }
                    else if (count < words[i].Length && count < sorted[n].Length && sorted[n][count] < words[i][count])
                    {
                        //if the character from original word is bigger than the character at nth index in sorted array element, move to next word for comparison if it exists 

                        if (sorted[n + 1] == null)
                        {
                            lastIndex++;
                            sorted[lastIndex] = words[i];
                            break;
                        }
                        else
                        {
                            n++; count = 0;
                            goto L1;
                        }
                    }
                    else
                    {
                        for (int j = n, k = lastIndex; j < sorted.Length && k >= n; j++, k--)
                        {
                            sorted[k + 1] = sorted[k];// move each of the  words to next index in sorted array to fit in the current one
                        }
                        sorted[n] = words[i];
                        lastIndex++;
                    }
                }

            }
            return sorted;
        }

        public string[] SortWithoutDuplicates(string[] words)
        {
            string[] sorted = new string[words.Length]; //declaring sorted array to store elements in sorted order
            int lastIndex = 0;//index upto which sorted array is filled (0 in the beginning)
            sorted[lastIndex] = words[0];// to begin with, filling up sorted array with first element from words to be sorted

            for (int i = 1, n = 0; i < words.Length; i++, n = 0) //i - to loop in words(starting the loop letter at 1st index as we already copied 0th index letter to sorted array)
            {
                //n holds the index of the character being compared in sorted array 
                //L1: if (sorted[n] > words[i]) //if the character from original words is smaller than the character at nth index in sorted array
                //count- count of letters in words or sorted array element - whichever smaller
                for (int count = 0; count < words[i].Length && count < sorted[n].Length; count++)
                {
                L1: if (count < words[i].Length && count < sorted[n].Length && sorted[n][count] > words[i][count])
                    {
                        for (int j = n, k = lastIndex; j < sorted.Length && k >= n; j++, k--)
                        {

                            sorted[k + 1] = sorted[k];// move each of the bigger words to next index in sorted array 

                        }
                        sorted[n] = words[i]; // add the word from words array prior to all bigger words in sorted array
                        lastIndex++;
                        break;
                    }
                    else if (count < words[i].Length && count < sorted[n].Length && sorted[n][count] == words[i][count])
                    {
                        count++;
                        goto L1; //repeat logic labelled as L1
                    }
                    else if (count < words[i].Length && count < sorted[n].Length && sorted[n][count] < words[i][count])
                    {
                        //if the character from original word is bigger than the character at nth index in sorted array element, move to next word for comparison if it exists 

                        if (sorted[n + 1] == null)
                        {
                            lastIndex++;
                            sorted[lastIndex] = words[i];
                            break;
                        }
                        else
                        {
                            n++; count = 0;
                            goto L1;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            return sorted;
        }

        public void WriteCharArrayToFile(char[][] words, string outPath)
        {
            if (File.Exists(outPath))
            {
                File.Delete(outPath);
            }
            foreach (var line in words)
            {
                File.AppendAllText(outPath, new string(line) + "\n");

            }
        }

        public void WriteStringArrayToFile(string[] sorted, string outPath)
        {
            if (File.Exists(outPath))
            {
                File.Delete(outPath);
            }
            foreach (var line in sorted)
            {
                if (!string.IsNullOrEmpty(line))
                    File.AppendAllText(outPath, line + "\n");

            }
        }
    }
}
