using UnityEngine.UI;
using UnityEngine;
   

public class DrawAxes : MonoBehaviour
{
    //public GameObject linePrefab; // The prefab for the line

    public GameObject orient;
    Vector3 centerPosition;
    public Toggle toggle;

    void Start()
    {
        centerPosition = Vector3.zero;
        orient.transform.position = centerPosition;
        orient.SetActive(false);

        
    }

    public void ToggleAxes(){
        if (toggle.isOn == true){
             orient.SetActive(true);
        }
        else if(toggle.isOn == false){
            orient.SetActive(false);
        }
    }
    
    void Update()
    {
        if (toggle.isOn == true){
        centerPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.5f ; // Center point one meter away from camera
        //centerPosition.y -= 0.1f;
        orient.transform.position = centerPosition;
        }

    }

    /* public void ResetOrientation()
    {
        // Get the current object rotation
        Quaternion currentRotation = transform.rotation;

        // Calculate the rotation needed to align the object's X-axis with the screen
        Vector3 screenNormal = Camera.main.transform.forward; // Assuming main camera is used
        Vector3 newForward = Vector3.ProjectOnPlane(screenNormal, Vector3.up).normalized;
        Quaternion newRotation = Quaternion.LookRotation(newForward, Vector3.up);

        // Apply the new rotation
        transform.rotation = newRotation;
    }
    */

}


