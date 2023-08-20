using JDA.Core.Loggers.Abstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Loggers.Implements
{
    public class Logger : ILogger
    {
        public void Debug(string messageTemplate)
        {
            Serilog.Log.Debug(messageTemplate);
        }

        public void Debug(string messageTemplate, params object?[]? propertyValues)
        {
            Serilog.Log.Debug(messageTemplate, propertyValues);
        }

        public void Debug(Exception? exception, string messageTemplate)
        {
            Serilog.Log.Debug(exception, messageTemplate);
        }

        public void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Serilog.Log.Debug(exception, messageTemplate, propertyValues);
        }

        public void Info(string messageTemplate)
        {
            Serilog.Log.Information(messageTemplate);
        }

        public void Info(string messageTemplate, params object?[]? propertyValues)
        {
            Serilog.Log.Information(messageTemplate, propertyValues);
        }

        public void Info(Exception? exception, string messageTemplate)
        {
            Serilog.Log.Information(exception, messageTemplate);
        }

        public void Info(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Serilog.Log.Information(exception, messageTemplate, propertyValues);
        }

        public void Warn(string messageTemplate)
        {
            Serilog.Log.Warning(messageTemplate);
        }

        public void Warn(string messageTemplate, params object?[]? propertyValues)
        {
            Serilog.Log.Warning(messageTemplate, propertyValues);
        }

        public void Warn(Exception? exception, string messageTemplate)
        {
            Serilog.Log.Warning(exception, messageTemplate);
        }

        public void Warn(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Serilog.Log.Warning(exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate)
        {
            Serilog.Log.Error(messageTemplate);
        }

        public void Error(string messageTemplate, params object?[]? propertyValues)
        {
            Serilog.Log.Error(messageTemplate, propertyValues);
        }

        public void Error(Exception? exception, string messageTemplate)
        {
            Serilog.Log.Error(exception, messageTemplate);
        }

        public void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Serilog.Log.Error(exception, messageTemplate, propertyValues);
        }
    }
}
