using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuRemover : MonoBehaviour
{
    public Animator logoAnimator;
    public Animator startButtonAnimator;
    public GameObject startMenu;

    void Start(){
        StartCoroutine(waitAndDisableStartMenu(3f));
        logoAnimator.Play("LogoRemoval");
        startButtonAnimator.Play("StartButtonRemoval");
        
    }
    public void playLogoAnimation(){
        
    }
    IEnumerator waitAndDisableStartMenu(float waitTime){
        yield return new WaitForSeconds(waitTime);
        startMenu.SetActive(false);
    }
}
