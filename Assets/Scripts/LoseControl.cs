using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseControl : MonoBehaviour {

    public Camera vrCamera;
    public Text main_text;

    // Use this for initialization
    public void EndGame (bool result) {
        gameObject.SetActive(true);
        if(result)
        {
            main_text.text = "Congrats, You Success Defense!"; 
        }
        else
        {
            main_text.text = "You Lose! Good Try!";
        }
        transform.eulerAngles = new Vector3(0, -180, 0);
        transform.position = vrCamera.transform.position + vrCamera.transform.forward/5;
        transform.LookAt(vrCamera.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
