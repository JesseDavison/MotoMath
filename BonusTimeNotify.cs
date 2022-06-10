using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusTimeNotify : MonoBehaviour
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

    public float fadeSpeed = 1;
    public TextMeshProUGUI TMProReference;

    public GameObject GlobalTimer;
    TimerGlobal timerGlobal;
    float timeToAdd;
    bool timeAdded = false;
    public float distanceToStartChangingTimer = 1f;
    public float yPositionToStopAt = -50f;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        startPos = rectTransform.anchoredPosition;  
        endPos = new Vector2(-300, -32);

        float midX = startPos.x + ((endPos.x - startPos.x) / 2);
        midpoint = new Vector2(midX, startPos.y);
        halfwayDistance = Vector2.Distance(startPos, midpoint);

        
        timerGlobal = GlobalTimer.GetComponent<TimerGlobal>();
        gameObject.SetActive(false);    // it has to start as active otherwise the rectTransform doesn't get assigned

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (readyToMove) {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endPos, Time.deltaTime * speed);
            speed *= speedMultiplier;
            if (Vector2.Distance(rectTransform.anchoredPosition, endPos) < halfwayDistance) {
                // start fading out
                //      get current color
                Color currentColor = TMProReference.color;
                float fadeAmount = currentColor.a - (fadeSpeed * Time.deltaTime);
                TMProReference.color = new Color(currentColor.r, currentColor.g, currentColor.b, fadeAmount);
            }
            if ((Vector2.Distance(rectTransform.anchoredPosition, endPos) < distanceToStartChangingTimer) && timeAdded == false) {

                // add the time to the clock
                timerGlobal.AddToGlobalTimer(timeToAdd);
                timeAdded = true;
                // it should be faded out by now
                //gameObject.SetActive(false);
            }

        }



    }

    public void BeginMove(float additionalTime) {
        // it starts as black, but we will change it to green and move it
        TMProReference.color = Color.green;        

        // the timer is paused by the PuzzleManager


        timeAdded = false;
        timeToAdd = additionalTime;
        //Debug.Log("BeginMove() for bonusTime about to start");
        rectTransform.anchoredPosition = startPos;

        if (additionalTime > 3)
        {
            TMProReference.text = "Bonus time: " + string.Format("{0:00}", additionalTime);
        }
        else
        {
            float milliseconds = additionalTime % 1 * 1000;
            TMProReference.text = "Bonus time: " + string.Format("{0:00}.{1:000}", additionalTime, milliseconds);
        }


        //TMProReference.text = "Bonus time: " + (int)additionalTime + " seconds";
        speed = defaultSpeed;
        gameObject.SetActive(true);
        readyToMove = true;
    }

}
