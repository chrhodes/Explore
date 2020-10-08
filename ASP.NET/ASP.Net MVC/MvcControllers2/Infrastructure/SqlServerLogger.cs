using System;

namespace MvcControllers2.Infrastructure
{
    public class SqlServerLogger : ILogger
    {
        public void Log(string message)
        {
            // write message to database here ....
        }
    }
}
