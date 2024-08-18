using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class DialogueLine
{
    public string dialogueKey;
    public string text;
    public Texture portrait;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue")]
public class DialogueHolder : ScriptableObject
{
    public DialogueLine[] lines;

    public Dictionary<string, (string, Texture)> getDialogueMap()
    {
        var dialogueMap = new Dictionary<string, (string, Texture)>();

        foreach(DialogueLine line in lines)
        {
            dialogueMap.Add(line.dialogueKey, (line.text, line.portrait));
        }

        return dialogueMap;
    }


}