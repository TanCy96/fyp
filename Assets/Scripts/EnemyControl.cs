using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public PlayerControl playerControl;
    public int health;
    public int damage;
    public float speed;
    public float attackRate;
    public Image hp_mask;
    private Animator anim;
    public AudioSource audioSfx_Attack;
    public AudioClip monster_Attack;
    public AudioClip monster_Thunder;
    public AudioClip monster_Snow;
    public AudioClip monster_Fire;

    private Transform[] target;
    private int current = 0;
    private float hp;
    private float attackTimer;
    private float AnimTimer;
    private float slowTimer;
    private float stunTimer;
    private float slowRate = 1f;
    private Color colorW = new Color(1f, 86f / 255f, 86f / 255f, 128f / 255f);
    private Color colorA = new Color(128f / 255f, 128f / 255f, 128f / 255f, 128f / 255f);

    private void Start()
    {
        hp = health;
        anim = GetComponent<Animator>();
        slowTimer = 0;
        stunTimer = 0;
    }

    public void OnBullet(string type)
    {
        if (type == "DamageBullet")
        {
            getdamage(2);
            audioSfx_Attack.PlayOneShot(monster_Fire, 0.8f);
        }
        else if (type == "StunBullet")
        {
            getdamage(1);
            stunTimer = 2f;
            audioSfx_Attack.PlayOneShot(monster_Snow, 0.8f);
        }
        else if (type == "SlowBullet")
        {
            getdamage(1);
            slowTimer = 3f;
            audioSfx_Attack.PlayOneShot(monster_Thunder, 0.8f);
        }
    }

    private void Update()
    {
        if (playerControl.health > 0)
        {
            UpdateHealth();
            if (slowTimer > 0)
            {
                slowRate = 0.5f;
                slowTimer -= Time.deltaTime * TimeControl.timeMultiplier;
            }
            else
            {
                slowRate = 1f;
            }

            if (stunTimer > 0)
            {
                stunTimer -= Time.deltaTime * TimeControl.timeMultiplier;
            }
            else
            {
                WalkingAndAttack();
            }
        }   
    }

    private void UpdateHealth()
    {
        hp_mask.fillAmount = health/hp;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void WalkingAndAttack()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if (current < target.Length)
        {
            if (transform.position != target[current].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, target[current].position, speed * slowRate * Time.deltaTime * TimeControl.timeMultiplier);
                transform.LookAt(target[current].position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
                transform.eulerAngles += new Vector3(0f, 180f, 0f);
            }
            else
            {
                current = current + 1;
            }

            anim.SetBool("IsWalking",true);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("IsWalking",false);
            if (attackTimer < 0)
            {
                anim.SetBool("IsAttacking", true);
                audioSfx_Attack.PlayOneShot(monster_Attack, 0.8f);
                RenderSettings.skybox.SetColor("_Tint", colorW);
                playerControl.health -= damage;
                attackTimer = attackRate;
                Invoke("AttackEnd", 2f);
            }
            else
            {                
                attackTimer -= Time.deltaTime * TimeControl.timeMultiplier * slowRate;
            }
        }
    }

    public void AttackEnd()
    {
        anim.SetBool("IsAttacking", false);
        RenderSettings.skybox.SetColor("_Tint", colorA);
    }

    public void SetAlive(bool isBool)
    {
        gameObject.SetActive(isBool);

        if (isBool)
        {
            current = 0;
            transform.position = target[current].position;
            attackTimer = attackRate;
        }
    }


    public void SetAlive(bool isBool,Transform[] path)
    {
        gameObject.SetActive(isBool);
        target = path;
    }

    public bool GetAlive()
    {
        return gameObject.activeInHierarchy;
    }


    public void getdamage(int damage)
    {
        health -= damage;
    }

}
