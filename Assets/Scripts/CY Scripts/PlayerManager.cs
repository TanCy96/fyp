using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity.Interaction;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager player;
    public string pos;
    public GameObject bullet;
    public UnityEngine.UI.Image[] shotHUD;
    public UnityEngine.UI.Text leftText;
    public UnityEngine.UI.Text rightText;

    CustomInteractionBehaviour intObj;
    bool cooldown;

    void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        else if (player != this)
        {
            Destroy(gameObject);
        }

        cooldown = false;
    }

    void Update ()
    {
        Controller control = new Controller();
        Frame frame = control.Frame();
        foreach (Hand hand in frame.Hands)
        {
            if (frame.Hands.Count == 2)
            {
                leftText.gameObject.SetActive(false);
                rightText.gameObject.SetActive(false);

                if (!frame.Hands[0].Fingers[0].IsExtended && !frame.Hands[0].Fingers[1].IsExtended && !frame.Hands[0].Fingers[2].IsExtended
                 && !frame.Hands[0].Fingers[3].IsExtended && !frame.Hands[0].Fingers[4].IsExtended
                  && !frame.Hands[1].Fingers[0].IsExtended && !frame.Hands[1].Fingers[1].IsExtended && !frame.Hands[1].Fingers[2].IsExtended
                   && !frame.Hands[1].Fingers[3].IsExtended && !frame.Hands[1].Fingers[4].IsExtended)   //close fist for both hands
                {
                    if (intObj != null && !cooldown)
                    {
                        intObj.shot = true;
                        for (int i=0; i<shotHUD.Length; i++)
                        {
                            shotHUD[i].gameObject.SetActive(true);
                        }

                        StartCoroutine(ShotCD());
                    }
                }
            }
            else if (frame.Hands.Count == 1)
            {
                if (frame.Hands[0].IsLeft)
                {
                    leftText.gameObject.SetActive(false);
                    rightText.gameObject.SetActive(true);
                }
                else
                {
                    leftText.gameObject.SetActive(true);
                    rightText.gameObject.SetActive(false);
                }
            }
        }

        if (frame.Hands.Count == 0)
        {
            leftText.gameObject.SetActive(true);
            rightText.gameObject.SetActive(true);
        }

        if (intObj != null && intObj.cloneValue < 0)
        {
            intObj = null;
            bullet = null;
        }
    }

    public void SetBullet(GameObject obj)
    {
        bullet = obj;
        intObj = bullet.GetComponent<CustomInteractionBehaviour>();
    }

    IEnumerator ShotCD()
    {
        cooldown = true;
        yield return new WaitForSeconds(1);
        cooldown = false;
    }
}
