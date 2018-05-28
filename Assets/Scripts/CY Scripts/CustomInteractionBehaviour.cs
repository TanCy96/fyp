using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity.Interaction;

[RequireComponent(typeof(InteractionBehaviour))]
public class CustomInteractionBehaviour : MonoBehaviour
{
    public bool shot;
    public bool magnet;
    public bool isLow;
    public bool isLeftHand;
    public int cloneValue;
    public GameObject cloneObj;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject bulletAnchor;
    public TextMesh ammoText;
    
    AnchorableBehaviour anchor;
    CustomAnchor ancScript;
    GameObject[] ammoClones;
    float height;
    float force;
    public string pos;

    void Awake()
    {
        anchor = GetComponent<AnchorableBehaviour>();
        cloneValue = 2;

        ammoClones = new GameObject[cloneValue + 1];
        for (int i = 0; i < ammoClones.Length; i++)
        {
            ammoClones[i] = Instantiate(cloneObj, transform.position, Quaternion.identity);
            ammoClones[i].GetComponent<AmmoClone>().bulletAnchor = bulletAnchor;
            ammoClones[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        gameObject.GetComponentInChildren<Collider>().enabled = true;
        cloneValue = 2;
        shot = false;
        magnet = false;
        isLow = false;
        height = 0;
        transform.localScale = Vector3.one;
    }

    void OnDisable()
    {
        ammoText.text = "0 / 3";
        anchor.Detach();
        cloneValue = -1;
    }

    void Update()
    {
        if (anchor.isAttached)
        {
            PlayerManager.player.SetBullet(gameObject);
            gameObject.GetComponentInChildren<Collider>().enabled = false;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            ammoText.text = (cloneValue + 1) + " / 3";
        }
    }

    void FixedUpdate()
    {
        if (shot)
        {
            ancScript = anchor.anchor.GetComponent<CustomAnchor>();
            height = (ancScript.aimLine.GetPosition(1).y - 0.05f) / 0.2f;
            force = (ancScript.forceLine.GetPosition(0).x - ancScript.forceLine.GetPosition(1).x) / 0.4f * 100;

            if(cloneValue >= 0)
            {
                ammoClones[cloneValue].SetActive(true);
                ammoClones[cloneValue].GetComponent<AmmoClone>().CloneShoot(true, height, force, transform.rotation);
                cloneValue--;
                shot = false;
            }
        }

        if (cloneValue < 0)
        {
            gameObject.SetActive(false);
        }

        if (pos == "tut")
        {
            if (transform.position.y < 0.3)
            {
                isLow = true;
            }
        }
        else
        {
            if (transform.position.y < 2.9)
            {
                isLow = true;
            }
        }

        if (magnet && !isLow)
        {
            if (isLeftHand)
            {
                transform.position = Vector3.MoveTowards(transform.position, leftHand.transform.position, 0.5f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, rightHand.transform.position, 0.5f * Time.deltaTime);
            }
        }
        magnet = false;
    }
}
