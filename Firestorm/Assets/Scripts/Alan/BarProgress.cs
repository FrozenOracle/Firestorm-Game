using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Gonna have to re-do the entire bar-filling, I think.

public class BarProgress : MonoBehaviour {

    Image foregroundImage;
    public BoxCollider2D regionChecker, regionCheckerTarget;
    public Vector2 regionCheckerInitPos, regionCheckerTargetPos;

    public bool isLerping;
    public float timeToLerp = 3f;
    float timeStartedLerping;


    //Define a new variable, Value, that is an int, and its get & set.
    //This is done so the image can be 'filled' like you want with a progress bar.
    public int Value
    {
        get
        {
            if (foregroundImage != null)
                return (int)(foregroundImage.fillAmount * 100);
            else
                return 0;
        }
        set
        {
            if (foregroundImage != null)
                foregroundImage.fillAmount = value / 100f;
        }
    }

    void Start() {
        foregroundImage = gameObject.GetComponent<Image>();
        Value = 0;
        regionChecker = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        regionCheckerTarget = gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>();
        regionCheckerInitPos = regionChecker.transform.position;
        regionCheckerTargetPos = regionCheckerTarget.gameObject.transform.position;
    }

    //This function can be called - via a button, another script, or anything else.
    public void UpdateProgress()
    {
        Hashtable param = new Hashtable(); //iTween needs a hashtable for its advanced features. The documentation on the website explains in more detail.
        param.Add("from", 0.0f);
        param.Add("to", 100);
        param.Add("time", 3.0f);
        // param.Add("onstart","MoveRegionChecker");
        //param.Add("onstarttarget", gameObject.transform.GetChild(0));
        param.Add("onupdate", "TweenedSomeValue"); //This calls the function "TweenedSomeValue" on every update.
        param.Add("onComplete", "OnFullProgress"); //This calls the function "OnFullProgress" when the bar fills all the way up.
        param.Add("onCompleteTarget", gameObject); //This tells the iTween what gameObject has the onComplete method on it, in this case, the same object this script is calling iTween on.
        iTween.ValueTo(gameObject, param); //Interpolates the value between the beginning 'from' and end 'to' over 'time' seconds.
        StartLerping();

    }

    public void TweenedSomeValue(int val)
    {
        Value = val; //Sets Value to the increased fill value of the bar.

        //Checker moves far, far too fast to the destination, and slows down when it gets there. Need it to be smooth, like the bar.
        //iTween.MoveTo(regionChecker.gameObject, regionCheckerTargetPos, 3.0f);

        //Lerp attempt. Same problem as MoveTo.
        //regionChecker.transform.position = Vector3.Lerp(regionCheckerInitPos, regionCheckerTargetPos, Time.deltaTime);
    }

    //This works. Will probably convert the bar itself to just lerp instead of using iTween, so that the values should be identical (since getting the checker to move with iTween didn't work out).
    void StartLerping()
    {
        isLerping = true;
        timeStartedLerping = Time.time;
    }

    void Update()
    {
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeToLerp;
            regionChecker.transform.position = Vector3.Lerp(regionCheckerInitPos, regionCheckerTargetPos, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
            }
        }
    }

   // public void MoveRegionChecker()
   // {

    //   Hashtable param = new Hashtable();
    //param.Add();
    //param.Add("from", regionCheckerInitPos);
    //param.Add("to", regionCheckerTargetPos);
    //param.Add("time", 3.0f);
    //param.Add("onupdate", "TweenedRegionCheckerPos");
    //iTween.ValueTo(regionChecker.gameObject, param);
    //}

    //public void TweenedRegionCheckerPos(Vector2 val)
    //{
    //    Debug.Log("Is this even doing anything");
    //    regionChecker.transform.position = val;

    //}

    public void OnFullProgress() 
    {
        Value = 0; //Resets the bar when it hits the end.
        
    }

}
