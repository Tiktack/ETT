using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace ETT.Utils.Logger
{
    public static class Logger
    {
        static Logger()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "../ETT.Logs");

            Directory.CreateDirectory(path);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine(path, "{Date}.log"))
                .CreateLogger();
        }

        public static void AddSerilog(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
        }

        public static void Debug(string messageTemplate)
        {
            Log.Debug(messageTemplate);
        }
        public static void Debug(string messageTemplate, params object[] propertyValues)
        {
            Log.Debug(messageTemplate, propertyValues);
        }
        public static void Debug(Exception exception, string messageTemplate)
        {
            Log.Debug(exception, messageTemplate);
        }
        public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Debug(exception, messageTemplate, propertyValues);
        }

        public static void Error(string messageTemplate)
        {
            Log.Error(messageTemplate);
        }
        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Log.Error(messageTemplate, propertyValues);
        }
        public static void Error(Exception exception, string messageTemplate)
        {
            Log.Error(exception, messageTemplate);
        }
        public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Error(exception, messageTemplate, propertyValues);
        }

        public static void Fatal(string messageTemplate)
        {
            Log.Fatal(messageTemplate);
        }
        public static void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Log.Fatal(messageTemplate, propertyValues);
        }
        public static void Fatal(Exception exception, string messageTemplate)
        {
            Log.Fatal(exception, messageTemplate);
        }
        public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Fatal(exception, messageTemplate, propertyValues);
        }        

        public static void Information(string messageTemplate)
        {
            Log.Information(messageTemplate);
        }
        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            Log.Information(messageTemplate, propertyValues);
        }
        public static void Information(Exception exception, string messageTemplate)
        {
            Log.Information(exception, messageTemplate);
        }
        public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Information(exception, messageTemplate, propertyValues);
        }

        public static void Verbose(string messageTemplate)
        {
            Log.Verbose(messageTemplate);
        }
        public static void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(messageTemplate, propertyValues);
        }
        public static void Verbose(Exception exception, string messageTemplate)
        {
            Log.Verbose(exception, messageTemplate);
        }
        public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(exception, messageTemplate, propertyValues);
        }

        public static void Warning(string messageTemplate)
        {
            Log.Warning(messageTemplate);
        }
        public static void Warning(string messageTemplate, params object[] propertyValues)
        {
            Log.Warning(messageTemplate, propertyValues);
        }
        public static void Warning(Exception exception, string messageTemplate)
        {
            Log.Warning(exception, messageTemplate);
        }
        public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Warning(exception, messageTemplate, propertyValues);
        }
    }
}
