using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarCheckerHitScript : MonoBehaviour {

    public bool isCollidingWithCorrectRegion;
    public Text hitTextPopup, missTextPopup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggerenter");
        if (other.gameObject.tag == "BarRegion")
        {
            isCollidingWithCorrectRegion = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("triggerexit");
        if (other.gameObject.tag == "BarRegion")
        {
            isCollidingWithCorrectRegion = false;
        }
    }

    public void CheckHit()
    {
        Debug.Log("Checking for Hit");
        if (isCollidingWithCorrectRegion)
        {
            Debug.Log("Hit!");
            missTextPopup.gameObject.SetActive(false);
            hitTextPopup.gameObject.SetActive(true);
            Invoke("DisableText", 2f);
        }
        else
        {
            Debug.Log("Miss!");
            hitTextPopup.gameObject.SetActive(false);
            missTextPopup.gameObject.SetActive(true);
            Invoke("DisableText", 1f);
        }
        
    }

    void DisableText()
    {
        hitTextPopup.gameObject.SetActive(false);
        missTextPopup.gameObject.SetActive(false);
    }
}
