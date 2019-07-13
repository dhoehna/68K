using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Assembler.Instructions;


/**
 *  Byte - 8 bits
 *  WORD - 16 bits
 *  Long word - 32 bits
 *  quad word - 64 bits
 */

//13 spaces for the op code

//start program
//end program

namespace Assembler
{
    class Program
    {
        const int OP_CODE_LENGTH = 13;
        const string START_INSTRUCTION = "start program";
        const string END_INSTRUCTION = "end program";
        static void Main(string[] args)
        {
           
            FileInfo asmFile = new FileInfo(args[0]);

            int startLineNumber = FindStart(asmFile.OpenText());
            int endLineNumber = FindEnd(asmFile.OpenText());

            //Finally, some assembly code.
            //First start out everyone is between start and end.

            StreamReader reader = asmFile.OpenText();

            for (int lineNumber = 0; lineNumber < startLineNumber; lineNumber++)
            {
                reader.ReadLine();
            }

            InstructionFactory instructionFactory = new InstructionFactory();
            bool foundEnd = false;

            while(!reader.EndOfStream || !foundEnd)
            {
                string instruction = reader.ReadLine();

                if(!instruction.Equals(END_INSTRUCTION, StringComparison.OrdinalIgnoreCase))
                {
                    IInstruction instructionObject = instructionFactory.GetInstruction(instruction.ToLower());
                }
                else
                {
                    foundEnd = true;
                }
            }

            // At this point we are one line past start program.
            // if we do a ReadLine here we will read the line after
            // start program.

            //We only care about Add binary
            //<ea> = effective Address


            reader.Close();


        }

        static int FindStart(StreamReader asmFile)
        {
            int lineNumber = 0;
            while (!asmFile.EndOfStream)
            {
                string lineOfAssembly = asmFile.ReadLine();
                lineNumber++;

                if (lineOfAssembly.Trim().Equals(START_INSTRUCTION, StringComparison.OrdinalIgnoreCase))
                {
                    return lineNumber;
                }
            }

            return -1;
        }

        static int FindEnd(StreamReader asmFile)
        {
            int lineNumber = 0;
            while (!asmFile.EndOfStream)
            {
                string lineOfAssembly = asmFile.ReadLine();
                lineNumber++;

                if (lineOfAssembly.Trim().Equals(END_INSTRUCTION, StringComparison.OrdinalIgnoreCase))
                {
                    return lineNumber;
                }
            }

            return -1;
        }
    }
}
