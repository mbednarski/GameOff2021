using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class InstructionPrefabScript : MonoBehaviour
{
    [SerializeField] TMP_InputField tmpInputField;

    public UnityEvent<int, string> onInstructionChanged;

    public int InstructionNumber { get; set; }
    void Awake()
    {
        tmpInputField = GetComponentInChildren<TMP_InputField>();
        tmpInputField.onEndEdit.AddListener(ValueChanged);
    }

    void ValueChanged(string val)
    {
        if(onInstructionChanged != null){
            onInstructionChanged.Invoke(InstructionNumber, val);
        }
    }

    public void SetLineText(string line)
    {
        tmpInputField.text = line;
    }
    public void SetEditable(bool editable)
    {
        tmpInputField.enabled = editable;
    }

    private void OnDestroy() {
        onInstructionChanged.RemoveAllListeners();
    }

    void Update()
    {
        
    }
}
