using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
