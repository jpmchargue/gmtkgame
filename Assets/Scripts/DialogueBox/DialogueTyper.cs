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
    public MainGameLoop mainGameLoop;
    public AudioClip soundDialogueAppear;
    public AudioClip soundDialogueDisappear;
    private Vector3 cameraLocation = new Vector3(-16.55f, 23.37f, -9f);

    public void TypeDialogue(List<(string, Texture)> dialogue, String dialogueType)
    {
        AudioSource.PlayClipAtPoint(soundDialogueAppear, cameraLocation, 0.5f);
        Debug.Log("APPEARING");
        portraitAnimator.Play("PortraitAppear");
        
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine(dialogue, dialogueType));
    }

    public IEnumerator TypeLine(List<(string, Texture)> dialogue, string dialogueType)
    {
        yield return new WaitForSeconds(0.5f);
        portraitAnimator.enabled = false;
        

        foreach (var line in dialogue)
        {
            portraitImage.setPortraitImage(line.Item2);

            Debug.Log(line);
            textComponent.text = "";
            int fastTextCount = 0; // Counter for fast characters

            foreach (char c in line.Item1.ToCharArray())
            {
                textComponent.text += c;

                float currentTextSpeed = textSpeed;
                if (fastTextCount > 0)
                {
                    currentTextSpeed = textSpeed / 20;
                    fastTextCount--;
                }

                float elapsedTime = 0f;
                while (elapsedTime < currentTextSpeed)
                {
                    if (Input.GetMouseButtonDown(0))
                    { 
                        fastTextCount = 20;
                        break;
                    }
                    else
                    {
                        yield return null; // Wait for the next frame
                        elapsedTime += Time.deltaTime;
                    }
                }

                if (elapsedTime < currentTextSpeed)
                {
                    yield return new WaitForSeconds(currentTextSpeed - elapsedTime);
                }
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0));
        }

        portraitAnimator.enabled = true;
        AudioSource.PlayClipAtPoint(soundDialogueDisappear, cameraLocation, 0.5f);
        Debug.Log("DISAPPEARING");
        portraitAnimator.Play("PortraitDisappear");
        mainGameLoop.DialogueComplete(dialogueType);
    }
}
