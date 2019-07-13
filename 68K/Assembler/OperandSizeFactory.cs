using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler
{
    public class OperandSizeFactory
    {
        private Dictionary<string, OperandSize.operandSize> operandSizes;

        public OperandSizeFactory()
        {
            operandSizes = new Dictionary<string, OperandSize.operandSize>
            {
                { "b", OperandSize.operandSize.BYTE },
                { "byte", OperandSize.operandSize.BYTE },

                { "w", OperandSize.operandSize.WORD },
                { "word", OperandSize.operandSize.WORD },

                { "l", OperandSize.operandSize.LONG_WORD },
                { "long", OperandSize.operandSize.LONG_WORD }
            };
        }
    }
}
