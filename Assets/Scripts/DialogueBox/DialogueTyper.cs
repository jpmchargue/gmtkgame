using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTyper : MonoBehaviour
{

    public GameObject dialogueBox;
    public Animator portraitAnimator;
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public SetPortraitImage portraitImage;
    public Texture myTexture;

    public void TypeDialogue(List<(string, Texture)> dialogue)
    {
        portraitAnimator.Play("PortraitAppear");
        
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine(dialogue));
    }

    public IEnumerator TypeLine(List<(string, Texture)> dialogue)
    {
        yield return new WaitForSeconds(0.5f);
        portraitAnimator.enabled = false;
        foreach (var line in dialogue)
        {
            
            portraitImage.setPortraitImage(line.Item2);



            Debug.Log(line);
            textComponent.text = "";
            foreach (char c in line.Item1.ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            // Wait for key press before proceeding to the next line
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0));
        }
        portraitAnimator.enabled = true;
        portraitAnimator.Play("PortraitDisappear");
    }
}
