using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{

    public AnimationCurve curve;
    public bool curveUpIfTrue = true;
    public float time;
    public bool readyToMove = true;
    public float speed = 10;
    public float defaultSpeed = 0.2f;
    public float curveSpeedMultiplier = 0.99f;
    public Vector2 startPosition;
    public Vector2 endPosition;


    public float zAxisRotationSpeed;
    public float zAxisSpeedModifier = 1f;


    public float speedForCurve = 5;

    public GameObject vehicle;
    VehicleBounce vehicleScript;

    // Start is called before the first frame update
    void Start()
    {
        vehicleScript = vehicle.GetComponent<VehicleBounce>();
    }

    // Update is called once per frame
    void Update()
    {

        if (readyToMove) {


            //time += Time.deltaTime * speedForCurve;
            //Vector2 pos = Vector2.Lerp(startPosition, destination, time);
            //if (curveUpIfTrue)
            //{
            //    pos.y += curve.Evaluate(time);      // for curve DOWNWARDS, just subtract instead of add
            //}
            //else
            //{
            //    pos.y -= curve.Evaluate(time);
            //}
            //transform.position = pos;
            //speedForCurve *= curveSpeedMultiplier;









            time += Time.deltaTime * speed;
            Vector2 pos = Vector2.Lerp(startPosition, endPosition, time);
            if (curveUpIfTrue)
            {
                pos.y += curve.Evaluate(time);      // for curve DOWNWARDS, just subtract instead of add
            }
            else
            {
                pos.y -= curve.Evaluate(time);
            }
            transform.position = pos;
            speed *= curveSpeedMultiplier;
            zAxisRotationSpeed *= zAxisSpeedModifier;

            //if (Vector2.Distance(transform.position, endPosition) < 0.4f) { 
            //    // start the "sparkle" effect of turning it on & off
            //}
            //if (time > 0.8f) {
            //    gameObject.SetActive(false);
            //} else if (time > 0.7f) {
            //    gameObject.SetActive(true);
            //}

            //if (time > 0.6f)
            //{
            //    gameObject.SetActive(false);
            //}

            //} else if (time > 0.5f) {
            //    gameObject.SetActive(true);
            //} else if (time > 0.4f) {
            //    gameObject.SetActive(false);
            //}


            if (Vector2.Distance(transform.position, endPosition) < 0.05f)
            {
                gameObject.SetActive(false);
                readyToMove = false;
                //destinationReached = true;
                speed = defaultSpeed;
                //Debug.Log("time: " + time);             // find the highest time that is reached, so we can evenly space out on&off flickering for the sparkle effect
                //PuzzleManager.instance.AnimatePuzzleSolved(null, true, true);
                //Debug.Log("sent message to animate puzzleSolved");
                vehicleScript.AddSparkleArrived();
            }
            DoRotationStuff();
        }


    }

    public void BeginSparkleMovement(Vector2 startPos, Vector2 endPos) {
        transform.position = startPos;
        startPosition = startPos;
        endPosition = endPos;
        // if it's moving right, rotate clockwise... & vice-versa
        //zAxisRotationSpeed = endPos.x - startPos.x;
        //zAxisRotationSpeed = startPos.x = endPos.x;

        speed = Random.Range(0.7f, 2);

        zAxisRotationSpeed = Random.Range(-2, 2);

        time = 0;
        //zAxisRotationSpeed = 2;    // starts at 2 and slows down
        gameObject.SetActive(true);
        readyToMove = true;
    }
    //      ROTATION STUFF
    //  ***************************************************************************************


    private void DoRotationStuff()
    {
        // simply rotate the object around the z-axis

        transform.Rotate(0, 0, zAxisRotationSpeed);        
        
        
        
        
        //_angle += RotateSpeed * Time.deltaTime;





    }
    //  ***************************************************************************************

}
