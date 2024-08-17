using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string dialogueKey;
    public string text;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue")]
public class DialogueHolder : ScriptableObject
{
    public DialogueLine[] lines;


    public Dictionary<string, string> getDialogueMap()
    {
        var dialogueMap = new Dictionary<string, string>();

        foreach(DialogueLine line in lines)
        {
            dialogueMap.Add(line.dialogueKey, line.text);
        }

        return dialogueMap;
    }


}