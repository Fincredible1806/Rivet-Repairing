using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class PlayerScoreManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI multText;
    public GameObject highScoreNotif;
    public PlayerHealth health;
    [Header("Variables")]
    [SerializeField] float currentScore;
    [SerializeField] float scoreMult = 1.0f;
    [SerializeField] float timeToIncreaseMult = 300f;
    [SerializeField] float timePassedSinceLastMult = 0f;
    private bool aboveHighScore = false;

    private void Start()
    {
        // Setting multiplier time to zero (just to be sure, setting mult text and highscore text (via playerprefs) 
        timePassedSinceLastMult = 0;
        multText.text = ("Current Multiplier: X" + scoreMult.ToString("F1"));
        string str = PlayerPrefs.GetFloat("HighScore", 0.1f).ToString("F0");
        highScoreText.text = "High Score \n " + str;
    }
    // Update is called once per frame
    void Update()
    {
        timePassedSinceLastMult += Time.deltaTime;
        if(!health.dead)
        {
            // If alive, add score per tick and check if the multiplier needs to increase
            ScoreTicker();
            ScoreMultiplierChecker();
            if(aboveHighScore)
            {
                //If player's score is above the highscore, update the value 
                highScoreText.text = ("High Score \n" + currentScore.ToString("F0"));
                UpdateHighScore();

            }
        }
        if(health.dead)
        {
            //IF the player is dead, make "Current" in score and multiplier say final, and if the high score
            // Is beaten, update the playerprefs with the new high score and save it
            currentScoreText.text = ("Final Score \n" + currentScore.ToString("F0"));
            multText.text = ("Final multiplier \n" + scoreMult.ToString("F1"));
            if (aboveHighScore)
            {
                UpdateHighScore();
                PlayerPrefs.Save();
            }
        }    
        if(currentScore >= PlayerPrefs.GetFloat("HighScore"))
        {
            aboveHighScore = true;
        }
    }

    private void ScoreTicker()
    {
        // Add score via deltatime, multiplied by the score multiplier
        currentScore += Time.deltaTime * scoreMult;
        currentScoreText.text = ("Current Score: \n" + currentScore.ToString("F0"));
    }

    public void AddScore(float addedScore)
    {
        // Public method that other methods can access, to add score as long as the player is alive
        if(!health.dead)
        currentScore += addedScore * scoreMult;
    }

    private void ScoreMultiplierChecker()
    {
        // checks if enough time has passed to increase the multiplier, and multiplies it by 2
        if(timePassedSinceLastMult >= timeToIncreaseMult)
        {
            scoreMult *= 1.2f;
            timePassedSinceLastMult = 0;
            multText.text = ("Current Multiplier: X" + scoreMult.ToString("F1"));
        }
    }

    private void UpdateHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", currentScore);
    }
}
