using System;
using System.Collections.Generic;
using UnityEngine;

public class IntelligenceProcesingUnit : MonoBehaviour {
    public Dictionary<string, byte> registers = new Dictionary<string, byte>();
    public AssemblyProgram program;
    public Action onStepExecuted;
    public Action<State> onStateChanged;

    public enum State
    {
        IDLE,
        RUN,
        STEP,
        ERROR,
        DONE
    }

    State state;
    
    private void Awake() 
    {
        registers["AX"] = 0xAA;
        registers["BX"] = 0xBB;
        registers["CX"] = 0xCC;
        registers["DX"] = 0xDD;
        registers["IP"] = 0;

        state = State.IDLE;
    }

    private void OnGUI() {
        GUI.Label(new Rect(10,10,100,50), state.ToString());
    }

    void Start()
    {
        ChangeState(State.IDLE);
    }

    void ChangeState(State newState)
    {
        Debug.Log($"Changing IPU state to {newState}");
        if(onStateChanged != null)
        {
            onStateChanged(newState);
        }
        state = newState;

    }

    public void Step()
    {
        if(state == State.STEP)
        {
            PerformStep();
        }
        else
        {
            ChangeState(State.STEP);
        }
    }

    private void PerformStep()
    {
        if(registers["IP"] >= program.instructions.Count)
        {
            FinishExecution();
            return;
        }

        var instruction = program.instructions[registers["IP"]];
        registers["IP"]++;

        ExecuteInstruction(instruction);
        StepExecuted();
    }

    private void FinishExecution()
    {
        CancelInvoke("PerformStep");
        ChangeState(State.DONE);
        // TODO: check win conditions
    }

    public void Run()
    {
        ChangeState(State.RUN);
        InvokeRepeating("PerformStep", 1f, 1f);
    }

    public void Stop()
    {
        CancelInvoke("PerformStep");
        ChangeState(State.IDLE);
    }


    private void ExecuteInstruction(Instruction instruction)
    {
        Debug.Log($"Executing {instruction}");
    }

    
    public void StepExecuted()
    {
        if(onStepExecuted != null)
        {
            onStepExecuted();
        }
    }
}