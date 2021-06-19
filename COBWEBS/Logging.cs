using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COBWEBS
{
    public class Logging
    {
        private readonly string logPath;
        private readonly StringBuilder sb;
        private static readonly object locker;

        static Logging()
        {
            locker = new object();
        }

        public Logging(string logPath)
        {
            sb = new StringBuilder();
            this.logPath = logPath;
        }

        public void DeleteFile()
        {
            File.Delete(logPath);
        }

        public void WriteToLog(string message)
        {
            lock (locker)
            {
                sb.Append(message);
            }
        }

        public void CreateLogFile()
        {
            File.AppendAllText(logPath, sb.ToString());
        }
    }
}
