using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assembler.Common;
namespace Assembler.Instructions
{
    public static class AddBinary
    {
        public static string ParseInstruction(string[] arguments)
        {
            string output = "1101";
            
            if(arguments[2][0] == 'D' || arguments[2][0] == 'd')
            {

            }

            return output;
        }

        private static string GetOpMode(string source, string operandType)
        {
            string output = string.Empty;
            if(source[0] == 'D' || source[0] == 'd')
            {
                if (operandType.Equals("B", StringComparison.OrdinalIgnoreCase)
                                    || operandType.Equals("BYTE", StringComparison.OrdinalIgnoreCase))
                {
                    output += "100";
                }
                else if (operandType.Equals("W", StringComparison.OrdinalIgnoreCase)
                    || operandType.Equals("WORD", StringComparison.OrdinalIgnoreCase))
                {
                    output += "101";
                }
                else if (operandType.Equals("L", StringComparison.OrdinalIgnoreCase)
                    || operandType.Equals("LONG", StringComparison.OrdinalIgnoreCase))
                {
                    output += "110";
                }
            }
            else
            if (operandType.Equals("B", StringComparison.OrdinalIgnoreCase)
                                    || operandType.Equals("BYTE", StringComparison.OrdinalIgnoreCase))
            {
                output += "000";
            }
            else if (operandType.Equals("W", StringComparison.OrdinalIgnoreCase)
                || operandType.Equals("WORD", StringComparison.OrdinalIgnoreCase))
            {
                output += "001";
            }
            else if (operandType.Equals("L", StringComparison.OrdinalIgnoreCase)
                || operandType.Equals("LONG", StringComparison.OrdinalIgnoreCase))
            {
                output += "010";
            }

            return output;
        }

        private static string ConvertStringToOctet(int number)
        {
            switch (number)
            {
                case 0:
                    return "000";
                case 1:
                    return "001";
                case 2:
                    return "010";
                case 3:
                    return "011";
                case 4:
                    return "100";
                case 5:
                    return "101";
                case 6:
                    return "110";
                case 7:
                    return "111";
                default:
                    return string.Empty;
            }
        }
    }
}
