using System;
using System.Collections.Generic;
using System.Globalization;

public class InstructionParser
{
    string[] allowedRegisters = { "AX", "BX", "CX", "DX", "IP" };
    string[] zeroInstructions = { "NOOP" };
    string[] unaryInstructions = { "JMP", "CJMP", "NEG" };
    string[] binaryInstructions = { "MOV", "GT", "LT", "LE", "GT", "EQ" };

    public class InstructionParseError
    {
        public string ErrorMessage;

        public InstructionParseError(string msg)
        {
            ErrorMessage = msg;
        }

        public override string ToString()
        {
            return ErrorMessage;
        }
    }

    private (Instruction, InstructionParseError) ParseZero(string[] tokens)
    {
        var mnemonic = tokens[0];
        if (Array.IndexOf(zeroInstructions, mnemonic) == -1)
        {
            return (null, new InstructionParseError($"Unknown 0-arg instruction {mnemonic}"));
        }
        return (new Instruction
        {
            mnemonic = mnemonic.ToUpper()
        }, null);
    }

    public (Instruction, InstructionParseError) Parse(string text)
    {
        text = text.Trim();
        var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length == 0)
        {
            return (null, new InstructionParseError("Instruction cannot be empty! Use NOOP instead."));
        }
        if (tokens.Length == 1)
        {
            return ParseZero(tokens);
        }
        if(tokens.Length == 2)
        {
            return Parse1arg(tokens);
        }
        if(tokens.Length == 3)
        {
            return Parse2arg(tokens);
        }
        return (null, new InstructionParseError("Too much tokens!"));
    }

    private bool IsValidRegistry(string token)
    {
        return Array.IndexOf(allowedRegisters, token) != -1;
    }

    private bool IsValidNumber(string token)
    {
        int value;
        bool parsed= false;
        if(token.ToLower().StartsWith("0x"))
        {
            token = token.Substring(2);
            parsed = Int32.TryParse(token,System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
        }
        else
        {
            parsed = Int32.TryParse(token, out value);
        }
        if(!parsed){
            return false;
        }

        return value >=0 && value <= 255;

    }

    private (Instruction, InstructionParseError) Parse1arg(string[] tokens)
    {
       var mnemonic = tokens[0].ToUpperInvariant();
       var op1 = tokens[1];

       if(Array.IndexOf(unaryInstructions, mnemonic) == -1){
           return (null, new InstructionParseError($"Unknown 1-arg instruction {mnemonic}"));
       }
       
       bool reg = IsValidRegistry(op1);
       bool num = IsValidNumber(op1);

        if (reg || num)
        {
            return (new Instruction{mnemonic=mnemonic, operand1=op1}, null);
        }

        return (null, new InstructionParseError(
            $"{op1} is not a valid registry name neither number!"
        ));
    }

    private (Instruction, InstructionParseError) Parse2arg(string[] tokens)
    {
       var mnemonic = tokens[0].ToUpperInvariant();
       var op1 = tokens[1];
       var op2 = tokens[2];

       if(Array.IndexOf(binaryInstructions, mnemonic) == -1){
           return (null, new InstructionParseError($"Unknown 2-arg instruction {mnemonic}"));
       }
       
       bool reg1 = IsValidRegistry(op1);
       bool num1 = IsValidNumber(op1);
       bool reg2 = IsValidRegistry(op2);
       bool num2 = IsValidNumber(op2);

       if(!reg1)
       {
           return (null, new InstructionParseError(
            $"First operand must be a registry. >{reg1}< is not a valid registry name!"
        ));
       }

        if (reg2 || num2)
        {
            return (new Instruction{mnemonic=mnemonic, operand1=op1, operand2=op2}, null);
        }

        return (null, new InstructionParseError(
            $"{op2} is not a valid registry name neither number!"
        ));
    }
}