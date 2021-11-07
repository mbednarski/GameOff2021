using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "AssemblyProgram", menuName = "Asm/AssemblyProgram")]
public class AssemblyProgram : ScriptableObject
{
    public List<Instruction> instructions;

    void Start()
    {
        Debug.Log("Awake Assemby program");
    }

    void LoadProgram(){
        instructions = new List<Instruction>();
        instructions.Add(new Instruction { mnemonic = "MOV", operand1 = "AX", operand2 = "0x00" });
        instructions.Add(new Instruction { mnemonic = "MOV", operand1 = "BX", operand2 = "0x00" });
        instructions.Add(new Instruction { mnemonic = "MOV", operand1 = "CX", operand2 = "0x00", editable=true });
        instructions.Add(new Instruction { mnemonic = "MOV", operand1 = "DX", operand2 = "0x00" });
    }

    public void EnsureProgramIsLoaded(){
        if(instructions == null){
            LoadProgram();
        }
    }
}
