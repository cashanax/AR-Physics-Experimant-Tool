using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.UI;

public class RecordButtons : MonoBehaviour
{
    [SerializeField]
    public Toggle toggle;
    public CalculateVelocity calculateVelocity;
    public ObjectMovementTracker objectMovementTracker;
    public TextArrayController textArrayController;
    public bool isRecording;
    string filePath;
    string fileName;
    Color green = new Color(0f, 1.0f, 0f);
    Color red = new Color(1f, 0f, 0f);
    void Start(){
        DateTime currentDateTime = DateTime.Now; // Retrieves the current date and time
        fileName = currentDateTime + " Pendulum.txt";
        filePath = Path.Combine(Application.persistentDataPath, fileName);
    }
    
     void SaveVelocityDataToFile()
    {

        // Prepare the data to be saved
        string velocityData = $"{Time.time};{objectMovementTracker.calibPosition.x:0.0000};{calculateVelocity.acceleration}\n"; // Change this line according to your desired data format
        // string velocityData = $"{Time.time}: positions: {objectMovementTracker.calibPosition.x:0.0000}; {objectMovementTracker.calibPosition.y:0.0000}; {objectMovementTracker.calibPosition.z:0.0000};"+ 
        //     $" velocities: {calculateVelocity.velocities.x:0.0000}; {calculateVelocity.velocities.y:0.0000}; {calculateVelocity.velocities.z:0.0000};\n"; // Change this line according to your desired data format
        //textArrayController.AddStringToArray(velocityData);
        // Append the data to the file or create a new file if it doesn't exist
        File.AppendAllText(filePath, velocityData);
    }
     public void ToggleRecording(){
        if(toggle.isOn){
            InvokeRepeating("SaveVelocityDataToFile", 0.1f, 0.04f);
            
        }
        else{
            CancelInvoke();
        }

    }

    public void ShowAxies(){
        if(toggle != null){
            if(toggle.isOn){
                //button.image.color = green;
            }
            else{
                //button.image.color = red;
            }
        }
    }
}

