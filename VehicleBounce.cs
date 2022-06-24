using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleBounce : MonoBehaviour
{
    Vector3 originalPos;
    public float currentYpos;
    public float minTimeBetweenBounce = 0.2f;
    public float maxTimeBetweenBounce = 2;
    public bool midBounce = false;
    public float postBounceFallSpeed = 5;
    public float bounceHeight = 0.1f;

    public float minHorizontalSpeed = 0.1f;
    public float maxHorizontalSpeed = 2;
    public float actualHorizontalSpeed;
    public float minXpos = -2;
    public float maxXpos = 0.5f;
    public bool moving = true;

    public float currentXpos;
    public float goalXpos;

    public float goalYpos;

    public float minTimeBetweenMOVE = 1;
    public float maxTimeBetweenMOVE = 3;

    public bool readyToMoveAgain;

    public Vector3 velocity = Vector3.zero;

    //public GameObject xpText;
    //public Vector3 xpTextOriginalPos;
    //public bool xpAppear = false;
    //public Vector3 xpTextEndPos;
    //public float xpTextSpeed = 3;


    public int sparklesArrived = 0;



    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        goalYpos = transform.position.y;
        StartCoroutine(bounceLoop());
        driftForwardBackward();
        //bounceLoop();
        //xpTextOriginalPos = xpText.transform.position;
        //xpTextEndPos = new Vector3(xpText.transform.position.x, xpText.transform.position.y + 2, xpText.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    private void FixedUpdate()
    {
        if (midBounce) {




            if (Mathf.Abs(transform.position.y - originalPos.y) < 0.001f)
            {
                midBounce = false;
                transform.position = new Vector3(transform.position.x, goalYpos, originalPos.z);
                StartCoroutine(bounceLoop());

            }
        }

        Vector3 newGoal = new Vector3(goalXpos, goalYpos, originalPos.z);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, goalYpos, originalPos.z), Time.deltaTime * postBounceFallSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, newGoal, ref velocity, Time.deltaTime * actualHorizontalSpeed);



        if (readyToMoveAgain) {
            if (Mathf.Abs(goalXpos - transform.position.x) < 0.01f && readyToMoveAgain)
            {
                // wait a random amount of time, then get a different goalXpos
                readyToMoveAgain = false;
                StartCoroutine(waitToMoveAgain());
            }
        }

        if (sparklesArrived == 6)
        {
            //ShowXPgain();
            GameManager.instance.ShowXPgain(10);
            sparklesArrived = 0;
        }
        //if (xpAppear) {
        //    xpText.transform.position = Vector3.MoveTowards(xpText.transform.position, xpTextEndPos, Time.deltaTime * xpTextSpeed);

        //    if (xpText.transform.position.y >= -1.4f) {
        //        xpAppear = false;
        //        xpText.SetActive(false);
        //    }
        //}
    }





    IEnumerator waitToMoveAgain() {
        float delay = Random.Range(minTimeBetweenMOVE, maxTimeBetweenMOVE);
        yield return new WaitForSeconds(delay);
        //Debug.Log("waiting for " + delay + " seconds");
        driftForwardBackward();
    }


    IEnumerator bounceLoop() {
        
        float delay = Random.Range(minTimeBetweenBounce, maxTimeBetweenBounce);
        yield return new WaitForSeconds(delay);
        //Debug.Log("bouncing");
        bounce();
    }


    void bounce() {
        transform.position = new Vector3(transform.position.x, transform.position.y + bounceHeight, transform.position.z);
        midBounce = true;

        //float currentTime = 
    }

    void driftForwardBackward() {
        // pick a spot to go to
        goalXpos = Random.Range(minXpos, maxXpos);


        // pick a speed
        actualHorizontalSpeed = Random.Range(minHorizontalSpeed, maxHorizontalSpeed);

        // toggle the boolean
        readyToMoveAgain = true;
        
    }

    public void AddSparkleArrived() {
        sparklesArrived += 1;
    }

    //public void ShowXPgain() {
    //    xpText.transform.position = xpTextOriginalPos;
    //    xpText.SetActive(true);
    //    xpAppear = true;
    //}


}
