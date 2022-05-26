using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class Operator {
        public string type;
        int numberOfThisTypeThatExist;
        int IDnumber;
        Vector2 position;
        float input1;
        float input2;
        float output;
        public char AorBorC;

        public Operator(string name, char ABC) {
            type = name;
            AorBorC = ABC;
            IDnumber = numberOfThisTypeThatExist;
            numberOfThisTypeThatExist += 1;
            position = new Vector2(0, 0);
        }

    }
    public class Circle {
        public float value;
        public bool trueInt_falseFraction;
        public int numerator;
        public int denominator;
        int numberOfThisTypeThatExist;
        int IDnumber;
        Vector2 position;

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
            toReturn.Add(-0.6666f);   // -2/3
            toReturn.Add(-0.5f);    // -1/2
            toReturn.Add(-0.3333f);   // -1/3
            toReturn.Add(-0.25f);   // -1/4
        }
        if (fractionsAllowed) {
            toReturn.Add(0.25f);
            toReturn.Add(0.3333f);
            toReturn.Add(0.5f);
            toReturn.Add(0.6666f);
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
        if (negativesAllowed && fractionsAllowed)
        {
            //toReturn.Add(-0.7500f);   // -3/4
            //toReturn.Add(-0.6666f);   // -2/3
            //toReturn.Add(-0.5f);    // -1/2     
            //toReturn.Add(-0.3333f);   // -1/3
            //toReturn.Add(-0.25f);   // -1/4
        }
        if (fractionsAllowed)
        {
            //toReturn.Add(0.25f);
            //toReturn.Add(0.3333f);
            //toReturn.Add(0.5f);
            //toReturn.Add(0.6666f);
            //toReturn.Add(0.75f);
        }
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
        if (negativesAllowed && fractionsAllowed)
        {
            //toReturn.Add(-0.7500f);   // -3/4
            //toReturn.Add(-0.6666f);   // -2/3
            //toReturn.Add(-0.5f);    // -1/2     
            //toReturn.Add(-0.3333f);   // -1/3
            //toReturn.Add(-0.25f);   // -1/4
        }
        if (fractionsAllowed)
        {
            //toReturn.Add(0.25f);
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
            } else if (Mathf.Abs(0.6666f - Mathf.Abs(num)) < margin) {
                numerator = 2;
                denominator = 3;
            } else if (Mathf.Abs(0.5f - Mathf.Abs(num)) < margin) {
                numerator = 1;
                denominator = 2;
            } else if (Mathf.Abs(0.3333f - Mathf.Abs(num)) < margin) {
                numerator = 1;
                denominator = 3;
            } else if (Mathf.Abs(0.25f - Mathf.Abs(num)) < margin) {
                numerator = 1;
                denominator = 4;
            }
            if (isNegative) {
                temp = new Circle(-1 * numerator, denominator);
            } else {
                temp = new Circle(numerator, denominator);
            }


        } else {
            temp = new Circle(num);
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
            else if (Mathf.Abs(0.66f - Mathf.Abs(num)) < margin)
            {
                numerator = 2;
                denominator = 3;
            }
            else if (Mathf.Abs(0.5f - Mathf.Abs(num)) < margin)
            {
                numerator = 1;
                denominator = 2;
            }
            else if (Mathf.Abs(0.33f - Mathf.Abs(num)) < margin)
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
            } else {
                temp = new Circle(numerator, denominator);
            }
        } else {
            temp = new Circle(num);
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

    public List<Operator> WhichOperatorsAreEligibleGivenCircle1(Circle newCircle1) {
        // this function will be used when Part A is already created, and we need to create Part B
        // circle 1, in this case, is the Result of Part A
        // circle 1 can be integers within the range of -225 to 225, and can include simple fractions between -3/4 and 3/4

        List<Operator> operatorList = new List<Operator>();
        bool additionOK = false;
        bool subtractionOK = false;
        bool multiplicationOK = false;
        bool divisionOK = false;
        bool exponent2OK = false;
        bool exponent3OK = false;
        bool squareRootOK = false;
        bool cubeRootOK = false;


        // we want the Result to be inside a given range
        //      so, look at each operator and make a list of possible outcomes
        //      if the list for a given operator is length == 0, then we can't include that operator to be randomly selected
        //      while we're doing this:
        //          each operator should come with its own list of Circle2 values that are eligible


        //  start with newCircle1
        //      look at each operator
        //      






        if (newCircle1.value == -225) { 
            // addition    OK, but don't want to add a negative number
            // subtract    OK, but don't want to subtract a positive number
            // multiplic   OK, but only if multiplying with a fraction & we end up with an integer 
            // division    OK
            // expone2 NOT OK, 
        }






        return operatorList;
    }







    public void CreateProblem() {
        int ABC = Random.Range(1, 4);
        char A_B_C = 'x';
        if (ABC == 1) {
            A_B_C = 'A';
        } else if (ABC == 2) {
            A_B_C = 'B';
        } else if (ABC == 3) {
            A_B_C = 'C';
        }
        //Operator operator1 = PickRandomOperator(8, A_B_C);
        Operator operator1 = new Operator("addition", 'A');

        if (operator1.type == "addition" || operator1.type == "subtraction" || operator1.type == "multiplication" || operator1.type == "division") {
            List<float> stuff = CreateListOfPossibleCircleValues_forArithmetic(50.0f, true, true, true);
            Circle inputCircleA = CreateRandomCircle(stuff);
            //Circle inputCircleA = new Circle(1, 3);

            //Debug.Log("for circle 1: possible values: ");
            //for (int i = 0; i < stuff.Count; i++) {
            //    Debug.Log(stuff[i]);
            //}
            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            Circle inputCircleB = CreateRandomSecondCircleThatResultsInInt(operator1, inputCircleA, 216, 50.0f, true, true, true);
            Circle outputCircle = CreateResultCircle(inputCircleA, inputCircleB, operator1);

            Debug.Log("second value: " + inputCircleB.value);
            Debug.Log("result: " + outputCircle.value);

        } else if (operator1.type == "exponent2") {
            List<float> stuff = CreateListOfPossibleCircleValues_forExponent2(225, true, true, true);
            Circle inputCircleA = CreateRandomCircle(stuff);

            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            Circle outputCircle = CreateResultCircle(inputCircleA, inputCircleA, operator1);

            Debug.Log("result: " + outputCircle.value);


        } else if (operator1.type == "exponent3") {
            List<float> stuff = CreateListOfPossibleCircleValues_forExponent3(225, true, true, true);
            Circle inputCircleA = CreateRandomCircle(stuff);

            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            Circle outputCircle = CreateResultCircle(inputCircleA, inputCircleA, operator1);

            Debug.Log("result: " + outputCircle.value);
        } else if (operator1.type == "squareRoot") {
            List<float> stuff = CreateListOfPossibleCircleValues_forSquareRoot(15, true, true, true);
            Circle inputCircleA = CreateRandomCircle(stuff);

            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            Circle outputCircle = CreateResultCircle(inputCircleA, inputCircleA, operator1);

            Debug.Log("result: " + outputCircle.value);



        } else if (operator1.type == "cubeRoot") {
            List<float> stuff = CreateListOfPossibleCircleValues_forCubeRoot(6, true, true, true);
            Circle inputCircleA = CreateRandomCircle(stuff);

            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            Circle outputCircle = CreateResultCircle(inputCircleA, inputCircleA, operator1);

            Debug.Log("result: " + outputCircle.value);
        }

        // for testing purposes, part A of the problem will be ADDITION
        //      the result from part A will be circleA of part B

        // CREATE THE OTHER PART OF THE PROBLEM
        //      unlike above, where we started with an operator, this time we will be starting with a circle
        //      so we need to determine which operators will work with this circle
        //          for example, if circle.value == 0.25, then we know we can't use exponent2 (as it will result in 0.25^2 = 0.125)
        //          for example, if circle.value == 100, then we know we can't use cubeRoot (as it will result in 100^(1/3) = 4.641588....





    }

}
