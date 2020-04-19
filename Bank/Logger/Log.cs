using System;
using System.IO;
using System.Text;
using System.Windows;

namespace Bank.Logger
{
    public class Log
    {
        private const string FILE_EXT = ".txt";
        private const string datetimeFormat = "dd-MM-yyyy HH:mm:ss";
        private const string datetimePathFormat = "dd-MM-yyyy";
        private readonly String logFileName;

        public Log()
        {
            logFileName = Directory.GetCurrentDirectory() + @"\Logs\" + DateTime.Now.Date.ToString(datetimePathFormat) + FILE_EXT;

            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Logs\"))
            {
                Directory.CreateDirectory("Logs");
            }

            if (!File.Exists(logFileName))
            {
                WriteNewRecord(string.Format("Log was created " + DateTime.Now.ToString(datetimeFormat)));
            }
        }

        public void Error(string text, string stackTrace)
        {
            CreateNewRecord(LogType.ERROR, text + "\n" + stackTrace);
        }

        public void Info(string text)
        {
            CreateNewRecord(LogType.INFO, text);
        }


        public void Warning(string text)
        {
            CreateNewRecord(LogType.WARNING, text);
        }

        private void WriteNewRecord(string text, bool append = true)
        {
            try
            {
                using (System.IO.StreamWriter writer =
                    new System.IO.StreamWriter(logFileName, append, Encoding.UTF8))
                {
                    if (!string.IsNullOrEmpty(text))
                    {
                        writer.WriteLine(text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Log problem");
            }
        }

        private void CreateNewRecord(LogType logType, string text)
        {
            String newRecord = String.Empty;
            switch (logType)
            {
                case LogType.INFO:
                    newRecord = DateTime.Now.ToString(datetimeFormat) + " [INFO]-";
                    break;
                case LogType.WARNING:
                    newRecord = DateTime.Now.ToString(datetimeFormat) + " [WARNING]-";
                    break;
                case LogType.ERROR:
                    newRecord = DateTime.Now.ToString(datetimeFormat) + " [ERROR]-";
                    break;
            }

            WriteNewRecord(newRecord + text);
        }

    }
}
