using System;
using System.Globalization;

namespace Instructions
{
    public enum ParseError
    {
        NO_ERROR,
        EMPTY_INSTRUCTION,
        UNKNOWN_OPERAND,
        INVALID_NUMBER,
        NUMBER_OUT_OF_RANGE
    }
    public class InstructionParser
    {
        private string[] mnemonics0arg = { "NOOP" };
        private string[] mnemonics1arg = { "INC", "DEC" };

        public (Instruction, ParseError) Parse(string txt)
        {
            /*
                1. Extract mnemonic
                2. Pass to specific subparser
             */

            txt = txt.Trim();

            if (txt.Length == 0)
            {
                return (null, ParseError.EMPTY_INSTRUCTION);
            }

            var pivot = txt.IndexOf(' ');
            
            var instructionString = txt.Substring(0, pivot).ToLower();
            var operandsString = txt.Substring(pivot).Trim().ToLower();

            throw new NotImplementedException();
        }

        void ParseOperands(string operandsString)
        {
            var rawTokens = operandsString.Split(',');
            string[] cleanTokens = new string[rawTokens.Length];
            for (int i = 0; i < rawTokens.Length; i++)
            {
                cleanTokens[i] = rawTokens[i].Trim();
            }
        }

        private bool IsRegistryName(string value)
        {
            return true;
        }

        public (Operand, ParseError) RecognizeOperand(string operand)
        {
            if (operand.StartsWith("0x"))
            {
                var numberString = operand.Substring(2);
                int value = 0;
                var succeded = Int32.TryParse(numberString, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture,  out value);
                if(!succeded){
                    return (null, ParseError.INVALID_NUMBER);
                }
                if(value < 0 || value > 255){
                    return(null, ParseError.NUMBER_OUT_OF_RANGE);
                }
                return (new Operand { type = OperandType.HEX_NUMBER, value = value }, ParseError.NO_ERROR);
            }
            if (operand.StartsWith("d"))
            {
                return (new Operand { type = OperandType.DEC_NUMBER, value = operand }, ParseError.NO_ERROR);
            }
            if (operand.StartsWith('[') && operand.EndsWith(']'))
            {
                var middle = operand.Substring(1, operand.Length - 1);
                if (middle.StartsWith("0x"))
                {
                    return (new Operand { type = OperandType.HEX_ADDR, value = operand }, ParseError.NO_ERROR);
                }
                if (IsRegistryName(operand))
                {
                    return (new Operand { type = OperandType.REG_ADDR, value = operand }, ParseError.NO_ERROR);
                }
                else
                {
                    return (null, ParseError.UNKNOWN_OPERAND);
                }
            }
            if (IsRegistryName(operand))
            {
                return (new Operand { type = OperandType.REG_NAME, value = operand }, ParseError.NO_ERROR);
            }
            return (null, ParseError.UNKNOWN_OPERAND);
        }
    }
}