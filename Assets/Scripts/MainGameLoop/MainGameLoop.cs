using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameLoop : MonoBehaviour
{
    public GameObject inGameMenu;
    public DialogueHolder villainDialogue;
    public DialogueTyper dialogueTyper;

    private Dictionary<string, string> villainDialogueMap;

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
        dialogueTyper.TypeDialogue(new string[] { villainDialogueMap["INTRO2"], villainDialogueMap["INTRO1"] });
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
