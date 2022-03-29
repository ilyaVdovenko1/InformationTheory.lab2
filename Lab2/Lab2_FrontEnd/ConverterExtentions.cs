using System;

namespace Lab2_FrontEnd
{
    internal static class ConverterExtentions
    {
        public static string CovertByteToBits(byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }


    }
}
