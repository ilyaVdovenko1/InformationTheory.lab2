using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_FrontEnd
{
    internal class KeyModel
    {
        public KeyModel(string key, int maxPower)
        {
            if (!this.IsKeyValid(key, maxPower))
            {
                throw new ArgumentException("Key start state is not valid");
            }
            var flags = new List<bool>();
            foreach (var item in key)
            {
                flags.Add(item == '1');
            }

            this.KeyStartState = flags;

        }

        public IEnumerable<bool> KeyStartState { get; private set; }


        public bool IsKeyValid(string keyStartState, int maxPower)
        {
            if (string.IsNullOrEmpty(keyStartState))
            {
                return false;
            }

            if (keyStartState.Length != maxPower)
            {
                return false;
            }

            if (keyStartState.Any(x => x != '1' && x != '0'))
            {
                return false;
            }

            return true;
        }
    }
}



