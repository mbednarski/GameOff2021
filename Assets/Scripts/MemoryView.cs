using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoryView : MonoBehaviour
{
    [SerializeField] GameObject ipuGameObject;
    IntelligenceProcesingUnit ipu;

    TextMeshProUGUI regAX, regBX, regCX, regDX, regIP;

    Dictionary<string, TextMeshProUGUI> registers;

    void Awake()
    {
        registers = new Dictionary<string, TextMeshProUGUI>();
    }

    void Start()
    {
        ipu = ipuGameObject.GetComponent<IntelligenceProcesingUnit>();
        ipu.onStepExecuted += IpuStepExecuted;

        registers["AX"] = transform.Find("AXText").gameObject.GetComponent<TextMeshProUGUI>();
        registers["BX"] = transform.Find("BXText").gameObject.GetComponent<TextMeshProUGUI>();
        registers["CX"] = transform.Find("CXText").gameObject.GetComponent<TextMeshProUGUI>();
        registers["DX"] = transform.Find("DXText").gameObject.GetComponent<TextMeshProUGUI>();
        registers["IP"] = transform.Find("IPText").gameObject.GetComponent<TextMeshProUGUI>();

        RefreshRegisterValues();
    }

    void IpuStepExecuted()
    {
        RefreshRegisterValues();
    }

    private void RefreshRegisterValues()
    {
        foreach (var registerName in registers.Keys)
        {
            registers[registerName].text = ipu.registers[registerName].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
