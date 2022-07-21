using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clickable_circle : MonoBehaviour
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
    public float curveHeightMultiplier;

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
    public float circleImageRotationSpeed_default;
    public float circleImageRotationSpeed_whenClicked;
    public GameObject RustyGear_GameObject;
    Animator RustyGear_Animator;
    //RectTransform RustyGear_RectTransform;
    
    bool sawBlade_active;
    bool rustyGear_active;
    bool rustySaw_active;

    public GameObject RustySaw_GameObject;
    Animator RustySaw_Animator;



    private void Awake()
    {
        //Debug.Log("Awake() method being run for " + gameObject.name);

        gameObject.SetActive(true);

        //SawBlade_GameObject.SetActive(true);
        //RustyGear_GameObject.SetActive(true);

        SawBlade_RectTransform = SawBlade_GameObject.GetComponent<RectTransform>();
        //RustyGear_RectTransform = RustyGear_GameObject.GetComponent<RectTransform>();
        RustyGear_Animator = RustyGear_GameObject.GetComponent<Animator>();
        //circleImageRotationSpeed_default = Random.Range(0.1f, 0.3f);
        RustySaw_Animator = RustySaw_GameObject.GetComponent<Animator>();

        //SawBlade_GameObject.SetActive(false);
        //DirtbikeTire_GameObject.SetActive(false);

        //gameObject.SetActive(false);
    }

    private void Start()
    {
        //Debug.Log("Start() method being run for " + gameObject.name);

        gameObject.SetActive(true);

        //SawBlade_GameObject.SetActive(true);
        //RustyGear_GameObject.SetActive(true);

        SawBlade_RectTransform = SawBlade_GameObject.GetComponent<RectTransform>();
        //RustyGear_RectTransform = RustyGear_GameObject.GetComponent<RectTransform>();
        RustyGear_Animator = RustyGear_GameObject.GetComponent<Animator>();
        //circleImageRotationSpeed_default = Random.Range(0.1f, 0.3f);
        RustySaw_Animator = RustySaw_GameObject.GetComponent<Animator>();

        //SawBlade_GameObject.SetActive(false);
        //DirtbikeTire_GameObject.SetActive(false);

        //gameObject.SetActive(false);
    }

    public void BeginMovementToTarget(Vector2 targetDestination, string targetName, bool curvedPath, bool curveUp)
    {
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
    public void BeginMovementToNewDefaultPosition(Vector2 target)
    {
        speed = defaultSpeed;
        defaultPosition = target;
        destination = defaultPosition;
        readyToMove = true;
    }
    public void BeginMovementOffscreenToLeft()
    {
        speed = defaultSpeed;
        curvePath = false;
        destination = transform.position - new Vector3(18, 0, 0);
        readyToMove = true;
        destinationText = "left of screen for nitrous";     // have to change destinationText so it doesn't trigger puzzle solve
    }
    public void BeginMovementToDefaultPosition(bool curvedPath, bool curveUp, bool fadingToWhite)
    {
        changingToWhite = fadingToWhite;
        if (fadingToWhite)
        {
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
    public void SendCircleToToilet(int fallingSpeed)
    {

        curvePath = false;
        curveUpIfTrue = true;
        if (fallingSpeed != 0)
        {
            speed = fallingSpeed;
        }
        else
        {
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
    public void TeleportToDefaultPosition()
    {
        speed = defaultSpeed;
        //Debug.Log("about to send " + gameObject.name + "to default position of " + defaultPosition);
        //if (partOfCurrentPuzzle == true) {
        transform.position = defaultPosition;
        readyToMove = false;
        //}


    }
    public void SetCircleAsPartOfCurrentPuzzle() {
        partOfCurrentPuzzle = true;
    }
    public void negate_SetCircleAsPartOfCurrentPuzzle() {
        partOfCurrentPuzzle = false;
    }
    public void BeginRotating()
    {
        rotating = true;
        // set animation speed to fast clockwise
        circleImageRotationSpeed_whenClicked = Random.Range(5, 10);
        
        if (rustyGear_active) {
            RustyGear_Animator.Play("rustyGear_clockwise_fast", -1, 0f);
            //RustyGear_Animator.speed = circleImageRotationSpeed_whenClicked;
        } else if (sawBlade_active) { 

        } else if (rustySaw_active) {
            RustySaw_Animator.Play("rustySaw_clockwise_fast", -1, 0f);
        }

    }
    public void EndRotating()
    {
        //Debug.Log("we are in EndRotating()");


        rotating = false;
        // set animation speed to slow counter-clockwise
        //circleImageRotationSpeed_default = Random.Range(0.1f, 0.3f);
        circleImageRotationSpeed_default = Random.Range(-0.3f, -0.1f);

        if (rustyGear_active) {
            //RustyGear_Animator.speed = circleImageRotationSpeed_default;
            RustyGear_Animator.Play("rustyGear_counterClockwise_slow", -1, 0f);
        } else if (sawBlade_active) { 

        } else if (rustySaw_active) {
            RustySaw_Animator.Play("rustySaw_counterClockwise_slow", -1, 0f);
        }

        BeginMovementToDefaultPosition(false, true, false);
    }
    public void ShrinkAndDisappear()
    {
        shrinking = true;
    }


    private void FixedUpdate()
    {
        if (readyToMove)
        {
            rotating = false;
            if (curvePath == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
                speed *= speedMultiplier;
                if (speed > speedCap)
                {
                    speed = speedCap;
                }
            }
            else
            {            //  https://answers.unity.com/questions/1515853/move-from-a-to-b-using-parabola-with-or-without-it.html

                time += Time.deltaTime * speedForCurve;
                Vector2 pos = Vector2.Lerp(startPosition, destination, time);
                if (curveUpIfTrue)
                {
                    pos.y += curve.Evaluate(time) * curveHeightMultiplier;      // for curve DOWNWARDS, just subtract instead of add
                }
                else
                {
                    pos.y -= curve.Evaluate(time) * curveHeightMultiplier;
                }
                transform.position = pos;
                speedForCurve *= curveSpeedMultiplier;
            }

            if (Vector2.Distance(transform.position, destination) < 0.3f)
            {
                readyToMove = false;
                destinationReached = true;
                speed = defaultSpeed;
                if (destination == defaultPosition)
                {
                    transform.position = defaultPosition;
                }
                else if (destinationText == "goal")
                {
                    gameObject.SetActive(false);
                    PuzzleManager.instance.AnimatePuzzleSolved(gameObject, true, false);
                }
                else if (destinationText == "operator")
                {

                    // need the 1-circle to NOT execute if it's supposed to be a 2-circle math problem
                    PuzzleManager.instance.ExecuteCompletionOf_oneCircle_Math(true, true);

                    /// the problem is that this script doesn't know whether it's resolving a 1-circle or 2-circle math problem
                    /// ... so we need a way for a message sent by this script to be effective for both types
                    ///         we don't know which of the 3 circles will be part of the math problem
                    ///         

                    PuzzleManager.instance.SetCircleAsDoneMoving(gameObject);
                    PuzzleManager.instance.ExecuteCompletionOf_twoCircle_Math();        // this will only work if both circles, separately, sent the SetCircleAsDoneMoving()

                }
                else if (destinationText == "goalThenToilet")
                {
                    PuzzleManager.instance.AnimatePuzzleFailed(gameObject, true, false);
                }
                else if (destinationText == "toilet")
                {
                    PuzzleManager.instance.AnimatePuzzleFailed(gameObject, true, true);
                }
            }
        }
        if (rotating)
        {
            //RustyGear_Animator.SetFloat("direction", 1);
            DoRotationStuff();

        }
        else
        {
            
            // default (slow) rotation speed goes here
            SawBlade_RectTransform.Rotate(0, 0, circleImageRotationSpeed_default);

            //RustyGear_RectTransform.Rotate(0, 0, circleImageRotationSpeed_default);
        }
        if (changingToWhite)
        {
            Color.Lerp(initialColor, Color.white, Time.time);
        }
        if (shrinking)
        {
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
        if (growing)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, defaultScale, Time.deltaTime * speed);
            speed *= speedMultiplier;
            if (transform.localScale.x >= defaultScale.x)
            {
                growing = false;
                speed = defaultSpeed;
                transform.localScale = defaultScale;
            }
        }
    }
    public void NotifyPuzzleManagerOfDestinationReached()
    {

    }

    private void OnMouseDown()
    {
        //Debug.Log("just clicked on " + gameObject.name);
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
        SawBlade_RectTransform.Rotate(0, 0, circleImageRotationSpeed_whenClicked);

        //RustyGear_RectTransform.Rotate(0, 0, circleImageRotationSpeed_whenClicked);


        // make the granddaddy parent object wiggle in a circular motion
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = defaultPosition + offset;
    }
    public Vector2 ReturnDefaultPosition() {
        return defaultPosition;
    }
    //  ***************************************************************************************
    public void RandomlyChooseBackgroundImage()
    {
        //Start();
        int rando = Random.Range(1, 2);
        if (rando == 1)
        {
            // saw blade
            sawBlade_active = true;
            rustyGear_active = false;
            rustySaw_active = false;
            SawBlade_GameObject.SetActive(true);
            RustyGear_GameObject.SetActive(false);
            RustySaw_GameObject.SetActive(false);
            //Debug.Log("this is: " + gameObject.name + " and saw is active");
        }
        else if (rando == 2)
        {
            // rusty gear
            sawBlade_active = false;
            rustyGear_active = true;
            rustySaw_active = false;
            SawBlade_GameObject.SetActive(false);
            RustyGear_GameObject.SetActive(true);
            RustySaw_GameObject.SetActive(false);
            // set speed
            RustyGear_Animator.Play("rustyGear_counterClockwise_slow", -1, 0f);
            //Debug.Log("this is: " + gameObject.name + " and tire is active");
        }
        else if (rando == 3)
        {
            sawBlade_active = false;
            rustyGear_active = false;
            rustySaw_active = true;
            SawBlade_GameObject.SetActive(false);
            RustyGear_GameObject.SetActive(false);
            RustySaw_GameObject.SetActive(true);
            RustySaw_Animator.Play("rustySaw_counterClockwise_slow", -1, 0f);




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