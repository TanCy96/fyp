using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public PlayerControl playerControl;
    public Text main_text;
    public GameObject text;
    public GameObject hpbar;
    public GameObject timer;
    public GameObject Main_text;

    private void Start()
    {
        playerControl.health = 10;
    }

    private void Update()
    {
        if(playerControl.health < 1)
        {
            EndGame(false);
        }
    }

    public void EndGame(bool result)
    {
        if (result)
        {
            main_text.text = "Congrats, You Success Defense!";
        }
        else
        {
            main_text.text = "You Lose! Good Try!";
        }
        Main_text.SetActive(true);
        text.SetActive(false);
        hpbar.SetActive(false);
        timer.SetActive(false);
    }

    public void StartGame()
    {
        
    }
}
