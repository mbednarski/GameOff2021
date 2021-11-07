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
    RectTransform arrowTransform;


    [SerializeField] float instructionOffset = 1f;

    private List<UnityEvent<int, string>> eventsToDispose = new List<UnityEvent<int,string>>();

    [SerializeField] GameObject listingParent;

    [SerializeField] Vector3 basePos = new Vector3(0,200,0);
    Vector3 arrowBasePos = new Vector3(-170,200,0);
    Vector3 offset;

    bool reloading = false;

    void Awake()
    {
        offset = new Vector3(0,-instructionOffset,0);
        Debug.Assert(listingParent != null);
        ipu = ipuGameObject.GetComponent<IntelligenceProcesingUnit>();
        ipu.onStepExecuted += IpuStepExecuted;  
        ipu.onStateChanged += IpuStateChanged;        

        arrowTransform = instructionArrow.GetComponent<RectTransform>();
        Debug.Assert(arrowTransform != null);

    }
    
    void Start()
    {
        
        Clear();
        FillEditor();   
        SetArrowPosition(0);
    }

    void IpuStateChanged(IntelligenceProcesingUnit.State state)
    {
        switch (state)
        {
            case IntelligenceProcesingUnit.State.IDLE:
                SetArrowVisibility(false);
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

            Debug.Log(line.GetComponent<RectTransform>().localPosition);
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


    void OnInstructionChanged(int idx, string newInstruction){
        var parsed = GenericInstruction.Parse(newInstruction);
        program.instructions[idx] = parsed;
        Debug.Log(program.instructions);
        Clear();
        FillEditor();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
