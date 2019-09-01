using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    public class EffectiveAddress
    {
        private string mode;
        private string register;

        public EffectiveAddress(string mode, string register = null)
        {
            this.mode = mode;
            this.register = register;
        }

        public string GetMode()
        {
            return mode;
        }

        public string GetRegister()
        {
            return register;
        }

        public void SetRegister(string register)
        {
            this.register = register;
        }

        public string CombineEaAndInstruciton(string instruction)
        {
            if(this.mode == "000" || this.mode == "001" || this.mode == "010")
            {
                return "0000000000" + this.mode + this.register;
            }
            else if (this.mode == "011")
            {

            }




            return null;
        }
    }
}
