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
        private const int OP_SIZE = 1;
        private const int SOURCE = 2;
        private const int DESTINATION = 3;

        public string ConvertToBinary(string instruction)
        {
            StringBuilder binary = new StringBuilder("1101");
            binary.Append(ConvertInstructionToBinary(instruction));

            bool isSourceDataReigster = false;

            //SOurce is a data register
            //append register
            string[] instructionParts = instruction.Split(' ');
            if (instructionParts[SOURCE][0] == 'd')
            {
                isSourceDataReigster = true;
            }

            if(isSourceDataReigster)
            {
                binary.Append(ConvertDestinationEA());
            }
            else
            {
                binary.Append(ConvertImmediateOrSourceEA(instructionParts));
            }

            return binary.ToString();

        }

        public string ConvertInstructionToBinary(string instruction)
        {
            StringBuilder binary = new StringBuilder();
            string[] instructionParts = instruction.Split(' ');

            bool isSourceDataReigster = false;
            Conversion converter = new Conversion();

            //SOurce is a data register
            //append register
            if (instructionParts[SOURCE][0] == 'd')
            {
                binary.Append(converter.DataRegisterToBinary(Convert.ToInt32(instructionParts[SOURCE][1])));
                isSourceDataReigster = true;
            }
            else
            {
                binary.Append(converter.DataRegisterToBinary(Convert.ToInt32(instructionParts[DESTINATION][1])));
            }

            OperandSizeFactory operandFactory = new OperandSizeFactory();
            OperandSize.operandSize operandSize = operandFactory.GetOperand(instructionParts[OP_SIZE]);

            //Append opmode
            binary.Append(GetBinaryFromOpmode(operandSize, isSourceDataReigster));

            //Append effective Address
            AddressingModes addressingModes = new AddressingModes();
            EffectiveAddress effectiveAddress = null;
            if (isSourceDataReigster)
            {
                effectiveAddress = addressingModes.GetAddressingMode(instructionParts[DESTINATION]);
            }
            else
            {
                effectiveAddress = addressingModes.GetAddressingMode(instructionParts[SOURCE]);
            }

            binary.Append(effectiveAddress.GetMode());
            binary.Append(effectiveAddress.GetRegister());

            return binary.ToString();
        }

        public string ConvertSpecialOperandSpecifiers()
        {
            return string.Empty;
        }

        public string ConvertImmediateOrSourceEA(string[] instructionParts)
        {
            StringBuilder binary = new StringBuilder();

            //If source is immediate
            if (instructionParts[2][0] == '#')
            {
                string immediateNumber = instructionParts[2].Substring(1);
                Int16 number = Convert.ToInt16(immediateNumber);
                binary.Append(Convert.ToString(number, 2));
            }
            else
            {
                AddressingModes addressingModes = new AddressingModes();
                EffectiveAddress effectiveAddress = addressingModes.GetAddressingMode(instructionParts[2]);
            }



            return binary.ToString();
        }

        public string ConvertDestinationEA()
        {
            return null;
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
