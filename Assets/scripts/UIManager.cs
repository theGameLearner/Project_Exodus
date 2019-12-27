using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
   
   [SerializeField] FloatData FuelData;
   [SerializeField] FloatData ScoreData;

   [SerializeField] FloatData abilityData;

   public GameObject fuelGauge;

   public GameObject abilityGauge;

   public Text ScoreText;

   public Text gameOverScoreText;

   private GameStates localGameState;

   public GameObject gameOverPanel;

   public GameObject pausePanel;


   /// <summary>
   /// Start is called on the frame when a script is enabled just before
   /// any of the Update methods is called the first time.
   /// </summary>
   void Start()
   {
       updateGameState();
   }


   /// <summary>
   /// Update is called every frame, if the MonoBehaviour is enabled.
   /// </summary>
   void Update()
   {
        if(localGameState!=GameManager.instance.gameState){
            updateGameState();
        }

        UpdateFuelGauge();

        UpdateScore();

   }

    private void updateGameState()
    {
       
            
        switch(GameManager.instance.gameState){
            case GameStates.running:
                gameOverPanel.SetActive(false);
                pausePanel.SetActive(false);
                break;
            case GameStates.GameOver:
                gameOverPanel.SetActive(true);
                gameOverScoreText.text = "Score: "+ScoreData.Data;
                pausePanel.SetActive(false);
                break;
            case GameStates.paused:
                gameOverPanel.SetActive(false);
                pausePanel.SetActive(true);
            break;
        }

        localGameState = GameManager.instance.gameState;
    }

    private void UpdateScore()
    {
        ScoreText.text = "Score: "+ScoreData.Data;
    }

    private void UpdateFuelGauge()
    {
        fuelGauge.transform.localScale = new Vector3( FuelData.Data/100, fuelGauge.transform.localScale.y, fuelGauge.transform.localScale.z);
    }

    private void UpdateAbilityGauge()
    {
        abilityGauge.transform.localScale = new Vector3( abilityData.Data/100, fuelGauge.transform.localScale.y, abilityGauge.transform.localScale.z);
    }
}
