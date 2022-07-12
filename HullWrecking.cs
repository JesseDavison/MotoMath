using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullWrecking : MonoBehaviour
{
    public AnimationCurve curve;

    public float time;
    public float speed;
    public float defaultSpeed;
    public float speedMultiplier;
    public bool speedAlreadyMultiplied = false;

    public float bounceSpeed;

    public Vector2 startPosition;
    public Vector2 endPosition;

    public float randomCurveMagnifier;
    public float minMagnifier;
    public float maxMagnifier;

    public float zAxisRotationSpeed;
    public float zAxisSpeedModifier;

    public bool doingABounce = false;
    public float bounceDistanceX;

    public bool finishedBouncing = false;
    public float bouncingOffset = 1.3f;

    public float distanceOfCurrentBounce;

    public GameObject playerVehicle;
    public bool swerveSent = false;

    public GameObject hullSprite;

    bool thisIsEnemyHull;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (doingABounce == true && finishedBouncing == false) {
            time += Time.deltaTime * bounceSpeed;
            //Vector2 pos = Vector2.Lerp(startPosition, endPosition, time);

            Vector2 pos = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * speed);

            //pos.y += curve.Evaluate(time) * randomCurveMagnifier;

            float extraYbump = curve.Evaluate(time) * randomCurveMagnifier;
            pos.y = startPosition.y + extraYbump + bouncingOffset;

            transform.position = pos;
            //speed *= speedMultiplier;
            //zAxisRotationSpeed *= zAxisSpeedModifier;


            //transform.Rotate(0, 0, zAxisRotationSpeed);

            //if (Mathf.Abs(transform.position.x - endPosition.x) < 0.01f) {
            //    doingABounce = false;
            //    ContinueBouncing();
            //}

            // if the hull has traveled the distanceOfCurrentBounce, then start a new bounce
            //      if current position is less than (originalPosition - distanceOfCurrentBounce), then....
            //if (transform.position.x < (startPosition.x - distanceOfCurrentBounce)) {
            //    startPosition = transform.position;
            //    time = 0;

            //    // change distanceOfCurrentBounce, perhaps randomly

            //}


            if (time >= 1) {
                time = 0;
                randomCurveMagnifier = GetRandomBounceMagnifier(0, 0);
                if (!speedAlreadyMultiplied) {
                    speed *= speedMultiplier;
                    speedAlreadyMultiplied = true;
                }

                endPosition = new Vector2(-16, bouncingOffset);
                zAxisRotationSpeed = Random.Range(30, 60f);
            } 
            //else if (thisIsEnemyHull == false) {
            //    time = 0;
            //    randomCurveMagnifier = GetRandomBounceMagnifier(0, 0);

            //    endPosition = new Vector2(1.5f, bouncingOffset);
            //    zAxisRotationSpeed = Random.Range(5, 10);
            //}

            // rotation stuff
            //gameObject.transform.GetChild(0);
            hullSprite.transform.Rotate(0, 0, zAxisRotationSpeed);



        }

        if (thisIsEnemyHull == true) 
        {
            if (transform.position.x < -4 && swerveSent == false)
            {
                swerveSent = true;
                playerVehicle.GetComponent<VehicleBounce>().BeginSwerve();
            }
            else if (transform.position.x < -15)
            {
                finishedBouncing = true;
                doingABounce = false;
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (transform.position.x < -15) 
            {
                finishedBouncing = true;
                doingABounce = false;
                gameObject.SetActive(false);
                GameManager.instance.DisplayGameOver(false, true);
            }

        }







    }
    float GetRandomBounceMagnifier(int newMin, int newMax) {
        float rando = 0;
        if (newMin != 0) {
            rando = Random.Range(newMin, newMax);
        } else {
            rando = Random.Range(minMagnifier, maxMagnifier);
        }
        
        return rando;
    }

    public void BeginBouncing(bool isThisEnemy) {
        if (isThisEnemy) {
            thisIsEnemyHull = true;
        } else {
            thisIsEnemyHull = false;
        }
        startPosition = transform.position;
        hullSprite.transform.Rotate(new Vector3(0, 0, 0));
        gameObject.SetActive(true);
        //endPosition = new Vector2(transform.position.x - bounceDistanceX, transform.position.y);
        float rando = Random.Range(-5, 5);
        endPosition = new Vector2(transform.position.x + rando, bouncingOffset);
        randomCurveMagnifier = GetRandomBounceMagnifier(4, 9);
        zAxisRotationSpeed = Random.Range(2, 13f);
        int randy = Random.Range(1, 3);
        if (randy == 1) {
            zAxisRotationSpeed *= -1;
        }
        speedAlreadyMultiplied = false;
        swerveSent = false;
        speed = defaultSpeed;
        doingABounce = true;
        finishedBouncing = false;
        time = 0;

        //GameManager.instance.SpawnLoot(startPosition);


    }
    void ContinueBouncing() {
        Debug.Log("entered ContinueBouncing()");
        startPosition = transform.position;
        //endPosition = new Vector2(transform.position.x - bounceDistanceX, transform.position.y);
        randomCurveMagnifier = GetRandomBounceMagnifier(0, 0);
        doingABounce = true;
        Debug.Log("startPosition: " + startPosition);
        Debug.Log("endPosition: " + endPosition);
        time = 0;
        speed = defaultSpeed;
    }

}
