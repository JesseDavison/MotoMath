using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropsBounce : MonoBehaviour
{
    bool doingABounce;
    public bool finishedBouncing;
    float t = 0;
    public float bounceSpeed;
    public float defaultBounceSpeed;
    public float fastestBounceSpeed;
    float time;
    public float speed;
    public float speedMultiplier;
    public float defaultSpeed;
    public float speed_stuckInGround;
    public bool stuckInGround = false;
    public float stuckDelay;


    public AnimationCurve curve;

    public float randomCurveMagnifier;
    public float minMagnifier;
    public float maxMagnifier;
    public float lowBounceMagnifier;

    public Vector2 startPosition;
    public Vector2 endPosition;
    public float bouncingOffset;
    bool speedAlreadyMultiplied;
    
    public float zAxisRotationSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;
    public float finalRotationSpeed_fast;
    
    public GameObject CaltropSprite;
    public GameObject PlayerVehicle;
    bool swerveSent;

    public int bounceCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (doingABounce == true && finishedBouncing == false)
        {
            time += Time.deltaTime * bounceSpeed;

            Vector2 pos = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * speed);

            float extraYbump = curve.Evaluate(time) * randomCurveMagnifier;
            pos.y = endPosition.y + extraYbump + bouncingOffset;

            transform.position = pos;


            if (time >= 1)
            {
                time = 0;
                bounceCount += 1;
                if (bounceCount == 1) {
                    // make it rotate quickly, and bounce very low several times
                    zAxisRotationSpeed = finalRotationSpeed_fast;
                    randomCurveMagnifier = lowBounceMagnifier;
                    bounceSpeed = fastestBounceSpeed;
                } 
                //else if (bounceCount >= 2) {
                //     //CaltropSprite.transform.Rotate(new Vector3(0, 0, 0));

                //}
                else if (transform.position.x <= endPosition.x) {
                    finishedBouncing = true;
                    CaltropSprite.transform.rotation = Quaternion.Euler(0, 0, -35);
                    StartCoroutine(DelayBeforeStuckInGround());
                }
                //randomCurveMagnifier = GetRandomBounceMagnifier(0, 0);
                //if (!speedAlreadyMultiplied)
                //{
                //    speed *= speedMultiplier;
                //    speedAlreadyMultiplied = true;
                //}

                //endPosition = new Vector2(-16, bouncingOffset);

                //zAxisRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);


            }

            CaltropSprite.transform.Rotate(0, 0, zAxisRotationSpeed);



        } else if (stuckInGround == true) {
            Vector2 pos = Vector2.MoveTowards(transform.position, new Vector3(-20, transform.position.y, transform.position.z), Time.deltaTime * speed_stuckInGround);
            transform.position = pos;
        }

        if (transform.position.x < 2.5f && swerveSent == false)
        {
            swerveSent = true;
            PlayerVehicle.GetComponent<VehicleBounce>().BeginSwerve();
        }
        else if (transform.position.x < -15)
        {

            doingABounce = false;
            gameObject.SetActive(false);
        }





    }
    float GetRandomBounceMagnifier(int newMin, int newMax)
    {
        float rando = 0;
        if (newMin != 0)
        {
            rando = Random.Range(newMin, newMax);
        }
        else
        {
            rando = Random.Range(minMagnifier, maxMagnifier);
        }

        return rando;
    }

    public void LaunchCaltrops(bool favorable)
    {
        if (favorable == true) {
            swerveSent = false;
        } else {
            swerveSent = true;      // this makes the player vehicle not swerve
        }
        startPosition = transform.position;
        CaltropSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.SetActive(true);
        //endPosition = new Vector2(transform.position.x - bounceDistanceX, transform.position.y);
        float rando = Random.Range(-1.75f, 1.75f);
        float yRando = Random.Range(-0.2f, 0.3f);
        endPosition = new Vector2(7 + rando, bouncingOffset + yRando);
        randomCurveMagnifier = GetRandomBounceMagnifier(4, 9);
        zAxisRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        int randy = Random.Range(1, 3);
        if (randy == 1)
        {
            zAxisRotationSpeed *= -1;
        }
        speedAlreadyMultiplied = false;
        speed = defaultSpeed;
        doingABounce = true;
        finishedBouncing = false;
        time = 0;
        bounceCount = 0;
        bounceSpeed = defaultBounceSpeed;
        stuckInGround = false;




    }
    IEnumerator DelayBeforeStuckInGround() {
        yield return new WaitForSeconds(stuckDelay);
        stuckInGround = true;
    }
}
