using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortAndReverse.BusinessLogic.Utils.Interfaces;

namespace SortAndReverse.BusinessLogic.Utils
{
    internal class LogManager : ILog
    {
        static FileStream file;

        private LogManager(string filePath)
        {
            file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);

        }
        static LogManager logManager { get; set; }
        public static LogManager GetLogger(string filePath)
        {
            if (logManager == null)
            {
                logManager = new LogManager(filePath);
            }
            return logManager;
        }
        public void Error(string msg)
        {
            byte[] byteMsg = GetBytes(DateTime.Now.ToString() + "ERROR:" + msg + "\n");
            file.Write(byteMsg);
        }

        public void Info(string msg)
        {
            byte[] byteMsg = GetBytes(DateTime.Now.ToString() + " INFO: " + msg + "\n");
            file.Write(byteMsg);
        }
        private byte[] GetBytes(string msg)
        {
            return Encoding.UTF8.GetBytes(msg);
        }
        public void Dispose()
        {
            file.Close();
        }
    }
}
