using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTyper : MonoBehaviour
{

    public GameObject dialogueBox;
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    public void TypeDialogue(string[] dialogue)
    {
        dialogueBox.SetActive(true);
        textComponent.text = string.Empty;
      
        StartCoroutine(TypeLine(dialogue));
    }

    public IEnumerator TypeLine(string[] dialogue)
    {
        foreach(var line in dialogue)
        {
            textComponent.text = "";
            foreach (char c in line.ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            // Wait for key press before proceeding to the next line
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0));
        }
        dialogueBox.SetActive(false);
    }
}
