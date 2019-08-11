using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Instructions
{
    public class ADD : IInstruction
    {
        public string ConvertToBinary(string instruction)
        {
            StringBuilder binary = new StringBuilder("1101");
            string[] instructionParts = instruction.Split(' ');

            bool isSourceDataReigster = false;
            Conversion converter = new Conversion();

            //SOurce is a data register
            //append register
            if(instructionParts[2][0] == 'd')
            {
                binary.Append(converter.DataRegisterToBinary(Convert.ToInt32(instructionParts[2][1])));
                isSourceDataReigster = true;
            }
            else
            {
                binary.Append(converter.DataRegisterToBinary(Convert.ToInt32(instructionParts[3][1])));
            }

            OperandSizeFactory operandFactory = new OperandSizeFactory();
            OperandSize.operandSize operandSize = operandFactory.GetOperand(instructionParts[1]);

            //Append opmode
            binary.Append(GetBinaryFromOpmode(operandSize, isSourceDataReigster));

            //Append effective Address
            AddressingModes addressingModes = new AddressingModes();
            EffectiveAddress effectiveAddress = null;
            if(isSourceDataReigster)
            {
                effectiveAddress = addressingModes.GetAddressingMode(instructionParts[3]);
            }
            else
            {
                effectiveAddress = addressingModes.GetAddressingMode(instructionParts[2]);
            }

            binary.Append(effectiveAddress.GetMode());

            if(effectiveAddress.GetRegister() != null)
            {
                binary.Append(effectiveAddress.GetRegister());
            }
            else
            {
                
            }

            return binary.ToString(); ;
        }

        private string GetBinaryFromOpmode(OperandSize.operandSize operandSize, bool isSourceDataRegister)
        {
            if(isSourceDataRegister)
            {
                if(operandSize == OperandSize.operandSize.BYTE)
                {
                    return "100";
                }
                else if (operandSize == OperandSize.operandSize.WORD)
                {
                    return "101";
                }
                else if(operandSize == OperandSize.operandSize.LONG_WORD)
                {
                    return "110";
                }
            }
            else
            {
                if (operandSize == OperandSize.operandSize.BYTE)
                {
                    return "000";
                }
                else if (operandSize == OperandSize.operandSize.WORD)
                {
                    return "001";
                }
                else if (operandSize == OperandSize.operandSize.LONG_WORD)
                {
                    return "010";
                }
            }

            throw new Exception("Stop fucking up");
        }
    }
}
