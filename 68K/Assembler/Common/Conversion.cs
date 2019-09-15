using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    public static class Conversion
    {
        public static string DataRegisterToBinary(int dataRegisterNumber)
        {
            if(dataRegisterNumber == 0)
            {
                return "000";
            }
            else if (dataRegisterNumber == 1)
            {
                return "001";
            }
            else if(dataRegisterNumber == 2)
            {
                return "010";
            }
            else if (dataRegisterNumber == 3)
            {
                return "011";
            } 
            else if(dataRegisterNumber == 4)
            {
                return "100";
            }
            else if (dataRegisterNumber == 5)
            {
                return "101";
            }
            else if(dataRegisterNumber == 6)
            {
                return "110";
            }
            else if (dataRegisterNumber == 7)
            {
                return "111";
            }
            else
            {
                throw new Exception("You done fucked up again");
            }
        }
    }
}
