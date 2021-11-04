using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInstruction
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

    public static GenericInstruction Parse(string text){
        var tokens = text.Split(' ');
        if (tokens.Length == 0){
            throw new NotImplementedException();
        }
        else if(tokens.Length ==1){
            if(tokens[0] == "NOOP"){
                return new GenericInstruction {mnemonic="NOOP", editable=true};
            }            
        }
        else if(tokens.Length ==2){
            if(tokens[0] == "MOV"){
                return new GenericInstruction {mnemonic="MOV", operand1=tokens[1], editable=true};
            }            
        }
        else if(tokens.Length == 3){
            return new GenericInstruction{mnemonic="MOV", operand1="AX", operand2="0", editable=true};
        }
        throw new NotImplementedException();


    }
}

[CreateAssetMenu(fileName = "AssemblyProgram", menuName = "Asm/AssemblyProgram")]
public class AssemblyProgram : ScriptableObject
{
    public List<GenericInstruction> instructions;

    void Start()
    {
        Debug.Log("Awake Assemby program");
    }

    void LoadProgram(){
        instructions = new List<GenericInstruction>();
        instructions.Add(new GenericInstruction { mnemonic = "MOV", operand1 = "AX", operand2 = "0x00" });
        instructions.Add(new GenericInstruction { mnemonic = "MOV", operand1 = "BX", operand2 = "0x00" });
        instructions.Add(new GenericInstruction { mnemonic = "MOV", operand1 = "CX", operand2 = "0x00", editable=true });
        instructions.Add(new GenericInstruction { mnemonic = "MOV", operand1 = "DX", operand2 = "0x00" });
    }

    public void EnsureProgramIsLoaded(){
        if(instructions == null){
            LoadProgram();
        }
    }
}
