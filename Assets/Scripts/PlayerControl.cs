using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public int health;
    public Image reticle_mask;
    public float hpvalue;
    public Image hp_mask;
    public Image hp_maskMain;
    public ParticleSystem particlesystem;

    private Color colorA = new Color(128f / 255f, 128f / 255f, 128f / 255f, 128f / 255f);
    /*
    public GameObject gazed;
    public GameObject target;
    public GameObject middle;
    public GameObject left;
    public GameObject right;
    */

    //*** For clarity purposes ***//

    //teleportation buttons availability
    public GameObject midButton;
    public GameObject rightButton;
    public GameObject leftButton;
    public GameObject menuButton;
    public UnityEvent OnChangingPosition;
    Vector3 midPos;
    Vector3 rightPos;
    Vector3 leftPos;

    private void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", colorA);
        midPos = new Vector3(0.95f, 29.8f, -60.9f);
        leftPos = new Vector3(-60.3f, 29.8f, -1.3f);
        rightPos = new Vector3(60.3f, 29.8f, 0.6f);

        transform.position = midPos;
        transform.eulerAngles = new Vector3(0, 0, 0);
        midButton.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        menuButton.SetActive(false);
        PlayerManager.player.pos = "mid";
        OnChangingPosition.Invoke();
    }

    private void Update()
    {
        CheckHealth();
        if (health < 1)
        {
            menuButton.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ToLeft()
    {
        transform.position = leftPos;
        transform.eulerAngles = new Vector3(0, 90, 0);
        midButton.SetActive(true);
        leftButton.SetActive(false);
        rightButton.SetActive(true);
        PlayerManager.player.pos = "left";

        if (PlayerManager.player.bullet != null)
        {
            PlayerManager.player.bullet.SetActive(false);
        }
        OnChangingPosition.Invoke();
    }

    public void ToRight()
    {
        transform.position = rightPos;
        transform.eulerAngles = new Vector3(0, -90, 0);
        midButton.SetActive(true);
        leftButton.SetActive(true);
        rightButton.SetActive(false);
        PlayerManager.player.pos = "right";

        if (PlayerManager.player.bullet != null)
        {
            PlayerManager.player.bullet.SetActive(false);
        }
        OnChangingPosition.Invoke();
    }

    public void ToMid()
    {
        transform.position = midPos;
        transform.eulerAngles = new Vector3(0, 0, 0);
        midButton.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        PlayerManager.player.pos = "mid";

        if (PlayerManager.player.bullet != null)
        {
            PlayerManager.player.bullet.SetActive(false);
        }
        OnChangingPosition.Invoke();
    }

    public void CheckHealth()
    {
        float hp = health / 10.0f;
        hpvalue = hp;
        hp_mask.fillAmount = hp;
        hp_maskMain.fillAmount = hp;
        if (health < 5)
        {
            particlesystem.Play();
        }
    }

}
