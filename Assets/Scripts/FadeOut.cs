using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup group;
    public bool fadeOut = false;
    public float timeToFade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOut == true) {
            if (group.alpha < 1) {
                group.alpha += timeToFade * Time.deltaTime;
            }
            else fadeOut = false;

        }
        
    }

    public void Fade()
    {
        fadeOut = true;
    }
}
