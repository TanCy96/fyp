using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    public GameObject obj;
    public GameObject collide;
    public ParticleSystem particle_system;

    private bool Shoot = false;
    private float Dtimer;

    public void ShootOn()
    {
        obj.SetActive(false);
        collide.SetActive(true);
        particle_system.Play();
        Dtimer = 1f;
        Shoot = true;

    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Shoot)
        {
            if (Dtimer < 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Dtimer -= Time.deltaTime * TimeControl.timeMultiplier;
            }
        }
    }

    void OnDisable()
    {
        Shoot = false;
    }
}
