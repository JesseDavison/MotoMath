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
    public float flatTireBounceHeight = 0.2f;
    public float defaultBounceHeight;

    public float minHorizontalSpeed = 0.1f;
    public float maxHorizontalSpeed = 2;
    public float actualHorizontalSpeed;
    public float actualHorizontalSpeed_default;
    public float minXpos = -2;
    public float minXpos_default;
    public float maxXpos = 0.5f;
    public float maxXpos_default;
    //public bool moving = true;

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


    //public int sparklesArrived = 0;

    public bool swerving = false;
    public Vector3 swerveZoom_1;
    public Vector3 swerveZoom_2;
    public float swerveSpeed_1 = 1;
    public float swerveSpeed_2 = 1;
    public bool swerve_1_complete = false;
    public bool swerve_2_complete = false;
    public float swerveSpeedMultiplier = 1;
    public int swerve1_Zvalue;
    public int swerve2_Zvalue;

    public bool initialAppearanceCompleted = false;

    public bool supposedToBeOffScreen = false;

    bool nitrousBoosting = false;
    //public bool isThisARocket = false;
    //public float rocketExplosionXPos;

    bool rockingFromBlownTire;
    public float rockingSpeed;
    public Transform vehicleSprite;

    public float flickMeAround;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        goalYpos = transform.position.y;

        StartCoroutine(bounceLoop());
        driftForwardBackward(30);

        //vehicleSprite = transform.GetChild(0);

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


        transform.position = Vector3.SmoothDamp(transform.position, newGoal, ref velocity, Time.deltaTime * actualHorizontalSpeed);     // with SmoothDamp a lower speed means FASTER movement



        if (readyToMoveAgain && supposedToBeOffScreen == false) {
            if (Mathf.Abs(goalXpos - transform.position.x) < 0.01f && readyToMoveAgain)
            {
                // wait a random amount of time, then get a different goalXpos
                readyToMoveAgain = false;
                StartCoroutine(waitToMoveAgain());
            }
        }

        if (supposedToBeOffScreen == true) {
            if (transform.position.x <= -9) {
                gameObject.SetActive(false);
                supposedToBeOffScreen = false;
                gameObject.transform.position = new Vector2(20, transform.position.y);
                GameManager.instance.enemyInRange = false;
            }
        }

        //if (sparklesArrived == 6)
        //{
        //    //ShowXPgain();
        //    //GameManager.instance.IncreaseXP();
        //    sparklesArrived = 0;
        //    //BeginSwerve();
        //    //GameManager.instance.PlayExplosion();
        //    GameManager.instance.ResolveConflictFavorably();
        //    //GameManager.instance.EnemyAppears();
        //    //GameManager.instance.EnemyDies();
        //    //GameManager.instance.FireRocket();
        //}
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
                transform.position = new Vector3(transform.position.x, transform.position.y, swerve1_Zvalue);
                transform.localScale = Vector3.Lerp(transform.localScale, swerveZoom_1, Time.deltaTime * swerveSpeed_1);
                //swerveSpeed_1 *= swerveSpeedMultiplier;


                if (Vector3.Distance(transform.localScale, swerveZoom_1) < 0.01f) {
                    swerve_1_complete = true;
                }


            } else if (swerve_2_complete == false) {
                // move towards second swerve position
                transform.position = new Vector3(transform.position.x, transform.position.y, swerve2_Zvalue);
                transform.localScale = Vector3.Lerp(transform.localScale, swerveZoom_2, Time.deltaTime * swerveSpeed_2);
                //swerveSpeed_1 *= swerveSpeedMultiplier;

                if (Vector3.Distance(transform.localScale, swerveZoom_2) < 0.01f) {
                    swerve_2_complete = true;
                }

            }
            else {
                // move towards resting zoom level of 1
                transform.position = new Vector3(transform.position.x, transform.position.y, 1);    // 1 is the default z value
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * swerveSpeed_2);

                if (Vector3.Distance(transform.localScale, Vector3.one) < 0.01f) {
                    swerving = false;
                    transform.localScale = Vector3.one;
                }
            }
        }
        //if (isThisARocket) { 
        //    if (transform.position.x > rocketExplosionXPos) {
        //        GameManager.instance.EnemyDies();
        //        gameObject.SetActive(false);
        //    }
        //}

        if (rockingFromBlownTire == true) {
            
            // go from -3 to 3, on the CHILD sprite object
            
            //transform.Rotate(0, 0, rockingSpeed);
            vehicleSprite.Rotate(0, 0, rockingSpeed);

            if (vehicleSprite.rotation.z > flickMeAround || vehicleSprite.rotation.z < -flickMeAround) {
                rockingSpeed *= -1;
            }

        }


    }



    IEnumerator Wait_thenRestoreDefaultXPosition() {
        yield return new WaitForSeconds(2);
        ResetXpositionsToDefaultAfterCaltrops();
    }

    IEnumerator waitToMoveAgain() {
        float delay = Random.Range(minTimeBetweenMOVE, maxTimeBetweenMOVE);
        yield return new WaitForSeconds(delay);
        //Debug.Log("waiting for " + delay + " seconds");
        driftForwardBackward(0);
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

    public void driftForwardBackward(float forcedSpeed) {
        // pick a spot to go to

        if (nitrousBoosting == true) {
            goalXpos = Random.Range(8.2f, 8.5f);
            readyToMoveAgain = true;
        } else if (supposedToBeOffScreen == false) {
            goalXpos = Random.Range(minXpos, maxXpos);


            // pick a speed


            if (forcedSpeed != 0)
            {
                actualHorizontalSpeed = forcedSpeed;
            }
            else
            {
                actualHorizontalSpeed = Random.Range(minHorizontalSpeed, maxHorizontalSpeed);
            }


            // toggle the boolean
            readyToMoveAgain = true;
        }


    }
    public void DriveToMiddle_forNitrousBoost() {
        nitrousBoosting = true;
        driftForwardBackward(0);
    }
    public void DriveToForwardPosition_forDroppingCaltrops() {
        // change min & max X positions
        minXpos = 11;
        maxXpos = 14;
        driftForwardBackward(0);
        StartCoroutine(Wait_thenRestoreDefaultXPosition());
    }
    public void ResetXpositionsToDefaultAfterCaltrops() {
        minXpos = minXpos_default;
        maxXpos = maxXpos_default;
    }
    public void EndNitrousBoost() {
        nitrousBoosting = false;
        driftForwardBackward(0);
    }
    public void DriveAwayForward() {
        goalXpos = 21;
        readyToMoveAgain = true;
        supposedToBeOffScreen = true;
    }
    public void DriveAwayBackward() {
        goalXpos = -10;
        readyToMoveAgain = true;
        supposedToBeOffScreen = true;
    }
    public void BringVehicleBackOnScreen() {
        supposedToBeOffScreen = false;
        driftForwardBackward(0);
    }
    public void AddSparkleArrived() {
        //sparklesArrived += 1;
    }

    //public void ShowXPgain() {
    //    xpText.transform.position = xpTextOriginalPos;
    //    xpText.SetActive(true);
    //    xpAppear = true;
    //}

    //public void BeginRandomSwerve() {
    //    BeginSwerve();
    //}
    public void BeginSwerve() {
        // if it swerves "left" then z-value should be 1

        // if it swerves "right" then z should be -1, and then get changed back to 1



        float temp1;
        float temp2;

        //if (swerveLeftToAvoidHull == true) {
        //    temp1 = 0.7f;   // go far "left" because the enemy hull will appear in FRONT OF the player vehicle
        //    temp2 = 1.05f;
        
        int rando = Random.Range(1, 3);
        if (rando == 1)
        {
            // swerving "right" first, so z should be -1
            temp1 = Random.Range(1.1f, 1.3f);
            temp2 = Random.Range(0.7f, 0.9f);
            swerve1_Zvalue = -1;
            swerve2_Zvalue = 1;
        }
        else
        {
            // swerving "left" first, so z should be 1
            temp2 = Random.Range(1.1f, 1.3f);
            temp1 = Random.Range(0.7f, 0.9f);
            swerve1_Zvalue = 1;
            swerve2_Zvalue = -1;
        }

        



        swerveSpeed_1 = Random.Range(5, 10f);
        swerveSpeed_2 = Random.Range(5, 10f);



        swerveZoom_1 = new Vector3(temp1, temp1, temp1);

        swerveZoom_2 = new Vector3(temp2, temp2, temp2);

        swerve_1_complete = false;
        swerve_2_complete = false;
        swerving = true;
    }

    public void AnimateBlownTire() {
        // increased bumpiness:
        minTimeBetweenBounce = 0.01f;
        maxTimeBetweenBounce = 0.3f;
        bounceHeight = flatTireBounceHeight;

        // vehicle rocks (rotates) back & forth slightly
        rockingFromBlownTire = true;

        


        // these need to be restored to defaults at some point

    }
    public void ResetBlownTireStuff() {
        minTimeBetweenBounce = 0.05f;
        maxTimeBetweenBounce = 0.7f;
        bounceHeight = 0.07f;
        rockingFromBlownTire = false;
    }
    public void SetSpeedToZeroAfterBlownTire() {
        actualHorizontalSpeed = 0;
        midBounce = false;
        postBounceFallSpeed = 0;
        readyToMoveAgain = false;
        swerving = false;
        rockingFromBlownTire = false;
        bounceHeight = 0;
        // stop animation
        transform.GetChild(0).GetComponent<Animator>().enabled = false;

    }
    public void RestoreSpeed() {
        actualHorizontalSpeed = actualHorizontalSpeed_default;

        /// and RESTORE ALL THE OTHER STUFF
    }



    //public void SetGoalPosForRocket(int xPos, int speed, float targetXPos) {
    //    goalXpos = xPos;
    //    actualHorizontalSpeed = speed;      // higher number means slower movement
    //    rocketExplosionXPos = targetXPos;


    //}

}
