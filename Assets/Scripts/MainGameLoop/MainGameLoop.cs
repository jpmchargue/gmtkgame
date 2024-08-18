using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainGameLoop : MonoBehaviour
{
    public GameObject inGameMenu;
    public DialogueHolder villainDialogue;
    public DialogueTyper dialogueTyper;
    public PieceSpawnerScript pieceSpawnerScript;
    public GameObject nextStageManager;


    private Dictionary<string, (string, Texture)> villainDialogueMap;

    void Setup() {
        inGameMenu.SetActive(true);
        villainDialogueMap = villainDialogue.getDialogueMap();
    }
    void Start()
    {
        Setup();
        InitiateTutorial();
    }

    public void InitiateTutorial()
    {
        dialogueTyper.TypeDialogue(new List<(string, Texture)> { 
            (villainDialogueMap["INTRO1"].Item1, villainDialogueMap["INTRO1"].Item2), 
            (villainDialogueMap["INTRO2"].Item1, villainDialogueMap["INTRO2"].Item2), 
            (villainDialogueMap["INTRO3"].Item1, villainDialogueMap["INTRO3"].Item2),
            (villainDialogueMap["INTRO4"].Item1, villainDialogueMap["INTRO4"].Item2)},
            "INTRO");
    }


    public void InitiateReset()
    {
        Debug.Log("Initiating Reset");
        pieceSpawnerScript.isActive = false;
        dialogueTyper.TypeDialogue(new List<(string, Texture)> {
            (villainDialogueMap["RESET1"].Item1, villainDialogueMap["RESET1"].Item2),
            (villainDialogueMap["RESET2"].Item1, villainDialogueMap["RESET2"].Item2),
            (villainDialogueMap["RESET3"].Item1, villainDialogueMap["RESET3"].Item2),
            (villainDialogueMap["RESET4"].Item1, villainDialogueMap["RESET4"].Item2)},
            "RESET");
    }

    public void DialogueComplete(String dialogueType)
    {
        if(dialogueType == "INTRO")
        {
            pieceSpawnerScript.isActive = true;
        }
        else if (dialogueType == "RESET")
        {
            Debug.Log("This is a warning message");
            Instantiate(nextStageManager, Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
