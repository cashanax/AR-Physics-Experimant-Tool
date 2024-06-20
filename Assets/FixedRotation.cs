using Unity.VisualScripting;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    private Vector3 globalRotation;
    public TextArrayController textArrayController;

    public CalculateVelocity calculateVelocity;
    [SerializeField] public PendulumScript pendulumScript;

    public GameObject xArrow;
    public GameObject yArrow;
    public GameObject zArrow;

    public int multipler;
    
    Transform xArrowTransform;
    Transform yArrowTransform;
    Transform zArrowTransform;
    void Start()
    {
        // Store the initial global rotation of the object
        
        globalRotation = transform.eulerAngles;
        
        
        xArrowTransform = xArrow.transform;
        yArrowTransform = yArrow.transform;
        zArrowTransform = zArrow.transform;      
    }

    void Update()
    {
        transform.eulerAngles = globalRotation;

        if (calculateVelocity != null && xArrowTransform != null && yArrowTransform != null && zArrowTransform != null && pendulumScript != null)
        {
            UpdateArrow(xArrow, xArrowTransform, pendulumScript.CalculateAverageCenter()); //optymize
            UpdateArrow(yArrow, yArrowTransform, pendulumScript.getTensionForce());
            UpdateArrow(zArrow, zArrowTransform, pendulumScript.getGravityForce());
        }
    }


    void UpdateArrow(GameObject arrow, Transform arrowTransform, float force)
    {
        MeshRenderer ArrowRenderer = arrow.GetComponent<MeshRenderer>();

        if (Mathf.Abs(force) > 0.01f)
        {
            ArrowRenderer.enabled = true;

            // Get the angle from PendulumScript
            float angle = pendulumScript.getAngle(); // Replace with the actual method to get the angle

            // Set the rotation based on the arrow type
            if (arrow == xArrow)
            {
                // Rotate xArrow by -90 degrees on the Z-axis
                arrowTransform.localRotation = Quaternion.Euler(0f, 0f, angle - 90f);
            }
            else if (arrow == yArrow)
            {
                // Rotate yArrow based on the angle
                arrowTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
            }
            else if (arrow == zArrow)
            {
                // Prevent zArrow from any rotation
                // arrowTransform.localRotation = Quaternion.identity;
            }

            // Set the arrow length based on the force
            Vector3 scale = new Vector3(0.5f, 2f * force, 0.5f);
            arrowTransform.localScale = scale;

            arrow.SetActive(true);
        }
        else
        {
            ArrowRenderer.enabled = false;

            // Reset rotation when force is zero
            arrowTransform.localRotation = Quaternion.identity;

            // Set the scale to zero when force is zero
            Vector3 scale = Vector3.zero;
            arrowTransform.localScale = scale;

            arrow.SetActive(false);
        }
    }      
}
