using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPnotify : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    Vector2 midpoint;
    float halfwayDistance;

    public RectTransform rectTransform;

    public bool readyToMove = false;

    public float speed = 200;
    public float defaultSpeed = 200;
    public float speedMultiplier = 0.99f;
    float defaultSpeedMultiplier = 0.99f;
    public float moveDownSpeed = 1100;

    public float fadeSpeed = 1;
    public TextMeshProUGUI TMProReference;

    //public GameObject GlobalTimer;
    //TimerGlobal timerGlobal;
    //float timeToAdd;
    //bool timeAdded = false;
    public float distanceToDisappearAt = 1f;
    public float yPositionToStopAt = -30f;

    public bool reachedTopOfFloat = false;
    public TextMeshProUGUI XPdisplay;
    public Vector2 XPdisplayLocation;
    public bool startFading = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        startPos = rectTransform.anchoredPosition;
        endPos = new Vector2(-215, -130);

        XPdisplayLocation = XPdisplay.rectTransform.position;

        //timerGlobal = GlobalTimer.GetComponent<TimerGlobal>();
        gameObject.SetActive(false);    // it has to start as active otherwise the rectTransform doesn't get assigned

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (readyToMove)
        {
            // have the text float upward, without fading
            if (reachedTopOfFloat == false) {
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endPos, Time.deltaTime * speed);
                speed *= speedMultiplier;

                if (Vector2.Distance(rectTransform.anchoredPosition, endPos) < 0.05f) {
                    reachedTopOfFloat = true;
                    endPos = new Vector2(90, -180);
                    //endPos = XPdisplayLocation;
                    speed = moveDownSpeed;
                    speedMultiplier = 1.01f;
                    midpoint = (rectTransform.anchoredPosition + endPos) / 2;
                    startFading = true;

                    //float midY = rectTransform.anchoredPosition.y + ((endPos.y - startPos.y) / 2);
                    //midpoint = new Vector2(startPos.x, midY);
                    //halfwayDistance = Vector2.Distance(startPos, midpoint);

                }
            } else {
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endPos, Time.deltaTime * speed);
                speed *= speedMultiplier;
                // at top of float, the text suddenly & quickly flies to the "Total XP: 123" display at center-bottom of screen
                //      so... just change the endPos and the speed

                if (startFading == true)
                {
                    // as "lands" there it fades out

                    Color currentColor = TMProReference.color;
                    float fadeAmount = currentColor.a - (fadeSpeed * Time.deltaTime);
                    TMProReference.color = new Color(currentColor.r, currentColor.g, currentColor.b, fadeAmount);
                } 
                //else if (Vector2.Distance(rectTransform.anchoredPosition, midpoint) < 0.1f && startFading == false) 
                //{
                //    startFading = true;
                //}




                if ((Vector2.Distance(rectTransform.anchoredPosition, endPos) < distanceToDisappearAt))
                {
                    GameManager.instance.ShowXPgainWithGreenFade();
                    gameObject.SetActive(false);
                    readyToMove = false;
                    reachedTopOfFloat = false;
                    startFading = false;
                }



            }





            // the "Total XP: 123" text suddenly turns green then changes back to black


            //if (Vector2.Distance(rectTransform.anchoredPosition, endPos) < halfwayDistance)
            //{
            //    // start fading out
            //    //      get current color
            //    Color currentColor = TMProReference.color;
            //    float fadeAmount = currentColor.a - (fadeSpeed * Time.deltaTime);
            //    TMProReference.color = new Color(currentColor.r, currentColor.g, currentColor.b, fadeAmount);
            //}


        }



    }

    public void BeginMove(float xpAmount)
    {
        // it starts as black, but we will change it to green and move it
        TMProReference.color = Color.green;

        // the timer is paused by the PuzzleManager


        //timeAdded = false;
        //timeToAdd = xpAmount;
        //Debug.Log("BeginMove() for bonusTime about to start");
        rectTransform.anchoredPosition = startPos;

        TMProReference.text = "+ " + xpAmount + " XP";


        //if (xpAmount > 3)
        //{
        //    TMProReference.text = "Bonus time: " + string.Format("{0:00}", xpAmount);
        //}
        //else
        //{
        //    float milliseconds = xpAmount % 1 * 1000;
        //    TMProReference.text = "Bonus time: " + string.Format("{0:00}.{1:000}", xpAmount, milliseconds);
        //}
        reachedTopOfFloat = false;

        //TMProReference.text = "Bonus time: " + (int)additionalTime + " seconds";
        speed = defaultSpeed;
        speedMultiplier = defaultSpeedMultiplier;
        endPos = new Vector2(-215, -130);
        gameObject.SetActive(true);
        readyToMove = true;
    }

}
