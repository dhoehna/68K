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

namespace _68KAssembler
{
    class Program
    {
        const int OP_CODE_LENGTH = 13;
        const string START_INSTRUCTION = "start program";
        const string END_INSTRUCTION = "end program";
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Argument 1 needs to be the file to assemble");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            FileInfo asmFile = new FileInfo(args[0]);

            if(!asmFile.Exists)
            {
                Console.WriteLine("Can not file the file " + asmFile.FullName);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }

            int startLineNumber = findStart(asmFile.OpenText());
            if(startLineNumber == -1)
            {
                Console.WriteLine("\"start program\" could not be found");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }

            int endLineNumber = findEnd(asmFile.OpenText());
            if (endLineNumber == -1)
            {
                Console.WriteLine("\"end program\" could not be found");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                return;
            }

            if(endLineNumber < startLineNumber)
            {
                Console.WriteLine("\"end program\" is before \"start program\"");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                return;
            }

            //Finally, some assembly code.
            //First start out everyone is between start and end.

            StreamReader reader = asmFile.OpenText();
            
            for(int lineNumber = 0; lineNumber < startLineNumber; lineNumber++)
            {
                reader.ReadLine();
            }

            // At this point we are one line past start program.
            // if we do a ReadLine here we will read the line after
            // start program.

            bool done = false;

            while (!done)
            {
                string instruction = reader.ReadLine();

                if (!instruction.Equals(END_INSTRUCTION, StringComparison.OrdinalIgnoreCase))
                {
                    //process the instruction
                }
                else
                {
                    done = true;
                }
            }


        }

        static int findStart(StreamReader asmFile)
        {
            int lineNumber = 0;
            while(!asmFile.EndOfStream)
            {
                string lineOfAssembly = asmFile.ReadLine();
                lineNumber++;

                if(lineOfAssembly.Trim().Equals(START_INSTRUCTION, StringComparison.OrdinalIgnoreCase))
                {
                    return lineNumber;
                }
            }

            return -1;
        }

        static int findEnd(StreamReader asmFile)
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
