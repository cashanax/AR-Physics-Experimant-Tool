using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    public FixedRotation fixedRotation;

    public GameObject forces;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) => {
            sliderText.text = "Scale: "+v.ToString("0.00");
            //fixedRotation.multipler = (int)v;

            //Vector3 scale; = forces.transform.localScale;
            Vector3 scale = new Vector3(v,v,v);
            forces.transform.localScale = scale;
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
