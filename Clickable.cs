using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clickable : MonoBehaviour
{
    // this script is used for both circles & operators, so we have the following:
    public float valueOfThisCircle_orGoal;
    public int IDnumberOfCircleDataAttachedToThis;

    public string typeOfThisOperator;
    public int IDnumberOfOperatorDataAttachedToThis;

    public bool readyToBeUsed = false;

    // moving circles
    public bool readyToMove = false;
    public Vector2 destination;
    public string destinationText = "";
    public float speed = 1;
    public float defaultSpeed = 1;
    public float speedMultiplier = 1.05f;
    public float speedForCurve = 5;
    public float speedForCurveDefault = 5;
    public float curveSpeedMultiplier = 1;
    public float speedCap = 25;
    public bool destinationReached = false;
    public Vector2 startPosition;
    float time;
    //float destinationXPosition;
    float midwayX;
    public Vector2 defaultPosition;

    public AnimationCurve curve;

    public bool changingToWhite = false;
    Color initialColor;


    public bool rotating = false;
    public bool curvePath;
    public bool curveUpIfTrue;

    public bool shrinking = false;
    public Vector3 minScale = new Vector3(0, 0, 0);
    public Vector3 originalScale = new Vector3(2.2f, 2.2f, 2.2f);

    public bool growing = false;
    public Vector3 defaultScale;

    public bool partOfCurrentPuzzle = false;

    public GameObject SawBlade_GameObject;
    RectTransform SawBlade_RectTransform;
    public float sawBladeSpeed_default;
    public float sawBladeSpeed_whenClicked;
    public GameObject DirtbikeTire_GameObject;
    RectTransform DirtbikeTire_RectTransform;


    private void Start()
    {
        gameObject.SetActive(true);

        SawBlade_GameObject.SetActive(true);
        DirtbikeTire_GameObject.SetActive(true);

        SawBlade_RectTransform = SawBlade_GameObject.GetComponent<RectTransform>();
        DirtbikeTire_RectTransform = DirtbikeTire_GameObject.GetComponent<RectTransform>();

        SawBlade_GameObject.SetActive(false);
        DirtbikeTire_GameObject.SetActive(false);

        //gameObject.SetActive(false);
    }

    public void BeginMovementToTarget(Vector2 targetDestination, string targetName, bool curvedPath, bool curveUp) {
        curvePath = curvedPath;
        curveUpIfTrue = curveUp;
        speed = defaultSpeed;
        startPosition = transform.position;
        time = 0;
        //destination = target.transform.position;
        destination = targetDestination;
        //destinationXPosition = destination.x;
        //midwayX = destinationXPosition - transform.position.x;
        destinationText = targetName;
        readyToMove = true;
    }
    public void BeginMovementToNewDefaultPosition(Vector2 target) {
        speed = defaultSpeed;
        defaultPosition = target;
        destination = defaultPosition;
        readyToMove = true;
    }
    public void BeginMovementOffscreenToLeft() {
        speed = defaultSpeed;
        curvePath = false;
        destination = transform.position - new Vector3(18, 0, 0);
        readyToMove = true;
        destinationText = "left of screen for nitrous";     // have to change destinationText so it doesn't trigger puzzle solve
    }
    public void BeginMovementToDefaultPosition(bool curvedPath, bool curveUp, bool fadingToWhite) {
        changingToWhite = fadingToWhite;
        if (fadingToWhite) {
            initialColor = gameObject.GetComponent<SpriteRenderer>().color;
        }
        curvePath = curvedPath;
        curveUpIfTrue = curveUp;
        speed = defaultSpeed;
        startPosition = transform.position;
        time = 0;
        destination = defaultPosition;
        //destinationXPosition = destination.x;
        //midwayX = destinationXPosition - transform.position.x;
        readyToMove = true;

        // as the RESULT circle moves to the left it should fade from green to white


    }
    public void SendCircleToToilet(int fallingSpeed) {
        
        curvePath = false;
        curveUpIfTrue = true;
        if (fallingSpeed != 0) {
            speed = fallingSpeed;
        } else {
            speed = defaultSpeed;
        }
        startPosition = transform.position;
        time = 0;
        //destination = target.transform.position;
        destination = new Vector2(startPosition.x, startPosition.y - 10);
        //destinationXPosition = destination.x;
        //midwayX = destinationXPosition - transform.position.x;
        destinationText = "toilet";
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(0.27f, .36f, .26f, 1);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        readyToMove = true;
    }
    public void TeleportToDefaultPosition() {
        speed = defaultSpeed;
        //Debug.Log("about to send " + gameObject.name + "to default position of " + defaultPosition);
        //if (partOfCurrentPuzzle == true) {
            transform.position = defaultPosition;
            readyToMove = false;
        //}
        

    }
    public void BeginRotating() {
        rotating = true;
    }
    public void EndRotating() {
        rotating = false;
        BeginMovementToDefaultPosition(false, true, false);
    }
    public void ShrinkAndDisappear() {
        shrinking = true;
    }


    private void FixedUpdate()
    {
        if (readyToMove) {
            rotating = false;
            if (curvePath == false) {
                transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
                speed *= speedMultiplier;
                if (speed > speedCap)
                {
                    speed = speedCap;
                }
            } else {            //  https://answers.unity.com/questions/1515853/move-from-a-to-b-using-parabola-with-or-without-it.html

                time += Time.deltaTime * speedForCurve;
                Vector2 pos = Vector2.Lerp(startPosition, destination, time);
                if (curveUpIfTrue) {
                    pos.y += curve.Evaluate(time);      // for curve DOWNWARDS, just subtract instead of add
                } else {
                    pos.y -= curve.Evaluate(time);      
                }
                transform.position = pos;
                speedForCurve *= curveSpeedMultiplier;
            }

            if (Vector2.Distance(transform.position, destination) < 0.1f) {
                readyToMove = false;
                destinationReached = true;
                speed = defaultSpeed;
                if (destination == defaultPosition) {
                    transform.position = defaultPosition;
                } else if (destinationText == "goal") {
                    PuzzleManager.instance.AnimatePuzzleSolved(gameObject, true, false);
                } else if (destinationText == "operator") {

                    // need the 1-circle to NOT execute if it's supposed to be a 2-circle math problem
                    PuzzleManager.instance.ExecuteCompletionOf_oneCircle_Math(true);

                    /// the problem is that this script doesn't know whether it's resolving a 1-circle or 2-circle math problem
                    /// ... so we need a way for a message sent by this script to be effective for both types
                    ///         we don't know which of the 3 circles will be part of the math problem
                    ///         

                    PuzzleManager.instance.SetCircleAsDoneMoving(gameObject);
                    PuzzleManager.instance.ExecuteCompletionOf_twoCircle_Math();        // this will only work if both circles, separately, sent the SetCircleAsDoneMoving()

                } else if (destinationText == "goalThenToilet") {
                    PuzzleManager.instance.AnimatePuzzleFailed(gameObject, true, false);
                } else if (destinationText == "toilet") {
                    PuzzleManager.instance.AnimatePuzzleFailed(gameObject, true, true);
                }
            }
        }
        if (rotating) {
            DoRotationStuff();
        } else {
            // default (slow) rotation speed goes here
            SawBlade_RectTransform.Rotate(0, 0, sawBladeSpeed_default);
            DirtbikeTire_RectTransform.Rotate(0, 0, sawBladeSpeed_default);
        }
        if (changingToWhite) {
            Color.Lerp(initialColor, Color.white, Time.time);
        }
        if (shrinking) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, minScale, Time.deltaTime * speed);
            speed *= speedMultiplier;
            if (speed > speedCap)
            {
                speed = speedCap;
            }

            if (transform.localScale.x <= 0.1f)
            {
                shrinking = false;
                //PuzzleManager.instance.AnimatePuzzleSolved(null, true, true);
                gameObject.SetActive(false);
                transform.localScale = originalScale;
                speed = defaultSpeed;
            }
        }
        if (growing) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, defaultScale, Time.deltaTime * speed);
            speed *= speedMultiplier;
            if (transform.localScale.x >= defaultScale.x) {
                growing = false;
                speed = defaultSpeed;
                transform.localScale = defaultScale;
            }
        }
    }
    public void NotifyPuzzleManagerOfDestinationReached() { 

    }

    private void OnMouseDown()
    {
        PuzzleManager.instance.AcceptClickedCircleOrOperator(gameObject);
    }

    //      ROTATION STUFF
    //  ***************************************************************************************
    public float RotateSpeed = 5f;
    public float Radius = 0.1f;

    //private Vector2 _centre;
    private float _angle;
    private void DoRotationStuff()
    {
        // make the sawblade spin
        SawBlade_RectTransform.Rotate(0, 0, sawBladeSpeed_whenClicked);
        DirtbikeTire_RectTransform.Rotate(0, 0, sawBladeSpeed_whenClicked);


        // make the granddaddy parent object wiggle in a circular motion
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = defaultPosition + offset;
    }
    //  ***************************************************************************************
    public void RandomlyChooseBackgroundImage() {
        int rando = Random.Range(1, 3);
        if (rando == 1) {
            // saw blade
            SawBlade_GameObject.SetActive(true);
            DirtbikeTire_GameObject.SetActive(false);
            Debug.Log("this is: " + gameObject.name + " and saw is active");
        } else if (rando == 2) {
            SawBlade_GameObject.SetActive(false);
            DirtbikeTire_GameObject.SetActive(true);
            Debug.Log("this is: " + gameObject.name + " and tire is active");
        }
    }



}




