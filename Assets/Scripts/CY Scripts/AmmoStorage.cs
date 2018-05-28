using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoStorage : MonoBehaviour
{
    public List<GameObject> store;
    public GameObject[] ammo;

    int size;
    string pos;
    Vector3 fixPosMid;
    Vector3 fixPosLeft;
    Vector3 fixPosRight;

    void Awake()
    {
        switch (gameObject.name)
        {
            case "AmmoStoreMid":
                pos = "mid";
                break;
            case "AmmoStoreLeft":
                pos = "left";
                break;
            case "AmmoStoreRight":
                pos = "right";
                break;
            case "AmmoStoreTutorial":
                pos = "tut";
                break;
        }
        for (int i=0; i<ammo.Length; i++)
        {
            ammo[i].GetComponent<CustomInteractionBehaviour>().pos = pos;
            ammo[i].SetActive(false);
        }

        fixPosMid = new Vector3(-0.45f, 3.0f, 1.9f);
        fixPosLeft = new Vector3(2.5f, 3.0f, -0.7f);
        fixPosRight = new Vector3(-2.5f, 3.0f, -0.8f);
        size = 5;
        while (size > 0)
        {
            int value = Random.Range(0, ammo.Length);
            if (!store.Contains(ammo[value]) && !ammo[value].activeInHierarchy)
            {
                ammo[value].SetActive(true);
                store.Add(ammo[value]);
                SetPosition(-size + 5);
                size--;
            }
        }
    }

    void Update()
    {
        for (int i=0; i<store.Count; i++)
        {
            if (!store[i].activeInHierarchy)
            {
                store.RemoveAt(i);
                int value = Random.Range(0, ammo.Length);
                while (store.Contains(ammo[value]) || ammo[value].activeInHierarchy)
                {
                    value = Random.Range(0, ammo.Length);
                }
                ammo[value].SetActive(true);
                store.Insert(i, ammo[value]);
                SetPosition(i);
            }
        }
    }

    void SetPosition(int num)
    {
        if (pos == "mid")
        {
            store[num].transform.position = fixPosMid + new Vector3(num * 0.2f, 0, 0);
        }
        else if (pos == "left")
        {
            store[num].transform.position = fixPosLeft + new Vector3(0, 0, num * 0.2f);
        }
        else if (pos == "right")
        {
            store[num].transform.position = fixPosRight + new Vector3(0, 0, num * 0.2f);
        }
        else
        {
            store[num].transform.position = new Vector3(-0.4f, 0.5f, -0.4f) + new Vector3(num * 0.2f, 0, 0);
        }
    }
}
