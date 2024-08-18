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

    void InitiateTutorial()
    {
        dialogueTyper.TypeDialogue(new List<(string, Texture)> { (villainDialogueMap["INTRO2"].Item1, villainDialogueMap["INTRO2"].Item2), (villainDialogueMap["INTRO1"].Item1, villainDialogueMap["INTRO1"].Item2) , (villainDialogueMap["INTRO3"].Item1, villainDialogueMap["INTRO3"].Item2) });
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
