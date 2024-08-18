using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;

    private void Start()
    {
        
    }

    public void ShowUI()
    {
        scoreText.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        scoreText.gameObject.SetActive(false);
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
    }
}
