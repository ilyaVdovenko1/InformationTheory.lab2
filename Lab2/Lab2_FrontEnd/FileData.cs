using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_FrontEnd
{
    internal class FileData
    {
        public string BytesAsBits { get; }

        public IEnumerable<byte> Bytes { get; }
        public FileData(byte[] bytesFromFile)
        {
            this.Bytes = bytesFromFile;
            var bitsBuilder = new StringBuilder();
            foreach (byte b in bytesFromFile)
            {
                bitsBuilder.Append(ConverterExtentions.CovertByteToBits(b));
            }
            
            this.BytesAsBits = bitsBuilder.ToString();
        }
    }
}
