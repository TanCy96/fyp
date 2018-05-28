using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity.Interaction;

[RequireComponent(typeof(Anchor))]
public class CustomAnchor : MonoBehaviour
{
    public LineRenderer forceLine;
    public LineRenderer aimLine;
    public GameObject leftPosition;
    public GameObject rightPosition;

    Anchor anchor;
    float maxHorizontal;
    float maxVertical;
    float leftMaxValue;
    float rightMaxValue;
    float verticalValue;
    float heightFormula;
    bool leftHand;
    bool rightHand;
    Vector3 leftDirection;
    Vector3 rightDirection;

    void Awake()
    {
        anchor = GetComponent<Anchor>();
        maxHorizontal = 0.25f;
        maxVertical = 0.4f;
    }

    void OnEnable()
    {
        leftHand = false;
        rightHand = false;
    }

    void Update()
    {
        leftDirection = leftPosition.transform.position - transform.position;
        rightDirection = rightPosition.transform.position - transform.position;

        if (anchor.hasAnchoredObjects && leftHand && rightHand)
        {
            forceLine.gameObject.SetActive(true);
            aimLine.gameObject.SetActive(true);

            ForceLineValue();

            //aimLine values
            verticalValue = leftDirection.y + rightDirection.y;
            if (verticalValue < -maxVertical)
            {
                verticalValue = -maxVertical;
            }
            else if (verticalValue > maxVertical)
            {
                verticalValue = maxVertical;
            }
            heightFormula = (verticalValue + maxVertical) * 0.25f;
            aimLine.SetPosition(0, Vector3.zero);
            aimLine.SetPosition(1, new Vector3(0, 0.05f + heightFormula, 0));
        }
        else
        {
            forceLine.gameObject.SetActive(false);
            aimLine.gameObject.SetActive(false);
        }
    }

    public void toggleLeft(bool value)
    {
        leftHand = value;
    }

    public void toggleRight(bool value)
    {
        rightHand = value;
    }

    void ForceLineValue()
    {
        if (GetComponentInParent<StaffScript>().position == "mid")
        {
            if (leftDirection.x > maxHorizontal)
            {
                leftMaxValue = maxHorizontal - 0.05f;
            }
            else if (leftDirection.x < 0.05f)
            {
                leftMaxValue = 0;
            }
            else
            {
                leftMaxValue = leftDirection.x - 0.05f;
            }
            if (rightDirection.x < -maxHorizontal)
            {
                rightMaxValue = -maxHorizontal + 0.05f;
            }
            else if (rightDirection.x > -0.05f)
            {
                rightMaxValue = 0;
            }
            else
            {
                rightMaxValue = rightDirection.x + 0.05f;
            }
            forceLine.SetPosition(0, new Vector3(leftMaxValue, 0, 0));
            forceLine.SetPosition(1, new Vector3(rightMaxValue, 0, 0));
        }
        else if (GetComponentInParent<StaffScript>().position == "left")
        {
            if (leftDirection.z < -maxHorizontal)
            {
                leftMaxValue = maxHorizontal - 0.05f;
            }
            else if (leftDirection.z > -0.05f)
            {
                leftMaxValue = 0;
            }
            else
            {
                leftMaxValue = -leftDirection.z - 0.05f;
            }
            if (rightDirection.z > maxHorizontal)
            {
                rightMaxValue = -maxHorizontal + 0.05f;
            }
            else if (rightDirection.z < 0.05f)
            {
                rightMaxValue = 0;
            }
            else
            {
                rightMaxValue = -rightDirection.z + 0.05f;
            }
            forceLine.SetPosition(0, new Vector3(leftMaxValue, 0, 0));
            forceLine.SetPosition(1, new Vector3(rightMaxValue, 0, 0));
        }
        else
        {
            if (leftDirection.z > maxHorizontal)
            {
                leftMaxValue = maxHorizontal - 0.05f;
            }
            else if (leftDirection.z < 0.05f)
            {
                leftMaxValue = 0;
            }
            else
            {
                leftMaxValue = leftDirection.z - 0.05f;
            }
            if (rightDirection.z < -maxHorizontal)
            {
                rightMaxValue = -maxHorizontal + 0.05f;
            }
            else if (rightDirection.z > -0.05f)
            {
                rightMaxValue = 0;
            }
            else
            {
                rightMaxValue = rightDirection.z + 0.05f;
            }
            forceLine.SetPosition(0, new Vector3(leftMaxValue, 0, 0));
            forceLine.SetPosition(1, new Vector3(rightMaxValue, 0, 0));
        }
    }
}
