using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_04_tracewriter
{
    public class FileLogTraceWriter : ITraceWriter
    {
        private string fileName;
        private TraceLevel _traceLevel;

        public FileLogTraceWriter(TraceLevel traceLevel, string sectionTitle)
        {
            _traceLevel = traceLevel;
            fileName = "JsonNETTraceLog" + "-" + _traceLevel.ToString() + ".log";
            string text = "*** " + sectionTitle + ": " + traceLevel.ToString() + " ***" + Environment.NewLine;
            System.IO.File.AppendAllText(fileName, text);
        }

        public TraceLevel LevelFilter
        {
            get
            {
                return _traceLevel;
            }
        }

        public void Trace(TraceLevel level, string message, Exception ex)
        {
            string logLine = DateTime.Now.ToString() + "\t" + level + "\t" + message + Environment.NewLine;
            System.IO.File.AppendAllText(fileName, logLine);
        }
    }
}
