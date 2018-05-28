using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClone : MonoBehaviour
{
    public float forceAmount;
    public GameObject bulletAnchor;
    Rigidbody rigid;
    float height;
    float force;
    bool shot;

    void Awake ()
    {
        rigid = GetComponent<Rigidbody>();
	}

    void OnEnable()
    {
        transform.position = bulletAnchor.transform.position;
        rigid.useGravity = false;
        shot = false;
    }

    void FixedUpdate ()
    {
        if (shot)
        {
            rigid.AddForce(transform.forward * force * Time.deltaTime);
            if (transform.position.y < -1)
            {
                gameObject.SetActive(false);
            }
        }
	}

    public void CloneShoot(bool x, float y, float z, Quaternion rot)
    {
        shot = x;
        height = y;
        force = z * forceAmount;
        transform.rotation = rot;
        rigid.useGravity = true;
        rigid.AddForce(new Vector3(0, 0.5f + height, 0) * force * Time.deltaTime, ForceMode.Impulse);
        rigid.AddForce(transform.forward * force * Time.deltaTime, ForceMode.Impulse);
    }
}
