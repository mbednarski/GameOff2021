using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionPrefabScript : MonoBehaviour
{
    [SerializeField] TMP_InputField tmpInputField;
    void Awake()
    {
        tmpInputField = GetComponentInChildren<TMP_InputField>();
        Debug.Log(tmpInputField);
    }

    public void SetLineText(string line)
    {
        tmpInputField.text = line;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
