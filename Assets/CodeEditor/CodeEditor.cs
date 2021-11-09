using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeEditor : MonoBehaviour
{
    [SerializeField] AssemblyProgram program;
    [SerializeField] GameObject instructionPrefab;
    [SerializeField] GameObject ipuGameObject;
    IntelligenceProcesingUnit ipu;
    [SerializeField] GameObject instructionArrow;

    [SerializeField] TerminalOutput terminalOutput;
    RectTransform arrowTransform;
    [SerializeField] float instructionOffset = 2f;
    private List<UnityEvent<int, string>> eventsToDispose = new List<UnityEvent<int,string>>();
    [SerializeField] GameObject listingParent;
    [SerializeField] Vector3 basePos = new Vector3(-100,0,0);
    Vector3 arrowBasePos = new Vector3(-0,0,0);
    Vector3 offset;
    InstructionParser instructionParser;


    void Awake()
    {
        offset = new Vector3(0,-instructionOffset,0);
        Debug.Assert(listingParent != null);
        ipu = ipuGameObject.GetComponent<IntelligenceProcesingUnit>();
        ipu.onStepExecuted += IpuStepExecuted;  
        ipu.onStateChanged += IpuStateChanged;        

        arrowTransform = instructionArrow.GetComponent<RectTransform>();
        Debug.Assert(arrowTransform != null);

        instructionParser = new InstructionParser();

        Debug.Assert(terminalOutput != null);
    }
    
    void Start()
    {
        Clear();
        FillEditor();   
    }

    void IpuStateChanged(IntelligenceProcesingUnit.State state)
    {
        switch (state)
        {
            case IntelligenceProcesingUnit.State.IDLE:
                SetArrowVisibility(false);                
                SetArrowPosition(0);
                break;
            case IntelligenceProcesingUnit.State.DONE:
                SetArrowVisibility(false);
                break;
            case IntelligenceProcesingUnit.State.RUN:
                SetArrowVisibility(true);
                break;
            case IntelligenceProcesingUnit.State.STEP:
                SetArrowVisibility(true);
                break;
        }
    }

    private void SetArrowVisibility(bool visibility)
    {
        instructionArrow.SetActive(visibility);
    }

    void Clear()
    {
        foreach (var item in eventsToDispose)
        {
            item.RemoveAllListeners();
        }
        eventsToDispose.Clear();

        while(listingParent.transform.childCount > 0)
        {
            DestroyImmediate(listingParent.transform.GetChild(0).gameObject);
        }
    }

    void FillEditor()
    {
        program.EnsureProgramIsLoaded();
        for (int i = 0; i < program.instructions.Count; i++)
        {
            var line = Instantiate(instructionPrefab);
            line.transform.SetParent(listingParent.transform);
            line.transform.localScale = Vector3.one;

            line.GetComponent<RectTransform>().localPosition = basePos + i * offset;

            var x = line.GetComponent<InstructionPrefabScript>();
            x.SetLineText(i, program.instructions[i].ToString());
            x.SetEditable(program.instructions[i].editable);
            x.InstructionNumber = i;
            x.onInstructionChanged.AddListener(OnInstructionChanged);
            eventsToDispose.Add(x.onInstructionChanged);
        }
    }

    void SetArrowPosition(int ip)
    {
        arrowTransform.localPosition = arrowBasePos + ip * offset;
    }

    void IpuStepExecuted()
    {
        var ip = ipu.registers["IP"];
        SetArrowPosition(ip);
    }

    void PrintErrorMessage(int line, string error)
    {
        var msg = $"Error at line: {line}: {error}";
        terminalOutput.Println(msg);
    }

    void OnInstructionChanged(int idx, string newInstruction){
        var (instruction, error) = instructionParser.Parse(newInstruction);
        if(error != null)
        {
            PrintErrorMessage(idx, error.ToString());
        }
        else
        {
            instruction.editable = true;
            program.instructions[idx] = instruction;

            StartCoroutine(ReloadEditorCoroutine());
        }

        IEnumerator ReloadEditorCoroutine()
        {
            yield return null;
            Clear();
            FillEditor();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
