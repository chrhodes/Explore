using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcControllers2.Infrastructure;

namespace MvcControllers2.Tests.TestDoubles
{
    class FakeLogger : ILogger
    {
        public void Log(string message)
        {
            LogResult = message;
        }

        public string LogResult { get; set; }
    }
}
