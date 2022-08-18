using System;

namespace StockTrack.common
{
    public class Utils
    {
        public static void Check(object obj)
        {
            if (obj is null)
                throw new Exception("Object is null");
        }
    }
}
