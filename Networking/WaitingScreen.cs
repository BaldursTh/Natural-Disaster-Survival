using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingScreen : MonoBehaviour
{
    public string code;
    [SerializeField] public TMP_Text codeText;

    public void UpdateWaitingScreen()
    {
        codeText.text = code;
    }
}
