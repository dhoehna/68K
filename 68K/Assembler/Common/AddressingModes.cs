using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    static class AddressingModes
    {
        private static Dictionary<string, EffectiveAddress> addressingModes;

        static AddressingModes()
        {
            addressingModes = new Dictionary<string, EffectiveAddress>();

            addressingModes.Add("dn", new EffectiveAddress("000"));
            addressingModes.Add("an", new EffectiveAddress("001"));
            addressingModes.Add("-(an)", new EffectiveAddress("100"));
            addressingModes.Add("#<xxx>", new EffectiveAddress("111", "100"));

            addressingModes.Add("(an)+", new EffectiveAddress("011"));
            addressingModes.Add("(xxx).w", new EffectiveAddress("111", "000"));
            addressingModes.Add("(xxx).l", new EffectiveAddress("111", "001"));
            addressingModes.Add("(an)", new EffectiveAddress("010"));


            addressingModes.Add("(d16,an)", new EffectiveAddress("101"));
            addressingModes.Add("(d16,pc)", new EffectiveAddress("111", "010"));

            addressingModes.Add("(d8,an,xn)", new EffectiveAddress("000"));
            addressingModes.Add("(d8,pc,xn)", new EffectiveAddress("111", "011"));
        }

        public static EffectiveAddress GetAddressingMode(string effectiveAddress)
        {
            string lowerCasedEffectiveAddress = effectiveAddress.ToLower();

            //Test for all addressing modes that don't start with a hash
            if(lowerCasedEffectiveAddress[0] == 'd')
            {
                EffectiveAddress ea = addressingModes["dn"];
                ea.SetRegister(Conversion.DataRegisterToBinary(Convert.ToInt32(lowerCasedEffectiveAddress[1])));

                return ea;
            }
            else if(lowerCasedEffectiveAddress[0] == 'a')
            {
                EffectiveAddress ea = addressingModes["an"];
                ea.SetRegister(Conversion.DataRegisterToBinary(Convert.ToInt32(lowerCasedEffectiveAddress[1])));

                return ea;
            }
            else if(lowerCasedEffectiveAddress[0] == '-')
            {
                EffectiveAddress ea = addressingModes["-(an)"];
                ea.SetRegister(Conversion.DataRegisterToBinary(Convert.ToInt32(lowerCasedEffectiveAddress[3])));

                return ea;
            }
            else if (lowerCasedEffectiveAddress[0] == '#')
            {
                return addressingModes["#<xxx>"];
            }

            lowerCasedEffectiveAddress = lowerCasedEffectiveAddress.Trim('(', ')');
            string[] effectiveAddressParts = lowerCasedEffectiveAddress.Split(',');

            if(effectiveAddressParts.Length == 1)
            {
                if (effectiveAddressParts[0][effectiveAddressParts.Length - 1] == '+')
                {
                    EffectiveAddress ea = addressingModes["(an)+"];
                    ea.SetRegister(Conversion.DataRegisterToBinary(Convert.ToInt32(effectiveAddressParts[0][1])));

                    return ea;
                }
                else if (effectiveAddressParts[0][effectiveAddressParts.Length - 1] == 'w')
                {
                    return addressingModes["(xxx).w"];
                }
                else if (effectiveAddressParts[0][effectiveAddressParts.Length - 1] == 'l')
                {
                    return addressingModes["(xxx).l"];
                }
                else if(effectiveAddressParts[0][0] == 'a')
                {
                    EffectiveAddress ea = addressingModes["(an)"];
                    ea.SetRegister(Conversion.DataRegisterToBinary(Convert.ToInt32(effectiveAddressParts[0][1])));

                    return ea;
                }
            }
            else if (effectiveAddressParts.Length == 2)
            {
                if(effectiveAddressParts[1].Equals("an"))
                {
                    EffectiveAddress ea = addressingModes["(d16,an)"];
                    ea.SetRegister(Conversion.DataRegisterToBinary(Convert.ToInt32(effectiveAddressParts[1][1])));

                    int displacment = Convert.ToInt16(effectiveAddressParts[0]);
                    ea.SetDisplacment(Convert.ToString(displacment, 2));

                    return ea;
                }
                else if(effectiveAddressParts[1].Equals("pc"))
                {
                    EffectiveAddress ea = addressingModes["(d16,pc)"];
                    int displacment = Convert.ToInt16(effectiveAddressParts[0]);
                    ea.SetDisplacment(Convert.ToString(displacment, 2));

                    return ea;
                }
            }
            else if(effectiveAddressParts.Length == 3)
            {
                if (effectiveAddressParts[1].Equals("an"))
                {
                    EffectiveAddress ea = addressingModes["(d8,an,xn)"];
                    ea.SetRegister(Conversion.DataRegisterToBinary(Convert.ToInt32(effectiveAddressParts[1][1])));

                    byte displacment = Convert.ToByte(effectiveAddressParts[0]);
                    ea.SetDisplacment(Convert.ToString(displacment, 2));

                    return ea;
                }
                else if (effectiveAddressParts[1].Equals("pc"))
                {
                    EffectiveAddress ea = addressingModes["(d8,pc,xn)"];
                    ea.SetRegister(Conversion.DataRegisterToBinary(Convert.ToInt32(effectiveAddressParts[1][1])));

                    byte displacment = Convert.ToByte(effectiveAddressParts[0]);
                    ea.SetDisplacment(Convert.ToString(displacment, 2));

                    return ea;
                }
            }

            throw new Exception("You done fucked up");
        }


    }
}
