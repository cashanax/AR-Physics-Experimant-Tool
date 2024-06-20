using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class CalculateVelocity : MonoBehaviour
{
    // private PendulumScript pendulumScript;
    public GameObject modelTarget;
    public TextArrayController textArrayController;
    public RecordButtons recordButtons;

    // Arrow code
    public GameObject xArrow;
    public GameObject yArrow; 
    public GameObject zArrow; 

    private Queue<float> xSpeedQueue = new Queue<float>(); // FIFO queue for X-axis speed
    private Queue<float> ySpeedQueue = new Queue<float>(); // FIFO queue for y-axis speed
    private Queue<float> zSpeedQueue = new Queue<float>(); // FIFO queue for Z-axis speed

    //public float xVelocity = 0;
    //public float yVelocity = 0;
    //public float zVelocity = 0;
    public Vector3 velocities;
    public float acceleration;

    void Start() {
        
    }

    void Update(){
    
    }

    public void DisplacementToVelocity(Vector3 movement){
              
        UpdateSpeedQueues(movement.x / Time.deltaTime,
                            movement.y / Time.deltaTime,
                            movement.z / Time.deltaTime);
    
        velocities.x = CalculateAverage(xSpeedQueue);
        velocities.y = CalculateAverage(ySpeedQueue);
        velocities.z = CalculateAverage(zSpeedQueue);
        Vector3 totalVelocity = new Vector3(velocities.x, velocities.y, velocities.z);

        acceleration = CalculateAverage(xSpeedQueue) / Time.deltaTime;
        // float centerForce = acceleration * pendulumScript.getMass();
        
        textArrayController.textArray[2] = $"Velocity [m/s] x:{velocities.x:F2} y:{velocities.y:F2} z:{velocities.z:F2} acceleration:{acceleration:F3}"; 
    }

    void UpdateSpeedQueues(float xSpeed, float ySpeed, float zSpeed)
    {
        // Enqueue the latest speed inputs
        xSpeedQueue.Enqueue(xSpeed);
        ySpeedQueue.Enqueue(ySpeed);
        zSpeedQueue.Enqueue(zSpeed);

        // Maintain the queue size to 4 (remove oldest elements if more than 4)
        while (xSpeedQueue.Count > 4)
        {
            xSpeedQueue.Dequeue();
        }
        while (ySpeedQueue.Count > 4)
        {
            ySpeedQueue.Dequeue();
        }
        while (zSpeedQueue.Count > 4)
        {
            zSpeedQueue.Dequeue();
        }
    }
    float CalculateAverage(Queue<float> queue)  
    {
        float sum = 0f;

        foreach (float value in queue)
        {
            sum += value;
        }

        return queue.Count > 0 ? sum / queue.Count : 0f;
    }
}
