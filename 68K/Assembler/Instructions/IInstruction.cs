using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Instructions
{
    public interface IInstruction
    {
        string ConvertToBinary(string instruction);
    }
}
