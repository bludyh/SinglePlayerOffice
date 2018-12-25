using System;
using System.IO;

namespace SinglePlayerOffice {
    public static class Logger {
        public static void Log(object message) {
            File.WriteAllText("SinglePlayerOffice.log", DateTime.Now + " : " + message + Environment.NewLine);
        }
    }
}