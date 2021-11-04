using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInstruction
{
    public string mnemonic;
    public string operand1;
    public string operand2;

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
        instructions.Add(new GenericInstruction { mnemonic = "MOV", operand1 = "CX", operand2 = "0x00" });
        instructions.Add(new GenericInstruction { mnemonic = "MOV", operand1 = "DX", operand2 = "0x00" });
    }

    public void EnsureProgramIsLoaded(){
        if(instructions == null){
            LoadProgram();
        }
    }
}
