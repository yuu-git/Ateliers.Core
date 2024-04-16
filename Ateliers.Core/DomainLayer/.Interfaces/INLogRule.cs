using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    public interface INLogRule
    {
        LogLevel MinLevel { get; }

        LogLevel MaxLevel { get; }

        Target Target { get; }

        string LoggerNamePattern { get; }
    }
}
