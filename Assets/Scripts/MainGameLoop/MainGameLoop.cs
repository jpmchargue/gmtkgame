using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MainGameLoop : MonoBehaviour
{
    public GameObject inGameMenu;
    public DialogueHolder villainDialogue;
    public DialogueTyper dialogueTyper;
    public PieceSpawnerScript pieceSpawnerScript;
    public GameObject nextStageManager;

    private bool alreadyResetFlag1 = false;
    public bool alreadyResetFlag2 = false;

    public Animator blackScreenAnimator;
    public Animator vinetteAnimator;
    public Animator fireScreenAnimator;
    public Animator cameraDropAnimator;

    public Animator scoreAnimator;
    public GameObject endGameScreenObject;


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
            (villainDialogueMap["INTRO4"].Item1, villainDialogueMap["INTRO4"].Item2),
            (villainDialogueMap["INTRO5"].Item1, villainDialogueMap["INTRO5"].Item2),
            (villainDialogueMap["INTRO6"].Item1, villainDialogueMap["INTRO6"].Item2),
            (villainDialogueMap["INTRO7"].Item1, villainDialogueMap["INTRO7"].Item2),
            (villainDialogueMap["INTRO8"].Item1, villainDialogueMap["INTRO8"].Item2)},
            "INTRO");
    }


    public void InitiateReset()
    {
        if(alreadyResetFlag1 == true){
            pieceSpawnerScript.isActive = false;
            Instantiate(nextStageManager, Vector3.zero, Quaternion.identity);
            return;
        }

        
        Debug.Log("Initiating Reset");
        pieceSpawnerScript.isActive = false;
        dialogueTyper.TypeDialogue(new List<(string, Texture)> {
            (villainDialogueMap["RESET1"].Item1, villainDialogueMap["RESET1"].Item2),
            (villainDialogueMap["RESET1.5"].Item1, villainDialogueMap["RESET1.5"].Item2),
            (villainDialogueMap["RESET2"].Item1, villainDialogueMap["RESET2"].Item2)},
            "RESET");


        alreadyResetFlag1 = true;
    }
    public void InitiateResetOutro()
    {
        if(alreadyResetFlag2 == true){
            pieceSpawnerScript.isActive = true;
            return;
        }
        dialogueTyper.TypeDialogue(new List<(string, Texture)> {
            (villainDialogueMap["RESETOUTRO1"].Item1, villainDialogueMap["RESETOUTRO1"].Item2),
            (villainDialogueMap["RESETOUTRO2"].Item1, villainDialogueMap["RESETOUTRO2"].Item2)},
           "RESETOUTRO");

        alreadyResetFlag2 = true;
    }

    public void InitiateGameFailure(){
        endGameScreenObject.SetActive(true);
        pieceSpawnerScript.isActive = false;
        dialogueTyper.TypeDialogue(new List<(string, Texture)> {
            (villainDialogueMap["FAILURE1"].Item1, villainDialogueMap["FAILURE1"].Item2),
            (villainDialogueMap["FAILURE2"].Item1, villainDialogueMap["FAILURE2"].Item2),
            (villainDialogueMap["FAILURE3"].Item1, villainDialogueMap["FAILURE3"].Item2)},
           "ENDGAME");
    }
    void playEndGameAnimations(){
        blackScreenAnimator.Play("BlackScreenPanel2Animation");
        fireScreenAnimator.Play("EndGameFirePanelAnimation");
        vinetteAnimator.Play("EndGameBlackPanelAnimation");
        cameraDropAnimator.Play("GameEndCameraAnimation");
    }


    public void DialogueComplete(String dialogueType)
    {
        if(dialogueType == "INTRO" || dialogueType == "RESETOUTRO")
        {
            pieceSpawnerScript.isActive = true;

        }
        else if (dialogueType == "RESET")
        {
            Instantiate(nextStageManager, Vector3.zero, Quaternion.identity);

        }
        else if (dialogueType == "ENDGAME")
        {
            playEndGameAnimations();
            StartCoroutine(waitAndReloadScene(2f));
        }

        if(dialogueType == "INTRO"){
            scoreAnimator.Play("ScoreAppear");
        }
    }

    IEnumerator waitAndRenableSpawner(float waitTime){
        yield return new WaitForSeconds(waitTime);
        pieceSpawnerScript.isActive = true;
    }

    IEnumerator waitAndReloadScene(float waitTime){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
