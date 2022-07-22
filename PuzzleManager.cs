using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    bool gameOver;

    public string gameType;     // this is set by GameManager.cs, in StartTimedGame() and StartEndlessGame(), and is either "endless" or "timed"

    public bool debugMessagesOn = true;    // change this to turn on/off debug.log messages, only changeable via code
    // this toggles whether DebugLOG() works or not
    // this is made irrelevant in GameManager, Debug.unityLogger.logEnabled = true;

    public bool debugModeOn;        // as of now this only affects whether circles & operators are placed randomly instead of the order in which they're created
    GameObject ToggleDebugON;
    GameObject ToggleDebugOFF;
    string debugPlayerPrefString = "debugON";

    public bool negativeNumbersAllowed;
    GameObject ToggleNegativesOFF;
    GameObject ToggleNegativesON;
    string negativesPlayerPrefString = "negativesON";
    int lowerLimitForResult = -125;     // this may change in Start() depending on negativeNumbersON
    int upperLimitForResult = 125;

    // tttppp.... 1 is the limit
    //int lowerLimitForCircleValue = -25;
    int upperLimitForCircleValue = 25;
    int upperLimit_ValueToBeSquared = 11;
    int upperLimit_ValueToBeCubed = 5;

    public bool fractionsAllowed;       // default is set to false because that's what the default is in the interfact, on the toggle
    GameObject ToggleFractionsON;
    GameObject ToggleFractionsOFF;
    string fractionsPlayerPrefString = "fractionsON";

    public bool exponentsAllowed;       // this includes x^2, x^3, x^0.5, x^(1/3)
    GameObject ToggleExponentsON;
    GameObject ToggleExponentsOFF;
    string exponentsPlayerPrefString = "exponentsON";

    public bool multDivideAllowed;
    GameObject ToggleMultDivideON;
    GameObject ToggleMultDivideOFF;
    string multDividePlayerPrefString = "multDivideON";

    public GameObject CircleA;
    public GameObject CircleB;
    public GameObject CircleC;
    float circleDefaultScale = 2;
    public Clickable_circle CircleAScript;
    public Clickable_circle CircleBScript;
    public Clickable_circle CircleCScript;
    //public GameObject MathInProgress;
    public GameObject OperatorA;
    public GameObject OperatorB;

    public float durationOfApertureOpening;
    public float delayBeforeGrabOccurs_shortDistance;
    public float delayBeforeGrabOccurs_longDistance;
    public float delayAfterGrabOccurs;
    bool oneGrabberHasReturned;
    bool grabber1_doneMoving = false;
    bool grabber2_doneMoving = false;

    public GameObject OpA_apertureCLOSED;
    public GameObject OpA_apertureAnimation;
    public GameObject OpA_apertureOPEN;
    public GameObject OpA_grabber_1;
    public GameObject OpA_grabber_2;

    public GameObject OpB_apertureCLOSED;
    public GameObject OpB_apertureAnimation;
    public GameObject OpB_apertureOPEN;
    public GameObject OpB_grabber_1;
    public GameObject OpB_grabber_2;

    float operatorDefaultScale = 2.2f;
    public Clickable_operator OperatorAScript;
    public Clickable_operator OperatorBScript;
    public GameObject Goal;
    float goalDefaultScale = 1.4f;
    // https://store.steampowered.com/app/868270/The_Cycle_Frontier/?snr=1_4_4__118
    public GameObject ExplosionImage;
    public Animator smallExplosionOfGoal;
    public float durationOfSmallExplosion;
    Explosion explosion;

    // if the player solves the puzzle, these strings will be used to update the stats via ChangeStat_Endless() & ChangeStat_Easy()
    string operator1_forStats;
    string operator2_forStats;



    //public GameObject sparkle1;
    //public GameObject sparkle2;
    //public GameObject sparkle3;
    //public GameObject sparkle4;
    //public GameObject sparkle5;
    //public GameObject sparkle6;
    //Sparkle sparkleScript1;
    //Sparkle sparkleScript2;
    //Sparkle sparkleScript3;
    //Sparkle sparkleScript4;
    //Sparkle sparkleScript5;
    //Sparkle sparkleScript6;



    public GameObject vehicle;


    public List<Circle> listOfAllCircles;
    public List<Operator> listOfAllOperators;


    // for performing math operations as the player attempts to solve puzzles
    bool circle1selected = false;
    public GameObject highlightedCircle1;
    float value1;

    public bool operatorSelected = false;
    public GameObject highlightedOperator;

    bool circle2selected = false;
    public GameObject highlightedCircle2;
    float value2;

    Color whiteColor = new Color(1, 1, 1, 1);
    Color highlightedColor = new Color(0.4f, 0.6f, 0.4f, 1);

    bool math_oneCircle_IsComplete = false;
    bool math_twoCircle_IsComplete = false;

    //bool firstPuzzleStarted = false;

    public GameObject GlobalTimer;
    public GameObject PuzzleTimer;
    public GameObject PuzzleTimerThatMoves;
    TimerGlobal timerGlobal;
    TimerPuzzle timerPuzzle;
    TimerPuzzle timerPuzzleThatMoves;
    BonusTimeNotify bonusTimeNotify;



    bool executingONEcircleMath;
    bool executingTWOcircleMath;

    //bool circle1StartedMoving = false;
    //bool circle2StartedMoving = false;
    bool circle1DoneMoving = false;
    bool circle2DoneMoving = false;


    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        listOfAllCircles = new List<Circle>();
        listOfAllOperators = new List<Operator>();
        timerGlobal = GlobalTimer.GetComponent<TimerGlobal>();
        timerPuzzle = PuzzleTimer.GetComponent<TimerPuzzle>();
        timerPuzzleThatMoves = PuzzleTimerThatMoves.GetComponent<TimerPuzzle>();
        bonusTimeNotify = PuzzleTimerThatMoves.GetComponent<BonusTimeNotify>();

        CircleAScript = CircleA.GetComponent<Clickable_circle>();
        CircleBScript = CircleB.GetComponent<Clickable_circle>();
        CircleCScript = CircleC.GetComponent<Clickable_circle>();
        OperatorAScript = OperatorA.GetComponent<Clickable_operator>();
        OperatorBScript = OperatorB.GetComponent<Clickable_operator>();
        explosion = ExplosionImage.GetComponent<Explosion>();

        CircleA.SetActive(true);
        CircleB.SetActive(true);
        CircleC.SetActive(true);
        CircleA.SetActive(false);
        CircleB.SetActive(false);
        CircleC.SetActive(false);       // doing this because for some reason CircleC's Start() method is not being called when game is first loaded
                                        //      ... and the circle's background image wasn't loading on the first puzzle, but it WAS loading in subsequent puzzles




        // toggles
        ToggleDebugON = GameObject.FindGameObjectWithTag("ToggleDebugON");      // the FindGameObjectWithTag function only works if the object is ACTIVE
        ToggleDebugOFF = GameObject.FindGameObjectWithTag("ToggleDebugOFF");
        ToggleNegativesON = GameObject.FindGameObjectWithTag("ToggleNegativesON");
        ToggleNegativesOFF = GameObject.FindGameObjectWithTag("ToggleNegativesOFF");
        ToggleFractionsON = GameObject.FindGameObjectWithTag("ToggleFractionsON");
        ToggleFractionsOFF = GameObject.FindGameObjectWithTag("ToggleFractionsOFF");
        ToggleExponentsON = GameObject.FindGameObjectWithTag("ToggleExponentsON");
        ToggleExponentsOFF = GameObject.FindGameObjectWithTag("ToggleExponentsOFF");
        ToggleMultDivideON = GameObject.FindGameObjectWithTag("ToggleMultDivideON");
        ToggleMultDivideOFF = GameObject.FindGameObjectWithTag("ToggleMultDivideOFF");

        // use playerPrefs to load the toggle values        
        if (PlayerPrefs.HasKey(debugPlayerPrefString) && PlayerPrefs.GetInt(debugPlayerPrefString) == 1)
        {
            TurnDebugModeON();
            ToggleDebugON.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            TurnDebugModeOFF();
            ToggleDebugOFF.GetComponent<Toggle>().isOn = true;
        }
        if (PlayerPrefs.HasKey(negativesPlayerPrefString) && PlayerPrefs.GetInt(negativesPlayerPrefString) == 1)
        {
            TurnNegativeNumbersON();
            ToggleNegativesON.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            TurnNegativeNumbersOFF();
            ToggleNegativesOFF.GetComponent<Toggle>().isOn = true;
        }
        if (PlayerPrefs.HasKey(fractionsPlayerPrefString) && PlayerPrefs.GetInt(fractionsPlayerPrefString) == 1)
        {
            TurnFractionsON();
            ToggleFractionsON.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            TurnFractionsOFF();
            ToggleFractionsOFF.GetComponent<Toggle>().isOn = true;
        }
        if (PlayerPrefs.HasKey(exponentsPlayerPrefString) && PlayerPrefs.GetInt(exponentsPlayerPrefString) == 1)
        {
            TurnExponentsON();
            ToggleExponentsON.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            TurnExponentsOFF();
            ToggleExponentsOFF.GetComponent<Toggle>().isOn = true;
        }
        if (PlayerPrefs.HasKey(multDividePlayerPrefString) && PlayerPrefs.GetInt(multDividePlayerPrefString) == 1)
        {
            TurnMultDivideON();
            ToggleMultDivideON.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            TurnMultDivideOFF();
            ToggleMultDivideOFF.GetComponent<Toggle>().isOn = true;
        }


        //sparkleScript1 = sparkle1.GetComponent<Sparkle>();
        //sparkleScript2 = sparkle2.GetComponent<Sparkle>();
        //sparkleScript3 = sparkle3.GetComponent<Sparkle>();
        //sparkleScript4 = sparkle4.GetComponent<Sparkle>();
        //sparkleScript5 = sparkle5.GetComponent<Sparkle>();
        //sparkleScript6 = sparkle6.GetComponent<Sparkle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class OpsAndCircles
    {
        //Vector2 position;
    }
    public class Operator : OpsAndCircles
    {
        public string type;
        public static int numberOfThisTypeThatExist;
        public int IDnumber;
        Vector2 position;
        float input1;
        float input2;
        float output;
        public char AorBorC;
        public string operatorGameObject_associatedWith;

        public Operator(string name, char ABC)
        {
            type = name;
            AorBorC = ABC;
            IDnumber = numberOfThisTypeThatExist;
            numberOfThisTypeThatExist += 1;
            position = new Vector2(0, 0);
        }

    }
    public class Circle : OpsAndCircles
    {
        public float value;
        public bool trueInt_falseFraction;
        public int numerator;
        public int denominator;
        public static int numberOfThisTypeThatExist;
        public int IDnumber;
        Vector2 position;
        public string circleGameObject_associatedWith;

        public Circle(float val)
        {
            //Debug.Log("about to create a new circle, with value: " + val);
            value = val;
            trueInt_falseFraction = true;
            IDnumber = numberOfThisTypeThatExist;
            numberOfThisTypeThatExist += 1;
            position = new Vector2(0, 0);
        }
        public Circle(float numerator, int denom)
        {
            value = numerator / denom;
            numerator = (int)numerator;
            denominator = denom;
            trueInt_falseFraction = false;
            IDnumber = numberOfThisTypeThatExist;
            numberOfThisTypeThatExist += 1;
            position = new Vector2(0, 0);
        }
    }


    public Operator PickRandomOperator(char ABC, bool secondOperatorForKiddyModeTrueOrFalse, Operator firstOperatorForKiddyMode)
    {
        string name = "";
        bool finished = false;

        string firstOperator_2or1;

        string TestWhetherONEorTWOcircleOperator(string opType)
        {

            if (opType == "addition" || opType == "subtraction" || opType == "multiplication" || opType == "division")
            {
                return "2circle";
            }
            else
            {
                return "1circle";
            }

        }
        if (secondOperatorForKiddyModeTrueOrFalse == true)
        {
            firstOperator_2or1 = TestWhetherONEorTWOcircleOperator(firstOperatorForKiddyMode.type);
        }
        else
        {
            firstOperator_2or1 = "";
        }

        while (finished == false)
        {
            if (multDivideAllowed == true && exponentsAllowed == true)
            {
                // then pick one of the 8
                int randomInt = Random.Range(1, 9);
                switch (randomInt)
                {
                    case 1:
                        name = "addition"; break;
                    case 2:
                        name = "subtraction"; break;
                    case 3:
                        name = "multiplication"; break;
                    case 4:
                        name = "division"; break;
                    case 5:
                        name = "exponent2"; break;
                    case 6:
                        name = "exponent3"; break;
                    case 7:
                        name = "squareRoot"; break;
                    case 8:
                        name = "cubeRoot"; break;
                }
                if (secondOperatorForKiddyModeTrueOrFalse == true && name != firstOperatorForKiddyMode.type && firstOperator_2or1 == TestWhetherONEorTWOcircleOperator(name))
                {
                    finished = true;
                }
                else if (secondOperatorForKiddyModeTrueOrFalse == false)
                {
                    finished = true;
                }
            }
            else if (multDivideAllowed == false && exponentsAllowed == true)
            {
                // only 6 are available
                int randomInt = Random.Range(1, 7);
                switch (randomInt)
                {
                    case 1:
                        name = "addition"; break;
                    case 2:
                        name = "subtraction"; break;
                    //case 3:
                    //name = "multiplication"; break;
                    //case 4:
                    //name = "division"; break;
                    case 3:
                        name = "exponent2"; break;
                    case 4:
                        name = "exponent3"; break;
                    case 5:
                        name = "squareRoot"; break;
                    case 6:
                        name = "cubeRoot"; break;
                }
                if (secondOperatorForKiddyModeTrueOrFalse == true && name != firstOperatorForKiddyMode.type && firstOperator_2or1 == TestWhetherONEorTWOcircleOperator(name))
                {
                    finished = true;
                }
                else if (secondOperatorForKiddyModeTrueOrFalse == false)
                {
                    finished = true;
                }
            }
            else if (multDivideAllowed == true && exponentsAllowed == false)
            {
                // only 4 are available
                int randomInt = Random.Range(1, 5);
                switch (randomInt)
                {
                    case 1:
                        name = "addition"; break;
                    case 2:
                        name = "subtraction"; break;
                    case 3:
                        name = "multiplication"; break;
                    case 4:
                        name = "division"; break;
                        //case 5:
                        //    name = "exponent2"; break;
                        //case 6:
                        //    name = "exponent3"; break;
                        //case 7:
                        //    name = "squareRoot"; break;
                        //case 8:
                        //    name = "cubeRoot"; break;
                }
                if (secondOperatorForKiddyModeTrueOrFalse == true && name != firstOperatorForKiddyMode.type)
                {
                    finished = true;
                }
                else if (secondOperatorForKiddyModeTrueOrFalse == false)
                {
                    finished = true;
                }
            }
            else if (multDivideAllowed == false && exponentsAllowed == false)
            {
                // only 2 are availabe   
                int randomInt = Random.Range(1, 3);
                switch (randomInt)
                {
                    case 1:
                        name = "addition"; break;
                    case 2:
                        name = "subtraction"; break;
                        //case 3:
                        //    name = "multiplication"; break;
                        //case 4:
                        //    name = "division"; break;
                        //case 5:
                        //    name = "exponent2"; break;
                        //case 6:
                        //    name = "exponent3"; break;
                        //case 7:
                        //    name = "squareRoot"; break;
                        //case 8:
                        //    name = "cubeRoot"; break;
                }
                if (secondOperatorForKiddyModeTrueOrFalse == true && name != firstOperatorForKiddyMode.type)
                {
                    finished = true;
                }
                else if (secondOperatorForKiddyModeTrueOrFalse == false)
                {
                    finished = true;
                }
            }
        }

        // tttppp
        //name = "division";        // use this line for bug hunting
        Operator temp = new Operator(name, ABC);
        listOfAllOperators.Add(temp);
        return temp;
    }

    public List<float> CreateListOfPossibleCircleValues_forArithmetic(float maxValue, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed)
    {
        List<float> toReturn = new List<float>();

        if (negativesAllowed)
        {
            for (int i = (int)maxValue * -1; i <= -2; i++)
            {
                toReturn.Add(i);
            }
        }
        if (negativesAllowed && oneAllowed)
        {
            toReturn.Add(-1);
        }
        if (negativesAllowed && fractionsAllowed)
        {
            // 
            toReturn.Add(-0.7500f);   // -3/4
            toReturn.Add(-2f / 3f);   // -2/3
            toReturn.Add(-0.5f);    // -1/2
            toReturn.Add(-1f / 3f);   // -1/3
            toReturn.Add(-0.25f);   // -1/4
        }
        if (fractionsAllowed)
        {
            toReturn.Add(0.25f);
            toReturn.Add(1f / 3f);
            toReturn.Add(0.5f);
            toReturn.Add(2f / 3f);
            toReturn.Add(0.75f);
        }
        if (oneAllowed)
        {
            toReturn.Add(1);
        }
        for (int i = 2; i <= (int)maxValue; i++)
        {
            toReturn.Add(i);
        }

        return toReturn;
    }

    public List<float> CreateListOfPossibleCircleValues_forExponent2(float resultCap, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed)
    {
        List<float> toReturn = new List<float>();
        int squareRootOfResultCap = Mathf.RoundToInt(Mathf.Pow(resultCap, 0.5f));
        if (negativesAllowed)
        {
            for (int i = (squareRootOfResultCap * -1); i <= -2; i++)
            {
                toReturn.Add(i);
            }
        }
        if (negativesAllowed && oneAllowed)
        {
            toReturn.Add(-1);
        }
        if (negativesAllowed && fractionsAllowed)
        {
            //toReturn.Add(-0.7500f);   // -3/4
            //toReturn.Add(-0.6666f);   // -2/3
            toReturn.Add(-0.5f);    // -1/2     because (-0.5)^2 = 0.25
            //toReturn.Add(-0.3333f);   // -1/3
            //toReturn.Add(-0.25f);   // -1/4
        }
        if (fractionsAllowed)
        {
            //toReturn.Add(0.25f);
            //toReturn.Add(0.3333f);
            toReturn.Add(0.5f);
            //toReturn.Add(0.6666f);
            //toReturn.Add(0.75f);
        }
        if (oneAllowed)
        {
            toReturn.Add(1);
        }
        for (int i = 2; i <= squareRootOfResultCap; i++)
        {
            toReturn.Add(i);
        }
        //for (int i = 0; i < toReturn.Count; i++) {
        //    Debug.Log("**************************************************************** CreateListOfPossibleCircleValues_forExponent2: " + toReturn[i]);
        //}
        return toReturn;
    }

    public List<float> CreateListOfPossibleCircleValues_forExponent3(float resultCap, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed)
    {
        List<float> toReturn = new List<float>();
        //int squareRootOfResultCap = (int)Mathf.Pow(resultCap, 0.5f);

        int cubeRootOfResultCap = Mathf.RoundToInt(Mathf.Pow(resultCap, 1f / 3f));



        if (negativesAllowed)
        {
            for (int i = (cubeRootOfResultCap * -1); i <= -2; i++)
            {
                toReturn.Add(i);
            }
        }
        if (negativesAllowed && oneAllowed)
        {
            toReturn.Add(-1);
        }
        //if (negativesAllowed && fractionsAllowed)
        //{
        //    //toReturn.Add(-0.7500f);   // -3/4
        //    //toReturn.Add(-0.6666f);   // -2/3
        //    //toReturn.Add(-0.5f);    // -1/2     
        //    //toReturn.Add(-0.3333f);   // -1/3
        //    //toReturn.Add(-0.25f);   // -1/4
        //}
        //if (fractionsAllowed)
        //{
        //    //toReturn.Add(0.25f);
        //    //toReturn.Add(0.3333f);
        //    //toReturn.Add(0.5f);
        //    //toReturn.Add(0.6666f);
        //    //toReturn.Add(0.75f);
        //}
        if (oneAllowed)
        {
            toReturn.Add(1);
        }
        for (int i = 2; i <= cubeRootOfResultCap; i++)
        {
            toReturn.Add(i);
        }
        return toReturn;
    }

    public List<float> CreateListOfPossibleCircleValues_forSquareRoot(float maxValue, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed)
    {
        List<float> toReturn = new List<float>();

        if (fractionsAllowed)
        {
            toReturn.Add(0.25f);
            //toReturn.Add(0.3333f);
            //toReturn.Add(0.5f);
            //toReturn.Add(0.6666f);
            //toReturn.Add(0.75f);
        }
        if (oneAllowed)
        {
            toReturn.Add(1);
        }
        for (int i = 2; i <= maxValue; i++)
        {
            toReturn.Add(Mathf.RoundToInt(Mathf.Pow(i, 2)));
        }
        return toReturn;
    }

    public List<float> CreateListOfPossibleCircleValues_forCubeRoot(float maxValue, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed)
    {
        List<float> toReturn = new List<float>();
        // maxValue is probably going to be 6, because 6^3 = 216 and we don't want to go higher than that
        if (negativesAllowed)
        {
            for (int i = (int)(maxValue * -1); i <= -2; i++)
            {
                toReturn.Add(Mathf.RoundToInt(Mathf.Pow(i, 3)));
            }
        }
        if (negativesAllowed && oneAllowed)
        {
            toReturn.Add(-1);
        }
        //if (negativesAllowed && fractionsAllowed)
        //{
        //    //toReturn.Add(-0.7500f);   // -3/4
        //    //toReturn.Add(-0.6666f);   // -2/3
        //    //toReturn.Add(-0.5f);    // -1/2     
        //    //toReturn.Add(-0.3333f);   // -1/3
        //    //toReturn.Add(-0.25f);   // -1/4
        //}
        //if (fractionsAllowed)
        //{
        //    //toReturn.Add(0.25f);
        //    //toReturn.Add(0.3333f);
        //    //toReturn.Add(0.5f);
        //    //toReturn.Add(0.6666f);
        //    //toReturn.Add(0.75f);
        //}
        if (oneAllowed)
        {
            toReturn.Add(1);
        }
        for (int i = 2; i <= maxValue; i++)
        {
            toReturn.Add(Mathf.RoundToInt(Mathf.Pow(i, 3)));
        }
        return toReturn;
    }

    public bool TestIfIsInteger(float value)
    {
        Debug.Log("we are in TestIfIsInteger(), value: " + value);
        float marginErr = 0.001f;
        bool toReturn = false;
        if (Mathf.Abs(value - Mathf.RoundToInt(value)) < marginErr)
        {
            Debug.Log(value + " IS an integer");
            toReturn = true;
        }
        else
        {
            Debug.Log(value + " is not an integer");
        }
        return toReturn;
    }



    public Circle CreateRandomCircle(List<float> values)
    {
        int randomIndex = Random.Range(0, values.Count);
        float num = values[randomIndex];
        //bool isNegative = false;
        //if (num < 0) {
        //    isNegative = true;
        //}
        Circle temp;
        //if (!TestIfIsInteger(num)) 
        //{
        //    float margin = 0.001f;
        //    float numerator = 0;
        //    int denominator = 0;
        //    if (Mathf.Abs(0.75f - Mathf.Abs(num)) < margin) {
        //        numerator = 3;
        //        denominator = 4;
        //    } else if (Mathf.Abs(2f/3f - Mathf.Abs(num)) < margin) {
        //        numerator = 2;
        //        denominator = 3;
        //    } else if (Mathf.Abs(0.5f - Mathf.Abs(num)) < margin) {
        //        numerator = 1;
        //        denominator = 2;
        //    } else if (Mathf.Abs(1f/3f - Mathf.Abs(num)) < margin) {
        //        numerator = 1;
        //        denominator = 3;
        //    } else if (Mathf.Abs(0.25f - Mathf.Abs(num)) < margin) {
        //        numerator = 1;
        //        denominator = 4;
        //    } else {
        //        Debug.Log("glitch glitch");
        //    }
        //    if (isNegative) {
        //        temp = new Circle(-1 * numerator, denominator);
        //        listOfAllCircles.Add(temp);
        //    } else {
        //        temp = new Circle(numerator, denominator);
        //        listOfAllCircles.Add(temp);
        //    }

        //} 
        //else 
        //{
        temp = new Circle(num);
        listOfAllCircles.Add(temp);
        //}
        return temp;
    }

    public Circle CreateSpecificCircle(float num)
    {
        DEBUG_outputCircleValues("At beginning of CreateSpecificCircle()");
        //bool isNegative = false;
        //if (num < 0)
        //{
        //    isNegative = true;
        //}
        Circle temp;
        //if (!TestIfIsInteger(num) && (Mathf.Abs(num) == 0.25f || CheckIfNumbersAreCloseEnough(Mathf.Abs(num), 1f / 3f) || Mathf.Abs(num) == 0.5f || CheckIfNumbersAreCloseEnough(Mathf.Abs(num), 2f / 3f) || Mathf.Abs(num) == 0.75f)) 
        //{
        //    float margin = 0.001f;
        //    float numerator = 0;
        //    int denominator = 0;
        //    if (Mathf.Abs(0.75f - Mathf.Abs(num)) < margin)
        //    {
        //        numerator = 3;
        //        denominator = 4;
        //    }
        //    else if (Mathf.Abs(2f/3f - Mathf.Abs(num)) < margin)
        //    {
        //        numerator = 2;
        //        denominator = 3;
        //    }
        //    else if (Mathf.Abs(0.5f - Mathf.Abs(num)) < margin)
        //    {
        //        numerator = 1;
        //        denominator = 2;
        //    }
        //    else if (Mathf.Abs(1f/3f - Mathf.Abs(num)) < margin)
        //    {
        //        numerator = 1;
        //        denominator = 3;
        //    }
        //    else if (Mathf.Abs(0.25f - Mathf.Abs(num)) < margin)
        //    {
        //        numerator = 1;
        //        denominator = 4;
        //    }

        //    if (isNegative) {
        //        Debug.Log("checkpoint alpha, value: " + num);
        //        temp = new Circle(-1 * numerator, denominator);
        //        listOfAllCircles.Add(temp);
        //    } else {
        //        Debug.Log("checkpoint bravo, value: " + num);
        //        temp = new Circle(numerator, denominator);
        //        listOfAllCircles.Add(temp);
        //    }
        //} 
        //else 
        //{
        Debug.Log("checkpoint delta, value: " + num);
        temp = new Circle(num);
        listOfAllCircles.Add(temp);
        //}
        DEBUG_outputCircleValues("At end of CreateSpecificCircle()");
        return temp;
    }

    public Circle CreateRandomSecondCircleThatResultsInInt(Operator op, Circle firstCircle, int resultCap, float maxValue, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed)
    {
        List<float> notSureIfPossibleValues = CreateListOfPossibleCircleValues_forArithmetic(maxValue, oneAllowed, negativesAllowed, fractionsAllowed);
        List<float> possibleValues = new List<float>();
        float result = 0;
        for (int i = 0; i < notSureIfPossibleValues.Count; i++)
        {
            if (op.type == "addition")
            {
                result = firstCircle.value + notSureIfPossibleValues[i];      // addition
            }
            else if (op.type == "subtraction")
            {
                //Debug.Log("about to test " + notSureIfPossibleValues[i]);
                result = firstCircle.value - notSureIfPossibleValues[i];      // subtraction
                //Debug.Log("        result: " + firstCircle.value + " + " + notSureIfPossibleValues[i] + " = " + result);
            }
            else if (op.type == "multiplication")
            {
                result = firstCircle.value * notSureIfPossibleValues[i];      // multiplication
            }
            else if (op.type == "division")
            {
                result = firstCircle.value / notSureIfPossibleValues[i];      // division
            }

            if (result >= lowerLimitForResult && result <= upperLimitForResult && TestIfIsInteger(result) && (Mathf.RoundToInt(result) != 0))
            {
                possibleValues.Add(notSureIfPossibleValues[i]);
                //Debug.Log("just added " + notSureIfPossibleValues[i] + "to possibleValues list");
                //Debug.Log("Mathf.Abs(result - 0) = " + Mathf.Abs(result - 0));
            }
        }

        //Debug.Log("for circle 2: possible values: ");
        //for (int i = 0; i < possibleValues.Count; i++)
        //{
        //    Debug.Log(possibleValues[i]);
        //}

        // now pick a random value
        //Debug.Log("about to try to create a random second circle using operator: " + op.type + " and circle1 value: " + firstCircle.value);
        //Debug.Log("list of possible values is of length: " + possibleValues.Count);
        //for (int i = 0; i < possibleValues.Count; i++) {
        //    Debug.Log(possibleValues[i]);
        //}
        Circle temp = CreateRandomCircle(possibleValues);
        return temp;
    }

    public Circle CreateResultCircle(Circle circle1, Circle circle2, Operator opera)
    {
        DEBUG_outputCircleValues("at beginning of CreateResultCircle()");

        float result = 0;
        Circle temp;
        if (opera.type == "addition")
        {
            result = circle1.value + circle2.value;
        }
        else if (opera.type == "subtraction")
        {
            //DEBUG_outputCircleValues("in subtraction section, before assigning result, of CreateResultCircle()");
            result = circle1.value - circle2.value;
            Debug.Log("result: " + result);
            //DEBUG_outputCircleValues("in subtraction section, after assigning result, of CreateResultCircle()");
        }
        else if (opera.type == "multiplication")
        {
            result = circle1.value * circle2.value;
        }
        else if (opera.type == "division")
        {
            result = circle1.value / circle2.value;
        }
        else if (opera.type == "exponent2")
        {
            result = Mathf.Pow(circle1.value, 2);
            if (TestIfIsInteger(result))
            {
                result = Mathf.RoundToInt(result);
            }




        }
        else if (opera.type == "exponent3")
        {
            result = Mathf.Pow(circle1.value, 3);
            if (TestIfIsInteger(result))
            {
                result = Mathf.RoundToInt(result);
            }



            if (TestIfIsInteger(result))
            {
                result = Mathf.RoundToInt(Mathf.Pow(circle1.value, 3));
            }
            else
            {
                result = Mathf.Pow(circle1.value, 3);
            }
        }
        else if (opera.type == "squareRoot")
        {
            result = Mathf.Pow(circle1.value, 0.5f);
            if (TestIfIsInteger(result))
            {
                result = Mathf.RoundToInt(result);
            }




        }
        else if (opera.type == "cubeRoot")
        {
            float value = circle1.value;
            if (value < 0)
            {
                result = -Mathf.Pow(-value, 1f / 3f);
                Debug.Log(" (((((((((((((((((((((((((((((((((((((((((((((((( just did cubeRoot, value: " + value + ", and result: " + result);
            }
            else if (value > 0)
            {
                result = Mathf.Pow(value, 1f / 3f);
            }
            else
            {
                Debug.Log("problem: the circle value is zero");
            }
            if (TestIfIsInteger(result))
            {
                result = Mathf.RoundToInt(result);
            }


        }
        Debug.Log("checkpoint charlie, result: " + result);
        temp = CreateSpecificCircle(result);
        DEBUG_outputCircleValues("at end of CreateResultCircle()");
        return temp;
    }

    // ************************************************************************************************************
    // ************************************************************************************************************

    public List<OpsAndCircles> CreatePartB_GivenInitial(Circle newCircle1, Operator operatorAlreadyUsed)
    {

        // List<OpsAndCircles>

        List<OpsAndCircles> toReturn = new List<OpsAndCircles>();

        List<string> operatorList = new List<string>();

        //  start with newCircle1
        //      look at each operator
        //          given newCircle1, and given this operator, which newCircle2 values will give us ***int*** Results that lie within the acceptable range?
        //          the newCircle2 values themselves must also be acceptable

        // define acceptable Result range
        //      between -225 & 225, for now

        // define acceptable newCircle2 range

        List<float> usableCircle2Values_Addition = new List<float>();
        List<float> usableCircle2Values_Subtraction = new List<float>();
        List<float> usableCircle2Values_Multiplication = new List<float>();
        List<float> usableCircle2Values_Division = new List<float>();

        List<float> circle2ValuesToConsider_Add_Subtract = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, true, negativeNumbersAllowed, fractionsAllowed);
        List<float> circle2ValuesToConsider_Mult_Divide = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, false, negativeNumbersAllowed, fractionsAllowed);

        void ConsiderAddition(Circle circle1)
        {
            float value = circle1.value;

            for (int i = 0; i < circle2ValuesToConsider_Add_Subtract.Count; i++)
            {
                float result = value + circle2ValuesToConsider_Add_Subtract[i];
                if (result >= lowerLimitForResult && result <= upperLimitForResult && TestIfIsInteger(result) && result != 0)
                {
                    usableCircle2Values_Addition.Add(circle2ValuesToConsider_Add_Subtract[i]);
                }
            }
            if (usableCircle2Values_Addition.Count > 0)
            {
                operatorList.Add("addition");
            }
        }
        void ConsiderSubtraction(Circle circle1)
        {
            float value = circle1.value;

            for (int i = 0; i < circle2ValuesToConsider_Add_Subtract.Count; i++)
            {
                float result = value - circle2ValuesToConsider_Add_Subtract[i];
                //Debug.Log("the result we're about to test is: " + result);
                if (result >= lowerLimitForResult && result <= upperLimitForResult && TestIfIsInteger(result) && result != 0)
                {
                    usableCircle2Values_Subtraction.Add(circle2ValuesToConsider_Add_Subtract[i]);
                    //Debug.Log("it appears that " + result + " is an acceptable potential result... lowerLimitForResult: " + lowerLimitForResult);
                }
            }
            if (usableCircle2Values_Subtraction.Count > 0)
            {
                operatorList.Add("subtraction");
            }

            // we're getting negative result numbers even though negativeNumbers are turned off, so we need to debug:
            //Debug.Log("eligible subtraction values: ");
            //for (int i = 0; i < usableCircle2Values_Subtraction.Count; i++) {
            //    Debug.Log(usableCircle2Values_Subtraction[i]);
            //}



        }
        void ConsiderMultiplication(Circle circle1)
        {
            float value = circle1.value;

            for (int i = 0; i < circle2ValuesToConsider_Mult_Divide.Count; i++)
            {
                float result = value * circle2ValuesToConsider_Mult_Divide[i];
                if (result >= lowerLimitForResult && result <= upperLimitForResult && TestIfIsInteger(result) && result != 0)
                {
                    usableCircle2Values_Multiplication.Add(circle2ValuesToConsider_Mult_Divide[i]);
                }
            }
            if (usableCircle2Values_Multiplication.Count > 0)
            {
                operatorList.Add("multiplication");
            }

        }
        void ConsiderDivision(Circle circle1)
        {
            float value = circle1.value;

            for (int i = 0; i < circle2ValuesToConsider_Mult_Divide.Count; i++)
            {
                float result = value / circle2ValuesToConsider_Mult_Divide[i];
                if (result >= lowerLimitForResult && result <= upperLimitForResult && TestIfIsInteger(result) && result != 0)
                {
                    usableCircle2Values_Division.Add(circle2ValuesToConsider_Mult_Divide[i]);
                }
            }
            if (usableCircle2Values_Division.Count > 0)
            {
                operatorList.Add("division");
            }

        }
        void ConsiderExponent2(Circle circle1)
        {
            float value = circle1.value;
            // see if *value* is on the list of usable circle1s for exponent2
            List<float> usableCircle1ValuesForExponent2 = CreateListOfPossibleCircleValues_forExponent2(upperLimitForResult, true, negativeNumbersAllowed, false);
            if (usableCircle1ValuesForExponent2.Contains(value))
            {
                //Debug.Log("the result from partA was: " + value + ", and we just added exponent2 as a potential operator for partB");
                operatorList.Add("exponent2");
            }
        }
        void ConsiderExponent3(Circle circle1)
        {
            float value = circle1.value;
            // see if *value* is on the list of usable circle1s for exponent3
            List<float> usableCircle1ValuesForExponent3 = CreateListOfPossibleCircleValues_forExponent3(upperLimitForResult, true, negativeNumbersAllowed, fractionsAllowed);
            if (usableCircle1ValuesForExponent3.Contains(value))
            {
                operatorList.Add("exponent3");
            }
        }
        void ConsiderSquareRoot(Circle circle1)
        {
            float value = circle1.value;

            List<float> usableCircle1ValuesForSquareRoot = CreateListOfPossibleCircleValues_forSquareRoot(upperLimit_ValueToBeSquared, true, false, fractionsAllowed);
            if (usableCircle1ValuesForSquareRoot.Contains(value))
            {
                operatorList.Add("squareRoot");
            }
        }
        void ConsiderCubeRoot(Circle circle1)
        {
            float value = circle1.value;
            List<float> usableCircle1ValuesForCubeRoot = CreateListOfPossibleCircleValues_forCubeRoot(upperLimit_ValueToBeCubed, true, negativeNumbersAllowed, fractionsAllowed);
            if (usableCircle1ValuesForCubeRoot.Contains(value))
            {
                operatorList.Add("cubeRoot");
            }
        }
        void ConsiderAllOperators(Circle circle1)
        {
            string otherOperator = operatorAlreadyUsed.type;
            // tttppp
            ConsiderAddition(circle1);
            // tttppp
            ConsiderSubtraction(circle1);
            if (multDivideAllowed == true)
            {
                // tttppp
                ConsiderMultiplication(circle1);
                ConsiderDivision(circle1);
            }
            if (exponentsAllowed == true)
            {
                if (otherOperator != "squareRoot")
                {
                    // tttppp
                    ConsiderExponent2(circle1);
                }
                if (otherOperator != "cubeRoot")
                {
                    // tttppp
                    ConsiderExponent3(circle1);
                }
                if (otherOperator != "exponent2")
                {
                    // tttppp
                    ConsiderSquareRoot(circle1);
                }
                if (otherOperator != "exponent3")
                {
                    // tttppp
                    ConsiderCubeRoot(circle1);
                }
            }
        }
        ConsiderAllOperators(newCircle1);

        // select the operator from operatorList, and add it to toReturn
        string operatorToUse = "";
        int rando = Random.Range(0, operatorList.Count);
        operatorToUse = operatorList[rando];
        Operator newOperator = new Operator(operatorToUse, 'B');
        listOfAllOperators.Add(newOperator);


        float value = 0;
        // select the newCircle2, and add it to toReturn
        if (operatorToUse == "addition")
        {
            int randomm = Random.Range(0, usableCircle2Values_Addition.Count);
            value = usableCircle2Values_Addition[randomm];
        }
        else if (operatorToUse == "subtraction")
        {
            int randomm = Random.Range(0, usableCircle2Values_Subtraction.Count);
            value = usableCircle2Values_Subtraction[randomm];
        }
        else if (operatorToUse == "multiplication")
        {
            int randomm = Random.Range(0, usableCircle2Values_Multiplication.Count);
            value = usableCircle2Values_Multiplication[randomm];
        }
        else if (operatorToUse == "division")
        {
            int randomm = Random.Range(0, usableCircle2Values_Division.Count);
            value = usableCircle2Values_Division[randomm];
        }
        else if (operatorToUse == "exponent2")
        {
            // no circle 2 needed
        }
        else if (operatorToUse == "exponent3")
        {
            // no circle 2 needed
        }
        else if (operatorToUse == "squareRoot")
        {
            // no circle 2 needed
        }
        else if (operatorToUse == "cubeRoot")
        {
            // no circle 2 needed
        }

        Circle partB_Circle2 = null;
        if (operatorToUse == "addition" || operatorToUse == "subtraction" || operatorToUse == "multiplication" || operatorToUse == "division")
        {
            partB_Circle2 = CreateSpecificCircle(value);
            Debug.Log("PartB newCircle2 value: " + partB_Circle2.value);
        }

        Debug.Log("PartB operatorToUse: " + newOperator.type);

        // obtain result
        Circle partB_result = CreateResultCircle(newCircle1, partB_Circle2, newOperator);
        Debug.Log("PartB result: " + partB_result.value);

        toReturn.Add(newOperator);
        toReturn.Add(partB_Circle2);
        toReturn.Add(partB_result);
        return toReturn;
    }

    public List<OpsAndCircles> CreatePartA_GivenInitial(Circle potentialResult1, Circle potentialResult2, Operator operatorAlreadyUsed)
    {
        //Debug.Log("about to start building PartA, given PartB");

        List<OpsAndCircles> toReturn = new List<OpsAndCircles>();

        // which of these two circles will we choose as the Result of PartA, which is what we're about to create

        // we want to pick the one that gives us the most options

        // let's look at potentialResultA
        //      which operators can give us this as a result?

        // GIVEN THAT WE KNOW the operator and the Result:
        //      go thru all possible circle1 values, and see if any of them can get us to Result


        List<string> operatorList_1 = new List<string>();
        float addition_circle1ValueToUse_1 = 0;
        float addition_circle2ValueToUse_1 = 0;
        float subtraction_circle1ValueToUse_1 = 0;
        float subtraction_circle2ValueToUse_1 = 0;
        float multiplication_circle1ValueToUse_1 = 0;
        float multiplication_circle2ValueToUse_1 = 0;
        float division_circle1ValueToUse_1 = 0;
        float division_circle2ValueToUse_1 = 0;
        float exponent2_circle1ValueToUse_1 = 0;
        float exponent3_circle1ValueToUse_1 = 0;
        float squareRoot_circle1ValueToUse_1 = 0;
        float cubeRoot_circle1ValueToUse_1 = 0;

        List<string> operatorList_2 = new List<string>();
        float addition_circle1ValueToUse_2 = 0;
        float addition_circle2ValueToUse_2 = 0;
        float subtraction_circle1ValueToUse_2 = 0;
        float subtraction_circle2ValueToUse_2 = 0;
        float multiplication_circle1ValueToUse_2 = 0;
        float multiplication_circle2ValueToUse_2 = 0;
        float division_circle1ValueToUse_2 = 0;
        float division_circle2ValueToUse_2 = 0;
        float exponent2_circle1ValueToUse_2 = 0;
        float exponent3_circle1ValueToUse_2 = 0;
        float squareRoot_circle1ValueToUse_2 = 0;
        float cubeRoot_circle1ValueToUse_2 = 0;

        List<float> circle2ValuesToConsider_Add_Subtract = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, true, negativeNumbersAllowed, fractionsAllowed);
        List<float> circle2ValuesToConsider_Mult_Divide = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, false, negativeNumbersAllowed, fractionsAllowed);

        void ConsiderAddition(float resultValue, char ABC)
        {
            // we know the operator, and we know the result
            //      instead of going thru EVERY SINGLE POSSIBLE circle1, just pick a random value within the acceptable range, and see if it works
            //      if it doesn't work, then pick another
            //      if it DOES work, then we've got our circle1 value... after all we were going to randomly pick one anyway
            //
            //      this doesn't address the possibility of choosing WHICH result we're going to use, though....... let's deal with that later


            // if the resultValue is a fraction, then the ONLY POSSIBLE circle1 value is also a fraction, and we don't need to waste time looking at integers
            // conversely, if resultValue is an integer, the we don't need to look at fractions

            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, true, negativeNumbersAllowed, fractionsAllowed);

            //if (TestIfIsInteger(resultValue)) { 
            //    // if resultValue is an integer, then circle1 will be an integer, and we can ignore fractions
            //    // but because there are so few fractions in the list to begin with, it may be quicker to not remove them as doing so requires that we iterate thru the whole list
            //} else { 
            //    // if resultValue is a fraction, then circle1 will be a fraction, and we can ignore integers
            //    for (int i = 0; i < circle1ValuesToConsider.Count; i ++) { 
            //        if (TestIfIsInteger(circle1ValuesToConsider[i])) {
            //            circle1ValuesToConsider.RemoveAt(i);
            //        }
            //    }
            //}

            bool doneWithLoop = false;
            while (doneWithLoop == false)
            {
                // step 1: pick a random circle1 value, within the acceptable range of -20 to 20
                int rando = Random.Range(0, circle1ValuesToConsider.Count);
                float potentialCircle1Value = circle1ValuesToConsider[rando];
                //Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                circle1ValuesToConsider.Remove(potentialCircle1Value);
                // step 2: see if it can work, to get us the predetermined result
                //      so we're considering addition... meaning we'll have: a + b = c, & we know a & c, therefore b = c - a
                float potentialCircle2Value = resultValue - potentialCircle1Value;
                // step 3: is this bValue acceptable? i.e., we don't want 5.123183943, or 2^0.5, or something weird. it has to be in the list
                if (circle2ValuesToConsider_Add_Subtract.Contains(potentialCircle2Value))
                {
                    //Debug.Log("addition is viable: " + potentialCircle1Value + " as circle1 and " + potentialCircle2Value + " as circle2 gets us result of " + resultValue);
                    if (ABC == 'A')
                    {
                        operatorList_1.Add("addition");
                        addition_circle1ValueToUse_1 = potentialCircle1Value;
                        addition_circle2ValueToUse_1 = potentialCircle2Value;
                        // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                        doneWithLoop = true;
                    }
                    else if (ABC == 'B')
                    {
                        operatorList_2.Add("addition");
                        addition_circle1ValueToUse_2 = potentialCircle1Value;
                        addition_circle2ValueToUse_2 = potentialCircle2Value;
                        doneWithLoop = true;
                    }
                    else if (ABC == 'C')
                    {

                    }
                }
                else
                {
                    //Debug.Log("this won't work: " + potentialCircle1Value + " + " + potentialCircle2Value + " = " + resultValue);
                    if (circle1ValuesToConsider.Count == 0)
                    {
                        //Debug.Log("*****************************addition will not work... we've tried every option");
                        doneWithLoop = true;
                    }
                }
            }

        }
        void ConsiderSubtraction(float resultValue, char ABC)
        {
            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, true, negativeNumbersAllowed, fractionsAllowed);

            bool doneWithLoop = false;
            while (doneWithLoop == false)
            {
                // step 1: pick a random circle1 value, within the acceptable range of -20 to 20
                int rando = Random.Range(0, circle1ValuesToConsider.Count);
                float potentialCircle1Value = circle1ValuesToConsider[rando];
                //Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                circle1ValuesToConsider.Remove(potentialCircle1Value);
                // step 2: see if it can work, to get us the predetermined result
                //      so we're considering subtraction... meaning we'll have: a - b = c, & we know a & c, therefore b = a - c
                float potentialCircle2Value = potentialCircle1Value - resultValue;
                // step 3: is this bValue acceptable? i.e., we don't want 5.123183943, or 2^0.5, or something weird. it has to be in the list
                if (circle2ValuesToConsider_Add_Subtract.Contains(potentialCircle2Value))
                {
                    //Debug.Log("subtraction is viable: " + potentialCircle1Value + " as circle1 and " + potentialCircle2Value + " as circle2 gets us result of " + resultValue);
                    if (ABC == 'A')
                    {
                        operatorList_1.Add("subtraction");
                        subtraction_circle1ValueToUse_1 = potentialCircle1Value;
                        subtraction_circle2ValueToUse_1 = potentialCircle2Value;
                        // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                        doneWithLoop = true;
                    }
                    else if (ABC == 'B')
                    {
                        operatorList_2.Add("subtraction");
                        subtraction_circle1ValueToUse_2 = potentialCircle1Value;
                        subtraction_circle2ValueToUse_2 = potentialCircle2Value;
                        doneWithLoop = true;
                    }
                    else if (ABC == 'C')
                    {

                    }

                }
                else
                {
                    //Debug.Log("this won't work: " + potentialCircle1Value + " - " + potentialCircle2Value + " = " + resultValue); 
                    if (circle1ValuesToConsider.Count == 0)
                    {
                        //Debug.Log("*****************************subtraction will not work... we've tried every option");
                        doneWithLoop = true;
                    }
                }
            }
        }
        bool IsThisNumberPrime(float resultValue)
        {
            int tally = 0;
            for (int i = 1; i <= Mathf.Abs(resultValue); i++)
            {
                if (resultValue % i == 0)
                {
                    tally += 1;
                }
            }
            if (tally == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void ConsiderMultiplication(float resultValue, char ABC)
        {
            // this is multiplication, so if the RESULT is a PRIME NUMBER, then we already know we don't want to use multiplication, because multiplying by 1 is boring

            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, false, negativeNumbersAllowed, fractionsAllowed);

            bool doneWithLoop = false;
            while (doneWithLoop == false)
            {
                // step 1: pick a random circle1 value, within the acceptable range of -20 to 20
                int rando = Random.Range(0, circle1ValuesToConsider.Count);
                float potentialCircle1Value = circle1ValuesToConsider[rando];
                //Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                circle1ValuesToConsider.Remove(potentialCircle1Value);
                // step 2: see if it can work, to get us the predetermined result
                //      so we're considering multiplication... meaning we'll have: a * b = c, & we know a & c, therefore b = c / a
                float potentialCircle2Value = resultValue / potentialCircle1Value;
                // step 3: is this bValue acceptable? i.e., we don't want 5.123183943, or 2^0.5, or something weird. it has to be in the list
                if (circle2ValuesToConsider_Mult_Divide.Contains(potentialCircle2Value))
                {
                    //Debug.Log("multiplication is viable: " + potentialCircle1Value + " as circle1 and " + potentialCircle2Value + " as circle2 gets us result of " + resultValue);
                    if (ABC == 'A')
                    {
                        operatorList_1.Add("multiplication");
                        multiplication_circle1ValueToUse_1 = potentialCircle1Value;
                        multiplication_circle2ValueToUse_1 = potentialCircle2Value;
                        // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                        doneWithLoop = true;
                    }
                    else if (ABC == 'B')
                    {
                        operatorList_2.Add("multiplication");
                        multiplication_circle1ValueToUse_2 = potentialCircle1Value;
                        multiplication_circle2ValueToUse_2 = potentialCircle2Value;
                        doneWithLoop = true;
                    }
                    else if (ABC == 'C')
                    {

                    }
                }
                else
                {
                    //Debug.Log("this won't work: " + potentialCircle1Value + " * " + potentialCircle2Value + " = " + resultValue); 
                    if (circle1ValuesToConsider.Count == 0)
                    {
                        //Debug.Log("*****************************multiplication will not work... we've tried every option");
                        doneWithLoop = true;
                    }
                }
            }
        }
        void ConsiderDivision(float resultValue, char ABC)
        {
            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, false, negativeNumbersAllowed, fractionsAllowed);

            bool doneWithLoop = false;
            while (doneWithLoop == false)
            {
                // step 1: pick a random circle1 value, within the acceptable range of -20 to 20
                int rando = Random.Range(0, circle1ValuesToConsider.Count);
                float potentialCircle1Value = circle1ValuesToConsider[rando];
                //Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                circle1ValuesToConsider.Remove(potentialCircle1Value);
                // step 2: see if it can work, to get us the predetermined result
                //      so we're considering division... meaning we'll have: a / b = c, & we know a & c, therefore b = a / c
                float potentialCircle2Value = potentialCircle1Value / resultValue;
                // step 3: is this bValue acceptable? i.e., we don't want 5.123183943, or 2^0.5, or something weird. it has to be in the list
                if (circle2ValuesToConsider_Mult_Divide.Contains(potentialCircle2Value))
                {
                    //Debug.Log("division is viable: " + potentialCircle1Value + " as circle1 and " + potentialCircle2Value + " as circle2 gets us result of " + resultValue);
                    if (ABC == 'A')
                    {
                        operatorList_1.Add("division");
                        division_circle1ValueToUse_1 = potentialCircle1Value;
                        division_circle2ValueToUse_1 = potentialCircle2Value;
                        // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                        doneWithLoop = true;
                    }
                    else if (ABC == 'B')
                    {
                        operatorList_2.Add("division");
                        division_circle1ValueToUse_2 = potentialCircle1Value;
                        division_circle2ValueToUse_2 = potentialCircle2Value;
                        doneWithLoop = true;
                    }
                    else if (ABC == 'C')
                    {

                    }
                }
                else
                {
                    //Debug.Log("this won't work: " + potentialCircle1Value + " / " + potentialCircle2Value + " = " + resultValue);
                    if (circle1ValuesToConsider.Count == 0)
                    {
                        //Debug.Log("*****************************division will not work... we've tried every option");
                        doneWithLoop = true;
                    }
                }
            }
        }
        void ConsiderExponent2(float resultValue, char ABC)
        {
            List<float> usableResults = new List<float>();
            if (fractionsAllowed)
            {
                usableResults = new List<float> { 0.25f, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121 };
            }
            else
            {
                usableResults = new List<float> { 4, 9, 16, 25, 36, 49, 64, 81, 100, 121 };
            }

            //List<float> usableResults = new List<float> { 0.25f, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121 };

            if (usableResults.Contains(resultValue))
            {
                // randomly assign circle1 to be either negative or positive            UNLESS NEGATIVES ARE TURNED OFF!!!!
                int rando = Random.Range(1, 3);
                if (negativeNumbersAllowed == false)
                {
                    rando = 1;                          // if negativeNumbers are turned off...
                }
                if (rando == 1)
                {
                    if (ABC == 'A')
                    {
                        exponent2_circle1ValueToUse_1 = Mathf.RoundToInt(Mathf.Pow(resultValue, 0.5f));
                    }
                    else if (ABC == 'B')
                    {
                        exponent2_circle1ValueToUse_2 = Mathf.RoundToInt(Mathf.Pow(resultValue, 0.5f));
                    }
                    else if (ABC == 'C')
                    {

                    }
                }
                else
                {
                    if (ABC == 'A')
                    {
                        exponent2_circle1ValueToUse_1 = Mathf.RoundToInt(-Mathf.Pow(resultValue, 0.5f));
                    }
                    else if (ABC == 'B')
                    {
                        exponent2_circle1ValueToUse_2 = Mathf.RoundToInt(-Mathf.Pow(resultValue, 0.5f));
                    }
                    else if (ABC == 'C')
                    {

                    }
                }
                //Debug.Log("exponent2 is viable: " + exponent2_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                if (ABC == 'A')
                {
                    operatorList_1.Add("exponent2");
                }
                else if (ABC == 'B')
                {
                    operatorList_2.Add("exponent2");
                }
                else if (ABC == 'C')
                {

                }
            }
            else
            {
                //Debug.Log("the resultValue isn't on the pre-approved list, so exponent2 can be ignored");
            }
        }
        void ConsiderExponent3(float resultValue, char ABC)
        {
            List<float> usableResults = new List<float>();
            if (negativeNumbersAllowed)
            {
                usableResults = new List<float> { -125, -64, -27, -8, 8, 27, 64, 125 };
            }
            else
            {
                usableResults = new List<float> { 8, 27, 64, 125 };
            }

            //List<float> usableResults = new List<float> { -125, -64, -27, -8, 8, 27, 64, 125 };

            if (usableResults.Contains(resultValue))
            {
                if (resultValue < 0)
                {
                    if (ABC == 'A')
                    {
                        exponent3_circle1ValueToUse_1 = Mathf.RoundToInt(-Mathf.Pow(-resultValue, 1f / 3f));
                    }
                    else if (ABC == 'B')
                    {
                        exponent3_circle1ValueToUse_2 = Mathf.RoundToInt(-Mathf.Pow(-resultValue, 1f / 3f));
                    }
                    else if (ABC == 'C')
                    {

                    }
                }
                else
                {
                    if (ABC == 'A')
                    {
                        exponent3_circle1ValueToUse_1 = Mathf.RoundToInt(Mathf.Pow(resultValue, 1f / 3f));
                    }
                    else if (ABC == 'B')
                    {
                        exponent3_circle1ValueToUse_2 = Mathf.RoundToInt(Mathf.Pow(resultValue, 1f / 3f));
                    }
                    else if (ABC == 'C')
                    {

                    }
                }
                //Debug.Log("exponent3 is viable: " + exponent3_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                if (ABC == 'A')
                {
                    operatorList_1.Add("exponent3");
                }
                else if (ABC == 'B')
                {
                    operatorList_2.Add("exponent3");
                }
                else if (ABC == 'C')
                {

                }
            }
            else
            {
                //Debug.Log("the resultValue isn't on the pre-approved list, so exponent3 can be ignored");
            }
        }

        void ConsiderSquareRoot(float resultValue, char ABC)
        {
            List<float> usableResults = new List<float> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            if (usableResults.Contains(resultValue))
            {
                if (ABC == 'A')
                {
                    squareRoot_circle1ValueToUse_1 = Mathf.RoundToInt(Mathf.Pow(resultValue, 2));
                    //Debug.Log("squareRoot is viable: " + squareRoot_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                    operatorList_1.Add("squareRoot");
                }
                else if (ABC == 'B')
                {
                    squareRoot_circle1ValueToUse_2 = Mathf.RoundToInt(Mathf.Pow(resultValue, 2));
                    //Debug.Log("squareRoot is viable: " + squareRoot_circle1ValueToUse_2 + " as circle1 gives us result of " + resultValue);
                    operatorList_2.Add("squareRoot");
                }
                else if (ABC == 'C')
                {

                }

                //bool doneWithLoop = false;
                //List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forSquareRoot(11, false, false, true);
                //while (doneWithLoop == false)
                //{
                //    // step 1: pick a random circle1 value, within the acceptable range
                //    int rando = Random.Range(0, circle1ValuesToConsider.Count);
                //    float potentialCircle1Value = circle1ValuesToConsider[rando];
                //    Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                //    circle1ValuesToConsider.Remove(potentialCircle1Value);
                //    // step 2: see if it can work, to get us the predetermined result
                //    //      so we're considering exponent2... meaning we'll have a^2 = c, and we know c, therefore c = a^(1/2)
                //    //      so all we need to do is see if this circle1 value satisfies c = a^(1/2)
                //    if (Mathf.Pow(potentialCircle1Value, 0.5f) == resultValue)
                //    {
                //        Debug.Log("squareRoot is viable: " + potentialCircle1Value + " as circle1 gives us result of " + resultValue);
                //        operatorList.Add("squareRoot");
                //        squareRoot_circle1ValueToUse = potentialCircle1Value;
                //        doneWithLoop = true;
                //    }
                //    else
                //    {
                //        Debug.Log("this won't work: " + potentialCircle1Value + " ^0.5 = " + resultValue);
                //        if (circle1ValuesToConsider.Count == 0)
                //        {
                //            Debug.Log("*****************************squareRoot will not work... we've tried every option");
                //            doneWithLoop = true;
                //        }
                //    }
                //}
            }
            else
            {
                //Debug.Log("the resultValue isn't on the pre-approved list, so squareRoot can be ignored");
            }
        }
        void ConsiderCubeRoot(float resultValue, char ABC)
        {
            List<float> usableResults = new List<float>();
            if (negativeNumbersAllowed)
            {
                usableResults = new List<float> { -5, -4, -3, -2, 2, 3, 4, 5 };
            }
            else
            {
                usableResults = new List<float> { 2, 3, 4, 5 };
            }

            //List<float> usableResults = new List<float> { -5, -4, -3, -2, 2, 3, 4, 5 };

            if (usableResults.Contains(resultValue))
            {
                if (ABC == 'A')
                {
                    cubeRoot_circle1ValueToUse_1 = Mathf.RoundToInt(Mathf.Pow(resultValue, 3));
                    //Debug.Log("cubeRoot is viable: " + cubeRoot_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                    operatorList_1.Add("cubeRoot");
                }
                else if (ABC == 'B')
                {
                    cubeRoot_circle1ValueToUse_2 = Mathf.RoundToInt(Mathf.Pow(resultValue, 3));
                    //Debug.Log("cubeRoot is viable: " + cubeRoot_circle1ValueToUse_2 + " as circle1 gives us result of " + resultValue);
                    operatorList_2.Add("cubeRoot");
                }
                else if (ABC == 'C')
                {

                }
            }
            else
            {
                //Debug.Log("the resultValue isn't on the pre-approved list, so cubeRoot can be ignored");
            }
        }

        void ConsiderAllOperators(Circle potentialResult_1or2, char AorB)
        {

            string otherOperator = operatorAlreadyUsed.type;
            float value = potentialResult_1or2.value;

            if (Mathf.Abs(value) <= 2 * upperLimitForCircleValue)
            {
                // tttppp
                ConsiderAddition(value, AorB);
                ConsiderSubtraction(value, AorB);
            }
            else
            {
                Debug.Log(value + " is too big, so we're skipping addition & subtraction");
            }
            // if valueOfResultA is a PRIME NUMBER, then we don't want to consider multiplication or division, because *1 and /1 are boring
            if (multDivideAllowed == true)
            {
                if (!IsThisNumberPrime(value))
                {
                    // tttppp
                    ConsiderMultiplication(value, AorB);
                    ConsiderDivision(value, AorB);
                }
                else
                {
                    Debug.Log(value + " is a prime number, so we're skipping multiplication & division");
                }
            }
            if (exponentsAllowed == true)
            {
                if (otherOperator != "squareRoot")
                {
                    // tttppp
                    ConsiderExponent2(value, AorB);
                }
                if (otherOperator != "cubeRoot")
                {
                    // tttppp
                    ConsiderExponent3(value, AorB);
                }
                if (otherOperator != "exponent2")
                {
                    // tttppp
                    ConsiderSquareRoot(value, AorB);
                }
                if (otherOperator != "exponent3")
                {
                    // tttppp
                    ConsiderCubeRoot(value, AorB);
                }
            }
        }

        ConsiderAllOperators(potentialResult1, 'A');
        if (potentialResult2 != null)
        {
            ConsiderAllOperators(potentialResult2, 'B');
        }

        Operator partA_operator = null;
        Circle partA_circle1 = null;
        Circle partA_circle2 = null;
        Circle partA_result = null;

        void GoWithResult_1()
        {
            // now we randomly select one of the operators, and bingo we're done
            int rando = Random.Range(0, operatorList_1.Count);
            string operatorWeUse = operatorList_1[rando];

            float circle1Value = 0;
            float circle2Value = 0;
            float resultPartA = 0;
            if (operatorWeUse == "addition")
            {
                partA_operator = new Operator("addition", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = addition_circle1ValueToUse_1;
                circle2Value = addition_circle2ValueToUse_1;
                resultPartA = circle1Value + circle2Value;
            }
            else if (operatorWeUse == "subtraction")
            {
                partA_operator = new Operator("subtraction", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = subtraction_circle1ValueToUse_1;
                circle2Value = subtraction_circle2ValueToUse_1;
                resultPartA = circle1Value - circle2Value;
            }
            else if (operatorWeUse == "multiplication")
            {
                partA_operator = new Operator("multiplication", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = multiplication_circle1ValueToUse_1;
                circle2Value = multiplication_circle2ValueToUse_1;
                resultPartA = circle1Value * circle2Value;
            }
            else if (operatorWeUse == "division")
            {
                partA_operator = new Operator("division", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = division_circle1ValueToUse_1;
                circle2Value = division_circle2ValueToUse_1;
                resultPartA = circle1Value / circle2Value;
            }
            else if (operatorWeUse == "exponent2")
            {
                partA_operator = new Operator("exponent2", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = exponent2_circle1ValueToUse_1;
                resultPartA = Mathf.RoundToInt(Mathf.Pow(circle1Value, 2));
            }
            else if (operatorWeUse == "exponent3")
            {
                partA_operator = new Operator("exponent3", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = exponent3_circle1ValueToUse_1;
                resultPartA = Mathf.RoundToInt(Mathf.Pow(circle1Value, 3));
            }
            else if (operatorWeUse == "squareRoot")
            {
                partA_operator = new Operator("squareRoot", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = squareRoot_circle1ValueToUse_1;
                resultPartA = Mathf.RoundToInt(Mathf.Pow(circle1Value, 0.5f));
            }
            else if (operatorWeUse == "cubeRoot")
            {
                partA_operator = new Operator("cubeRoot", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = cubeRoot_circle1ValueToUse_1;
                if (circle1Value < 0)
                {
                    resultPartA = Mathf.RoundToInt(-Mathf.Pow(-circle1Value, 1f / 3f));
                }
                else if (circle1Value > 0)
                {
                    resultPartA = Mathf.RoundToInt(Mathf.Pow(circle1Value, 1f / 3f));
                }

            }
            if (circle1Value != 0)
            {
                partA_circle1 = CreateSpecificCircle(circle1Value);
            }
            else
            {
                Debug.Log("(((((((((((((((((((((((((((((((((((((((((( caught a zero as circle1Value");
            }
            //partA_circle1 = CreateSpecificCircle(circle1Value);
            if (circle2Value != 0)
            {
                partA_circle2 = CreateSpecificCircle(circle2Value);
            }
            else
            {
                Debug.Log("(((((((((((((((((((((((((((((((((((((((((( caught a zero as circle1Value");
            }
            //partA_circle2 = CreateSpecificCircle(circle2Value);
            partA_result = CreateSpecificCircle(resultPartA);

            //Debug.Log("for PartA, circle1.value is: " + circle1Value);
            //Debug.Log("for PartA, circle2.value is: " + circle2Value);
            //Debug.Log("for PartA, using operator: " + operatorWeUse);
            //Debug.Log("for PartA, result is: " + resultPartA);
        }
        void GoWithResult_2()
        {
            // now we randomly select one of the operators, and bingo we're done
            int rando = Random.Range(0, operatorList_2.Count);
            string operatorWeUse = operatorList_2[rando];

            float circle1Value = 0;
            float circle2Value = 0;
            float resultPartA = 0;
            if (operatorWeUse == "addition")
            {
                partA_operator = new Operator("addition", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = addition_circle1ValueToUse_2;
                circle2Value = addition_circle2ValueToUse_2;
                resultPartA = circle1Value + circle2Value;
            }
            else if (operatorWeUse == "subtraction")
            {
                partA_operator = new Operator("subtraction", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = subtraction_circle1ValueToUse_2;
                circle2Value = subtraction_circle2ValueToUse_2;
                resultPartA = circle1Value - circle2Value;
            }
            else if (operatorWeUse == "multiplication")
            {
                partA_operator = new Operator("multiplication", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = multiplication_circle1ValueToUse_2;
                circle2Value = multiplication_circle2ValueToUse_2;
                resultPartA = circle1Value * circle2Value;
            }
            else if (operatorWeUse == "division")
            {
                partA_operator = new Operator("division", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = division_circle1ValueToUse_2;
                circle2Value = division_circle2ValueToUse_2;
                resultPartA = circle1Value / circle2Value;
            }
            else if (operatorWeUse == "exponent2")
            {
                partA_operator = new Operator("exponent2", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = exponent2_circle1ValueToUse_2;
                resultPartA = Mathf.RoundToInt(Mathf.Pow(circle1Value, 2));
            }
            else if (operatorWeUse == "exponent3")
            {
                partA_operator = new Operator("exponent3", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = exponent3_circle1ValueToUse_2;
                resultPartA = Mathf.RoundToInt(Mathf.Pow(circle1Value, 3));
            }
            else if (operatorWeUse == "squareRoot")
            {
                partA_operator = new Operator("squareRoot", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = squareRoot_circle1ValueToUse_2;
                resultPartA = Mathf.RoundToInt(Mathf.Pow(circle1Value, 0.5f));
            }
            else if (operatorWeUse == "cubeRoot")
            {
                partA_operator = new Operator("cubeRoot", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = cubeRoot_circle1ValueToUse_2;
                if (circle1Value < 0)
                {
                    resultPartA = Mathf.RoundToInt(-Mathf.Pow(-circle1Value, 1f / 3f));
                }
                else if (circle1Value > 0)
                {
                    resultPartA = Mathf.RoundToInt(Mathf.Pow(circle1Value, 1f / 3f));
                }
            }
            if (circle1Value != 0)
            {
                partA_circle1 = CreateSpecificCircle(circle1Value);
            }
            else
            {
                Debug.Log("(((((((((((((((((((((((((((((((((((((((((( caught a zero as circle1Value");
            }

            //partA_circle1 = CreateSpecificCircle(circle1Value);
            // we want partA_circle2 to REMAIN NULL if it's not going to be used!!!, not have a value of zero.... the only time a circle has a zero value and is on-screen is when the player screws up
            if (circle2Value != 0)
            {
                partA_circle2 = CreateSpecificCircle(circle2Value);
            }
            else
            {
                Debug.Log("(((((((((((((((((((((((((((((((((((((((((( caught a zero as circle1Value");
            }
            partA_result = CreateSpecificCircle(resultPartA);

            //Debug.Log("for PartA, circle1.value is: " + circle1Value);
            //Debug.Log("for PartA, circle2.value is: " + circle2Value);
            //Debug.Log("for PartA, using operator: " + operatorWeUse);
            //Debug.Log("for PartA, result is: " + resultPartA);
        }

        //Debug.Log("operatorList_A contains " + operatorList_1.Count + " possible operators");
        //Debug.Log("operatorList_B contains " + operatorList_2.Count + " possible operators");

        // compare the two operatorLists and see which one has the most options

        if (operatorList_1.Count > operatorList_2.Count)
        {
            Debug.Log("going with ResultA");
            GoWithResult_1();
        }
        else if (operatorList_1.Count < operatorList_2.Count)
        {
            Debug.Log("going with ResultB");
            GoWithResult_2();
        }
        else
        {
            int rando = Random.Range(1, 3);
            if (rando == 1)
            {
                Debug.Log("going with ResultA, chosen randomly because the lists are the same size");
                GoWithResult_1();
            }
            else
            {
                Debug.Log("going with ResultB, chosen randomly because the lists are the same size");
                GoWithResult_2();
            }
        }

        toReturn.Add(partA_operator);
        toReturn.Add(partA_circle1);
        toReturn.Add(partA_circle2);
        toReturn.Add(partA_result);
        return toReturn;


    }

    // ************************************************************************************************************
    // ************************************************************************************************************

    public void SetCircle(GameObject circleGameObject, Circle circleData)
    {
        if (circleData == null) //  || circleData.value == -999999999)
        {
            //CircleA.transform.GetChild(0).GetComponent<TextMeshPro>().text = circle1.value.ToString();
            circleGameObject.SetActive(false);
            Debug.Log("in SetCircle()....  circleData == null, so.... and circleData.value is nonexistent");
            circleGameObject.GetComponent<Clickable_circle>().negate_SetCircleAsPartOfCurrentPuzzle();
            //return false;       // this circle is active = FALSE
        }
        else
        {
            circleGameObject.GetComponent<Clickable_circle>().SetCircleAsPartOfCurrentPuzzle();
            Debug.Log("in SetCircle()....  circleData is not null, circleData.value = " + circleData.value);
            float value = circleData.value;
            if (TestIfIsInteger(value))
            {
                circleGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = value.ToString();
                // turn off all the fraction stuff
                for (int i = 1; i <= 3; i++)
                {
                    circleGameObject.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            else
            // see whether a rational number will work, or if we have to use decimals
            {
                bool isNegative = false;
                if (value < 0)
                {
                    isNegative = true;
                    Debug.Log("um yah it is negative");
                }

                value = Mathf.Abs(value);

                int numerator = -99999;
                int denominator = -99999;

                bool rationalFound = false;
                for (int i = 1; i <= 30 && rationalFound == false; i++)
                {
                    for (int j = 2; j <= 20 && rationalFound == false; j++)
                    {
                        if (CheckIfNumbersAreCloseEnough(value, (float)i / (float)j))
                        {
                            if (rationalFound == false)
                            {
                                numerator = i;
                                if (isNegative)
                                {
                                    numerator = -numerator;
                                }
                                denominator = j;

                                Debug.Log("numerator: " + i + ", denominator: " + j);
                                rationalFound = true;
                            }
                        }
                    }
                }

                if (rationalFound == true)
                {
                    // here we set the values into the circle gameObject
                    circleGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";       // this child is for ints
                    for (int i = 1; i <= 3; i++)
                    {
                        circleGameObject.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    circleGameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = numerator.ToString();    // numerator value
                    circleGameObject.transform.GetChild(3).GetComponent<TextMeshPro>().text = denominator.ToString();

                }
                else
                {
                    // if we got to this point it means that we have to portray the number in decimals

                    // IS THE NUMBER NEGATIVE?????
                    if (isNegative)
                    {
                        value *= -1;
                    }

                    circleGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = value.ToString("F3"); // this is for ints only
                    for (int i = 1; i <= 3; i++)
                    {
                        circleGameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }

            //if (Mathf.Abs(value) == 0.25f || CheckIfNumbersAreCloseEnough(Mathf.Abs(value), 1f / 3f) || Mathf.Abs(value) == 0.5f || CheckIfNumbersAreCloseEnough(Mathf.Abs(value), 2f / 3f) || Mathf.Abs(value) == 0.75f) 
            //{
            //    // if the player screws up and gets a weird non-int number, we still need that number to appear on the circle
            //    // so, if it's one of our pre-approved fractions, go ahead with the below code, otherwise set the textMeshPro text to the new value


            //    circleGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            //    for (int i = 1; i <= 7; i++) {
            //        circleGameObject.transform.GetChild(i).gameObject.SetActive(false);
            //    }
            //    if (value < 0) {
            //        circleGameObject.transform.GetChild(1).gameObject.SetActive(true);      // numerator_negative
            //    } else {
            //        circleGameObject.transform.GetChild(1).gameObject.SetActive(false);     // numerator_negative
            //    }
            //    float absValue = Mathf.Abs(value);
            //    if (absValue == 0.25f) {
            //        circleGameObject.transform.GetChild(2).gameObject.SetActive(true);      // numerator_1
            //        circleGameObject.transform.GetChild(7).gameObject.SetActive(true);      // denominator_4
            //    } else if (CheckIfNumbersAreCloseEnough(absValue, 1f / 3f)) {
            //        circleGameObject.transform.GetChild(2).gameObject.SetActive(true);      // numerator_1
            //        circleGameObject.transform.GetChild(6).gameObject.SetActive(true);      // denominator_3
            //    } else if (absValue == 0.5f) {
            //        circleGameObject.transform.GetChild(2).gameObject.SetActive(true);      // numerator_1
            //        circleGameObject.transform.GetChild(5).gameObject.SetActive(true);      // denominator_2
            //    } else if (CheckIfNumbersAreCloseEnough(absValue, 2f / 3f)) {
            //        circleGameObject.transform.GetChild(3).gameObject.SetActive(true);      // numerator_2
            //        circleGameObject.transform.GetChild(6).gameObject.SetActive(true);      // denominator_3
            //    } else if (absValue == 0.75f) {
            //        circleGameObject.transform.GetChild(4).gameObject.SetActive(true);      // numerator_3
            //        circleGameObject.transform.GetChild(7).gameObject.SetActive(true);      // denominator_4
            //    }
            //} 
            //else 
            //{
            //    Debug.Log("screwy value: " + value);
            //    // if we got to this point then the value is a random useless (for the purposes of solving the puzzle) non-int
            //    circleGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = value.ToString("F3");
            //    // turn off all the fraction stuff
            //    for (int i = 1; i <= 7; i++)
            //    {
            //        circleGameObject.transform.GetChild(i).gameObject.SetActive(false);
            //    }
            //}

            circleGameObject.SetActive(true);
            circleGameObject.GetComponent<Clickable_circle>().valueOfThisCircle_orGoal = value;
            circleGameObject.GetComponent<Clickable_circle>().IDnumberOfCircleDataAttachedToThis = circleData.IDnumber;        // this marks the GameObject 

            if (circleGameObject == CircleA)
            {
                circleData.circleGameObject_associatedWith = "CircleA";                                                 // this marks the circleData Circle class object
            }
            else if (circleGameObject == CircleB)
            {
                circleData.circleGameObject_associatedWith = "CircleB";
            }
            else if (circleGameObject == CircleC)
            {
                circleData.circleGameObject_associatedWith = "CircleC";
            }

        }
        //return true;
    }
    public void SetOperator(GameObject opAorB, Operator oppy)
    {
        opAorB.SetActive(true);

        if (oppy.type == "addition")
        {
            opAorB.GetComponent<Clickable_operator>().typeOfThisOperator = "addition";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+";
            opAorB.transform.GetChild(1).gameObject.SetActive(false);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(false);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(false);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(false);                   // exponent_X
        }
        else if (oppy.type == "subtraction")
        {
            opAorB.GetComponent<Clickable_operator>().typeOfThisOperator = "subtraction";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-";
            opAorB.transform.GetChild(1).gameObject.SetActive(false);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(false);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(false);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(false);                   // exponent_X
        }
        else if (oppy.type == "multiplication")
        {
            opAorB.GetComponent<Clickable_operator>().typeOfThisOperator = "multiplication";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "*";
            opAorB.transform.GetChild(1).gameObject.SetActive(false);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(false);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(false);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(false);                   // exponent_X
        }
        else if (oppy.type == "division")
        {
            opAorB.GetComponent<Clickable_operator>().typeOfThisOperator = "division";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "/";
            opAorB.transform.GetChild(1).gameObject.SetActive(false);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(false);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(false);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(false);                   // exponent_X
        }
        else if (oppy.type == "exponent2")
        {
            opAorB.GetComponent<Clickable_operator>().typeOfThisOperator = "exponent2";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            opAorB.transform.GetChild(1).gameObject.SetActive(false);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(false);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(false);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(true);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(true);                   // exponent_X
        }
        else if (oppy.type == "exponent3")
        {
            opAorB.GetComponent<Clickable_operator>().typeOfThisOperator = "exponent3";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            opAorB.transform.GetChild(1).gameObject.SetActive(false);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(false);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(false);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(true);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(true);                   // exponent_X
        }
        else if (oppy.type == "squareRoot")
        {
            opAorB.GetComponent<Clickable_operator>().typeOfThisOperator = "squareRoot";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            opAorB.transform.GetChild(1).gameObject.SetActive(true);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(true);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(false);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(false);                   // exponent_X
        }
        else if (oppy.type == "cubeRoot")
        {
            opAorB.GetComponent<Clickable_operator>().typeOfThisOperator = "cubeRoot";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            opAorB.transform.GetChild(1).gameObject.SetActive(true);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(true);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(true);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(false);                   // exponent_X
        }
        opAorB.GetComponent<Clickable_operator>().IDnumberOfOperatorDataAttachedToThis = oppy.IDnumber;                  // this marks the GameObject
        if (opAorB == OperatorA)
        {                                                                              // this marks the Operator class object
            oppy.operatorGameObject_associatedWith = "OperatorA";
        }
        else if (opAorB == OperatorB)
        {
            oppy.operatorGameObject_associatedWith = "OperatorB";
        }
    }
    public void DisableOperatorText(GameObject oppy) {
        oppy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        oppy.transform.GetChild(1).gameObject.SetActive(false);                   // squareRootImage
        oppy.transform.GetChild(2).gameObject.SetActive(false);                   // squareRoot_X
        oppy.transform.GetChild(3).gameObject.SetActive(false);                   // cubeRoot3
        oppy.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
        oppy.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
        oppy.transform.GetChild(6).gameObject.SetActive(false);                   // exponent_X
    }
    public void RandomizeDefaultPositionsOfCircles()
    {

    }
    public void UpdateDefaultPositionsOfCircles(bool randomizingThisTimeWouldBeAnnoying)
    {
        // need to know how many circles are active, and which ones are active
        int tally = 0;
        float defaultX = -4f;
        if (CircleA.activeSelf)
        {
            tally += 1;
        }
        if (CircleB.activeSelf)
        {
            tally += 1;
        }
        if (CircleC.activeSelf)
        {
            tally += 1;
        }
        if (tally == 3)
        {
            float circleA_Yvalue = 2.5f;
            float cirlceB_Yvalue = 0;
            float circleC_Yvalue = -2.5f;

            if (debugModeOn == false)
            {         // no debugMode means random ordering
                // 3 circles means 3! ways to configure them
                int rando = Random.Range(1, 7);
                if (rando == 1)
                {               // ABC
                    // do nothing, they're already in the correct spots
                }
                else if (rando == 2)
                {        // ACB
                    circleA_Yvalue = 2.5f;
                    cirlceB_Yvalue = -2.5f;
                    circleC_Yvalue = 0;
                }
                else if (rando == 3)
                {        // BAC
                    circleA_Yvalue = 0;
                    cirlceB_Yvalue = 2.5f;
                    circleC_Yvalue = -2.5f;
                }
                else if (rando == 4)
                {        // BCA
                    circleA_Yvalue = 0;
                    cirlceB_Yvalue = -2.5f;
                    circleC_Yvalue = 2.5f;
                }
                else if (rando == 5)
                {        // CAB
                    circleA_Yvalue = -2.5f;
                    cirlceB_Yvalue = 2.5f;
                    circleC_Yvalue = 0;
                }
                else if (rando == 6)
                {        // CBA
                    circleA_Yvalue = -2.5f;
                    cirlceB_Yvalue = 0;
                    circleC_Yvalue = 2.5f;
                }
                else
                {
                    Debug.Log("shouldn't have gotten here");
                }
            }

            CircleAScript.defaultPosition = new Vector2(defaultX, circleA_Yvalue);
            CircleBScript.defaultPosition = new Vector2(defaultX, cirlceB_Yvalue);
            CircleCScript.defaultPosition = new Vector2(defaultX, circleC_Yvalue);
        }
        else if (tally == 2)
        {
            float circleA_Yvalue = 1.5f;
            float circleB_Yvalue = -1.5f;

            if (debugModeOn == false && randomizingThisTimeWouldBeAnnoying == false)
            {
                // there are 2! ways to order them, so this will be quick
                Debug.Log("2 circles, gonna randomize positions");
                int rando = Random.Range(1, 3);
                if (rando == 2)
                {
                    circleA_Yvalue = -1.5f;
                    circleB_Yvalue = 1.5f;
                }
            }

            if (randomizingThisTimeWouldBeAnnoying)
            {
                // so, after an equation is solved, one of the two circles will be near the operators... the OTHER circle, if it's down low, should remain down low or vice-versa

                // A is to the right, so examine B & C
                if (CircleA.activeSelf && CircleA.transform.position.x > -2)
                {
                    // CircleA just got changed, therefore the OTHER circle needs to be moved as little as possible... but right now we don't know if that other circle is CircleB or CircleC
                    if (CircleB.activeSelf)
                    {
                        if (CircleB.transform.position.y == 2.5f)
                        {
                            CircleBScript.defaultPosition = new Vector2(defaultX, 1.5f);        // keep it high
                            CircleAScript.defaultPosition = new Vector2(defaultX, -1.5f);
                        }
                        else if (CircleB.transform.position.y == 0)
                        {
                            CircleAScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleBScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                        else if (CircleB.transform.position.y == -2.5f)
                        {
                            CircleAScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleBScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                    }
                    else if (CircleC.activeSelf)
                    {
                        if (CircleC.transform.position.y == 2.5f)
                        {
                            CircleCScript.defaultPosition = new Vector2(defaultX, 1.5f);        // keep it high
                            CircleAScript.defaultPosition = new Vector2(defaultX, -1.5f);
                        }
                        else if (CircleC.transform.position.y == 0)
                        {
                            CircleAScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleCScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                        else if (CircleC.transform.position.y == -2.5f)
                        {
                            CircleAScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleCScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                    }

                }
                // B is to the right, so examine A & C
                else if (CircleB.activeSelf && CircleB.transform.position.x > -2)
                {
                    if (CircleA.activeSelf)
                    {
                        if (CircleA.transform.position.y == 2.5f)
                        {
                            CircleAScript.defaultPosition = new Vector2(defaultX, 1.5f);        // keep it high
                            CircleBScript.defaultPosition = new Vector2(defaultX, -1.5f);
                        }
                        else if (CircleA.transform.position.y == 0)
                        {
                            CircleBScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleAScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                        else if (CircleA.transform.position.y == -2.5f)
                        {
                            CircleBScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleAScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                    }
                    else if (CircleC.activeSelf)
                    {
                        if (CircleC.transform.position.y == 2.5f)
                        {
                            CircleCScript.defaultPosition = new Vector2(defaultX, 1.5f);        // keep it high
                            CircleBScript.defaultPosition = new Vector2(defaultX, -1.5f);
                        }
                        else if (CircleC.transform.position.y == 0)
                        {
                            CircleBScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleCScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                        else if (CircleC.transform.position.y == -2.5f)
                        {
                            CircleBScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleCScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                    }

                }
                // C is to the right, so examine A & B
                else if (CircleC.activeSelf && CircleC.transform.position.x > -2)
                {
                    if (CircleA.activeSelf)
                    {
                        if (CircleA.transform.position.y == 2.5f)
                        {
                            CircleAScript.defaultPosition = new Vector2(defaultX, 1.5f);        // keep it high
                            CircleCScript.defaultPosition = new Vector2(defaultX, -1.5f);
                        }
                        else if (CircleA.transform.position.y == 0)
                        {
                            CircleCScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleAScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                        else if (CircleA.transform.position.y == -2.5f)
                        {
                            CircleCScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleAScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                    }
                    else if (CircleB.activeSelf)
                    {
                        if (CircleB.transform.position.y == 2.5f)
                        {
                            CircleBScript.defaultPosition = new Vector2(defaultX, 1.5f);        // keep it high
                            CircleCScript.defaultPosition = new Vector2(defaultX, -1.5f);
                        }
                        else if (CircleB.transform.position.y == 0)
                        {
                            CircleCScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleBScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                        else if (CircleB.transform.position.y == -2.5f)
                        {
                            CircleCScript.defaultPosition = new Vector2(defaultX, 1.5f);
                            CircleBScript.defaultPosition = new Vector2(defaultX, -1.5f);        // move it down
                        }
                    }
                }
            }
            else
            {
                if (CircleA.activeSelf && CircleB.activeSelf)
                {
                    CircleAScript.defaultPosition = new Vector2(defaultX, circleA_Yvalue);
                    CircleBScript.defaultPosition = new Vector2(defaultX, circleB_Yvalue);
                }
                else if (CircleA.activeSelf && CircleC.activeSelf)
                {
                    CircleAScript.defaultPosition = new Vector2(defaultX, circleA_Yvalue);
                    CircleCScript.defaultPosition = new Vector2(defaultX, circleB_Yvalue);
                }
                else if (CircleB.activeSelf && CircleC.activeSelf)
                {
                    CircleBScript.defaultPosition = new Vector2(defaultX, circleA_Yvalue);
                    CircleCScript.defaultPosition = new Vector2(defaultX, circleB_Yvalue);
                }
            }
        }
        else if (tally == 1)
        {
            if (CircleA.activeSelf) { CircleAScript.defaultPosition = new Vector2(defaultX, 0); }
            else if (CircleB.activeSelf) { CircleBScript.defaultPosition = new Vector2(defaultX, 0); }
            else if (CircleC.activeSelf) { CircleCScript.defaultPosition = new Vector2(defaultX, 0); }
        }
        else if (tally == 0)
        {
            Debug.Log("problem: we have tally of 0");
        }





    }
    public void SendAllCirclesToDefaultPositions()
    {
        CircleAScript.BeginMovementToDefaultPosition(false, true, true);
        CircleBScript.BeginMovementToDefaultPosition(false, true, true);
        CircleCScript.BeginMovementToDefaultPosition(false, true, true);

    }
    public void TeleportAllCirclesToDefaultPositions()
    {
        CircleAScript.TeleportToDefaultPosition();
        CircleBScript.TeleportToDefaultPosition();
        CircleCScript.TeleportToDefaultPosition();
    }

    public void UpdateDefaultPositionsOfOperators()
    {
        float operatorA_Yvalue = 1.5f;
        float operatorB_Yvalue = -1.5f;

        if (debugModeOn == false)
        {
            // there are 2! ways to order them, so this will be quick
            int rando = Random.Range(1, 3);
            if (rando == 2)
            {
                operatorA_Yvalue = -1.5f;
                operatorB_Yvalue = 1.5f;
            }
        }

        OperatorAScript.defaultPosition = new Vector2(0.3f, operatorA_Yvalue);
        OperatorBScript.defaultPosition = new Vector2(0.3f, operatorB_Yvalue);

    }
    public void SendAllOperatorsToDefaultPositions()
    {

    }
    public void TeleportAllOperatorsToDefaultPositions()
    {
        OperatorAScript.TeleportToDefaultPosition();
        OperatorBScript.TeleportToDefaultPosition();
    }
    public void SkipPuzzle()
    {

        CreateNewPuzzle();
    }
    //bool noErrorReceived = true;
    //private void OnEnable()
    //{
    //    Application.logMessageReceived += HandleLog;
    //}
    //void HandleLog(string logString, string stackTrace, LogType type) { 
    //    if (type == LogType.Error) {
    //        noErrorReceived = false;
    //    }
    //}
    //public void SkipPuzzleForever() {
    //    // this method exists to suss out errors for debugging purposes

    //    // https://stackoverflow.com/questions/45341179/how-to-detect-if-there-is-an-error-in-unity-c-sharp-when-running-without-see-the
    //    while (noErrorReceived) {
    //        Debug.Log("*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$*$$");
    //        CreateNewPuzzle();



    //    }
    //}
    // ************************************************************************************************************
    // ************************************************************************************************************
    public void TurnDebugModeON()
    {
        debugModeOn = true;
        PlayerPrefs.SetInt(debugPlayerPrefString, 1);
    }
    public void TurnDebugModeOFF()
    {
        debugModeOn = false;
        PlayerPrefs.SetInt(debugPlayerPrefString, 0);
    }

    public void TurnNegativeNumbersON()
    {
        negativeNumbersAllowed = true;
        lowerLimitForResult = -125;
        //lowerLimitForCircleValue = -20;
        PlayerPrefs.SetInt(negativesPlayerPrefString, 1);
        //GameManager.instance.negativeNumbersAllowed = true;
        //GameManager.instance.TurnNegativeNumbersON();
    }
    public void TurnNegativeNumbersOFF()
    {
        negativeNumbersAllowed = false;
        lowerLimitForResult = 1;
        //lowerLimitForCircleValue = 1;
        PlayerPrefs.SetInt(negativesPlayerPrefString, 0);
        //GameManager.instance.negativeNumbersAllowed = false;
        //GameManager.instance.TurnNegativeNumbersOFF();
    }
    public bool ReturnNegativeNumbersON_OFF()
    {
        return negativeNumbersAllowed;
    }
    public void TurnFractionsON()
    {
        fractionsAllowed = true;
        PlayerPrefs.SetInt(fractionsPlayerPrefString, 1);
        //GameManager.instance.fractionsAllowed = true;
    }
    public void TurnFractionsOFF()
    {
        fractionsAllowed = false;
        PlayerPrefs.SetInt(fractionsPlayerPrefString, 0);
        //GameManager.instance.fractionsAllowed = false;
    }
    public bool ReturnFractionsON_OFF()
    {
        return fractionsAllowed;
    }
    public void TurnExponentsON()
    {
        exponentsAllowed = true;
        PlayerPrefs.SetInt(exponentsPlayerPrefString, 1);
        //GameManager.instance.exponentsAllowed = true;
    }
    public void TurnExponentsOFF()
    {
        exponentsAllowed = false;
        PlayerPrefs.SetInt(exponentsPlayerPrefString, 0);
        //GameManager.instance.exponentsAllowed = false;
    }
    public bool ReturnExponentsON_OFF()
    {
        return exponentsAllowed;
    }
    public void TurnMultDivideON()
    {
        multDivideAllowed = true;
        PlayerPrefs.SetInt(multDividePlayerPrefString, 1);
        //GameManager.instance.multDivideAllowed = true;
    }
    public void TurnMultDivideOFF()
    {
        multDivideAllowed = false;
        PlayerPrefs.SetInt(multDividePlayerPrefString, 0);
        //GameManager.instance.multDivideAllowed = false;
    }
    public bool ReturnMultDivideON_OFF()
    {
        return multDivideAllowed;
    }
    // ************************************************************************************************************
    // ************************************************************************************************************
    public bool CheckIfNumbersAreCloseEnough(float num1, float num2)
    {
        float marginOfError = 0.00001f;
        if (Mathf.Abs(num1 - num2) < marginOfError)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void CreateNewPuzzle()
    {
        Debug.Log("tesssst");
        Debug.Log("test message, CircleA.name: " + CircleA.name);
        DEBUG_outputCircleValues("CreateNewPuzzle at start");
        // get rid of all the Circles & Operators we created in past puzzles
        Circle.numberOfThisTypeThatExist = 0;
        listOfAllCircles.Clear();
        Operator.numberOfThisTypeThatExist = 0;
        listOfAllOperators.Clear();

        ResetColorsAndMath_Circles_Operators();

        Debug.Log("***********************        *************************");
        int ABC = Random.Range(1, 3);   // this will determine whether we're starting by creating PartA, or starting by creating PartB
        char A_B_C = 'x';
        if (ABC == 1)
        {
            A_B_C = 'A';
            Debug.Log("we start by creating PartA");
        }
        else if (ABC == 2)
        {
            A_B_C = 'B';
            Debug.Log("we start by creating PartB");
        }
        else if (ABC == 3)
        {
            A_B_C = 'C';
            //Debug.Log("we start by creating PartC");        // this isn't currently supported
        }
        if (gameType == "kiddy")
        {
            // hmm....
            //A_B_C = 'B';
            // we set it to 'B' because this forces the result to be an integer
        }


        Operator oppy = PickRandomOperator(A_B_C, false, null);       // PickRandomOperator() will contain the logic to handle the allowal/disallowal of multDivide & exponents



        Debug.Log("operator selected: " + oppy.type);

        Circle inputCircleA = null;
        Circle inputCircleB = null;
        Circle outputCircle = null;

        if (oppy.type == "addition")
        {
            List<float> stuff = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, true, negativeNumbersAllowed, fractionsAllowed);
            inputCircleA = CreateRandomCircle(stuff);
            inputCircleB = CreateRandomSecondCircleThatResultsInInt(oppy, inputCircleA, upperLimitForResult, upperLimitForCircleValue, true, negativeNumbersAllowed, fractionsAllowed);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleB, oppy);
        }
        else if (oppy.type == "subtraction")
        {
            List<float> stuff = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, false, negativeNumbersAllowed, false);
            // don't want circle1 to be 1, because 1 - x = B, where B MUST BE positive and not a fraction, is impossible
            inputCircleA = CreateRandomCircle(stuff);
            inputCircleB = CreateRandomSecondCircleThatResultsInInt(oppy, inputCircleA, upperLimitForResult, upperLimitForCircleValue, true, negativeNumbersAllowed, fractionsAllowed);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleB, oppy);
        }
        else if (oppy.type == "multiplication" || oppy.type == "division")
        {
            List<float> stuff = CreateListOfPossibleCircleValues_forArithmetic(upperLimitForCircleValue, false, negativeNumbersAllowed, fractionsAllowed);
            inputCircleA = CreateRandomCircle(stuff);
            inputCircleB = CreateRandomSecondCircleThatResultsInInt(oppy, inputCircleA, upperLimitForResult, upperLimitForCircleValue, false, negativeNumbersAllowed, fractionsAllowed);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleB, oppy);
            Debug.Log("%%%%%%%%%%%%%%%% ok we did division, and inputCircleA.value = " + inputCircleA.value + ", and inputCircleB.value = " + inputCircleB.value);
        }
        else if (oppy.type == "exponent2")
        {
            List<float> stuff = null;
            if (gameType == "kiddy")
            {
                stuff = CreateListOfPossibleCircleValues_forExponent2(upperLimitForResult, false, negativeNumbersAllowed, false);
            }
            else
            {
                if (A_B_C == 'A')
                {
                    stuff = CreateListOfPossibleCircleValues_forExponent2(upperLimitForResult, false, negativeNumbersAllowed, fractionsAllowed);
                }
                else if (A_B_C == 'B')
                {
                    stuff = CreateListOfPossibleCircleValues_forExponent2(upperLimitForResult, false, negativeNumbersAllowed, false);
                }
            }
            inputCircleA = CreateRandomCircle(stuff);
            //inputCircleB = CreateSpecificCircle(-999999999);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, oppy);
        }
        else if (oppy.type == "exponent3")
        {
            List<float> stuff = null;
            if (gameType == "kiddy")
            {
                stuff = CreateListOfPossibleCircleValues_forExponent3(upperLimitForResult, false, negativeNumbersAllowed, false);
            }
            else
            {
                stuff = CreateListOfPossibleCircleValues_forExponent3(upperLimitForResult, false, negativeNumbersAllowed, fractionsAllowed);
            }
            inputCircleA = CreateRandomCircle(stuff);
            //inputCircleB = CreateSpecificCircle(-999999999);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, oppy);
        }
        else if (oppy.type == "squareRoot")
        {
            List<float> stuff = null;
            if (gameType == "kiddy")
            {
                stuff = CreateListOfPossibleCircleValues_forSquareRoot(upperLimit_ValueToBeSquared, false, negativeNumbersAllowed, false);
            }
            else
            {
                if (A_B_C == 'B')
                {
                    stuff = CreateListOfPossibleCircleValues_forSquareRoot(upperLimit_ValueToBeSquared, false, negativeNumbersAllowed, false);
                }
                else if (A_B_C == 'A')
                {
                    stuff = CreateListOfPossibleCircleValues_forSquareRoot(upperLimit_ValueToBeSquared, false, negativeNumbersAllowed, fractionsAllowed);
                }
            }
            inputCircleA = CreateRandomCircle(stuff);
            //inputCircleB = CreateSpecificCircle(-999999999);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, oppy);
        }
        else if (oppy.type == "cubeRoot")
        {
            List<float> stuff = null;
            if (gameType == "kiddy")
            {
                stuff = CreateListOfPossibleCircleValues_forCubeRoot(upperLimit_ValueToBeCubed, false, negativeNumbersAllowed, false);
            }
            else
            {
                stuff = CreateListOfPossibleCircleValues_forCubeRoot(upperLimit_ValueToBeCubed, false, negativeNumbersAllowed, fractionsAllowed);
            }
            inputCircleA = CreateRandomCircle(stuff);
            //inputCircleB = CreateSpecificCircle(-999999999);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, oppy);
        }
        Debug.Log("first value: " + inputCircleA.value);
        Debug.Log("operator: " + oppy.type);
        if (inputCircleB != null)
        {
            Debug.Log("second value: " + inputCircleB.value);
        }
        Debug.Log("result: " + outputCircle.value);

        // CREATE THE OTHER PART OF THE PROBLEM
        //      unlike above, where we started with an operator, this time we will be starting with a circle
        //      so we need to determine which operators will work with this circle
        //          for example, if circle.value == 0.25, then we know we can't use exponent2 (as it will result in 0.25^2 = 0.125)
        //          for example, if circle.value == 100, then we know we can't use cubeRoot (as it will result in 100^(1/3) = 4.641588....

        Circle circle1 = null;
        Circle circle2 = null;
        Circle circle3 = null;
        //Circle circle4 = null;
        //Circle circle5 = null;
        Operator operator1 = null;
        Operator operator2 = null;
        //Operator operator3 = null;
        Circle result = null;

        if (gameType == "kiddy")
        {
            // there will only be two circles
            // one operator is already defined, and we just need to add in a random 2nd operator for the user to not use it
            circle1 = inputCircleA;
            circle2 = inputCircleB;
            //circle3 DNE
            operator1 = oppy;
            // operator2 needs to be different from operator1/oppy
            operator2 = PickRandomOperator('x', true, oppy);
            result = outputCircle;


        }
        else
        {
            // depending on what A_B_C is, will affect which of the following we choose:
            if (A_B_C == 'A')
            {
                // we already created PartA, so now we create PartB
                List<OpsAndCircles> PartBStuff = CreatePartB_GivenInitial(outputCircle, oppy);
                circle1 = inputCircleA;
                //if (inputCircleB != null) {
                circle2 = inputCircleB; // might be null
                                        //}
                                        //if ((Circle)PartBStuff[1] != null) {
                circle3 = (Circle)PartBStuff[1]; // might be null
                                                 //}
                operator1 = oppy;
                operator2 = (Operator)PartBStuff[0];
                result = (Circle)PartBStuff[2];
                if (!TestIfIsInteger(result.value))
                {
                    Debug.Log("*********************************************************************************************");
                    Debug.Log("*********************************************************************************************");
                    Debug.Log("result is NOT AN INTEGER: " + result.value);
                    Debug.Log("*********************************************************************************************");
                    Debug.Log("*********************************************************************************************");
                }
            }
            else if (A_B_C == 'B')
            {
                // we already created PartB, so now we create PartA
                Debug.Log("%%%%%%%%%%%%%%%% about to create PartA");
                List<OpsAndCircles> PartAStuff = CreatePartA_GivenInitial(inputCircleA, inputCircleB, oppy);
                //Debug.Log("hopefully this catches the bug: circleA: " + inputCircleA.value + " circleB: " + inputCircleB.value + " operator: " + oppy.type);
                circle1 = (Circle)PartAStuff[1];
                //if ((Circle)PartAStuff[2] != null) {
                circle2 = (Circle)PartAStuff[2]; // might be null
                                                 //}
                                                 // the above function will tell us which PartA result was used as the first input for PartB
                                                 //      we need to identify which PartA result was used, and then make the OTHER ONE appear in the puzzle
                Circle resultFromPartA = (Circle)PartAStuff[3];
                if (circle1 != null && circle2 != null)
                {
                    Debug.Log("%%%%%%%%%%%%%%%%%%%% finished creating PartA, and circle1.value = " + circle1.value + ", and circle2.value = " + circle2.value);
                }
                Debug.Log("%%%%%%%%%%%%%%%%%%%%%%%%% resultFromPartA = " + resultFromPartA.value);
                //Debug.Log("resultFromPartA value: " + resultFromPartA.value);
                //Debug.Log("PartB inputCircleA: " + inputCircleA.value + "  ...   inputCircleB: " + inputCircleB.value);
                if (CheckIfNumbersAreCloseEnough(resultFromPartA.value, inputCircleA.value))
                //if (resultFromPartA.value == inputCircleA.value)        // THIS LINE seems to be where errors are spawning
                {
                    Debug.Log("%%%%%%%%%% aaa");
                    circle3 = inputCircleB;
                }
                else if (CheckIfNumbersAreCloseEnough(resultFromPartA.value, inputCircleB.value))
                //if (resultFromPartA.value == inputCircleB.value)       // error spawned here: start with PartB, 5^2 = 25, .... i think i fixed it: when 1circle math, force creating of inputCircleB.value = 0
                {
                    Debug.Log("%%%%%%%%%% bbb");
                    circle3 = inputCircleA;
                }
                else
                {
                    Debug.Log("%%%%%%%%% it DID NOT EQUAL EITHER OF THEM and i think i found the bug, because circle3 is remaining null");
                    Debug.Log("%%%%%% resultFromPartA: " + resultFromPartA.value + ", " + inputCircleA.value + ", " + inputCircleB.value);
                }
                operator1 = (Operator)PartAStuff[0];
                Debug.Log("%%%%%%%%%%%%%%%% partA operator: " + operator1.type);
                operator2 = oppy;
                result = outputCircle;
            }
        }


        //Debug.Log("******************************************");
        //Debug.Log("circle1: " + circle1.value);
        //if (circle2 != null) {
        //    Debug.Log("circle2: " + circle2.value);
        //}
        //if (circle3 != null) {
        //    Debug.Log("circle3: " + circle3.value);
        //}
        //Debug.Log("operator1: " + operator1.type);
        //Debug.Log("operator2: " + operator2.type);
        //Debug.Log("final result: " + result.value);


        void PutPuzzleIntoGame()
        {
            // put the puzzle into the game for the user to see

            // how many valid circles do we have? 1 or 2 or 3?

            // also, if the value of a circle is zero, as far as we're concerned it's null, so don't show that circle
            //bool circleA_active = false;
            //bool circleB_active = false;
            //bool circleC_active = false;
            // ONE OPTION HERE IS TO set the circle to null if the value is zero... but i also want to catch the bug
            Debug.Log("about to call SetCircle() for CircleA & circle1");
            SetCircle(CircleA, circle1);

            Debug.Log("about to call SetCircle() for CircleB & circle2");
            SetCircle(CircleB, circle2);
            Debug.Log("about to call SetCircle() for CircleC & circle3");
            SetCircle(CircleC, circle3);




            //bool A_active = CircleA.activeSelf;
            //bool B_active = CircleB.activeSelf;
            //bool C_active = CircleC.activeSelf;

            UpdateDefaultPositionsOfCircles(false);
            //CircleA.SetActive(false);
            //CircleB.SetActive(false);
            //CircleC.SetActive(false);
            //SendAllCirclesToDefaultPositions();
            TeleportAllCirclesToDefaultPositions();
            //CircleA.SetActive(A_active);
            //CircleB.SetActive(B_active);
            //CircleC.SetActive(C_active);



            //MathInProgress.GetComponent<TextMeshPro>().text = "";

            SetOperator(OperatorA, operator1);
            SetOperator(OperatorB, operator2);
            operator1_forStats = operator1.type;        // if the player solves the puzzle, these strings will be used to update stats contained in PlayerPrefs
            operator2_forStats = operator2.type;
            UpdateDefaultPositionsOfOperators();
            TeleportAllOperatorsToDefaultPositions();


            Goal.transform.GetChild(0).GetComponent<TextMeshPro>().text = result.value.ToString("F0");      // goal is always an int anyway
            Goal.GetComponent<Clickable>().valueOfThisCircle_orGoal = result.value;


            // need to catch the bug where not enough circles are created
            Debug.Log("***********************        *************************");
            float circle1value;
            float circle2value;
            float circle3value;
            if (circle1 != null)
            {
                circle1value = circle1.value;
            }
            else
            {
                circle1value = -99999;
            }
            if (circle2 != null)
            {
                circle2value = circle2.value;
            }
            else
            {
                circle2value = -99999;
            }
            if (circle3 != null)
            {
                circle3value = circle3.value;
            }
            else
            {
                circle3value = -99999;
            }
            Debug.Log("circle1.value: " + circle1value + "         circle2.value: " + circle2value + "         circle3.value: " + circle3value);
            Debug.Log("operator1.type: " + operator1.type + "             operator2.type: " + operator2.type);
            Debug.Log("goal: " + result.value);
        }

        //GameManager.instance.DisplayLevel();
        PutPuzzleIntoGame();

        // do the Timer stuff here

        //      when the game starts, and the very first puzzle is created
        //          the global timer is set to 90 seconds & starts counting down
        //          the puzzle timer is RESET to 10 seconds & starts counting down


        if (gameType == "timed")
        {
            timerPuzzle.ResetPuzzleTimer();
            timerGlobal.UnpauseGlobalTimer();
            timerPuzzle.UnpausePuzzleTimer();

            GameManager.instance.DecreaseNumberOfPuzzlesRemaining(1);
        }

        // now make the circles operators & goal invisible so we can do animations before loading new puzzle
        CircleA.SetActive(false);
        CircleB.SetActive(false);
        CircleC.SetActive(false);
        OperatorA.SetActive(false);
        OperatorB.SetActive(false);
        Goal.SetActive(false);

        if (gameOver == false && GameManager.instance.enemyInRange == false) {
            Debug.Log("abc abc 123");
            StartCoroutine(WaitBeforeNext_MakePuzzleAppear());
        }




    }
    public void SetGameOver() {
        gameOver = true;
    }
    public void UndoGameOver() {
        gameOver = false;
        Goal.GetComponent<Clickable>().CancelShrinkIt();
    }
    public void MakePuzzleAppearAfterOneSecond() {
        StartCoroutine(WaitBeforeNext_MakePuzzleAppear());
    }
    IEnumerator WaitBeforeNext_MakePuzzleAppear() {
        yield return new WaitForSeconds(1);
        MakePuzzleAppear();
    }
    public void MakePuzzleAppear() {


        if (CircleAScript.partOfCurrentPuzzle == true) {
            CircleA.SetActive(true);
            //CircleAScript.defaultScale = CircleA.GetComponent<RectTransform>().localScale;
            CircleA.GetComponent<RectTransform>().localScale = new Vector2(0.1f, 0.1f);

            // randomly choose which background image to use for this circle
            CircleAScript.RandomlyChooseBackgroundImage();
        }
        if (CircleBScript.partOfCurrentPuzzle == true) {
            CircleB.SetActive(true);
            //CircleBScript.defaultScale = CircleB.GetComponent<RectTransform>().localScale;
            CircleB.GetComponent<RectTransform>().localScale = new Vector2(0.1f, 0.1f);
            CircleBScript.RandomlyChooseBackgroundImage();
        }
        if (CircleCScript.partOfCurrentPuzzle == true) {
            CircleC.SetActive(true);
            //CircleCScript.defaultScale = CircleC.GetComponent<RectTransform>().localScale;
            CircleC.GetComponent<RectTransform>().localScale = new Vector2(0.1f, 0.1f);
            CircleCScript.RandomlyChooseBackgroundImage();
        }
        
        OperatorA.SetActive(true);
        //OperatorAScript.defaultScale = OperatorA.GetComponent<Transform>().localScale;
        //OperatorAScript.defaultScale = OperatorA.GetComponent<RectTransform>().localScale;
        //OperatorA.GetComponent<RectTransform>().localScale = new Vector2(0.1f, 0.1f);
        OperatorA.GetComponent<Transform>().localScale = new Vector2(0.1f, 0.1f);


        OperatorB.SetActive(true);
        //OperatorBScript.defaultScale = OperatorB.GetComponent<Transform>().localScale;
        //OperatorAScript.defaultScale = OperatorA.GetComponent<RectTransform>().localScale;
        //OperatorA.GetComponent<RectTransform>().localScale = new Vector2(0.1f, 0.1f);
        OperatorB.GetComponent<Transform>().localScale = new Vector2(0.1f, 0.1f);


        Goal.SetActive(true);
        //Goal.GetComponent<Clickable>().defaultScale = Goal.GetComponent<RectTransform>().localScale;
        //Goal.GetComponent<Clickable>().defaultScale = Goal.GetComponent<Transform>().localScale;
        //Goal.GetComponent<RectTransform>().localScale = new Vector2(0.1f, 0.1f);
        Goal.GetComponent<Transform>().localScale = new Vector2(0.1f, 0.1f);
        Goal.transform.position = new Vector2(4.5f, 0);


        CircleAScript.growing = true;
        CircleBScript.growing = true;
        CircleCScript.growing = true;
        OperatorAScript.growing = true;
        OperatorBScript.growing = true;
        Goal.GetComponent<Clickable>().CancelShrinkIt();
        Goal.GetComponent<Clickable>().growing = true;

    }
    public void MakeCirclesOperatorsGoal_moveLeftForNitrous() {
        CircleAScript.BeginMovementOffscreenToLeft();
        CircleBScript.BeginMovementOffscreenToLeft();
        CircleCScript.BeginMovementOffscreenToLeft();
        OperatorAScript.BeginMovementOffscreenToLeft();
        OperatorBScript.BeginMovementOffscreenToLeft();
        Goal.GetComponent<Clickable>().BeginMovementOffscreenToLeft();
    }
    // ************************************************************************************************************
    // ************************************************************************************************************

    public void ResetColorsAndMath_Circles_Operators()
    {
        CircleA.GetComponent<SpriteRenderer>().color = whiteColor;
        CircleB.GetComponent<SpriteRenderer>().color = whiteColor;
        CircleC.GetComponent<SpriteRenderer>().color = whiteColor;
        OperatorA.GetComponent<SpriteRenderer>().color = whiteColor;
        OperatorB.GetComponent<SpriteRenderer>().color = whiteColor;

        circle1selected = false;
        highlightedCircle1 = null;
        value1 = 0;

        operatorSelected = false;
        highlightedOperator = null;

        circle2selected = false;
        highlightedCircle2 = null;
        value2 = 0;

        math_oneCircle_IsComplete = false;
        math_twoCircle_IsComplete = false;

        CircleAScript.rotating = false;
        CircleBScript.rotating = false;
        CircleCScript.rotating = false;
        OperatorAScript.rotating = false;
        OperatorBScript.rotating = false;
    }
    public void HighlightCircleGameObject(GameObject obby, int oneORtwo)
    {
        if (oneORtwo == 1)
        {
            highlightedCircle1 = obby;
            highlightedCircle1.GetComponent<SpriteRenderer>().color = highlightedColor;
            // move the circle over to -3.9f on x axis
            //highlightedCircle1.GetComponent<Clickable>().defaultPosition = new Vector2(-4, transform.position.y);
            // and the circle "wiggles" a bit
            //highlightedCircle1.GetComponent<Clickable>().wiggling = true;
            highlightedCircle1.GetComponent<Clickable_circle>().BeginRotating();

        }
        else if (oneORtwo == 2)
        {
            highlightedCircle2 = obby;
            highlightedCircle2.GetComponent<SpriteRenderer>().color = highlightedColor;
            highlightedCircle2.GetComponent<Clickable_circle>().BeginRotating();
        }
    }
    public void UNHighlightCircleGameObject(GameObject obby)
    {
        // stop the highlight animation and/or reverse the highlighted color and make it the normal color
        obby.GetComponent<SpriteRenderer>().color = whiteColor;
        //highlightedCircle1.GetComponent<Clickable>().wiggling = false;
        //highlightedCircle1.GetComponent<Clickable>().BeginMovementToDefaultPosition();
        obby.GetComponent<Clickable_circle>().EndRotating();
    }
    public void HighLightOperatorGameObject(GameObject obby)
    {
        highlightedOperator = obby;
        highlightedOperator.GetComponent<SpriteRenderer>().color = highlightedColor;
        //highlightedOperator.GetComponent<Clickable_operator>().BeginRotating();
        Debug.Log("the operator should be rotating now " + highlightedOperator.name);
    }
    public void UNHighlightOperatorGameObject(GameObject obby)
    {
        obby.GetComponent<SpriteRenderer>().color = whiteColor;
        obby.GetComponent<Clickable_operator>().EndRotating();

    }

    // ************************************************************************************************************
    // ************************************************************************************************************
    public void AcceptClickedCircleOrOperator(GameObject gameObjectClicked)
    {
        Debug.Log("------------------------------------------------------------------ just entered AcceptClickedCircleOrOperator()");
        //Debug.Log("circle1selected: " + circle1selected);
        if (circle1selected == false && operatorSelected == false && math_oneCircle_IsComplete == false && math_twoCircle_IsComplete == false)
        {
            // this is the stage where we select the first Circle... the player can change which one they want up until they click an operator
            if (gameObjectClicked.CompareTag("circle"))
            {
                value1 = GetValueOfClickedCircle(gameObjectClicked);
                HighlightCircleGameObject(gameObjectClicked, 1);
                circle1selected = true;
                Debug.Log("circle1selected just set to true");
            }
            else if (gameObjectClicked.CompareTag("operator"))
            {
                // do nothing
                Debug.Log("you clicked an operator, but nothing will happen");
            }
        }
        else if (circle1selected == true && operatorSelected == false && math_oneCircle_IsComplete == false && math_twoCircle_IsComplete == false)
        {
            if (gameObjectClicked.CompareTag("circle"))
            {
                if (gameObjectClicked != highlightedCircle1)
                {
                    // simply change which circle is highlighted
                    value1 = GetValueOfClickedCircle(gameObjectClicked);
                    UNHighlightCircleGameObject(highlightedCircle1);
                    HighlightCircleGameObject(gameObjectClicked, 1);
                }
                else if (gameObjectClicked == highlightedCircle1)
                {
                    //Debug.Log("just clicked the circle that is already highlighted");
                    // the highlighted circle is no longer highlighted
                    value1 = 0;
                    UNHighlightCircleGameObject(highlightedCircle1);
                    circle1selected = false;
                    Debug.Log("circle1selected just set to false");
                    highlightedCircle1 = null;
                }
            }
            else if (gameObjectClicked.CompareTag("operator"))
            {
                //Debug.Log("reached line 3322");
                highlightedOperator = gameObjectClicked;
                HighLightOperatorGameObject(gameObjectClicked);
                operatorSelected = true;    // this prevents the player from quickly clicking the other operator while the animation plays out, and causing a glitch
                //Debug.Log("reached line 3326, operatorSelected: " + operatorSelected);
                DetermineWhether_oneCircle_MathIsComplete();
                //Debug.Log("reached line 3328, operatorSelected: " + operatorSelected);
                if (math_oneCircle_IsComplete)
                {
                    executingONEcircleMath = true;
                    executingTWOcircleMath = false;
                    ExecuteCompletionOf_oneCircle_Math(false, false);
                    //DetermineWhetherPuzzleIsSolved();     July 21 2022
                }
                // adding this June 30 2022
                DetermineWhether_twoCircle_MathIsComplete();
                if (math_twoCircle_IsComplete)
                {
                    executingONEcircleMath = false;
                    executingTWOcircleMath = true;
                    ExecuteCompletionOf_twoCircle_Math(false);
                }
            }
        }
        else if (circle1selected == true && operatorSelected == true && math_oneCircle_IsComplete == false && math_twoCircle_IsComplete == false)
        {
            //Debug.Log("reached line 3348");
            // ready to accept circle2... CANNOT change circle1 at this point... but CAN CHANGE operator
            if (gameObjectClicked.CompareTag("circle"))
            {
                if (gameObjectClicked == highlightedCircle1)
                {
                    // buuuut... you CANNOT click the same circle because it has already disappeared
                    // but if you COULD click it: 
                    // undo EVERYTHING
                    value1 = 0;
                    UNHighlightCircleGameObject(highlightedCircle1);
                    circle1selected = false;
                    highlightedCircle1 = null;
                    UNHighlightOperatorGameObject(highlightedOperator);
                    highlightedOperator = null;
                    operatorSelected = false;
                }
                else
                {
                    value2 = GetValueOfClickedCircle(gameObjectClicked);
                    HighlightCircleGameObject(gameObjectClicked, 2);
                    circle2selected = true;
                    DetermineWhether_twoCircle_MathIsComplete();
                    if (math_twoCircle_IsComplete)
                    {
                        executingONEcircleMath = false;
                        executingTWOcircleMath = true;
                        ExecuteCompletionOf_twoCircle_Math(false);
                        //DetermineWhetherPuzzleIsSolved();     July 21 2022
                    }
                }
            }
            else if (gameObjectClicked.CompareTag("operator"))
            {
                if (gameObjectClicked == highlightedOperator)
                {
                    // you CANNOT CLICK on this operator because it has already disappeared
                    // unhighlight
                    UNHighlightOperatorGameObject(highlightedOperator);
                    highlightedOperator = null;
                    operatorSelected = false;
                }
                else
                {
                    // change the operator
                    UNHighlightOperatorGameObject(highlightedOperator);
                    highlightedOperator = gameObjectClicked;
                    HighLightOperatorGameObject(gameObjectClicked);
                    DetermineWhether_oneCircle_MathIsComplete();
                    if (math_oneCircle_IsComplete)
                    {
                        executingONEcircleMath = true;
                        executingTWOcircleMath = false;
                        ExecuteCompletionOf_oneCircle_Math(false, false);
                    }
                    // adding this June 30 2022
                    DetermineWhether_twoCircle_MathIsComplete();
                    if (math_twoCircle_IsComplete)
                    {
                        executingONEcircleMath = false;
                        executingTWOcircleMath = true;
                        ExecuteCompletionOf_twoCircle_Math(false);
                    }
                }
            }
        }






    }
    // ************************************************************************************************************
    // ************************************************************************************************************
    public string GetTypeOfClickedOperator(GameObject oppy)
    {
        string toReturn = oppy.GetComponent<Clickable_operator>().typeOfThisOperator;
        return toReturn;
    }

    public float GetValueOfClickedCircle(GameObject circleClicked)
    {
        float value = 0;
        if (circleClicked == CircleA)
        {
            //Debug.Log("CircleA was clicked");
            //Debug.Log("by the way, the length of listOfAllCircles is: " + listOfAllCircles.Count);
            //Debug.Log("numberOfThisTypeThatExist: " + Circle.numberOfThisTypeThatExist);
            // find the circleData associated with CircleA... we can find it because we previously marked both the GameObject and the Circle so we know they're associated
            int IDnum = CircleA.GetComponent<Clickable_circle>().IDnumberOfCircleDataAttachedToThis;
            for (int i = 0; i < listOfAllCircles.Count; i++)
            {
                if (listOfAllCircles[i].IDnumber == IDnum)
                {
                    value = listOfAllCircles[i].value;
                }
            }
            //Debug.Log("value of CircleA: " + value);

        }
        else if (circleClicked == CircleB)
        {
            //Debug.Log("CircleB was clicked");
            //Debug.Log("by the way, the length of listOfAllCircles is: " + listOfAllCircles.Count);
            //Debug.Log("numberOfThisTypeThatExist: " + Circle.numberOfThisTypeThatExist);
            int IDnum = CircleB.GetComponent<Clickable_circle>().IDnumberOfCircleDataAttachedToThis;
            for (int i = 0; i < listOfAllCircles.Count; i++)
            {
                if (listOfAllCircles[i].IDnumber == IDnum)
                {
                    value = listOfAllCircles[i].value;
                }
            }
            //Debug.Log("value of CircleB: " + value);
        }
        else if (circleClicked == CircleC)
        {
            //Debug.Log("CircleC was clicked");
            //Debug.Log("by the way, the length of listOfAllCircles is: " + listOfAllCircles.Count);
            //Debug.Log("numberOfThisTypeThatExist: " + Circle.numberOfThisTypeThatExist);
            int IDnum = CircleC.GetComponent<Clickable_circle>().IDnumberOfCircleDataAttachedToThis;
            for (int i = 0; i < listOfAllCircles.Count; i++)
            {
                if (listOfAllCircles[i].IDnumber == IDnum)
                {
                    value = listOfAllCircles[i].value;
                }
            }
            //Debug.Log("value of CircleC: " + value);
        }
        return value;

    }

    public float GetValueOfGoal()
    {
        return Goal.GetComponent<Clickable>().valueOfThisCircle_orGoal;
    }

    public Circle GetCircle_classObject_OfClickedCircle_gameObject(GameObject circleClicked)
    {
        int IDnum = circleClicked.GetComponent<Clickable_circle>().IDnumberOfCircleDataAttachedToThis;
        for (int i = 0; i < listOfAllCircles.Count; i++)
        {
            if (listOfAllCircles[i].IDnumber == IDnum)
            {
                return listOfAllCircles[i];
            }
        }
        Debug.Log("Error 5757, this message should never display");
        return null;
    }

    public Operator GetOperator_classObject_OfClickedOperator_gameObject(GameObject operatorClicked)
    {
        int IDnum = operatorClicked.GetComponent<Clickable_operator>().IDnumberOfOperatorDataAttachedToThis;
        for (int i = 0; i < listOfAllOperators.Count; i++)
        {
            if (listOfAllOperators[i].IDnumber == IDnum)
            {
                return listOfAllOperators[i];
            }
        }
        Debug.Log("Error 1414");
        return null;
    }

    public void DetermineWhether_oneCircle_MathIsComplete()
    {
        // if the operator is ADD/SUBT/MULT/DIVI, then a second Circle must be clicked
        //      otherwise, just 1 circle and then 1 operator is enough

        //math_oneCircle_IsComplete = true;

        if (circle1selected && operatorSelected)
        {
            string temp = GetTypeOfClickedOperator(highlightedOperator);
            if (temp == "exponent2" || temp == "exponent3" || temp == "squareRoot" || temp == "cubeRoot")
            {
                math_oneCircle_IsComplete = true;
            }
        }
    }
    public void DetermineWhether_twoCircle_MathIsComplete()
    {
        DEBUG_outputCircleValues("DetermineWhether_twoCircle_MathIsComplete");
        // if the operator is ADD/SUBT/MULT/DIVI, then a second Circle must be clicked
        //      otherwise, just 1 circle and then 1 operator is enough

        //math_twoCircle_IsComplete = true;

        if (circle1selected && operatorSelected && circle2selected)
        {
            string temp = GetTypeOfClickedOperator(highlightedOperator);
            if (temp == "exponent2" || temp == "exponent3" || temp == "squareRoot" || temp == "cubeRoot")
            {
                // do nothing
            }
            else
            {
                math_twoCircle_IsComplete = true;
            }
        }

    }
    public void SetCircleAsDoneMoving(GameObject circleThatMoved)
    {
        //asdfasdf        // need to reset the moving status at end of the math
        // identify which circle this is
        if (circleThatMoved == highlightedCircle1)
        {
            circle1DoneMoving = true;
        }
        else if (circleThatMoved == highlightedCircle2)
        {
            circle2DoneMoving = true;
        }

    }
    public void ExecuteCompletionOf_oneCircle_Math(bool grabberAnimationStarted, bool circleDoneMovingToOperator)
    {

        // July 2022... installing aperture & grabber animations, to replace original movement of circle to operator

        bool useLongerGrabberAnimation = false;

        if (grabberAnimationStarted == false && executingONEcircleMath) {
            //Time.timeScale = 0.1f;
            float grabberDefaultZPosition = -0.2f;
            // set the position & rotation of the grabber
            //      need y-value of highlighted operator & highlighted circle
            float yValueOfThisOperator = highlightedOperator.transform.position.y;
            float yValueOfOperatorA = OperatorA.transform.position.y;
            //float yValueOfOperatorB = OperatorB.transform.position.y;
            float yValueOfCircle = highlightedCircle1.GetComponent<Clickable_circle>().defaultPosition.y;
            Debug.Log("yValueOfCircle is: " + yValueOfCircle);
            float verticalGap = yValueOfCircle - yValueOfThisOperator;
            Debug.Log("verticalGap: " + verticalGap);

            //"get DEFAULT position of the circle, because as it rotates its position is never exactly 0, 0"

            bool operatorA_notOperatorB = false;



            if (yValueOfThisOperator == yValueOfOperatorA) {
                Debug.Log("this is operatorA");
                operatorA_notOperatorB = true;
                // possible y-values of the circle: 0, 1.5, -1.5, 2.5, -2.5
                // possible y-values of the operator: 1.5, -1.5

                if (verticalGap == 4)
                { // i.e., the circle is 4 units ABOVE the operator
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.03f, 1.03f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -45);
                    useLongerGrabberAnimation = true;
                } else if (verticalGap == 3) {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.03f, 0.7f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -34.9f);
                    useLongerGrabberAnimation = true;
                } else if (verticalGap == 1.5f) {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.14f, 0.4f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -19.23f);
                    useLongerGrabberAnimation = false;
                } else if (verticalGap == 1) {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.16f, 0.28f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -13.1f);
                    useLongerGrabberAnimation = false;
                } else if (verticalGap == 0) {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.21f, 0, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 0);
                    useLongerGrabberAnimation = false;
                } else if (verticalGap == -1) {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.16f, -0.28f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 13.1f);
                    useLongerGrabberAnimation = false;
                } else if (verticalGap == -1.5f) {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.14f, -0.4f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 19.23f);
                    useLongerGrabberAnimation = false;
                } else if (verticalGap == -3) {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.03f, -0.7f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 34.9f);
                    useLongerGrabberAnimation = true;
                } else if (verticalGap == -4) {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.03f, -1.03f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 45);
                    useLongerGrabberAnimation = true;
                }

                // begin aperture opening animation
                OpA_apertureAnimation.SetActive(true);
                OpA_apertureCLOSED.SetActive(false);

                DisableOperatorText(highlightedOperator);

                // turn OFF the aperture animation gameObject
                StartCoroutine(TurnOffApertureAnimationWhenOver_1circleMath(operatorA_notOperatorB, useLongerGrabberAnimation));

                // turn ON the open aperture image

                // start the grabber animation

                // when the grabber reaches the circle, the circle moves with the grabber
            }
            else 
            {
                Debug.Log("this is operatorB");
                operatorA_notOperatorB = false;

                if (verticalGap == 4)
                { // i.e., the circle is 4 units ABOVE the operator
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.03f, 1.03f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -45);
                    useLongerGrabberAnimation = true;
                }
                else if (verticalGap == 3)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.03f, 0.7f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -34.9f);
                    useLongerGrabberAnimation = true;
                }
                else if (verticalGap == 1.5f)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.14f, 0.4f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -19.23f);
                    useLongerGrabberAnimation = false;
                }
                else if (verticalGap == 1)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.16f, 0.28f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -13.1f);
                    useLongerGrabberAnimation = false;
                }
                else if (verticalGap == 0)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.21f, 0, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 0);
                    useLongerGrabberAnimation = false;
                }
                else if (verticalGap == -1)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.16f, -0.28f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 13.1f);
                    useLongerGrabberAnimation = false;
                }
                else if (verticalGap == -1.5f)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.14f, -0.4f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 19.23f);
                    useLongerGrabberAnimation = false;
                }
                else if (verticalGap == -3)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.03f, -0.7f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 34.9f);
                    useLongerGrabberAnimation = true;
                }
                else if (verticalGap == -4)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.03f, -1.03f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 45);
                    useLongerGrabberAnimation = true;
                }

                // begin aperture opening animation
                OpB_apertureAnimation.SetActive(true);
                OpB_apertureCLOSED.SetActive(false);
                // turn off the operator text
                DisableOperatorText(highlightedOperator);


                // turn OFF the aperture animation gameObject
                StartCoroutine(TurnOffApertureAnimationWhenOver_1circleMath(operatorA_notOperatorB, useLongerGrabberAnimation));

                // turn ON the open aperture image

                // start the grabber animation

                // when the grabber reaches the circle, the circle moves with the grabber
            }

        }

        // move the circle to the operator
        else if (grabberAnimationStarted == true && circleDoneMovingToOperator == false && executingONEcircleMath)
        {

            // make the operator & circle move toward each other in the middle (-1, 0)
            //Vector2 midPoint = new Vector2(-1, 0);



            //highlightedOperator.GetComponent<Clickable>().BeginMovementToTarget(midPoint, "temp", true, false);

            // depending on the operator, there may be a slightly different "midPoint" for the circle to travel to
            // TODO: this stuff is not necessary for a MVP, so this will be done later if at all

            //highlightedCircle1.GetComponent<SpriteRenderer>().color = highlightedColor;
            highlightedCircle1.GetComponent<Clickable_circle>().BeginMovementToTarget(highlightedOperator.transform.position, "operator", false, true, useLongerGrabberAnimation);
        }

        if (circleDoneMovingToOperator == true && executingONEcircleMath)
        {
            // when the circle gets to a certain point, it disappears
            highlightedCircle1.SetActive(false);
            highlightedOperator.SetActive(false);
            Circle c1 = GetCircle_classObject_OfClickedCircle_gameObject(highlightedCircle1);
            Operator oppy = GetOperator_classObject_OfClickedOperator_gameObject(highlightedOperator);

            Circle result = CreateResultCircle(c1, c1, oppy);

            Debug.Log("in ExecuteCompletionOf_oneCircle_Math(), just created Circle result, where value is " + result.value);
            SetCircle(highlightedCircle1, result);

            highlightedCircle1.SetActive(true);
            highlightedCircle1.GetComponent<Clickable_circle>().EndRotating();
            //HighlightCircleGameObject(highlightedCircle1, 1);
            ResetColorsAndMath_Circles_Operators();


            //highlightedCircle1.GetComponent<Clickable_circle>().BeginMovementToDefaultPosition();
            UpdateDefaultPositionsOfCircles(true);
            SendAllCirclesToDefaultPositions();
            DetermineWhetherPuzzleIsSolved();


        }


    }
    IEnumerator TurnOffApertureAnimationWhenOver_1circleMath(bool usingOperatorA, bool useLongerGrabber) {
        yield return new WaitForSeconds(durationOfApertureOpening);
        if (usingOperatorA) {
            OpA_apertureAnimation.SetActive(false);
            OpA_apertureOPEN.SetActive(true);
            if (useLongerGrabber == true)
            {
                OpA_grabber_1.SetActive(true);
                OpA_grabber_1.GetComponent<Animator>().Play("grabber_longer", -1, 0);
            }
            else
            {
                // use shorter
                OpA_grabber_1.SetActive(true);
                OpA_grabber_1.GetComponent<Animator>().Play("grabber", -1, 0);
            }
        } else {
            OpB_apertureAnimation.SetActive(false);
            OpB_apertureOPEN.SetActive(true);
            if (useLongerGrabber == true)
            {
                OpB_grabber_1.SetActive(true);
                OpB_grabber_1.GetComponent<Animator>().Play("grabber_longer", -1, 0);
            }
            else
            {
                // use shorter
                OpB_grabber_1.SetActive(true);
                OpB_grabber_1.GetComponent<Animator>().Play("grabber", -1, 0);
            }
        }
        StartCoroutine(TurnOffGrabberAnimationWhenOver_1circleMath(usingOperatorA, useLongerGrabber));
    }
    IEnumerator TurnOffGrabberAnimationWhenOver_1circleMath(bool usingOperatorA, bool usingLongerGrabber) {
        if (usingLongerGrabber) {
            yield return new WaitForSeconds(delayBeforeGrabOccurs_longDistance);
        } else {
            yield return new WaitForSeconds(delayBeforeGrabOccurs_shortDistance);
        }

        ExecuteCompletionOf_oneCircle_Math(true, false);
        
        yield return new WaitForSeconds(delayAfterGrabOccurs);
        // the circle moves WITH the grabber, to the operator/aperture

        Time.timeScale = 1;

        if (usingOperatorA) {
            OpA_apertureCLOSED.SetActive(true);
            OpA_apertureOPEN.SetActive(false);
            OpA_grabber_1.SetActive(false);
        } else {
            OpB_apertureCLOSED.SetActive(true);
            OpB_apertureOPEN.SetActive(false);
            OpB_grabber_1.SetActive(false);
        }

    }
    public void ExecuteCompletionOf_twoCircle_Math(bool grabberAnimationStarted)
    {
        //Time.timeScale = 0.2f;
        Debug.Log("at beginning of ExecuteCompletionOf_twoCircle_Math()");
        DEBUG_outputCircleValues("about to ExecuteCompletionOf_twoCircle_Math()");

        bool useLongerGrabberAnimation_forVerticalGap1 = false;
        bool useLongerGrabberAnimation_forVerticalGap2 = false;

        if (grabberAnimationStarted == false && executingTWOcircleMath) 
        {
            //Time.timeScale = 0.1f;
            float grabberDefaultZPosition = -0.2f;
            // set the position & rotation of the grabber
            //      need y-value of highlighted operator & highlighted circle
            float yValueOfThisOperator = highlightedOperator.transform.position.y;
            float yValueOfOperatorA = OperatorA.transform.position.y;
            //float yValueOfOperatorB = OperatorB.transform.position.y;
            float yValueOfCircle1 = highlightedCircle1.GetComponent<Clickable_circle>().defaultPosition.y;
            float yValueOfCircle2 = highlightedCircle2.GetComponent<Clickable_circle>().defaultPosition.y;
            Debug.Log("yValueOfCircle is: " + yValueOfCircle1);
            float verticalGap1 = yValueOfCircle1 - yValueOfThisOperator;
            float verticalGap2 = yValueOfCircle2 - yValueOfThisOperator;
            Debug.Log("verticalGaps: " + verticalGap1 + ", " + verticalGap2);

            //"get DEFAULT position of the circle, because as it rotates its position is never exactly 0, 0"

            bool operatorA_notOperatorB = false;




            if (yValueOfThisOperator == yValueOfOperatorA)
            {
                Debug.Log("this is operatorA");
                operatorA_notOperatorB = true;
                // possible y-values of the circle: 0, 1.5, -1.5, 2.5, -2.5
                // possible y-values of the operator: 1.5, -1.5

                // ********************************************************************************************************************
                if (verticalGap1 == 4)
                { // i.e., the circle is 4 units ABOVE the operator
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.03f, 1.03f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -45);
                    useLongerGrabberAnimation_forVerticalGap1 = true;
                }
                else if (verticalGap1 == 3)
                {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.03f, 0.7f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -34.9f);
                    useLongerGrabberAnimation_forVerticalGap1 = true;
                }
                else if (verticalGap1 == 1.5f)
                {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.14f, 0.4f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -19.23f);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == 1)
                {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.16f, 0.28f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -13.1f);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == 0)
                {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.21f, 0, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 0);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == -1)
                {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.16f, -0.28f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 13.1f);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == -1.5f)
                {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.14f, -0.4f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 19.23f);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == -3)
                {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.03f, -0.7f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 34.9f);
                    useLongerGrabberAnimation_forVerticalGap1 = true;
                }
                else if (verticalGap1 == -4)
                {
                    OpA_grabber_1.transform.localPosition = new Vector3(-1.03f, -1.03f, grabberDefaultZPosition);
                    OpA_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 45);
                    useLongerGrabberAnimation_forVerticalGap1 = true;
                }
                // ********************************************************************************************************************
                if (verticalGap2 == 4)
                { // i.e., the circle is 4 units ABOVE the operator
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.03f, 1.03f, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, -45);
                    useLongerGrabberAnimation_forVerticalGap2 = true;
                }
                else if (verticalGap2 == 3)
                {
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.03f, 0.7f, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, -34.9f);
                    useLongerGrabberAnimation_forVerticalGap2 = true;
                }
                else if (verticalGap2 == 1.5f)
                {
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.14f, 0.4f, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, -19.23f);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == 1)
                {
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.16f, 0.28f, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, -13.1f);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == 0)
                {
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.21f, 0, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 0);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == -1)
                {
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.16f, -0.28f, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 13.1f);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == -1.5f)
                {
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.14f, -0.4f, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 19.23f);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == -3)
                {
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.03f, -0.7f, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 34.9f);
                    useLongerGrabberAnimation_forVerticalGap2 = true;
                }
                else if (verticalGap2 == -4)
                {
                    OpA_grabber_2.transform.localPosition = new Vector3(-1.03f, -1.03f, grabberDefaultZPosition);
                    OpA_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 45);
                    useLongerGrabberAnimation_forVerticalGap2 = true;
                }
                // ********************************************************************************************************************


                // begin aperture opening animation
                OpA_apertureAnimation.SetActive(true);
                OpA_apertureCLOSED.SetActive(false);

                DisableOperatorText(highlightedOperator);

                // turn OFF the aperture animation gameObject
                StartCoroutine(TurnOffApertureAnimationWhenOver_2circleMath(operatorA_notOperatorB, useLongerGrabberAnimation_forVerticalGap1, useLongerGrabberAnimation_forVerticalGap2));

                // turn ON the open aperture image

                // start the grabber animation

                // when the grabber reaches the circle, the circle moves with the grabber
            } else {
                Debug.Log("this is operatorB");
                operatorA_notOperatorB = false;
                // possible y-values of the circle: 0, 1.5, -1.5, 2.5, -2.5
                // possible y-values of the operator: 1.5, -1.5

                // ********************************************************************************************************************
                if (verticalGap1 == 4)
                { // i.e., the circle is 4 units ABOVE the operator
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.03f, 1.03f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -45);
                    useLongerGrabberAnimation_forVerticalGap1 = true;
                }
                else if (verticalGap1 == 3)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.03f, 0.7f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -34.9f);
                    useLongerGrabberAnimation_forVerticalGap1 = true;
                }
                else if (verticalGap1 == 1.5f)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.14f, 0.4f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -19.23f);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == 1)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.16f, 0.28f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, -13.1f);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == 0)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.21f, 0, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 0);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == -1)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.16f, -0.28f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 13.1f);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == -1.5f)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.14f, -0.4f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 19.23f);
                    useLongerGrabberAnimation_forVerticalGap1 = false;
                }
                else if (verticalGap1 == -3)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.03f, -0.7f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 34.9f);
                    useLongerGrabberAnimation_forVerticalGap1 = true;
                }
                else if (verticalGap1 == -4)
                {
                    OpB_grabber_1.transform.localPosition = new Vector3(-1.03f, -1.03f, grabberDefaultZPosition);
                    OpB_grabber_1.transform.rotation = Quaternion.Euler(0, 0, 45);
                    useLongerGrabberAnimation_forVerticalGap1 = true;
                }
                // ********************************************************************************************************************
                if (verticalGap2 == 4)
                { // i.e., the circle is 4 units ABOVE the operator
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.03f, 1.03f, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, -45);
                    useLongerGrabberAnimation_forVerticalGap2 = true;
                }
                else if (verticalGap2 == 3)
                {
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.03f, 0.7f, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, -34.9f);
                    useLongerGrabberAnimation_forVerticalGap2 = true;
                }
                else if (verticalGap2 == 1.5f)
                {
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.14f, 0.4f, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, -19.23f);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == 1)
                {
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.16f, 0.28f, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, -13.1f);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == 0)
                {
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.21f, 0, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 0);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == -1)
                {
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.16f, -0.28f, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 13.1f);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == -1.5f)
                {
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.14f, -0.4f, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 19.23f);
                    useLongerGrabberAnimation_forVerticalGap2 = false;
                }
                else if (verticalGap2 == -3)
                {
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.03f, -0.7f, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 34.9f);
                    useLongerGrabberAnimation_forVerticalGap2 = true;
                }
                else if (verticalGap2 == -4)
                {
                    OpB_grabber_2.transform.localPosition = new Vector3(-1.03f, -1.03f, grabberDefaultZPosition);
                    OpB_grabber_2.transform.rotation = Quaternion.Euler(0, 0, 45);
                    useLongerGrabberAnimation_forVerticalGap2 = true;
                }
                // ********************************************************************************************************************


                // begin aperture opening animation
                OpB_apertureAnimation.SetActive(true);
                OpB_apertureCLOSED.SetActive(false);

                DisableOperatorText(highlightedOperator);

                // turn OFF the aperture animation gameObject
                StartCoroutine(TurnOffApertureAnimationWhenOver_2circleMath(operatorA_notOperatorB, useLongerGrabberAnimation_forVerticalGap1, useLongerGrabberAnimation_forVerticalGap2));

                // turn ON the open aperture image

                // start the grabber animation

                // when the grabber reaches the circle, the circle moves with the grabber
            }


        }

        else if (executingTWOcircleMath && circle1DoneMoving == false && circle2DoneMoving == false)
        {
            // identify which circle is higher, so they curve up & down correctly
            if (highlightedCircle1.transform.position.y > highlightedCircle2.transform.position.y)
            {
                highlightedCircle1.GetComponent<Clickable_circle>().BeginMovementToTarget(highlightedOperator.transform.position, "operator", false, true, useLongerGrabberAnimation_forVerticalGap1);
                highlightedCircle2.GetComponent<Clickable_circle>().BeginMovementToTarget(highlightedOperator.transform.position, "operator", false, false, useLongerGrabberAnimation_forVerticalGap2);
            }
            else
            {
                highlightedCircle1.GetComponent<Clickable_circle>().BeginMovementToTarget(highlightedOperator.transform.position, "operator", false, false, useLongerGrabberAnimation_forVerticalGap1);
                highlightedCircle2.GetComponent<Clickable_circle>().BeginMovementToTarget(highlightedOperator.transform.position, "operator", false, true, useLongerGrabberAnimation_forVerticalGap2);
            }


        }

        if (circle1DoneMoving && circle2DoneMoving)
        {

            //Vector2 sparkleStartPos = highlightedOperator.transform.position;
            //Vector2 offset1 = new Vector2(Random.Range(-2, 5), Random.Range(-1, 2));
            //Vector2 offset2 = new Vector2(Random.Range(-2, 5), Random.Range(-1, 2));
            //Vector2 offset3 = new Vector2(Random.Range(-2, 5), Random.Range(-1, 2));
            //Vector2 offset4 = new Vector2(Random.Range(-2, 5), Random.Range(-1, 2));
            //Vector2 offset5 = new Vector2(Random.Range(-2, 5), Random.Range(-1, 2));
            //Vector2 offset6 = new Vector2(Random.Range(-2, 5), Random.Range(-1, 2));


            //// start sparkles
            //sparkleScript1.BeginSparkleMovement(sparkleStartPos, offset1);
            //sparkleScript2.BeginSparkleMovement(sparkleStartPos, offset2);
            //sparkleScript3.BeginSparkleMovement(sparkleStartPos, offset3);
            //sparkleScript4.BeginSparkleMovement(sparkleStartPos, offset4);
            //sparkleScript5.BeginSparkleMovement(sparkleStartPos, offset5);
            //sparkleScript6.BeginSparkleMovement(sparkleStartPos, offset6);


            highlightedCircle1.SetActive(false);
            highlightedOperator.SetActive(false);
            highlightedCircle2.SetActive(false);
            Circle c1 = GetCircle_classObject_OfClickedCircle_gameObject(highlightedCircle1);
            Circle c2 = GetCircle_classObject_OfClickedCircle_gameObject(highlightedCircle2);
            Operator oppy = GetOperator_classObject_OfClickedOperator_gameObject(highlightedOperator);

            DEBUG_outputCircleValues("right after c1 c2 oppy, in ExecuteCompletionOf_twoCircle_Math()");

            Circle result = CreateResultCircle(c1, c2, oppy);
            DEBUG_outputCircleValues("right after CreateResultCircle(), in ExecuteCompletionOf_twoCircle_Math()");
            Debug.Log("result.value:::: " + result.value);
            SetCircle(highlightedCircle1, result);
            DEBUG_outputCircleValues("right after SetCircle(), in ExecuteCompletionOf_twoCircle_Math()");
            highlightedCircle1.SetActive(true);
            highlightedCircle1.GetComponent<Clickable_circle>().EndRotating();

            ResetColorsAndMath_Circles_Operators();
            UpdateDefaultPositionsOfCircles(true);
            SendAllCirclesToDefaultPositions();
            DetermineWhetherPuzzleIsSolved();

            circle1DoneMoving = false;
            circle2DoneMoving = false;

            DEBUG_outputCircleValues("at end of ExecuteCompletionOf_twoCircle_Math()");
        }



    }
    IEnumerator TurnOffApertureAnimationWhenOver_2circleMath(bool usingOperatorA, bool useLongerGrabber_for1, bool useLongerGrabber_for2) {
        yield return new WaitForSeconds(durationOfApertureOpening);
        if (usingOperatorA)
        {
            OpA_apertureAnimation.SetActive(false);
            OpA_apertureOPEN.SetActive(true);
            if (useLongerGrabber_for1 == true)
            {
                OpA_grabber_1.SetActive(true);
                OpA_grabber_1.GetComponent<Animator>().Play("grabber_longer", -1, 0);
            }
            else
            {
                // use shorter
                OpA_grabber_1.SetActive(true);
                OpA_grabber_1.GetComponent<Animator>().Play("grabber", -1, 0);
            }
            // ********************************************************************************************************************
            if (useLongerGrabber_for2 == true) {
                OpA_grabber_2.SetActive(true);
                OpA_grabber_2.GetComponent<Animator>().Play("grabber_longer", -1, 0);
            }
            else
            {
                OpA_grabber_2.SetActive(true);
                OpA_grabber_2.GetComponent<Animator>().Play("grabber", -1, 0);
            }

        }
        else
        {
            OpB_apertureAnimation.SetActive(false);
            OpB_apertureOPEN.SetActive(true);
            if (useLongerGrabber_for1 == true)
            {
                OpB_grabber_1.SetActive(true);
                OpB_grabber_1.GetComponent<Animator>().Play("grabber_longer", -1, 0);
            }
            else
            {
                // use shorter
                OpB_grabber_1.SetActive(true);
                OpB_grabber_1.GetComponent<Animator>().Play("grabber", -1, 0);
            }
            // ********************************************************************************************************************
            if (useLongerGrabber_for2 == true) {
                OpB_grabber_2.SetActive(true);
                OpB_grabber_2.GetComponent<Animator>().Play("grabber_longer", -1, 0);
            }
            else
            {
                OpB_grabber_2.SetActive(true);
                OpB_grabber_2.GetComponent<Animator>().Play("grabber", -1, 0);
            }





        }
        //StartCoroutine(TurnOffGrabberAnimationWhenOver_2circleMath(usingOperatorA, useLongerGrabber_for1, useLongerGrabber_for2));
        StartCoroutine(TurnOffGrabberAnimation_forCircle1(usingOperatorA, useLongerGrabber_for1));
        StartCoroutine(TurnOffGrabberAnimation_forCircle2(usingOperatorA, useLongerGrabber_for2));
    }
    IEnumerator TurnOffGrabberAnimation_forCircle1(bool usingOperatorA, bool useLongerGrabber) {
        //Debug.Log("starting TurnOffGrabber1, " + oneGrabberHasReturned + "(oneGrabber)");
        grabber1_doneMoving = true;
        if (useLongerGrabber) {
            yield return new WaitForSeconds(delayBeforeGrabOccurs_longDistance);
        } else {
            yield return new WaitForSeconds(delayBeforeGrabOccurs_shortDistance);
        }

        if (grabber1_doneMoving && grabber2_doneMoving) {
            Debug.Log("9988");
            ExecuteCompletionOf_twoCircle_Math(true);
        }


        yield return new WaitForSeconds(delayAfterGrabOccurs);
        Time.timeScale = 1;

        if (usingOperatorA) {
            OpA_grabber_1.SetActive(false);
        } else {
            OpB_grabber_1.SetActive(false);
        }

        if (grabber1_doneMoving && grabber2_doneMoving) {
            Time.timeScale = 1;
            Debug.Log("circle1... they are both done moving, and now we reset the operator animations");
            if (usingOperatorA) {
                OpA_apertureCLOSED.SetActive(true);
                OpA_apertureOPEN.SetActive(false);
            } else {
                OpB_apertureCLOSED.SetActive(true);
                OpB_apertureOPEN.SetActive(false);

            }
            grabber1_doneMoving = false;
            grabber2_doneMoving = false;
        } 
    }
    IEnumerator TurnOffGrabberAnimation_forCircle2(bool usingOperatorA, bool useLongerGrabber) {
        //Debug.Log("starting TurnOffGrabber2, " + oneGrabberHasReturned + "(oneGrabber)");
        grabber2_doneMoving = true;

        if (useLongerGrabber) {
            yield return new WaitForSeconds(delayBeforeGrabOccurs_longDistance);
        } else {
            yield return new WaitForSeconds(delayBeforeGrabOccurs_shortDistance);
        }

        if (grabber1_doneMoving && grabber2_doneMoving) {
            Debug.Log("8877");
            ExecuteCompletionOf_twoCircle_Math(true);
        }

        yield return new WaitForSeconds(delayAfterGrabOccurs);
        Time.timeScale = 1;

        if (usingOperatorA) {
            OpA_grabber_2.SetActive(false);
        } else {
            OpB_grabber_2.SetActive(false);
        }

        if (grabber1_doneMoving && grabber2_doneMoving) {
            Time.timeScale = 1;
            Debug.Log("circle2... they are both done moving, and now we reset the operator animations");
            if (usingOperatorA) {
                OpA_apertureCLOSED.SetActive(true);
                OpA_apertureOPEN.SetActive(false);
            } else {
                OpB_apertureCLOSED.SetActive(true);
                OpB_apertureOPEN.SetActive(false);
            }
            grabber1_doneMoving = false;
            grabber2_doneMoving = false;
        } 
    }


    

    public void DetermineWhetherPuzzleIsSolved()
    {
        Debug.Log("about to DetermineWhetherPuzzleIsSolved()");
        // if there is only 1 circle remaining, and 0 operators remaining
        int circleTally = 0;
        GameObject temp = null;
        if (CircleA.activeSelf)
        {
            circleTally += 1;
            temp = CircleA;
        }
        if (CircleB.activeSelf)
        {
            circleTally += 1;
            temp = CircleB;
        }
        if (CircleC.activeSelf)
        {
            circleTally += 1;
            temp = CircleC;
        }

        int operatorTally = 0;
        if (OperatorA.activeSelf)
        {
            operatorTally += 1;
        }
        if (OperatorB.activeSelf)
        {
            operatorTally += 1;
        }

        if (gameType == "kiddy")
        {
            if (circleTally == 1 && operatorTally == 1)
            {
                float circleValue = GetValueOfClickedCircle(temp);
                float goalValue = GetValueOfGoal();
                if (CheckIfNumbersAreCloseEnough(circleValue, goalValue))
                {
                    //Debug.Log("in kiddy mode: there is 1 circle and 1 operators, and the goal has been reached");
                    GameManager.instance.IncreaseNumberOfCompleted(1);
                    GameManager.instance.ChangeStat_Easy("solved", 1);
                    // get rid of the useless operator
                    if (OperatorA.activeSelf)
                    {
                        // if this is the remaining operator, then make it fall away
                        //OperatorA.GetComponent<Clickable_operator>().SendCircleToToilet(0);
                        OperatorA.GetComponent<Clickable_operator>().ShrinkAndDisappear();
                        Operator tempOp = GetOperator_classObject_OfClickedOperator_gameObject(OperatorB);
                        Debug.Log("about to change stat for OperatorB, type: " + tempOp.type);
                        GameManager.instance.ChangeStat_Easy(tempOp.type, 1);
                    }
                    if (OperatorB.activeSelf)
                    {
                        // if this is the remaining operator, then make it fall away
                        //OperatorB.GetComponent<Clickable_operator>().SendCircleToToilet(0);
                        OperatorB.GetComponent<Clickable_operator>().ShrinkAndDisappear();
                        Operator tempOp = GetOperator_classObject_OfClickedOperator_gameObject(OperatorA);
                        Debug.Log("about to change stat for OperatorA, type: " + tempOp.type);
                        GameManager.instance.ChangeStat_Easy(tempOp.type, 1);
                    }
                    AnimatePuzzleSolved(temp, false, false);
                    GameManager.instance.ResolveConflictFavorably();
                }
                else
                {
                    //Debug.Log("there is 1 circle and 0 operators------------------------------------------ but the goal is not reached! you FAIL!");
                    // get rid of the useless operator
                    if (OperatorA.activeSelf)
                    {
                        // if this is the remaining operator, then make it fall away
                        //OperatorA.GetComponent<Clickable_operator>().SendCircleToToilet(0);
                        OperatorA.GetComponent<Clickable_operator>().ShrinkAndDisappear();
                    }
                    if (OperatorB.activeSelf)
                    {
                        // if this is the remaining operator, then make it fall away
                        //OperatorB.GetComponent<Clickable_operator>().SendCircleToToilet(0);
                        OperatorB.GetComponent<Clickable_operator>().ShrinkAndDisappear();
                    }
                    GameManager.instance.IncreaseNumberOfFailed(1);
                    GameManager.instance.ChangeStat_Easy("failed", 1);
                    AnimatePuzzleFailed(temp, false, false);
                    GameManager.instance.ResolveConflictUNFAVORABLY();
                }
            }
            else
            {
                DEBUG_outputCircleValues("puzzle not solved yet (kiddy)");

            }
        }
        else if (gameType == "timed" || gameType == "endless")
        {
            if (circleTally == 1 && operatorTally == 0)
            {
                // if the 1 circle value is the same as the goal
                float circleValue = GetValueOfClickedCircle(temp);
                float goalValue = GetValueOfGoal();
                if (CheckIfNumbersAreCloseEnough(circleValue, goalValue))
                {
                    //Debug.Log("there is 1 circle and 0 operators, and the goal has been reached");
                    if (gameType == "timed")
                    {
                        GameManager.instance.IncreaseScore(10);
                    }
                    else if (gameType == "endless")
                    {
                        GameManager.instance.IncreaseNumberOfCompleted(1);
                        GameManager.instance.ChangeStat_Endless("solved", 1);
                        GameManager.instance.ChangeStat_Endless(operator1_forStats, 1);
                        GameManager.instance.ChangeStat_Endless(operator2_forStats, 1);
                    }
                    AnimatePuzzleSolved(temp, false, false);
                    GameManager.instance.ResolveConflictFavorably();
                }
                else
                {
                    //Debug.Log("there is 1 circle and 0 operators------------------------------------------ but the goal is not reached! you FAIL!");
                    if (gameType == "endless")
                    {
                        GameManager.instance.IncreaseNumberOfFailed(1);
                        GameManager.instance.ChangeStat_Endless("failed", 1);
                    }
                    AnimatePuzzleFailed(temp, false, false);
                    GameManager.instance.ResolveConflictUNFAVORABLY();
                }
            }
            else
            {
                DEBUG_outputCircleValues("puzzle not solved yet (timed/endless)");
            }
        }

    }



    public void AnimatePuzzleSolved(GameObject finalCircle, bool circleHasReachedGoal, bool explosionFinished)
    {
        Debug.Log("at beginning of AnimatePuzzleSolved()");
        if (circleHasReachedGoal == false)
        {
            if (gameType == "timed")
            {
                // stop the timers
                timerGlobal.PauseGlobalTimer();
                timerPuzzle.PausePuzzleTimer();
                timerPuzzleThatMoves.PausePuzzleTimer();
                float timeToAdd = timerPuzzle.GetPuzzleTimeRemaining();
                //timerGlobal.AddToGlobalTimer(timeToAdd);      this will be called when the "bonusTime" green text reaches the timer text on-screen
                if (timeToAdd > 0)
                {
                    bonusTimeNotify.BeginMove(timeToAdd);
                }
            }




            finalCircle.GetComponent<Clickable_circle>().BeginMovementToTarget(Goal.transform.position, "goal", true, true, false);

        }
        else
        {
            // begin "animating" the explosion

            
            //explosion.StartExplosion();
            float xDestination = -7;
            float yDestination = -3.5f;
            //sparkleScript1.BeginSparkleMovement(Goal.transform.position, new Vector2(xDestination, yDestination));
            //sparkleScript2.BeginSparkleMovement(Goal.transform.position, new Vector2(xDestination, yDestination));
            //sparkleScript3.BeginSparkleMovement(Goal.transform.position, new Vector2(xDestination, yDestination));
            //sparkleScript4.BeginSparkleMovement(Goal.transform.position, new Vector2(xDestination, yDestination));
            //sparkleScript5.BeginSparkleMovement(Goal.transform.position, new Vector2(xDestination, yDestination));
            //sparkleScript6.BeginSparkleMovement(Goal.transform.position, new Vector2(xDestination, yDestination));

            //Debug.Log("Abc 123");

            smallExplosionOfGoal.gameObject.SetActive(true);
            smallExplosionOfGoal.Play("mushroomCloud", -1, 0);
            StartCoroutine(ExplosionDisableAfterAnimation());





            if (gameType == "timed")
            {
                if (timerGlobal.GetTimeRemaining() > 0)
                {
                    //Debug.Log("AnimatePuzzleSolved() is now about to CreateNewPuzzle()");
                    CreateNewPuzzle();

                    //          it may be a good idea to create the new puzzle before it's needed, but that will require some work
                }
            }
            else
            {
                CreateNewPuzzle();

                //// temporary
                //GameManager.instance.BuyRocket();
            }


        }
    }
    IEnumerator ExplosionDisableAfterAnimation() {
        yield return new WaitForSeconds(durationOfSmallExplosion);
        smallExplosionOfGoal.gameObject.SetActive(false);
    }
    public void AnimatePuzzleFailed(GameObject finalCircle, bool circleHasReachedGoal, bool fallingIntoToiletComplete)
    {
        if (circleHasReachedGoal == false)
        {
            if (gameType == "timed")
            {
                // stop the timers
                timerGlobal.PauseGlobalTimer();
                timerPuzzle.PausePuzzleTimer();
                timerPuzzleThatMoves.PausePuzzleTimer();
                //timerGlobal.SubtractFromGlobalTimer(0);       no time penalty for failing a puzzle
            }

            float newX = Goal.transform.position.x - 1.5f;
            float newY = Goal.transform.position.y;
            finalCircle.GetComponent<Clickable_circle>().BeginMovementToTarget(new Vector2(newX, newY), "goalThenToilet", true, true, false);

        }
        else
        {
            // animate the circle bouncing OFF OF the goal and falling down


            if (fallingIntoToiletComplete == true)
            {
                // then load a new puzzle
                if (gameType == "timed")
                {
                    if (timerGlobal.GetTimeRemaining() > 0)
                    {
                        CreateNewPuzzle();
                    }
                }
                else
                {
                    CreateNewPuzzle();
                }

            }
            else
            {
                finalCircle.GetComponent<Clickable_circle>().SendCircleToToilet(0);
                Goal.GetComponent<Clickable>().ShrinkIt();

            }

        }


        // to do: put in puzzle failed animations & sounds




    }




    public void CombineCircle1_and_Operator()
    {

    }
    public void Reverse_CombineCircle1_and_Operator()
    {

    }



    public void ShrinkAndDisappear_allCirclesOperatorsGoal() {
        CircleAScript.ShrinkAndDisappear();
        CircleBScript.ShrinkAndDisappear();
        CircleCScript.ShrinkAndDisappear();
        OperatorAScript.ShrinkAndDisappear();
        OperatorAScript.ShrinkAndDisappear();
        Goal.GetComponent<Clickable>().ShrinkAndDisappear();
    }








    public void DEBUG_outputCircleValues(string location)
    {
        if (debugMessagesOn)
        {
            Debug.Log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ location: " + location);
            Debug.Log("CircleA: " + CircleA.GetComponent<Clickable_circle>().valueOfThisCircle_orGoal);
            Debug.Log("CircleB: " + CircleB.GetComponent<Clickable_circle>().valueOfThisCircle_orGoal);
            Debug.Log("CircleC: " + CircleC.GetComponent<Clickable_circle>().valueOfThisCircle_orGoal);
        }
    }

    //public void DebugLOG(string message)
    //{
    //    if (debugMessagesOn == true)
    //    {
    //        Debug.Log(message);
    //    }
    //}

    public void ConvertToRational()
    {

        float testFloat = 4.999f;
        Debug.Log("testFloat: " + testFloat);
        Debug.Log("using (int): " + (int)testFloat);
        Debug.Log("using RoundToInt: " + Mathf.RoundToInt(testFloat));

        Circle temp = GetCircle_classObject_OfClickedCircle_gameObject(CircleA);
        float value = temp.value;
        bool isNegative = false;
        if (value < 0)
        {
            isNegative = true;
        }

        Debug.Log("value: " + value);

        value = Mathf.Abs(value);


        bool rationalFound = false;

        for (int i = 1; i <= 30; i++)
        {
            for (int j = 2; j <= 10; j++)
            {
                if (CheckIfNumbersAreCloseEnough(value, (float)i / (float)j))
                {
                    if (rationalFound == false)
                    {
                        if (isNegative)
                        {
                            Debug.Log("negative: ");
                        }
                        Debug.Log("numerator: " + i + ", denominator: " + j);
                        rationalFound = true;
                        //break;
                    }


                }
            }
            //break;
        }


        // next step: have the function return a List<int> containing the numerator & denominator
    }


}