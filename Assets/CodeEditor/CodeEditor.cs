using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeEditor : MonoBehaviour
{
    [SerializeField] AssemblyProgram program;
    [SerializeField] GameObject instructionPrefab;

    [SerializeField] float instructionOffset = 1f;

    private List<UnityEvent<int, string>> eventsToDispose = new List<UnityEvent<int,string>>();


    bool reloading = false;

    void Clear()
    {
        foreach (var item in eventsToDispose)
        {
            item.RemoveAllListeners();
        }
        eventsToDispose.Clear();

        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    void FillEditor()
    {
        Vector3 basePos = new Vector3(-250,200,0);
        Vector3 offset = new Vector3(0,-instructionOffset,0);

        program.EnsureProgramIsLoaded();
        for (int i = 0; i < program.instructions.Count; i++)
        {
            var line = Instantiate(instructionPrefab);
            line.transform.SetParent(gameObject.transform);
            line.transform.localScale = Vector3.one;

            Debug.Log(line.GetComponent<RectTransform>().localPosition);
            line.GetComponent<RectTransform>().localPosition = basePos + i * offset;

            var x = line.GetComponent<InstructionPrefabScript>();
            x.SetLineText(program.instructions[i].ToString());
            x.SetEditable(program.instructions[i].editable);
            x.InstructionNumber = i;
            x.onInstructionChanged.AddListener(OnInstructionChanged);
            eventsToDispose.Add(x.onInstructionChanged);
        }
    }

    void Start()
    {
        Clear();
        FillEditor();   
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
