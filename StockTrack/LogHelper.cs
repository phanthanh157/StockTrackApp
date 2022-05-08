using System.Runtime.CompilerServices;

namespace StockTrack
{
    public static class LogHelper
    {
        public static log4net.ILog GetLogger([CallerFilePath] string fileName = "")
        {
            return log4net.LogManager.GetLogger(fileName);
        }
    }
}
