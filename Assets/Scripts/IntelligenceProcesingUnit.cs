using System;
using System.Collections.Generic;
using UnityEngine;

public class IntelligenceProcesingUnit : MonoBehaviour {
    public Dictionary<string, byte> registers = new Dictionary<string, byte>();
    public AssemblyProgram program;

    public Action onStepExecuted;
    
    private void Awake() 
    {
        registers["AX"] = 0xAA;
        registers["BX"] = 0xBB;
        registers["CX"] = 0xCC;
        registers["DX"] = 0xDD;
        registers["IP"] = 0;
    }

    public void Step()
    {
        if(registers["IP"] >= program.instructions.Count)        
            return;

        var instruction = program.instructions[registers["IP"]];
        registers["IP"]++;

        ExecuteInstruction(instruction);

        StepExecuted();
    }

    public void StepExecuted()
    {
        if(onStepExecuted != null)
        {
            onStepExecuted();
        }
    }

    private void ExecuteInstruction(GenericInstruction instruction)
    {
        Debug.Log($"Executing {instruction}");
    }
}