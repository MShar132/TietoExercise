using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAndReverse.BusinessLogic.Utils.Interfaces
{
    internal interface ILog : IDisposable
    {
        void Error(string msg);
        void Info(string msg);
    }
}
