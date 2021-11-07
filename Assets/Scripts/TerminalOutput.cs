using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalOutput : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI terminalText;

    private string buffer;

    private void Start() {
        terminalText.text= "";
    }

    public void Println(string text)
    {
        buffer = buffer + $"<color=\"red\"> {text} </color> \n";
        terminalText.text = buffer;
        
    }
}