// DOUBLE CLICK
//private void OnMouseOver()
//{
//    if (Input.GetMouseButtonDown(0))
//    {
//        float timeSinceLastClick = Time.time - lastClickTime;       // https://www.youtube.com/watch?v=9pKXXNgCgq8

//        if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
//        {
//            // do double click actions here
//            GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
//            foreach (GameObject goal in goals)
//            {
//                if (goal.GetComponent<Goal>().goalNumber == valueOfThisThing && goal.GetComponent<Goal>().goalFulfilled == false)
//                {
//                    //transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, Time.deltaTime * 2);

//                    // destroy the circle and change the color of the goal to reflect the fact that it's been fulfilled
//                    goal.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.1f, 0.1f, 1f);
//                    // also, make this goal unable to interact with anything
//                    goal.GetComponent<Goal>().goalFulfilled = true;
//                    //GameManager.instance.AddPoints(1);
//                    //Destroy(gameObject);
//                    gameObject.SetActive(false);
//                    GameManager.instance.backButton.SetActive(true);
//                    GameManager.instance.ReduceScore(1);
//                    GameManager.instance.ContributeToKillFeed("goal", goal.GetComponent<Goal>().goalNumber, 0, 0);
//                    gameManager.checkIfLevelIsOver();
//                    GameManager.instance.TakeSnapshot();



//                    break;
//                }
//            }
//        }
//        else
//        {
//            // do normal click actions here
//        }
//        lastClickTime = Time.time;
//    }
//}





// WIGGLING

//public float wiggleSpeed = 1;
//public float amplitude = 0.5f;
//public int octaves = 4;
//Vector2 vel = Vector2.zero;
//public bool wiggling = false;

//Vector2 wiggleDestination;
//int currentTime = 0;
//void FixedUpdate()
//{
//    if (wiggling == true)
//    {
//        if (currentTime > octaves)
//        {
//            currentTime = 0;
//            wiggleDestination = defaultPosition + generateRandomVector(amplitude);
//            print("new Vector Generated: " + wiggleDestination);
//        }

//        // smoothly moves the object to the random destination
//        transform.position = Vector2.SmoothDamp(transform.position, wiggleDestination, ref vel, wiggleSpeed);

//        currentTime++;
//    }
//    // if number of frames played since last change of direction > octaves create a new destination
//}
//Vector2 generateRandomVector(float amp)
//{
//    Vector2 result = new Vector2();
//    for (int i = 0; i < 2; i++)
//    {
//        float x = Random.Range(-amp, amp);
//        result[i] = x;
//    }
//    return result;
//}