using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;
using System.IO;
using UnityEditor;
using System.Linq;


public class ObjectMovementTracker : MonoBehaviour
{
    public GameObject modelTarget; // Assign your model target object here

    public CalculateVelocity calculateVelocity;
    public PendulumScript pendulumScript;

    public UnityEvent onTrackingFoundEvent;

    public TextArrayController textArrayController;
    public Vector3 currentPosition;
    public Vector3 calibPosition;
    Vector3 movement;
    private enum Direction
    {
        Right,
        Left,
        Forward,
        Backward
    }

    private Dictionary<Direction, GameObject> activeArrows = new Dictionary<Direction, GameObject>();


    private Vector3 previousPosition;

    private bool isTracking = false;
    


     public void OnTrackingFound()
    {
        isTracking = true;
        //textArrayController.AddStringToArray("\nTracking started.");
        //Debug.Log("Tracking started.");
          
    }

    public void OnTrackingLost()
    {
        isTracking = false;
    
        //textArrayController.AddStringToArray("\nTracking lost.");
    }

    void Start()
    {
        //eventDispatcher = GetComponent<TrackingEventDispatcher>();
        previousPosition = modelTarget.transform.position;
        calibPosition = previousPosition;
    }

    void Update()
    {
         if (isTracking)
        {
            // Calculate movement
            currentPosition = modelTarget.transform.position;
            movement = currentPosition - previousPosition;
            previousPosition = currentPosition;

            calibPosition.x += movement.x;
            calibPosition.y += movement.y;
            calibPosition.z += movement.z;
            // If you need to calculate movement relative to the ground plane, you can adjust here
            // For example, ignore the Y-axis if you only care about horizontal movement
            //movement.z = 0;
            calculateVelocity.DisplacementToVelocity(movement);
            pendulumScript.CalculatePendulum(calibPosition); //
            //textArrayController.UpdatePositionString(currentPosition.x, currentPosition.y, currentPosition.z);
            //textArrayController.UpdateAngleString(currentPosition.x);
            //textArrayController.UpdateCustomPositionString(calibPosition.x, calibPosition.y, calibPosition.z);
            textArrayController.textArray[0] = $"Global position x:{currentPosition.x:F2} y:{currentPosition.y:F2} z:{currentPosition.z:F2} ";
            textArrayController.textArray[1] = $"Calibrated position x:{calibPosition.x:F2} y:{calibPosition.y:F2} z:{calibPosition.z:F2} ";

        }
    }
    public void ResetX()
    {
        calibPosition.x = 0;
        calibPosition.y = 0;
        calibPosition.z = 0;
    }
}