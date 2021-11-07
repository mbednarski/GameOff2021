using System;

public class Instruction
{
    public string mnemonic;
    public string operand1;
    public string operand2;
    public bool editable = false;

    public override string ToString()
    {
        if (operand1 == null & operand2 == null)
        {
            return $"{mnemonic}";
        }
        else if (operand1 != null && operand2 == null)
        {
            return $"{mnemonic} {operand1}";
        }
        else if (operand1 != null && operand2 != null)
        {
            return $"{mnemonic} {operand1}, {operand2}";
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public static Instruction Parse(string text){
        var tokens = text.Split(' ');
        if (tokens.Length == 0){
            throw new NotImplementedException();
        }
        else if(tokens.Length ==1){
            if(tokens[0] == "NOOP"){
                return new Instruction {mnemonic="NOOP", editable=true};
            }            
        }
        else if(tokens.Length ==2){
            if(tokens[0] == "MOV"){
                return new Instruction {mnemonic="MOV", operand1=tokens[1], editable=true};
            }            
        }
        else if(tokens.Length == 3){
            return new Instruction{mnemonic="MOV", operand1="AX", operand2="0", editable=true};
        }
        throw new NotImplementedException();


    }
}