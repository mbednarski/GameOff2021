using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeEditor : MonoBehaviour
{
    [SerializeField] AssemblyProgram program;
    [SerializeField] GameObject instructionPrefab;

    [SerializeField] float instructionOffset = 1f;

    void Start()
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
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
