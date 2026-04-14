using System;
using System.IO;
using System.Web;

public static class Logger
{
    // Static method to get the log file path
    private static string LogFilePath
    {
        get
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/LogFile.txt");
        }
    }

    // Static method to log messages
    public static void Log(string message)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(LogFilePath, true))
            {
                sw.WriteLine($"{DateTime.Now}: {message}");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log to a different file, notify admin, etc.)
            Console.WriteLine($"Failed to write log: {ex.Message}");
        }
    }
}
