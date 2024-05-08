using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerScoreManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI multText;
    public PlayerHealth health;
    [Header("Variables")]
    [SerializeField] float currentScore;
    [SerializeField] float scoreMult = 1.0f;
    [SerializeField] float timeToIncreaseMult = 300f;
    [SerializeField] float timePassedSinceLastMult = 0f;

    private void Start()
    {
        timePassedSinceLastMult = 0;
        multText.text = ("Current Multiplier: X" + scoreMult.ToString("F1"));
    }
    // Update is called once per frame
    void Update()
    {
        timePassedSinceLastMult += Time.deltaTime;
        if(!health.dead)
        {
            ScoreTicker();
            ScoreMultiplierChecker();
        }
        if(health.dead)
        {
            currentScoreText.text = ("Final Score \n" + currentScore.ToString("F0"));
            multText.text = ("Final multiplier \n" + scoreMult.ToString("F1"));
        }    
    }

    private void ScoreTicker()
    {
        currentScore += Time.deltaTime * scoreMult;
        currentScoreText.text = ("Current Score: \n" + currentScore.ToString("F0"));
    }

    public void AddScore(float addedScore)
    {
        if(!health.dead)
        currentScore += addedScore * scoreMult;
    }

    private void ScoreMultiplierChecker()
    {
        if(timePassedSinceLastMult >= timeToIncreaseMult)
        {
            scoreMult *= 1.2f;
            timePassedSinceLastMult = 0;
            multText.text = ("Current Multiplier: X" + scoreMult.ToString("F1"));
        }
    }
}
