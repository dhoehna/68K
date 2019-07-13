using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Instructions;

namespace Assembler
{
    public class InstructionFactory
    {
        private Dictionary<string, IInstruction> instructions;

        public InstructionFactory()
        {
            this.instructions = new Dictionary<string, IInstruction>();

            instructions.Add("add", new ADD());
            instructions.Add("addbinary", new ADD());
        }

        public IInstruction GetInstruction(string instruction)
        {
            if(this.instructions.ContainsKey(instruction))
            {
                return this.instructions[instruction];
            }
            else
            {
                throw new KeyNotFoundException("Instruction " + instruction + " does not exist");
            }
        }
    }
}
