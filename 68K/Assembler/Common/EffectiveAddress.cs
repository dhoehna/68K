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
        private string displacment;

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

        public string GetDisplacment()
        {
            return this.displacment;
        }

        public void SetDisplacment(string displacment)
        {
            this.displacment = displacment;
        }
    }
}
