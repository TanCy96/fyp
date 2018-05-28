using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffScript : MonoBehaviour
{
    public Camera cam;
    public string position;
    float yPos;
    Vector3 rot;

    void Start()
    {
        yPos = transform.position.y;
        rot = transform.eulerAngles;
    }

    void Update ()
    {
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        rot.y = transform.eulerAngles.y;
        rot.z = transform.eulerAngles.z;
        transform.eulerAngles = rot;
    }

    public void SetState()
    {
        position = PlayerManager.player.pos;
    }
}
