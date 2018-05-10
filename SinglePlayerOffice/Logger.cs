using System;
using System.IO;

public static class Logger {
    public static void Log(object message) {
        File.AppendAllText("SinglePlayerOffice.log", DateTime.Now + " : " + message + Environment.NewLine);
    }
}