using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    class Program
    {
        static Int32[] DataRegister = new int[8];
        static Int32[] AddressRegister = new int[8];
        static Int32 ProgramCounter = 0;
        
        [Flags]
        enum ConditionCodeRegister
        {

        }


        static void Main(string[] args)
        {
        }
    }
}
