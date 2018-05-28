using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public Camera vrCamera;
    public Text main_text;
    public LevelControl LevelControl;
    public Image mask_image;
    public float gazeTimer;

    private float timer;
    private bool updateGaze;

    private void Start()
    {
        //main_text.text = "Welcome to SuperDrifter!";
        mask_image.fillAmount = 0;
        updateGaze = false;
        timer = gazeTimer;
    }

    private void Update()
    {
        if (updateGaze)
        {
            if (timer < 0)
            {
                LevelControl.StartGame();
                timer = gazeTimer;
            }
            else
            {
                timer -= Time.deltaTime;
                mask_image.fillAmount += 0.25f * Time.deltaTime;
            }
        }
        else
        {
            mask_image.fillAmount = 0;
            timer = gazeTimer;
        }
    }

    public void gazed(bool GazedAt)
    {
        updateGaze = GazedAt;
    }
}
