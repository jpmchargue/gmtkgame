using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInDetector : MonoBehaviour
{

    private static bool isSceneLoaded = false;
    public Animator fadeInScreenAnimator;
    void Start()
    {
        if (isSceneLoaded)
        {
            Debug.Log("This is not the first time the scene is being loaded.");
            fadeInScreenAnimator.Play("FadeInAnimation");
        }
        else
        {
            Debug.Log("This is the first time the scene is being loaded.");
    
            isSceneLoaded = true;
        }
    }

    void Update()
    {
        
    }
}
