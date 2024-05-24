using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Loggers.Abstractions
{
    public interface ILogger
    {
        void Debug(string messageTemplate);
        void Debug(string messageTemplate, params object?[]? propertyValues);
        void Debug(Exception? exception, string messageTemplate);
        void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues);
        void Info(string messageTemplate);
        void Info(string messageTemplate, params object?[]? propertyValues);
        void Info(Exception? exception, string messageTemplate);
        void Info(Exception? exception, string messageTemplate, params object?[]? propertyValues);
        void Warn(string messageTemplate);
        void Warn(string messageTemplate, params object?[]? propertyValues);
        void Warn(Exception? exception, string messageTemplate);
        void Warn(Exception? exception, string messageTemplate, params object?[]? propertyValues);
        void Error(Exception? exception);
        void Error(string messageTemplate);
        void Error(Exception? exception, string messageTemplate);
        void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    }
}
