using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAndReverse.BusinessLogic.Utils.Interfaces
{
    internal interface IUtilities
    {
        string[] ReadFile(string path);
        public void WriteCharArrayToFile(char[][] words, string outPath);
        public void WriteStringArrayToFile(string[] sorted, string outPath);
        char[][] Reverse(string[] words);
        string[] Sort(string[] words);
        string[] SortWithoutDuplicates(string[] words);



    }
}
