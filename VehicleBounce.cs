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

    public bool swerving = false;
    public Vector3 swerveZoom_1;
    public Vector3 swerveZoom_2;
    public float swerveSpeed_1 = 1;
    public float swerveSpeed_2 = 1;
    public bool swerve_1_complete = false;
    public bool swerve_2_complete = false;
    public float swerveSpeedMultiplier = 1;


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
            GameManager.instance.IncreaseXP();
            sparklesArrived = 0;
            BeginSwerve();
        }
        //if (xpAppear) {
        //    xpText.transform.position = Vector3.MoveTowards(xpText.transform.position, xpTextEndPos, Time.deltaTime * xpTextSpeed);

        //    if (xpText.transform.position.y >= -1.4f) {
        //        xpAppear = false;
        //        xpText.SetActive(false);
        //    }
        //}

        if (swerving) { 
            // move "back" by changing zoom, then over-correct, then back to zoom 1
            if (swerve_1_complete == false) {
                // move towards first swerve position

                transform.localScale = Vector3.Lerp(transform.localScale, swerveZoom_1, Time.deltaTime * swerveSpeed_1);
                //swerveSpeed_1 *= swerveSpeedMultiplier;


                if (Vector3.Distance(transform.localScale, swerveZoom_1) < 0.01f) {
                    swerve_1_complete = true;
                }


            } else if (swerve_2_complete == false) {
                // move towards second swerve position
                transform.localScale = Vector3.Lerp(transform.localScale, swerveZoom_2, Time.deltaTime * swerveSpeed_2);
                //swerveSpeed_1 *= swerveSpeedMultiplier;

                if (Vector3.Distance(transform.localScale, swerveZoom_2) < 0.01f) {
                    swerve_2_complete = true;
                }

            }
            else {
                // move towards resting zoom level of 1
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * swerveSpeed_2);

                if (Vector3.Distance(transform.localScale, Vector3.one) < 0.01f) {
                    swerving = false;
                    transform.localScale = Vector3.one;
                }
            }
        }


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


    public void BeginSwerve() {

        int rando = Random.Range(1, 3);
        float temp1;
        float temp2;


        if (rando == 1) { 
            temp1 = Random.Range(1.1f, 1.3f);
            temp2 = Random.Range(0.7f, 0.9f);
        } else {
            temp2 = Random.Range(1.1f, 1.3f);
            temp1 = Random.Range(0.7f, 0.9f);
        }

        swerveSpeed_1 = Random.Range(5, 10f);
        swerveSpeed_2 = Random.Range(5, 10f);


        swerveZoom_1 = new Vector3(temp1, temp1, temp1);

        swerveZoom_2 = new Vector3(temp2, temp2, temp2);

        swerve_1_complete = false;
        swerve_2_complete = false;
        swerving = true;
    }


}
