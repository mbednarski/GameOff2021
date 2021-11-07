using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtonsEnabler : MonoBehaviour
{
    [SerializeField] GameObject ipuGameObject;
    [SerializeField] Button stopButton, stepButton, runButton;

    IntelligenceProcesingUnit ipu;
    void Awake() {
        ipu = ipuGameObject.GetComponent<IntelligenceProcesingUnit>();
        Debug.Assert(ipu != null);

        Debug.Assert(stopButton != null);
        Debug.Assert(stepButton != null);
        Debug.Assert(runButton != null);

        ipu.onStateChanged += ipuStateChanged;


    }

    private void ipuStateChanged(IntelligenceProcesingUnit.State state)
    {
        switch(state)
        {
            case IntelligenceProcesingUnit.State.IDLE:
                SetIdle();
                break;
            case IntelligenceProcesingUnit.State.RUN:
                SetRun();
                break;
            case IntelligenceProcesingUnit.State.STEP:
                SetStep();
                break;
            case IntelligenceProcesingUnit.State.ERROR:
                SetError();
                break;
            case IntelligenceProcesingUnit.State.DONE:
                SetDone();
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void SetDone()
    {
        stopButton.interactable = false;
        runButton.interactable = false;
        stepButton.interactable = false;
    }

    private void SetError()
    {
        throw new NotImplementedException();
    }

    private void SetStep()
    {        
        stopButton.interactable = true;
        runButton.interactable = true;
        stepButton.interactable = true;
    }

    private void SetRun()
    {
        Debug.Log("SetRun");
        stopButton.interactable = true;
        runButton.interactable = false;
        stepButton.interactable = false;
    }

    private void SetIdle()
    {
        stopButton.interactable = false;
        runButton.interactable = true;
        stepButton.interactable = true;
    }
}
