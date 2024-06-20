using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextArrayController : MonoBehaviour
{
    
    [SerializeField] private TMP_Text textField;

    [SerializeField] private TMP_Text fnTMP;
    [SerializeField] private TMP_Text fwTMP;

    [SerializeField] private TMP_Text fgTMP;

    [SerializeField] Toggle toggleLog;

    public string[] textArray;
    [SerializeField] GameObject graph;


    private int currentIndex = 3;

    // Start is called before the first frame update
    void Start()
    {
        ObjectMovementTracker objectMovementTracker = GetComponent<ObjectMovementTracker>();

        // Initialize the text field with the first element of the array
        if (textField != null && textArray.Length > 0)
        {
            textField.text = textArray[currentIndex];
        }
        Array.Resize(ref textArray, textArray.Length + 4);

    }

    void Update(){

        var logText = string.Empty;
        if (toggleLog.isOn){
        foreach (var item in textArray){
            logText += item;
        }
        graph.SetActive(true);
        }
        else graph.SetActive(false);
        textField.text = logText;

    }

    public void DisplayForcesData(double fn, double fw, double fg ){
        if(toggleLog.isOn){
            fnTMP.text = $"Fn = {fn:F3}N";
            fwTMP.text = $"Fw = {fw:F3}N";
            fgTMP.text = $"Fg = {fg:F3}N";
            
        }
        else {
            fnTMP.text = string.Empty;
            fwTMP.text = string.Empty;
            fgTMP.text = string.Empty;
        }
    
    }
}
