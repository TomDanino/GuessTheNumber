using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly bool createLogFile;

        private static readonly object locker;

        static Logging()
        {
            locker = new object();
        }

        public Logging()
        {
            sb = new StringBuilder();
            this.logPath = ConfigurationManager.AppSettings[Consts.LOG_PATH];
            this.createLogFile = bool.Parse(ConfigurationManager.AppSettings[Consts.CREATE_LOG_FILE]);
        }

        public void DeleteFile()
        {
            try
            {
                if (createLogFile)
                { 
                    File.Delete(logPath);
                    }
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't delete '{logPath}' log file, please choose a valid path", ex);
            }
        }

        public void WriteToLog(string message)
        {
            lock (locker)
            {
                sb.AppendLine(message);
            }
        }

        public void CreateLogFile()
        {
            try
            {
                if (createLogFile)
                {
                    File.AppendAllText(logPath, sb.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't create '{logPath}' log file, please choose a valid path", ex);
            }
        }
    }
}
