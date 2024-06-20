using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PendulumScript : MonoBehaviour
{
    public GameObject modelTarget;
    public GameObject xArrow;
    public GameObject yArrow; 
    public GameObject zArrow; 

    public TextArrayController textArrayController;

    // Constants
    private float mass = 0.058f; // typical tennis ball mass
    private float gravity = 9.80665f;
    private double angleRad;
    private double angleDeg = 0;
    private float sinus;
    private float length = 0.77f;
    private double period;
    private double frequency;

    private string lengthString;
    [SerializeField] TMP_InputField lengthInputField;
    private string massString;
    [SerializeField] TMP_InputField massInputField;
    
    // Forces
    private double gravityForce;
    private double tensionForce;
    private double centerForce;

    private Queue<float> centerQueueue = new Queue<float>(); // FIFO queue for X-axis speed

    void UpdateCenterQueueue(double centerForce){
        centerQueueue.Enqueue((float)centerForce);
        while (centerQueueue.Count > 6)
        {
            centerQueueue.Dequeue();
        }
    }
    public float CalculateAverageCenter()  
    {
        float sum = 0f;

        foreach (float value in centerQueueue)
        {
            sum += value;
        }

        return centerQueueue.Count > 0 ? sum / centerQueueue.Count : 0f;
    }

    public void CalculatePendulum(Vector3 movement) {
        // Angle
        sinus = movement.x / length;
        angleRad = Math.Asin(sinus);
        angleDeg = angleRad * (180 / Math.PI);

        //Gravity Force
        gravityForce = gravity * mass;

        // Tension Force
        tensionForce = mass * gravity * Math.Cos(angleRad);

        // Center Force
        centerForce = -(mass * gravity * sinus);

        //Period and Freq
        period = 2 * Math.PI * Math.Sqrt(length / gravity);
        frequency = 1 / period;
        UpdateCenterQueueue(centerForce);
        //textArrayController.UpdatePendulumForces(tensionForce, CalculateAverageCenter(), angleDeg);
        textArrayController.DisplayForcesData(tensionForce, centerForce, gravityForce);
        textArrayController.textArray[3] = $"\nAngle {angleDeg:F2}";
    }

    public float getGravityForce()
    {
        return (float)this.gravityForce;
    }

    public float getCenterForce()
    {
        return (float)this.centerForce;
    }

    public float getTensionForce()
    {
        return (float)this.tensionForce;
    }

    public float getAngle()
    {
        return (float)this.angleDeg;
    }

    public float getMass()
    {
        return this.mass;
    }

    public void OnLengthInputChange()
    {
        lengthString = lengthInputField.text;
        length = float.Parse(lengthString);
    }

    public void OnMassInputChange()
    {
        massString = massInputField.text;
        mass = float.Parse(massString);
    }
}
