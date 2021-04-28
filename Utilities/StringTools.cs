using System;

namespace Utilities
{
    public static class StringTools
    {
        public static string Combine(string string1, string string2)
        {
            return string1 + " " + string2;
        }

        public static string SubString(string src, int offset, int length)
        {
            //Make sure the length is okay
            if (src.Length < offset + length)
                return "";

            //Get substring
            return src.Substring(offset, length);
        }
    }
}
