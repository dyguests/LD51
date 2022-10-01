using System;

namespace Tools
{
    public static class StringEx
    {
        public static int ParseInt(this string s)
        {
            return Int32.Parse(s);
        }
    }
}