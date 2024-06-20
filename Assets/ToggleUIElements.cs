using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUIElements : MonoBehaviour
{
    public GameObject[] elementsToToggle; // Add UI elements here in the Inspector
    private bool areElementsHidden = false;
    private Color originalColor; // Store the original color of the button

    private TextMeshProUGUI  buttonText; // Reference to the Text component of the button

    void Start()
    {
        // Ensure the initial state is as expected
        //ToggleElements();
    }

    void Awake()
    {
        // Get the Text component of the button
        buttonText = GetComponentInChildren<TextMeshProUGUI >();
        UpdateButtonText();
    }

    public void ToggleElements()
    {
        areElementsHidden = !areElementsHidden;

        // Ensure we have a Graphic component on the button
        Graphic buttonGraphic = GetComponent<Graphic>();
        if (buttonGraphic != null)
        {
            // If it's the first time, store the original color
            if (originalColor == Color.clear)
            {
                originalColor = buttonGraphic.color;
            }

            // Get the original color and set the alpha component to 50%
            Color newColor = originalColor;
            newColor.a = areElementsHidden ? 0.5f : 1f;

            // Set the new color with adjusted alpha
            buttonGraphic.color = newColor;
        }

        foreach (GameObject element in elementsToToggle)
        {
            if (element != null && element != gameObject)
            {
                // Optionally, you can also toggle the gameObject's active state
                element.SetActive(!areElementsHidden);
            }
        }

        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        // Set the button text based on whether elements are hidden or shown
        if (buttonText != null)
        {
            buttonText.text = areElementsHidden ? "Show" : "Hide";
        }
    }
}
