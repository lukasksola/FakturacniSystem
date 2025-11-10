using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturacniSystem.Code
{
    public class InputHandler
    {
        public static int ParseInputtedTextToInt(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result; 
            }
            //pokud neni platne
            return -1;
        }
    }
}
