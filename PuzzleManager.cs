using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    
    public GameObject CircleA;
    public GameObject CircleB;
    public GameObject CircleC;
    public GameObject MathInProgress;
    public GameObject OperatorA;
    public GameObject OperatorB;
    public GameObject Goal;

    public List<Circle> listOfAllCircles;
    public List<Operator> listOfAllOperators;


    // for performing math operations as the player attempts to solve puzzles
    bool circle1selected = false;
    GameObject highlightedCircle1;
    float value1;

    bool operatorSelected = false;
    GameObject highlightedOperator;

    bool circle2selected = false;
    GameObject highlightedCircle2;
    float value2;

    Color whiteColor = new Color(1, 1, 1, 1);
    Color highlightedColor = new Color(0.4f, 0.6f, 0.4f, 1);

    bool math_oneCircle_IsComplete = false;
    bool math_twoCircle_IsComplete = false;

    bool firstPuzzleStarted = false;

    public GameObject GlobalTimer;
    public GameObject PuzzleTimer;
    TimerGlobal timerGlobal;
    TimerPuzzle timerPuzzle;



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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class OpsAndCircles {
        //Vector2 position;
    }
    public class Operator : OpsAndCircles {
        public string type;
        public static int numberOfThisTypeThatExist;
        public int IDnumber;
        Vector2 position;
        float input1;
        float input2;
        float output;
        public char AorBorC;
        public string operatorGameObject_associatedWith;

        public Operator(string name, char ABC) {
            type = name;
            AorBorC = ABC;
            IDnumber = numberOfThisTypeThatExist;
            numberOfThisTypeThatExist += 1;
            position = new Vector2(0, 0);
        }

    }
    public class Circle : OpsAndCircles {
        public float value;
        public bool trueInt_falseFraction;
        public int numerator;
        public int denominator;
        public static int numberOfThisTypeThatExist;
        public int IDnumber;
        Vector2 position;
        public string circleGameObject_associatedWith;

        public Circle(float val) {
            value = val;
            trueInt_falseFraction = true;
            IDnumber = numberOfThisTypeThatExist;
            numberOfThisTypeThatExist += 1;
            position = new Vector2(0, 0);
        }
        public Circle(float numerator, int denom) {
            value = numerator / denom;
            numerator = (int)numerator;
            denominator = denom;
            trueInt_falseFraction = false;
            IDnumber = numberOfThisTypeThatExist;
            numberOfThisTypeThatExist += 1;
            position = new Vector2(0, 0);
        }
    }


    public Operator PickRandomOperator(int highest, char ABC) {
        int randomInt = Random.Range(1, highest + 1);
        string name = "";
        switch (randomInt) {
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
        Operator temp = new Operator(name, ABC);
        listOfAllOperators.Add(temp);
        return temp;
    }

    public List<float> CreateListOfPossibleCircleValues_forArithmetic(float maxValue, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed) {
        List<float> toReturn = new List<float>();

        if (negativesAllowed) {
            for (int i = (int)maxValue * -1; i <= -2; i++) {
                toReturn.Add(i);
            }
        }
        if (negativesAllowed && oneAllowed) {
            toReturn.Add(-1);
        }
        if (negativesAllowed && fractionsAllowed) {
            // 
            toReturn.Add(-0.7500f);   // -3/4
            toReturn.Add(-2f/3f);   // -2/3
            toReturn.Add(-0.5f);    // -1/2
            toReturn.Add(-1f/3f);   // -1/3
            toReturn.Add(-0.25f);   // -1/4
        }
        if (fractionsAllowed) {
            toReturn.Add(0.25f);
            toReturn.Add(1f/3f);
            toReturn.Add(0.5f);
            toReturn.Add(2f/3f);
            toReturn.Add(0.75f);
        }
        if (oneAllowed) {
            toReturn.Add(1);
        }
        for (int i = 2; i <= (int)maxValue; i++) {
            toReturn.Add(i);
        }

        return toReturn;
    }

    public List<float> CreateListOfPossibleCircleValues_forExponent2(float resultCap, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed) {
        List<float> toReturn = new List<float>();
        int squareRootOfResultCap = (int)Mathf.Pow(resultCap, 0.5f);
        if (negativesAllowed) {
            for (int i = (squareRootOfResultCap * -1); i <= -2; i++) {
                toReturn.Add(i);
            }
        }
        if (negativesAllowed && oneAllowed) {
            toReturn.Add(-1);
        }
        if (negativesAllowed && fractionsAllowed) {
            //toReturn.Add(-0.7500f);   // -3/4
            //toReturn.Add(-0.6666f);   // -2/3
            toReturn.Add(-0.5f);    // -1/2     because (-0.5)^2 = 0.25
            //toReturn.Add(-0.3333f);   // -1/3
            //toReturn.Add(-0.25f);   // -1/4
        }
        if (fractionsAllowed) {
            //toReturn.Add(0.25f);
            //toReturn.Add(0.3333f);
            toReturn.Add(0.5f);
            //toReturn.Add(0.6666f);
            //toReturn.Add(0.75f);
        }
        if (oneAllowed) {
            toReturn.Add(1);
        }
        for (int i = 2; i <= squareRootOfResultCap; i++) {
            toReturn.Add(i);
        }
        return toReturn;
    }

    public List<float> CreateListOfPossibleCircleValues_forExponent3(float resultCap, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed) {
        List<float> toReturn = new List<float>();
        //int squareRootOfResultCap = (int)Mathf.Pow(resultCap, 0.5f);

        int cubeRootOfResultCap = (int)Mathf.Pow(resultCap, 0.33333333333333333333333333f);



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

    public List<float> CreateListOfPossibleCircleValues_forSquareRoot(float maxValue, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed) {
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
            toReturn.Add(Mathf.Pow(i, 2));
        }
        return toReturn;
    }

    public List<float> CreateListOfPossibleCircleValues_forCubeRoot(float maxValue, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed) {
        List<float> toReturn = new List<float>();
                                                            // maxValue is probably going to be 6, because 6^3 = 216 and we don't want to go higher than that
        if (negativesAllowed)
        {
            for (int i = (int)(maxValue * -1); i <= -2; i++)
            {
                toReturn.Add(Mathf.Pow(i, 3));
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
            toReturn.Add(Mathf.Pow(i, 3));
        }
        return toReturn;
    }
    
    public bool TestIfIsInteger(float value) {
        float marginErr = 0.001f;
        bool toReturn = false;
        if (Mathf.Abs(value - Mathf.RoundToInt(value)) < marginErr) {
            toReturn = true;
        } else {
            //Debug.Log(value + " is not an integer");
        }
        return toReturn;
    }



    public Circle CreateRandomCircle(List<float> values) {
        int randomIndex = Random.Range(0, values.Count);
        float num = values[randomIndex];
        bool isNegative = false;
        if (num < 0) {
            isNegative = true;
        }
        Circle temp;
        if (!TestIfIsInteger(num)) {
            float margin = 0.001f;
            float numerator = 0;
            int denominator = 0;
            if (Mathf.Abs(0.75f - Mathf.Abs(num)) < margin) {
                numerator = 3;
                denominator = 4;
            } else if (Mathf.Abs(2f/3f - Mathf.Abs(num)) < margin) {
                numerator = 2;
                denominator = 3;
            } else if (Mathf.Abs(0.5f - Mathf.Abs(num)) < margin) {
                numerator = 1;
                denominator = 2;
            } else if (Mathf.Abs(1f/3f - Mathf.Abs(num)) < margin) {
                numerator = 1;
                denominator = 3;
            } else if (Mathf.Abs(0.25f - Mathf.Abs(num)) < margin) {
                numerator = 1;
                denominator = 4;
            }
            if (isNegative) {
                temp = new Circle(-1 * numerator, denominator);
                listOfAllCircles.Add(temp);
            } else {
                temp = new Circle(numerator, denominator);
                listOfAllCircles.Add(temp);
            }

        } else {
            temp = new Circle(num);
            listOfAllCircles.Add(temp);
        }
        return temp;
    }

    public Circle CreateSpecificCircle(float num) {
        bool isNegative = false;
        if (num < 0)
        {
            isNegative = true;
        }
        Circle temp;
        if (!TestIfIsInteger(num))
        {
            float margin = 0.001f;
            float numerator = 0;
            int denominator = 0;
            if (Mathf.Abs(0.75f - Mathf.Abs(num)) < margin)
            {
                numerator = 3;
                denominator = 4;
            }
            else if (Mathf.Abs(2f/3f - Mathf.Abs(num)) < margin)
            {
                numerator = 2;
                denominator = 3;
            }
            else if (Mathf.Abs(0.5f - Mathf.Abs(num)) < margin)
            {
                numerator = 1;
                denominator = 2;
            }
            else if (Mathf.Abs(1f/3f - Mathf.Abs(num)) < margin)
            {
                numerator = 1;
                denominator = 3;
            }
            else if (Mathf.Abs(0.25f - Mathf.Abs(num)) < margin)
            {
                numerator = 1;
                denominator = 4;
            }

            if (isNegative) {
                temp = new Circle(-1 * numerator, denominator);
                listOfAllCircles.Add(temp);
            } else {
                temp = new Circle(numerator, denominator);
                listOfAllCircles.Add(temp);
            }
        } else {
            temp = new Circle(num);
            listOfAllCircles.Add(temp);
        }
        return temp;
    }

    public Circle CreateRandomSecondCircleThatResultsInInt(Operator op, Circle firstCircle, int resultCap, float maxValue, bool oneAllowed, bool negativesAllowed, bool fractionsAllowed) {
        List<float> notSureIfPossibleValues = CreateListOfPossibleCircleValues_forArithmetic(maxValue, oneAllowed, negativesAllowed, fractionsAllowed);
        List<float> possibleValues = new List<float>();
        float result = 0;
        for (int i = 0; i < notSureIfPossibleValues.Count; i++) {
            if (op.type == "addition") {
                result = firstCircle.value + notSureIfPossibleValues[i];      // addition
            } else if (op.type == "subtraction") {
                //Debug.Log("about to test " + notSureIfPossibleValues[i]);
                result = firstCircle.value - notSureIfPossibleValues[i];      // subtraction
                //Debug.Log("        result: " + firstCircle.value + " + " + notSureIfPossibleValues[i] + " = " + result);
            }
            else if (op.type == "multiplication") {
                result = firstCircle.value * notSureIfPossibleValues[i];      // multiplication
            } else if (op.type == "division") {
                result = firstCircle.value / notSureIfPossibleValues[i];      // division
            }

            if (Mathf.Abs(result) <= resultCap && TestIfIsInteger(result) && (Mathf.RoundToInt(result) != 0)) {
                possibleValues.Add(notSureIfPossibleValues[i]);
                //Debug.Log("just added " + notSureIfPossibleValues[i] + "to possibleValues list");
                //Debug.Log("Mathf.Abs(result - 0) = " + Mathf.Abs(result - 0));
            }
        }

        //Debug.Log("for circle 2: possible values: ");
        //for (int i = 0; i < possibleValues.Count; i++) {
        //    Debug.Log(possibleValues[i]);
        //}

        // now pick a random value
        Circle temp = CreateRandomCircle(possibleValues);
        return temp;
    }

    public Circle CreateResultCircle(Circle circle1, Circle circle2, Operator opera) {
        float result = 0;
        Circle temp;
        if (opera.type == "addition") {
            result = circle1.value + circle2.value;
        } else if (opera.type == "subtraction") {
            result = circle1.value - circle2.value;
        } else if (opera.type == "multiplication") {
            result = circle1.value * circle2.value;
        } else if (opera.type == "division") {
            result = circle1.value / circle2.value;
        } else if (opera.type == "exponent2") {
            result = Mathf.Pow(circle1.value, 2);
        } else if (opera.type == "exponent3") {
            result = Mathf.Pow(circle1.value, 3);
        } else if (opera.type == "squareRoot") {
            result = Mathf.Pow(circle1.value, 0.5f);
        } else if (opera.type == "cubeRoot") {
            float value = circle1.value;
            if (value < 0) {
                result = -Mathf.Pow(-value, 1f / 3f);
            } else if (value > 0) {
                result = Mathf.Pow(value, 1f / 3f);
            } else {
                Debug.Log("problem: the circle value is zero");
            }
        }
        temp = CreateSpecificCircle(result);
        return temp;
    }

    // ************************************************************************************************************
    // ************************************************************************************************************

    public List<OpsAndCircles> CreatePartB_GivenInitial(Circle newCircle1, Operator operatorAlreadyUsed) {

        // List<OpsAndCircles>

        List<OpsAndCircles> toReturn = new List<OpsAndCircles>();

        // this function should be named something like,
        //
        // GivenCircle1HereIsOperatorAndCircle2()
        // or
        // CreatePartB()
        //
        // and it can return an ArrayList that contains the operator & the circle2

        // former name:     public List<Operator> WhichOperatorsAreEligibleGivenCircle1(Circle newCircle1) {


        // this function will be used when Part A is already created, and we need to create Part B
        // circle 1, in this case, is the Result of Part A
        // circle 1 can be integers within the range of -225 to 225, and can include simple fractions between -3/4 and 3/4

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

        List<float> circle2ValuesToConsider_Add_Subtract = CreateListOfPossibleCircleValues_forArithmetic(25f, true, true, true);
        List<float> circle2ValuesToConsider_Mult_Divide = CreateListOfPossibleCircleValues_forArithmetic(25f, false, true, true);

        void ConsiderAddition(Circle circle1) {
            float value = circle1.value;

            for (int i = 0; i < circle2ValuesToConsider_Add_Subtract.Count; i++) {
                float result = value + circle2ValuesToConsider_Add_Subtract[i];
                if (result >= -225 && result <= 225 && TestIfIsInteger(result) && result != 0) {
                    usableCircle2Values_Addition.Add(circle2ValuesToConsider_Add_Subtract[i]);
                }
            }
            if (usableCircle2Values_Addition.Count > 0) {
                operatorList.Add("addition");
            }
        }
        void ConsiderSubtraction(Circle circle1) {
            float value = circle1.value;

            for (int i = 0; i < circle2ValuesToConsider_Add_Subtract.Count; i++) {
                float result = value - circle2ValuesToConsider_Add_Subtract[i];
                if (result >= -225 && result <= 225 && TestIfIsInteger(result) && result != 0) {
                    usableCircle2Values_Subtraction.Add(circle2ValuesToConsider_Add_Subtract[i]);
                }
            }
            if (usableCircle2Values_Subtraction.Count > 0) {
                operatorList.Add("subtraction");
            }

        }
        void ConsiderMultiplication(Circle circle1)
        {
            float value = circle1.value;

            for (int i = 0; i < circle2ValuesToConsider_Mult_Divide.Count; i++)
            {
                float result = value * circle2ValuesToConsider_Mult_Divide[i];
                if (result >= -125 && result <= 125 && TestIfIsInteger(result) && result != 0)
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
                if (result >= -125 && result <= 125 && TestIfIsInteger(result) && result != 0)
                {
                    usableCircle2Values_Division.Add(circle2ValuesToConsider_Mult_Divide[i]);
                }
            }
            if (usableCircle2Values_Division.Count > 0)
            {
                operatorList.Add("division");
            }

        }
        void ConsiderExponent2(Circle circle1) {
            float value = circle1.value;
            // see if *value* is on the list of usable circle1s for exponent2
            List<float> usableCircle1ValuesForExponent2 = CreateListOfPossibleCircleValues_forExponent2(121, true, true, true);
            if (usableCircle1ValuesForExponent2.Contains(value)) {
                operatorList.Add("exponent2");
            }
        }
        void ConsiderExponent3(Circle circle1) {
            float value = circle1.value;
            // see if *value* is on the list of usable circle1s for exponent3
            List<float> usableCircle1ValuesForExponent3 = CreateListOfPossibleCircleValues_forExponent3(125, true, true, true);
            if (usableCircle1ValuesForExponent3.Contains(value)) {
                operatorList.Add("exponent3");
            }
        }
        void ConsiderSquareRoot(Circle circle1) {
            float value = circle1.value;

            List<float> usableCircle1ValuesForSquareRoot = CreateListOfPossibleCircleValues_forSquareRoot(11, true, false, true);
            if (usableCircle1ValuesForSquareRoot.Contains(value)) {
                operatorList.Add("squareRoot");
            }
        }
        void ConsiderCubeRoot(Circle circle1) {
            float value = circle1.value;
            List<float> usableCircle1ValuesForCubeRoot = CreateListOfPossibleCircleValues_forCubeRoot(5, true, true, true);
            if (usableCircle1ValuesForCubeRoot.Contains(value)) {
                operatorList.Add("cubeRoot");
            }
        }
        void ConsiderAllOperators(Circle circle1) {
            string otherOperator = operatorAlreadyUsed.type;
            ConsiderAddition(circle1);
            ConsiderSubtraction(circle1);
            ConsiderMultiplication(circle1);
            ConsiderDivision(circle1);
            if (otherOperator != "squareRoot") {
                ConsiderExponent2(circle1);
            }
            if (otherOperator != "cubeRoot") {
                ConsiderExponent3(circle1);
            }
            if (otherOperator != "exponent2") {
                ConsiderSquareRoot(circle1);
            }
            if (otherOperator != "exponent3") {
                ConsiderCubeRoot(circle1);
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
        if (operatorToUse == "addition") {
            int randomm = Random.Range(0, usableCircle2Values_Addition.Count);
            value = usableCircle2Values_Addition[randomm];
        } else if (operatorToUse == "subtraction") {
            int randomm = Random.Range(0, usableCircle2Values_Subtraction.Count);
            value = usableCircle2Values_Subtraction[randomm];
        } else if (operatorToUse == "multiplication") {
            int randomm = Random.Range(0, usableCircle2Values_Multiplication.Count);
            value = usableCircle2Values_Multiplication[randomm];
        } else if (operatorToUse == "division") {
            int randomm = Random.Range(0, usableCircle2Values_Division.Count);
            value = usableCircle2Values_Division[randomm];
        } else if (operatorToUse == "exponent2") { 
            // no circle 2 needed
        } else if (operatorToUse == "exponent3") {
            // no circle 2 needed
        } else if (operatorToUse == "squareRoot") {
            // no circle 2 needed
        } else if (operatorToUse == "cubeRoot") {
            // no circle 2 needed
        }

        Circle partB_Circle2 = null;
        if (operatorToUse == "addition" || operatorToUse == "subtraction" || operatorToUse == "multiplication" || operatorToUse == "division") {
            partB_Circle2 = CreateSpecificCircle(value);
            //Debug.Log("PartB newCircle2 value: " + partB_Circle2.value);
        }

        //Debug.Log("PartB operatorToUse: " + newOperator.type);

        // obtain result
        Circle partB_result = CreateResultCircle(newCircle1, partB_Circle2, newOperator);
        //Debug.Log("PartB result: " + partB_result.value);

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

        List<float> circle2ValuesToConsider_Add_Subtract = CreateListOfPossibleCircleValues_forArithmetic(25f, true, true, true);
        List<float> circle2ValuesToConsider_Mult_Divide = CreateListOfPossibleCircleValues_forArithmetic(25f, false, true, true);

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
            
            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(25f, true, true, true);

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
                    if (ABC == 'A') {
                        operatorList_1.Add("addition");
                        addition_circle1ValueToUse_1 = potentialCircle1Value;
                        addition_circle2ValueToUse_1 = potentialCircle2Value;
                        // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                        doneWithLoop = true;
                    } else if (ABC == 'B') {
                        operatorList_2.Add("addition");
                        addition_circle1ValueToUse_2 = potentialCircle1Value;
                        addition_circle2ValueToUse_2 = potentialCircle2Value;
                        doneWithLoop = true;
                    } else if (ABC == 'C') { 

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
            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(25f, true, true, true);

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
                    if (ABC == 'A') {
                        operatorList_1.Add("subtraction");
                        subtraction_circle1ValueToUse_1 = potentialCircle1Value;
                        subtraction_circle2ValueToUse_1 = potentialCircle2Value;
                        // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                        doneWithLoop = true;
                    } else if (ABC == 'B') {
                        operatorList_2.Add("subtraction");
                        subtraction_circle1ValueToUse_2 = potentialCircle1Value;
                        subtraction_circle2ValueToUse_2 = potentialCircle2Value;
                        doneWithLoop = true;
                    } else if (ABC == 'C') { 

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
        bool IsThisNumberPrime(float resultValue) {
            int tally = 0;
            for (int i = 1; i <= Mathf.Abs(resultValue); i++) { 
                if (resultValue % i == 0) {
                    tally += 1;
                }
            }
            if (tally == 2) {
                return true;
            } else {
                return false;
            }
        }
        void ConsiderMultiplication(float resultValue, char ABC)
        {
            // this is multiplication, so if the RESULT is a PRIME NUMBER, then we already know we don't want to use multiplication, because multiplying by 1 is boring

            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(25f, false, true, true);

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
                    if (ABC == 'A') {
                        operatorList_1.Add("multiplication");
                        multiplication_circle1ValueToUse_1 = potentialCircle1Value;
                        multiplication_circle2ValueToUse_1 = potentialCircle2Value;
                        // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                        doneWithLoop = true;
                    } else if (ABC == 'B') {
                        operatorList_2.Add("multiplication");
                        multiplication_circle1ValueToUse_2 = potentialCircle1Value;
                        multiplication_circle2ValueToUse_2 = potentialCircle2Value;
                        doneWithLoop = true;
                    } else if (ABC == 'C') { 

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
            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(25f, false, true, true);

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
                    if (ABC == 'A') {
                        operatorList_1.Add("division");
                        division_circle1ValueToUse_1 = potentialCircle1Value;
                        division_circle2ValueToUse_1 = potentialCircle2Value;
                        // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                        doneWithLoop = true;
                    } else if (ABC == 'B') {
                        operatorList_2.Add("division");
                        division_circle1ValueToUse_2 = potentialCircle1Value;
                        division_circle2ValueToUse_2 = potentialCircle2Value;
                        doneWithLoop = true;
                    } else if (ABC == 'C') { 

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
            List<float> usableResults = new List<float> { 0.25f, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121 };

            if (usableResults.Contains(resultValue))
            {
                // randomly assign circle1 to be either negative or positive
                int rando = Random.Range(1, 3);
                if (rando == 1) {
                    if (ABC == 'A') {
                        exponent2_circle1ValueToUse_1 = Mathf.Pow(resultValue, 0.5f);
                    } else if (ABC == 'B') {
                        exponent2_circle1ValueToUse_2 = Mathf.Pow(resultValue, 0.5f);
                    } else if (ABC == 'C') { 

                    }
                } else {
                    if (ABC == 'A') {
                        exponent2_circle1ValueToUse_1 = -Mathf.Pow(resultValue, 0.5f);
                    } else if (ABC == 'B') {
                        exponent2_circle1ValueToUse_2 = -Mathf.Pow(resultValue, 0.5f);
                    } else if (ABC == 'C') {

                    }
                }
                //Debug.Log("exponent2 is viable: " + exponent2_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                if (ABC == 'A') {
                    operatorList_1.Add("exponent2");
                } else if (ABC == 'B') {
                    operatorList_2.Add("exponent2");
                } else if (ABC == 'C') {

                }
            }
            else
            {
                //Debug.Log("the resultValue isn't on the pre-approved list, so exponent2 can be ignored");
            }
        }
        void ConsiderExponent3(float resultValue, char ABC)
        {
            List<float> usableResults = new List<float> { -125, -64, -27, -8, 8, 27, 64, 125 };

            if (usableResults.Contains(resultValue))
            {
                if (resultValue < 0) {
                    if (ABC == 'A') {
                        exponent3_circle1ValueToUse_1 = -Mathf.Pow(-resultValue, 1f / 3f);
                    } else if (ABC == 'B') {
                        exponent3_circle1ValueToUse_2 = -Mathf.Pow(-resultValue, 1f / 3f);
                    } else if (ABC == 'C') {

                    }
                } else {
                    if (ABC == 'A') {
                        exponent3_circle1ValueToUse_1 = Mathf.Pow(resultValue, 1f / 3f);
                    } else if (ABC == 'B') {
                        exponent3_circle1ValueToUse_2 = Mathf.Pow(resultValue, 1f / 3f);
                    } else if (ABC == 'C') {

                    }
                }
                //Debug.Log("exponent3 is viable: " + exponent3_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                if (ABC == 'A') {
                    operatorList_1.Add("exponent3");
                } else if (ABC == 'B') {
                    operatorList_2.Add("exponent3");
                } else if (ABC == 'C') {

                }
            }
            else
            {
                //Debug.Log("the resultValue isn't on the pre-approved list, so exponent3 can be ignored");
            }
        }
        void oldConsider() {
            //void ConsiderExponent2(float resultValue) {
            //    // this should be pretty easy, as there's no circle2 needed

            //    // let's be smart:
            //    //      IF RESULTVALUE IS NEGATIVE, THERE IS NO CIRCLE1 VALUE WHICH, IF SQUARED, WILL GIVE US THAT NEGATIVE NUMBER

            //    // let's be even smarter:
            //    //      if the operator is exponent2, then the only results that are compatible are:
            //    //      1/4, 1 (if we want it to), 4, 9, 16, 25, 36, 49, 64, 81, 100, 121           that's it
            //    //      if resultValue isn't ONE OF THOSE THINGS, then we're done

            //    List<float> usableResults = new List<float> {0.25f, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121};

            //    if (usableResults.Contains(resultValue)) {
            //        bool doneWithLoop = false;
            //        List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forExponent2(121, false, false, true);
            //        while (doneWithLoop == false)
            //        {
            //            // step 1: pick a random circle1 value, within the acceptable range 
            //            int rando = Random.Range(0, circle1ValuesToConsider.Count);
            //            float potentialCircle1Value = circle1ValuesToConsider[rando];
            //            Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
            //            circle1ValuesToConsider.Remove(potentialCircle1Value);
            //            // step 2: see if it can work, to get us the predetermined result
            //            //      so we're considering exponent2... meaning we'll have a^2 = c, and we know c, therefore c = a^(1/2)
            //            //      so all we need to do is see if this circle1 value satisfies c = a^(1/2)
            //            if (Mathf.Pow(potentialCircle1Value, 2) == resultValue)
            //            {
            //                Debug.Log("exponent2 is viable: " + potentialCircle1Value + " as circle1 gives us result of " + resultValue);
            //                operatorList.Add("exponent2");
            //                exponent2_circle1ValueToUse = potentialCircle1Value;
            //                doneWithLoop = true;
            //            }
            //            else
            //            {
            //                Debug.Log("this won't work: " + potentialCircle1Value + " squared = " + resultValue);
            //                if (circle1ValuesToConsider.Count == 0)
            //                {
            //                    Debug.Log("*****************************exponent2 will not work... we've tried every option");
            //                    doneWithLoop = true;
            //                }
            //            }
            //        }
            //    } else {
            //        Debug.Log("the resultValue isn't on the (short) pre-approved list, so exponent2 can be ignored");
            //    }
            //}
            //void ConsiderExponent3(float resultValue)
            //{
            //    // this should be pretty easy, as there's no circle2 needed

            //    // let's be smart:
            //    //      if the operator is exponent3, then the only results that are compatible are:
            //    //      -125, -64, -27, -8, 8, 27, 64, 125
            //    //      if resultValue isn't ONE OF THOSE THINGS, then we're done

            //    List<float> usableResults = new List<float> { -125, -64, -27, -8, 8, 27, 64, 125 };

            //    if (usableResults.Contains(resultValue))
            //    {
            //        bool doneWithLoop = false;
            //        List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forExponent3(125, false, true, true);
            //        while (doneWithLoop == false)
            //        {
            //            // step 1: pick a random circle1 value, within the acceptable range 
            //            int rando = Random.Range(0, circle1ValuesToConsider.Count);
            //            float potentialCircle1Value = circle1ValuesToConsider[rando];
            //            Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
            //            circle1ValuesToConsider.Remove(potentialCircle1Value);
            //            // step 2: see if it can work, to get us the predetermined result
            //            //      so we're considering exponent2... meaning we'll have a^2 = c, and we know c, therefore c = a^(1/2)
            //            //      so all we need to do is see if this circle1 value satisfies c = a^(1/2)
            //            if (Mathf.Pow(potentialCircle1Value, 3) == resultValue)
            //            {
            //                Debug.Log("exponent3 is viable: " + potentialCircle1Value + " as circle1 gives us result of " + resultValue);
            //                operatorList.Add("exponent3");
            //                exponent3_circle1ValueToUse = potentialCircle1Value;
            //                doneWithLoop = true;
            //            }
            //            else
            //            {
            //                Debug.Log("this won't work: " + potentialCircle1Value + " cubed = " + resultValue);
            //                if (circle1ValuesToConsider.Count == 0)
            //                {
            //                    Debug.Log("*****************************exponent3 will not work... we've tried every option");
            //                    doneWithLoop = true;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("the resultValue isn't on the pre-approved list, so exponent3 can be ignored");
            //    }
            //}

            //void ConsiderCubeRoot(float resultValue)
            //{
            //    // this should be pretty easy, as there's no circle2 needed

            //    // let's be smart:
            //    //      if the operator is cubeRoot, then the only results that are compatible are:
            //    //      -5, -4, -3, -2, 2, 3, 4, 5
            //    //      if resultValue isn't ONE OF THOSE THINGS, then we're done

            //    List<float> usableResults = new List<float> { -5, -4, -3, -2, 2, 3, 4, 5 };

            //    if (usableResults.Contains(resultValue))
            //    {
            //        bool doneWithLoop = false;
            //        List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forCubeRoot(5, false, true, true);
            //        while (doneWithLoop == false)
            //        {
            //            // step 1: pick a random circle1 value, within the acceptable range
            //            int rando = Random.Range(0, circle1ValuesToConsider.Count);
            //            float potentialCircle1Value = circle1ValuesToConsider[rando];
            //            Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
            //            circle1ValuesToConsider.Remove(potentialCircle1Value);
            //            // step 2: see if it can work, to get us the predetermined result
            //            //      so we're considering exponent2... meaning we'll have a^2 = c, and we know c, therefore c = a^(1/2)
            //            //      so all we need to do is see if this circle1 value satisfies c = a^(1/2)
            //            if (Mathf.Pow(potentialCircle1Value, 1f/3f) == resultValue)
            //            {
            //                Debug.Log("cubeRoot is viable: " + potentialCircle1Value + " as circle1 gives us result of " + resultValue);
            //                operatorList.Add("cubeRoot");
            //                cubeRoot_circle1ValueToUse = potentialCircle1Value;
            //                doneWithLoop = true;
            //            }
            //            else
            //            {
            //                Debug.Log("this won't work: " + potentialCircle1Value + " ^(1/3) = " + resultValue);
            //                if (circle1ValuesToConsider.Count == 0)
            //                {
            //                    Debug.Log("*****************************cubeRoot will not work... we've tried every option");
            //                    doneWithLoop = true;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("the resultValue isn't on the pre-approved list, so cubeRoot can be ignored");
            //    }
            //}
        }
        void ConsiderSquareRoot(float resultValue, char ABC) {
            List<float> usableResults = new List<float> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            if (usableResults.Contains(resultValue))
            {
                if (ABC == 'A')
                {
                    squareRoot_circle1ValueToUse_1 = Mathf.Pow(resultValue, 2);
                    //Debug.Log("squareRoot is viable: " + squareRoot_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                    operatorList_1.Add("squareRoot");
                }
                else if (ABC == 'B')
                {
                    squareRoot_circle1ValueToUse_2 = Mathf.Pow(resultValue, 2);
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
            List<float> usableResults = new List<float> { -5, -4, -3, -2, 2, 3, 4, 5 };

            if (usableResults.Contains(resultValue))
            {
                if (ABC == 'A') {
                    cubeRoot_circle1ValueToUse_1 = Mathf.Pow(resultValue, 3);
                    //Debug.Log("cubeRoot is viable: " + cubeRoot_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                    operatorList_1.Add("cubeRoot");
                } else if (ABC == 'B') {
                    cubeRoot_circle1ValueToUse_2 = Mathf.Pow(resultValue, 3);
                    //Debug.Log("cubeRoot is viable: " + cubeRoot_circle1ValueToUse_2 + " as circle1 gives us result of " + resultValue);
                    operatorList_2.Add("cubeRoot");
                } else if (ABC == 'C') {

                }
            }
            else
            {
                //Debug.Log("the resultValue isn't on the pre-approved list, so cubeRoot can be ignored");
            }
        }

        void ConsiderAllOperators(Circle potentialResult_1or2, char AorB) {

            string otherOperator = operatorAlreadyUsed.type;
            float value = potentialResult_1or2.value;

            if (Mathf.Abs(value) <= 40) {
                ConsiderAddition(value, AorB);
                ConsiderSubtraction(value, AorB);
            } else {
                //Debug.Log(value + " is too big, so we're skipping addition & subtraction");
            }
            // if valueOfResultA is a PRIME NUMBER, then we don't want to consider multiplication or division, because *1 and /1 are boring
            if (!IsThisNumberPrime(value)) {
                ConsiderMultiplication(value, AorB);
                ConsiderDivision(value, AorB);
            } else {
                //Debug.Log(value + " is a prime number, so we're skipping multiplication & division");
            }
            if (otherOperator != "squareRoot") {
                ConsiderExponent2(value, AorB);
            }
            if (otherOperator != "cubeRoot") {
                ConsiderExponent3(value, AorB);
            }
            if (otherOperator != "exponent2") {
                ConsiderSquareRoot(value, AorB);
            }
            if (otherOperator != "exponent3") {
                ConsiderCubeRoot(value, AorB);
            }
        }

        ConsiderAllOperators(potentialResult1, 'A');
        if (potentialResult2 != null) {
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
                resultPartA = Mathf.Pow(circle1Value, 2);
            }
            else if (operatorWeUse == "exponent3")
            {
                partA_operator = new Operator("exponent3", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = exponent3_circle1ValueToUse_1;
                resultPartA = Mathf.Pow(circle1Value, 3);
            }
            else if (operatorWeUse == "squareRoot")
            {
                partA_operator = new Operator("squareRoot", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = squareRoot_circle1ValueToUse_1;
                resultPartA = Mathf.Pow(circle1Value, 0.5f);
            }
            else if (operatorWeUse == "cubeRoot")
            {
                partA_operator = new Operator("cubeRoot", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = cubeRoot_circle1ValueToUse_1;
                if (circle1Value < 0) {
                    resultPartA = -Mathf.Pow(-circle1Value, 1f / 3f);
                } else if (circle1Value > 0) {
                    resultPartA = Mathf.Pow(circle1Value, 1f / 3f);
                }

            }

            partA_circle1 = CreateSpecificCircle(circle1Value);
            partA_circle2 = CreateSpecificCircle(circle2Value);
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
                resultPartA = Mathf.Pow(circle1Value, 2);
            }
            else if (operatorWeUse == "exponent3")
            {
                partA_operator = new Operator("exponent3", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = exponent3_circle1ValueToUse_2;
                resultPartA = Mathf.Pow(circle1Value, 3);
            }
            else if (operatorWeUse == "squareRoot")
            {
                partA_operator = new Operator("squareRoot", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = squareRoot_circle1ValueToUse_2;
                resultPartA = Mathf.Pow(circle1Value, 0.5f);
            }
            else if (operatorWeUse == "cubeRoot")
            {
                partA_operator = new Operator("cubeRoot", 'A');
                listOfAllOperators.Add(partA_operator);
                circle1Value = cubeRoot_circle1ValueToUse_2;
                if (circle1Value < 0) {
                    resultPartA = -Mathf.Pow(-circle1Value, 1f / 3f);
                } else if (circle1Value > 0) {
                    resultPartA = Mathf.Pow(circle1Value, 1f / 3f);
                }
            }

            partA_circle1 = CreateSpecificCircle(circle1Value);
            partA_circle2 = CreateSpecificCircle(circle2Value);
            partA_result = CreateSpecificCircle(resultPartA);

            //Debug.Log("for PartA, circle1.value is: " + circle1Value);
            //Debug.Log("for PartA, circle2.value is: " + circle2Value);
            //Debug.Log("for PartA, using operator: " + operatorWeUse);
            //Debug.Log("for PartA, result is: " + resultPartA);
        }

        //Debug.Log("operatorList_A contains " + operatorList_1.Count + " possible operators");
        //Debug.Log("operatorList_B contains " + operatorList_2.Count + " possible operators");

        // compare the two operatorLists and see which one has the most options

        if (operatorList_1.Count > operatorList_2.Count) {
            //Debug.Log("going with ResultA");
            GoWithResult_1();
        } else if (operatorList_1.Count < operatorList_2.Count) {
            //Debug.Log("going with ResultB");
            GoWithResult_2();
        } else {
            int rando = Random.Range(1, 3);
            if (rando == 1) {
                //Debug.Log("going with ResultA, chosen randomly because the lists are the same size");
                GoWithResult_1();
            } else {
                //Debug.Log("going with ResultB, chosen randomly because the lists are the same size");
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
        if (circleData == null || circleData.value == 0)
        {
            //CircleA.transform.GetChild(0).GetComponent<TextMeshPro>().text = circle1.value.ToString();
            circleGameObject.SetActive(false);
        }
        else
        {
            if (TestIfIsInteger(circleData.value))
            {
                circleGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = circleData.value.ToString();
            }
            else
            {
                circleGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = circleData.value.ToString("F2");
            }

            circleGameObject.SetActive(true);
            circleGameObject.GetComponent<Clickable>().valueOfThisCircle_orGoal = circleData.value;
            circleGameObject.GetComponent<Clickable>().IDnumberOfCircleDataAttachedToThis = circleData.IDnumber;        // this marks the GameObject 

            if (circleGameObject == CircleA) {
                circleData.circleGameObject_associatedWith = "CircleA";                                                 // this marks the circleData Circle class object
            } else if (circleGameObject == CircleB) {
                circleData.circleGameObject_associatedWith = "CircleB";
            } else if (circleGameObject == CircleC) {
                circleData.circleGameObject_associatedWith = "CircleC";
            }

        }
    }
    public void SetOperator(GameObject opAorB, Operator oppy)
    {
        opAorB.SetActive(true);

        if (oppy.type == "addition")
        {
            opAorB.GetComponent<Clickable>().typeOfThisOperator = "addition";
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
            opAorB.GetComponent<Clickable>().typeOfThisOperator = "subtraction";
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
            opAorB.GetComponent<Clickable>().typeOfThisOperator = "multiplication";
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
            opAorB.GetComponent<Clickable>().typeOfThisOperator = "division";
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
            opAorB.GetComponent<Clickable>().typeOfThisOperator = "exponent2";
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
            opAorB.GetComponent<Clickable>().typeOfThisOperator = "exponent3";
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
            opAorB.GetComponent<Clickable>().typeOfThisOperator = "squareRoot";
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
            opAorB.GetComponent<Clickable>().typeOfThisOperator = "cubeRoot";
            opAorB.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
            opAorB.transform.GetChild(1).gameObject.SetActive(true);                   // squareRootImage
            opAorB.transform.GetChild(2).gameObject.SetActive(true);                   // squareRoot_X
            opAorB.transform.GetChild(3).gameObject.SetActive(true);                   // cubeRoot3
            opAorB.transform.GetChild(4).gameObject.SetActive(false);                   // exponent2
            opAorB.transform.GetChild(5).gameObject.SetActive(false);                   // exponent3
            opAorB.transform.GetChild(6).gameObject.SetActive(false);                   // exponent_X
        }
        opAorB.GetComponent<Clickable>().IDnumberOfOperatorDataAttachedToThis = oppy.IDnumber;                  // this marks the GameObject
        if (opAorB == OperatorA) {                                                                              // this marks the Operator class object
            oppy.operatorGameObject_associatedWith = "OperatorA";
        } else if (opAorB == OperatorB) {
            oppy.operatorGameObject_associatedWith = "OperatorB";
        }
    }

    public void CreateNewPuzzle() 
    {
        // get rid of all the Circles & Operators we created in past puzzles
        Circle.numberOfThisTypeThatExist = 0;
        listOfAllCircles.Clear();
        Operator.numberOfThisTypeThatExist = 0;
        listOfAllOperators.Clear();

        ResetColorsAndMath_Circles_Operators();




        int ABC = Random.Range(1, 3);   // this will determine whether we're starting by creating PartA, or starting by creating PartB
        char A_B_C = 'x';
        if (ABC == 1) {
            A_B_C = 'A';
            //Debug.Log("we start by creating PartA");
        } else if (ABC == 2) {
            A_B_C = 'B';
            //Debug.Log("we start by creating PartB");
        } else if (ABC == 3) {
            A_B_C = 'C';
            //Debug.Log("we start by creating PartC");        // this isn't currently supported
        }
        Operator oppy = PickRandomOperator(8, A_B_C);

        Circle inputCircleA = null;
        Circle inputCircleB = null;
        Circle outputCircle = null;

        if (oppy.type == "addition" || oppy.type == "subtraction") {
            List<float> stuff = CreateListOfPossibleCircleValues_forArithmetic(20.0f, true, true, true);
            inputCircleA = CreateRandomCircle(stuff);
            inputCircleB = CreateRandomSecondCircleThatResultsInInt(oppy, inputCircleA, 121, 20.0f, true, true, true);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleB, oppy);
        } else if (oppy.type == "multiplication" || oppy.type == "division") {
            List<float> stuff = CreateListOfPossibleCircleValues_forArithmetic(20.0f, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);
            inputCircleB = CreateRandomSecondCircleThatResultsInInt(oppy, inputCircleA, 121, 20.0f, false, true, true);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleB, oppy);
        } else if (oppy.type == "exponent2") {
            List<float> stuff = CreateListOfPossibleCircleValues_forExponent2(121, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, oppy);
        } else if (oppy.type == "exponent3") {
            List<float> stuff = CreateListOfPossibleCircleValues_forExponent3(125, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, oppy);
        } else if (oppy.type == "squareRoot") {
            List<float> stuff = CreateListOfPossibleCircleValues_forSquareRoot(11, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, oppy);
        } else if (oppy.type == "cubeRoot") {
            List<float> stuff = CreateListOfPossibleCircleValues_forCubeRoot(5, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, oppy);
        }
        //Debug.Log("first value: " + inputCircleA.value);
        //Debug.Log("operator: " + oppy.type);
        //if (inputCircleB != null) {
        //    Debug.Log("second value: " + inputCircleB.value);
        //}
        //Debug.Log("result: " + outputCircle.value);

        // CREATE THE OTHER PART OF THE PROBLEM
        //      unlike above, where we started with an operator, this time we will be starting with a circle
        //      so we need to determine which operators will work with this circle
        //          for example, if circle.value == 0.25, then we know we can't use exponent2 (as it will result in 0.25^2 = 0.125)
        //          for example, if circle.value == 100, then we know we can't use cubeRoot (as it will result in 100^(1/3) = 4.641588....


        Circle circle1 = null;
        Circle circle2 = null;
        Circle circle3 = null;
        Circle circle4 = null;
        Circle circle5 = null;
        Operator operator1 = null;
        Operator operator2 = null;
        Operator operator3 = null;
        Circle result = null;

        // depending on what A_B_C is, will affect which of the following we choose:
        if (A_B_C == 'A') {
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
        } else if (A_B_C == 'B') {
            // we already created PartB, so now we create PartA
            List<OpsAndCircles> PartAStuff = CreatePartA_GivenInitial(inputCircleA, inputCircleB, oppy);
            circle1 = (Circle)PartAStuff[1];
            //if ((Circle)PartAStuff[2] != null) {
                circle2 = (Circle)PartAStuff[2]; // might be null
            //}
            // the above function will tell us which PartA result was used as the first input for PartB
            //      we need to identify which PartA result was used, and then make the OTHER ONE appear in the puzzle
            Circle resultFromPartA = (Circle)PartAStuff[3];
            if (resultFromPartA.value == inputCircleA.value) {
                circle3 = inputCircleB;
            } else if (resultFromPartA.value == inputCircleB.value) {
                circle3 = inputCircleA;
            }
            operator1 = (Operator)PartAStuff[0];
            operator2 = oppy;
            result = outputCircle;
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


        void PutPuzzleIntoGame() {
            // put the puzzle into the game for the user to see

            // how many valid circles do we have? 1 or 2 or 3?

            // also, if the value of a circle is zero, as far as we're concerned it's null, so don't show that circle

            SetCircle(CircleA, circle1);
            SetCircle(CircleB, circle2);
            SetCircle(CircleC, circle3);

            // change positions depending on how many valid circles (for visual symmetry)
            //      do this later

            MathInProgress.GetComponent<TextMeshPro>().text = "";

            SetOperator(OperatorA, operator1);
            SetOperator(OperatorB, operator2);

            Goal.transform.GetChild(0).GetComponent<TextMeshPro>().text = result.value.ToString("F0");      // goal is always an int anyway
            Goal.GetComponent<Clickable>().valueOfThisCircle_orGoal = result.value;
        }

        //GameManager.instance.DisplayLevel();
        PutPuzzleIntoGame();

        // do the Timer stuff here

        //      when the game starts, and the very first puzzle is created
        //          the global timer is set to 90 seconds & starts counting down
        //          the puzzle timer is RESET to 10 seconds & starts counting down


        timerPuzzle.ResetPuzzleTimer();
        timerGlobal.UnpauseGlobalTimer();
        timerPuzzle.UnpausePuzzleTimer();











        //      when a new puzzle is created, after the very first puzzle








    }

    // ************************************************************************************************************
    // ************************************************************************************************************

    public void ResetColorsAndMath_Circles_Operators() {
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
    }
    public void HighlightCircleGameObject(GameObject obby, int oneORtwo) {
        if (oneORtwo == 1) {
            highlightedCircle1 = obby;
            highlightedCircle1.GetComponent<SpriteRenderer>().color = highlightedColor;
        } else if (oneORtwo == 2) {
            highlightedCircle2 = obby;
            highlightedCircle2.GetComponent<SpriteRenderer>().color = highlightedColor;
        }
    }
    public void UNHighlightCircleGameObject(GameObject obby) {
        // stop the highlight animation and/or reverse the highlighted color and make it the normal color
        obby.GetComponent<SpriteRenderer>().color = whiteColor;
    }
    public void HighLightOperatorGameObject(GameObject obby) {
        highlightedOperator = obby;
        highlightedOperator.GetComponent<SpriteRenderer>().color = highlightedColor;
    }
    public void UNHighlightOperatorGameObject(GameObject obby) {
        obby.GetComponent<SpriteRenderer>().color = whiteColor;
    }

    // ************************************************************************************************************
    // ************************************************************************************************************
    public void AcceptClickedCircleOrOperator(GameObject gameObjectClicked) {
        if (circle1selected == false && operatorSelected == false && math_oneCircle_IsComplete == false && math_twoCircle_IsComplete == false)
        {
            // this is the stage where we select the first Circle... the player can change which one they want up until they click an operator
            if (gameObjectClicked.CompareTag("circle"))
            {
                value1 = GetValueOfClickedCircle(gameObjectClicked);
                HighlightCircleGameObject(gameObjectClicked, 1);
                circle1selected = true;
            }
            else if (gameObjectClicked.CompareTag("operator"))
            {
                // do nothing
            }
        } else if (circle1selected == true && operatorSelected == false && math_oneCircle_IsComplete == false && math_twoCircle_IsComplete == false) {
            if (gameObjectClicked.CompareTag("circle")) {
                if (gameObjectClicked != highlightedCircle1)
                {
                    // simply change which circle is highlighted
                    value1 = GetValueOfClickedCircle(gameObjectClicked);
                    UNHighlightCircleGameObject(highlightedCircle1);
                    HighlightCircleGameObject(gameObjectClicked, 1);
                }
                else if (gameObjectClicked == highlightedCircle1)
                {
                    // the highlighted circle is no longer highlighted
                    value1 = 0;
                    UNHighlightCircleGameObject(highlightedCircle1);
                    circle1selected = false;
                    highlightedCircle1 = null;
                }
            } else if (gameObjectClicked.CompareTag("operator")) {
                highlightedOperator = gameObjectClicked;
                HighLightOperatorGameObject(gameObjectClicked);
                operatorSelected = true;    // this prevents the player from quickly clicking the other operator while the animation plays out, and causing a glitch
                DetermineWhether_oneCircle_MathIsComplete();
                if (math_oneCircle_IsComplete) {
                    ExecuteCompletionOf_oneCircle_Math();
                    DetermineWhetherPuzzleIsSolved();
                }
            }
        } else if (circle1selected == true && operatorSelected == true && math_oneCircle_IsComplete == false && math_twoCircle_IsComplete == false) { 
            // ready to accept circle2... CANNOT change circle1 at this point... but CAN CHANGE operator
            if (gameObjectClicked.CompareTag("circle")) { 
                if (gameObjectClicked == highlightedCircle1) {
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
                } else {
                    value2 = GetValueOfClickedCircle(gameObjectClicked);
                    HighlightCircleGameObject(gameObjectClicked, 2);
                    circle2selected = true;
                    DetermineWhether_twoCircle_MathIsComplete();
                    if (math_twoCircle_IsComplete) {
                        ExecuteCompletionOf_twoCircle_Math();
                        DetermineWhetherPuzzleIsSolved();
                    }
                }
            } else if (gameObjectClicked.CompareTag("operator")) { 
                if (gameObjectClicked == highlightedOperator) {
                    // you CANNOT CLICK on this operator because it has already disappeared
                    // unhighlight
                    UNHighlightOperatorGameObject(highlightedOperator);
                    highlightedOperator = null;
                    operatorSelected = false;
                } else {
                    // change the operator
                    UNHighlightOperatorGameObject(highlightedOperator);
                    highlightedOperator = gameObjectClicked;
                    HighLightOperatorGameObject(gameObjectClicked);
                    DetermineWhether_oneCircle_MathIsComplete();
                    if (math_oneCircle_IsComplete) {
                        ExecuteCompletionOf_oneCircle_Math();
                    }
                }
            }
        }






    }
    // ************************************************************************************************************
    // ************************************************************************************************************
    public string GetTypeOfClickedOperator(GameObject oppy) {
        string toReturn = oppy.GetComponent<Clickable>().typeOfThisOperator;
        return toReturn;
    }

    public float GetValueOfClickedCircle(GameObject circleClicked) {
        float value = 0;
        if (circleClicked == CircleA) {
            //Debug.Log("CircleA was clicked");
            //Debug.Log("by the way, the length of listOfAllCircles is: " + listOfAllCircles.Count);
            //Debug.Log("numberOfThisTypeThatExist: " + Circle.numberOfThisTypeThatExist);
            // find the circleData associated with CircleA... we can find it because we previously marked both the GameObject and the Circle so we know they're associated
            int IDnum = CircleA.GetComponent<Clickable>().IDnumberOfCircleDataAttachedToThis;
            for (int i = 0; i < listOfAllCircles.Count; i++) { 
                if (listOfAllCircles[i].IDnumber == IDnum) {
                    value = listOfAllCircles[i].value;
                }
            }
            //Debug.Log("value of CircleA: " + value);

        } else if (circleClicked == CircleB) {
            //Debug.Log("CircleB was clicked");
            //Debug.Log("by the way, the length of listOfAllCircles is: " + listOfAllCircles.Count);
            //Debug.Log("numberOfThisTypeThatExist: " + Circle.numberOfThisTypeThatExist);
            int IDnum = CircleB.GetComponent<Clickable>().IDnumberOfCircleDataAttachedToThis;
            for (int i = 0; i < listOfAllCircles.Count; i++)
            {
                if (listOfAllCircles[i].IDnumber == IDnum)
                {
                    value = listOfAllCircles[i].value;
                }
            }
            //Debug.Log("value of CircleB: " + value);
        } else if (circleClicked == CircleC) {
            //Debug.Log("CircleC was clicked");
            //Debug.Log("by the way, the length of listOfAllCircles is: " + listOfAllCircles.Count);
            //Debug.Log("numberOfThisTypeThatExist: " + Circle.numberOfThisTypeThatExist);
            int IDnum = CircleC.GetComponent<Clickable>().IDnumberOfCircleDataAttachedToThis;
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

    public float GetValueOfGoal() {
        return Goal.GetComponent<Clickable>().valueOfThisCircle_orGoal;
    }

    public Circle GetCircle_classObject_OfClickedCircle_gameObject(GameObject circleClicked) {
        int IDnum = circleClicked.GetComponent<Clickable>().IDnumberOfCircleDataAttachedToThis;
        for (int i = 0; i < listOfAllCircles.Count; i++) { 
            if (listOfAllCircles[i].IDnumber == IDnum) {
                return listOfAllCircles[i];
            }
        }
        Debug.Log("Error 5757");
        return null;
    }

    public Operator GetOperator_classObject_OfClickedOperator_gameObject(GameObject operatorClicked) {
        int IDnum = operatorClicked.GetComponent<Clickable>().IDnumberOfOperatorDataAttachedToThis;
        for (int i = 0; i < listOfAllOperators.Count; i++) { 
            if (listOfAllOperators[i].IDnumber == IDnum) {
                return listOfAllOperators[i];
            }
        }
        Debug.Log("Error 1414");
        return null;
    }

    public void DetermineWhether_oneCircle_MathIsComplete() {
        // if the operator is ADD/SUBT/MULT/DIVI, then a second Circle must be clicked
        //      otherwise, just 1 circle and then 1 operator is enough

        //math_oneCircle_IsComplete = true;

        if (circle1selected && operatorSelected) {
            string temp = GetTypeOfClickedOperator(highlightedOperator);
            if (temp == "exponent2" || temp == "exponent3" || temp == "squareRoot" || temp == "cubeRoot") {
                math_oneCircle_IsComplete = true;
            }
        }
    }
    public void DetermineWhether_twoCircle_MathIsComplete()
    {
        // if the operator is ADD/SUBT/MULT/DIVI, then a second Circle must be clicked
        //      otherwise, just 1 circle and then 1 operator is enough

        //math_twoCircle_IsComplete = true;

        if (circle1selected && operatorSelected && circle2selected) {
            string temp = GetTypeOfClickedOperator(highlightedOperator);
            if (temp == "exponent2" || temp == "exponent3" || temp == "squareRoot" || temp == "cubeRoot") {
                // do nothing
            } else {
                math_twoCircle_IsComplete = true;
            }
        }

    }

    public void ExecuteCompletionOf_oneCircle_Math() {
        // for now, make circle1 & operator disappear, and then circle1 is rebuilt with the new value
        highlightedCircle1.SetActive(false);
        highlightedOperator.SetActive(false);
        Circle c1 = GetCircle_classObject_OfClickedCircle_gameObject(highlightedCircle1);
        Operator oppy = GetOperator_classObject_OfClickedOperator_gameObject(highlightedOperator);

        Circle result = CreateResultCircle(c1, c1, oppy);
        SetCircle(highlightedCircle1, result);

        highlightedCircle1.SetActive(true);

        ResetColorsAndMath_Circles_Operators();

    }
    public void ExecuteCompletionOf_twoCircle_Math()
    {
        highlightedCircle1.SetActive(false);
        highlightedOperator.SetActive(false);
        highlightedCircle2.SetActive(false);
        Circle c1 = GetCircle_classObject_OfClickedCircle_gameObject(highlightedCircle1);
        Circle c2 = GetCircle_classObject_OfClickedCircle_gameObject(highlightedCircle2);
        Operator oppy = GetOperator_classObject_OfClickedOperator_gameObject(highlightedOperator);

        Circle result = CreateResultCircle(c1, c2, oppy);
        SetCircle(highlightedCircle1, result);

        highlightedCircle1.SetActive(true);

        ResetColorsAndMath_Circles_Operators();
    }

    public void DetermineWhetherPuzzleIsSolved() {
        // if there is only 1 circle remaining, and 0 operators remaining
        int circleTally = 0;
        GameObject temp = null;
        if (CircleA.activeSelf) {
            circleTally += 1;
            temp = CircleA;
        }
        if (CircleB.activeSelf) {
            circleTally += 1;
            temp = CircleB;
        }
        if (CircleC.activeSelf) {
            circleTally += 1;
            temp = CircleC;
        }

        int operatorTally = 0;
        if (OperatorA.activeSelf) {
            operatorTally += 1;
        }
        if (OperatorB.activeSelf) {
            operatorTally += 1;
        }

        if (circleTally == 1 && operatorTally == 0) {
            // if the 1 circle value is the same as the goal
            float circleValue = GetValueOfClickedCircle(temp);
            float goalValue = GetValueOfGoal();
            if (circleValue == goalValue) {
                Debug.Log("there is 1 circle and 0 operators, and the goal has been reached");
                AnimatePuzzleSolved();
            } else {
                Debug.Log("there is 1 circle and 0 operators------------------------------------------ but the goal is not reached! you FAIL!");
                AnimatePuzzleFailed();
            }
        }

    }



    public void AnimatePuzzleSolved() {
        // stop the timers
        timerGlobal.PauseGlobalTimer();
        timerPuzzle.PausePuzzleTimer();
        timerGlobal.AddToGlobalTimer(timerPuzzle.GetPuzzleTimeRemaining());

        // to do: put in animations & sounds



        // then load a new puzzle
        if (timerGlobal.GetTimeRemaining() > 0) {
            CreateNewPuzzle();
        }


        // then 
    }
    public void AnimatePuzzleFailed() {
        // stop the timers
        timerGlobal.PauseGlobalTimer();
        timerPuzzle.PausePuzzleTimer();
        timerGlobal.SubtractFromGlobalTimer(5);



        // to do: put in puzzle failed animations & sounds

        // then load a new puzzle
        if (timerGlobal.GetTimeRemaining() > 0) {
            CreateNewPuzzle();
        }
    }





    public void CombineCircle1_and_Operator() { 

    }
    public void Reverse_CombineCircle1_and_Operator() { 

    }



}