using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


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
            if (args.Length == 0)
            {
                Console.WriteLine("Argument 1 needs to be the file to assemble");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            FileInfo asmFile = new FileInfo(args[0]);

            if (!asmFile.Exists)
            {
                Console.WriteLine("Can not file the file " + asmFile.FullName);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            int startLineNumber = FindStart(asmFile.OpenText());
            if (startLineNumber == -1)
            {
                Console.WriteLine("\"start program\" could not be found");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }

            int endLineNumber = FindEnd(asmFile.OpenText());
            if (endLineNumber == -1)
            {
                Console.WriteLine("\"end program\" could not be found");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                return;
            }

            if (endLineNumber < startLineNumber)
            {
                Console.WriteLine("\"end program\" is before \"start program\"");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                return;
            }

            //Finally, some assembly code.
            //First start out everyone is between start and end.

            StreamReader reader = asmFile.OpenText();

            for (int lineNumber = 0; lineNumber < startLineNumber; lineNumber++)
            {
                reader.ReadLine();
            }

            // At this point we are one line past start program.
            // if we do a ReadLine here we will read the line after
            // start program.

            //We only care about Add binary
            //<ea> = effective Address

            StreamWriter writer = new StreamWriter(Environment.CurrentDirectory + "\\output.txt");
            bool done = false;

            while (!done)
            {
                string instruction = reader.ReadLine();

                if (!instruction.Equals(END_INSTRUCTION, StringComparison.OrdinalIgnoreCase))
                {
                    string[] instructionParts = instruction.Split(' ');
                    string instructionMnemonic = instructionParts[0];

                    string binaryInstruction = string.Empty;

                    if(instructionMnemonic.Equals("ADD", StringComparison.OrdinalIgnoreCase) 
                        || instructionMnemonic.Equals("AddBinary", StringComparison.OrdinalIgnoreCase))
                    {
                        binaryInstruction = ProcessAdd(instructionParts);
                    }
                }
                else
                {
                    done = true;
                }
            }

            writer.Flush();
            writer.Close();

            reader.Close();


        }

        static string ProcessAdd(string[] arguments)
        {
            string output = "1101";
            //Source mode
            //Opmode

            string instructionMnemonic = arguments[0];
            string mode = arguments[1];
            string source = arguments[2];
            string destination = arguments[3];

            if(source[0] == 'D')
            {
                switch(source[1])
                {
                    case '0':
                        output += "000";
                        break;
                    case '1':
                        output += "001";
                        break;
                    case '2':
                        output += "010";
                        break;
                    case '3':
                        output += "011";
                        break;
                    case '4':
                        output += "100";
                        break;
                    case '5':
                        output += "101";
                        break;
                    case '6':
                        output += "110";
                        break;
                    case '7':
                        output += "111";
                        break;

                }
            }
            else
            {
                switch (destination[1])
                {
                    case '0':
                        output += "000";
                        break;
                    case '1':
                        output += "001";
                        break;
                    case '2':
                        output += "010";
                        break;
                    case '3':
                        output += "011";
                        break;
                    case '4':
                        output += "100";
                        break;
                    case '5':
                        output += "101";
                        break;
                    case '6':
                        output += "110";
                        break;
                    case '7':
                        output += "111";
                        break;

                }
            }

            if(source[0] == 'D')
            {
                if(mode.Equals("B", StringComparison.OrdinalIgnoreCase)
                    || mode.Equals("BYTE", StringComparison.OrdinalIgnoreCase))
                {
                    output += "100";
                }
                else if (mode.Equals("W", StringComparison.OrdinalIgnoreCase)
                    || mode.Equals("WORD", StringComparison.OrdinalIgnoreCase))
                {
                    output += "101";
                }
                else if (mode.Equals("L", StringComparison.OrdinalIgnoreCase)
                    || mode.Equals("LONG", StringComparison.OrdinalIgnoreCase))
                {
                    output += "110";
                }
            }
            else
            {
                if (mode.Equals("B", StringComparison.OrdinalIgnoreCase)
                    || mode.Equals("BYTE", StringComparison.OrdinalIgnoreCase))
                {
                    output += "000";
                }
                else if (mode.Equals("W", StringComparison.OrdinalIgnoreCase)
                    || mode.Equals("WORD", StringComparison.OrdinalIgnoreCase))
                {
                    output += "010";
                }
                else if (mode.Equals("L", StringComparison.OrdinalIgnoreCase)
                    || mode.Equals("LONG", StringComparison.OrdinalIgnoreCase))
                {
                    output += "011";
                }
            }

            return output;
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
