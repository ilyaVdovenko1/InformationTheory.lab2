using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_FrontEnd
{
    internal class TaskModel
    {
        public TaskModel(string bitsToXor, string maxPower)
        {
            if (string.IsNullOrEmpty(bitsToXor))
            {
                throw new ArgumentNullException(nameof(bitsToXor));
            }

            if (string.IsNullOrEmpty(maxPower))
            {
                throw new ArgumentNullException(nameof(maxPower));
            }

            var bits = bitsToXor.Split(",");

            if (bits.Length == 0)
            {
                throw new ArgumentException("Could not recognize bits to xor.");
            }

            if (!int.TryParse(maxPower, out var power))
            {
                throw new ArgumentException("Could not recognize maximal power.");
            }

            if(power <= 0)
            {
                throw new ArgumentException("Power can not be less then 0.");
            }



            var bitsAsNumbers = bits.Select(x => uint.Parse(x) - 1).Where(x => x >= 0 || x < power).ToList();
            bitsAsNumbers.Sort();

            if (bitsAsNumbers.Count == 0 || bitsAsNumbers.Count % 2 == 1)
            {
                throw new ArgumentException("Wrong bits to xor");
            }


            this.BitsToXor = bitsAsNumbers;
            this.MaxPower = power;

            

        }

        public List<uint> BitsToXor { get; set; }

        public int MaxPower { get; set; }

       
    }
}
