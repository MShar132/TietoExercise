using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortAndReverse.BusinessLogic.Utils;

namespace SortAndReverse.Tests
{
    internal class UnitTest
    {

        public static string[] TestSort(string[] strings)
        {
            FileUtil fileUtil = new FileUtil();
            return fileUtil.Sort(strings);
        }
    }
}
