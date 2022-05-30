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

    public class OpsAndCircles {
        //Vector2 position;
    }
    public class Operator : OpsAndCircles {
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
    public class Circle : OpsAndCircles {
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

    public List<OpsAndCircles> CreatePartB_GivenInitial(Circle newCircle1) {

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

        List<float> circle2ValuesToConsider_Add_Subtract = CreateListOfPossibleCircleValues_forArithmetic(20f, true, true, true);
        List<float> circle2ValuesToConsider_Mult_Divide = CreateListOfPossibleCircleValues_forArithmetic(20f, false, true, true);

        void ConsiderAddition(Circle circle1) {
            float value = circle1.value;

            for (int i = 0; i < circle2ValuesToConsider_Add_Subtract.Count; i++) {
                float result = value + circle2ValuesToConsider_Add_Subtract[i];
                if (result >= -225 && result <= 225 && TestIfIsInteger(result)) {
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
                if (result >= -225 && result <= 225 && TestIfIsInteger(result)) {
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
                if (result >= -125 && result <= 125 && TestIfIsInteger(result))
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
                if (result >= -125 && result <= 125 && TestIfIsInteger(result))
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
            ConsiderAddition(circle1);
            ConsiderSubtraction(circle1);
            ConsiderMultiplication(circle1);
            ConsiderDivision(circle1);
            ConsiderExponent2(circle1);
            ConsiderExponent3(circle1);
            ConsiderSquareRoot(circle1);
            ConsiderCubeRoot(circle1);
        }
        ConsiderAllOperators(newCircle1);

        // select the operator from operatorList, and add it to toReturn
        string operatorToUse = "";
        int rando = Random.Range(0, operatorList.Count);
        operatorToUse = operatorList[rando];
        Operator newOperator = new Operator(operatorToUse, 'B');


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

    public List<OpsAndCircles> CreatePartA_GivenInitial(Circle potentialResult1, Circle potentialResult2)
    {
        Debug.Log("about to start building PartA, given PartB");

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

        List<float> circle2ValuesToConsider_Add_Subtract = CreateListOfPossibleCircleValues_forArithmetic(20f, true, true, true);
        List<float> circle2ValuesToConsider_Mult_Divide = CreateListOfPossibleCircleValues_forArithmetic(20f, false, true, true);

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
            
            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(20f, true, true, true);

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
                Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                circle1ValuesToConsider.Remove(potentialCircle1Value);
                // step 2: see if it can work, to get us the predetermined result
                //      so we're considering addition... meaning we'll have: a + b = c, & we know a & c, therefore b = c - a
                float potentialCircle2Value = resultValue - potentialCircle1Value;
                // step 3: is this bValue acceptable? i.e., we don't want 5.123183943, or 2^0.5, or something weird. it has to be in the list
                if (circle2ValuesToConsider_Add_Subtract.Contains(potentialCircle2Value))
                {
                    Debug.Log("addition is viable: " + potentialCircle1Value + " as circle1 and " + potentialCircle2Value + " as circle2 gets us result of " + resultValue);
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
                    Debug.Log("this won't work: " + potentialCircle1Value + " + " + potentialCircle2Value + " = " + resultValue);
                    if (circle1ValuesToConsider.Count == 0)
                    {
                        Debug.Log("*****************************addition will not work... we've tried every option");
                        doneWithLoop = true;
                    }
                }
            }

        }
        void ConsiderSubtraction(float resultValue, char ABC)
        {
            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(20f, true, true, true);

            bool doneWithLoop = false;
            while (doneWithLoop == false)
            {
                // step 1: pick a random circle1 value, within the acceptable range of -20 to 20
                int rando = Random.Range(0, circle1ValuesToConsider.Count);
                float potentialCircle1Value = circle1ValuesToConsider[rando];
                Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                circle1ValuesToConsider.Remove(potentialCircle1Value);
                // step 2: see if it can work, to get us the predetermined result
                //      so we're considering subtraction... meaning we'll have: a - b = c, & we know a & c, therefore b = a - c
                float potentialCircle2Value = potentialCircle1Value - resultValue;
                // step 3: is this bValue acceptable? i.e., we don't want 5.123183943, or 2^0.5, or something weird. it has to be in the list
                if (circle2ValuesToConsider_Add_Subtract.Contains(potentialCircle2Value))
                {
                    Debug.Log("subtraction is viable: " + potentialCircle1Value + " as circle1 and " + potentialCircle2Value + " as circle2 gets us result of " + resultValue);
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
                    Debug.Log("this won't work: " + potentialCircle1Value + " - " + potentialCircle2Value + " = " + resultValue); 
                    if (circle1ValuesToConsider.Count == 0)
                    {
                        Debug.Log("*****************************subtraction will not work... we've tried every option");
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

            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(20f, false, true, true);

            bool doneWithLoop = false;
            while (doneWithLoop == false)
            {
                // step 1: pick a random circle1 value, within the acceptable range of -20 to 20
                int rando = Random.Range(0, circle1ValuesToConsider.Count);
                float potentialCircle1Value = circle1ValuesToConsider[rando];
                Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                circle1ValuesToConsider.Remove(potentialCircle1Value);
                // step 2: see if it can work, to get us the predetermined result
                //      so we're considering multiplication... meaning we'll have: a * b = c, & we know a & c, therefore b = c / a
                float potentialCircle2Value = resultValue / potentialCircle1Value;
                // step 3: is this bValue acceptable? i.e., we don't want 5.123183943, or 2^0.5, or something weird. it has to be in the list
                if (circle2ValuesToConsider_Mult_Divide.Contains(potentialCircle2Value))
                {
                    Debug.Log("multiplication is viable: " + potentialCircle1Value + " as circle1 and " + potentialCircle2Value + " as circle2 gets us result of " + resultValue);
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
                    Debug.Log("this won't work: " + potentialCircle1Value + " * " + potentialCircle2Value + " = " + resultValue); 
                    if (circle1ValuesToConsider.Count == 0)
                    {
                        Debug.Log("*****************************multiplication will not work... we've tried every option");
                        doneWithLoop = true;
                    }
                }
            }
        }
        void ConsiderDivision(float resultValue, char ABC)
        {
            List<float> circle1ValuesToConsider = CreateListOfPossibleCircleValues_forArithmetic(20f, false, true, true);

            bool doneWithLoop = false;
            while (doneWithLoop == false)
            {
                // step 1: pick a random circle1 value, within the acceptable range of -20 to 20
                int rando = Random.Range(0, circle1ValuesToConsider.Count);
                float potentialCircle1Value = circle1ValuesToConsider[rando];
                Debug.Log(" ... length of circle1ValuesToConsider: " + circle1ValuesToConsider.Count);
                circle1ValuesToConsider.Remove(potentialCircle1Value);
                // step 2: see if it can work, to get us the predetermined result
                //      so we're considering division... meaning we'll have: a / b = c, & we know a & c, therefore b = a / c
                float potentialCircle2Value = potentialCircle1Value / resultValue;
                // step 3: is this bValue acceptable? i.e., we don't want 5.123183943, or 2^0.5, or something weird. it has to be in the list
                if (circle2ValuesToConsider_Mult_Divide.Contains(potentialCircle2Value))
                {
                    Debug.Log("division is viable: " + potentialCircle1Value + " as circle1 and " + potentialCircle2Value + " as circle2 gets us result of " + resultValue);
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
                    Debug.Log("this won't work: " + potentialCircle1Value + " / " + potentialCircle2Value + " = " + resultValue);
                    if (circle1ValuesToConsider.Count == 0)
                    {
                        Debug.Log("*****************************division will not work... we've tried every option");
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
                Debug.Log("exponent2 is viable: " + exponent2_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                if (ABC == 'A') {
                    operatorList_1.Add("exponent2");
                } else if (ABC == 'B') {
                    operatorList_2.Add("exponent2");
                } else if (ABC == 'C') {

                }
            }
            else
            {
                Debug.Log("the resultValue isn't on the pre-approved list, so exponent2 can be ignored");
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
                Debug.Log("exponent3 is viable: " + exponent3_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                if (ABC == 'A') {
                    operatorList_1.Add("exponent3");
                } else if (ABC == 'B') {
                    operatorList_2.Add("exponent3");
                } else if (ABC == 'C') {

                }
            }
            else
            {
                Debug.Log("the resultValue isn't on the pre-approved list, so exponent3 can be ignored");
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
                    Debug.Log("squareRoot is viable: " + squareRoot_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                    operatorList_1.Add("squareRoot");
                }
                else if (ABC == 'B')
                {
                    squareRoot_circle1ValueToUse_2 = Mathf.Pow(resultValue, 2);
                    Debug.Log("squareRoot is viable: " + squareRoot_circle1ValueToUse_2 + " as circle1 gives us result of " + resultValue);
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
                Debug.Log("the resultValue isn't on the pre-approved list, so squareRoot can be ignored");
            }
        }
        void ConsiderCubeRoot(float resultValue, char ABC)
        {
            List<float> usableResults = new List<float> { -5, -4, -3, -2, 2, 3, 4, 5 };

            if (usableResults.Contains(resultValue))
            {
                if (ABC == 'A') {
                    cubeRoot_circle1ValueToUse_1 = Mathf.Pow(resultValue, 3);
                    Debug.Log("cubeRoot is viable: " + cubeRoot_circle1ValueToUse_1 + " as circle1 gives us result of " + resultValue);
                    operatorList_1.Add("cubeRoot");
                } else if (ABC == 'B') {
                    cubeRoot_circle1ValueToUse_2 = Mathf.Pow(resultValue, 3);
                    Debug.Log("cubeRoot is viable: " + cubeRoot_circle1ValueToUse_2 + " as circle1 gives us result of " + resultValue);
                    operatorList_2.Add("cubeRoot");
                } else if (ABC == 'C') {

                }
            }
            else
            {
                Debug.Log("the resultValue isn't on the pre-approved list, so cubeRoot can be ignored");
            }
        }

        void ConsiderAllOperators_A() {

            float value = potentialResult1.value;

            if (Mathf.Abs(value) <= 40) {
                ConsiderAddition(value, 'A');
                ConsiderSubtraction(value, 'A');
            } else {
                Debug.Log(value + " is too big, so we're skipping addition & subtraction");
            }
            // if valueOfResultA is a PRIME NUMBER, then we don't want to consider multiplication or division, because *1 and /1 are boring
            if (!IsThisNumberPrime(value)) {
                ConsiderMultiplication(value, 'A');
                ConsiderDivision(value, 'A');
            } else {
                Debug.Log(value + " is a prime number, so we're skipping multiplication & division");
            }
            ConsiderExponent2(value, 'A');
            ConsiderExponent3(value, 'A');
            ConsiderSquareRoot(value, 'A');
            ConsiderCubeRoot(value, 'A');
        }
        void ConsiderAllOperators_B() {

            float value = potentialResult2.value;

            if (Mathf.Abs(value) <= 40) {
                ConsiderAddition(value, 'B');
                ConsiderSubtraction(value, 'B');
            } else {
                Debug.Log(value + " is too big, so we're skipping addition & subtraction");
            }
            // if valueOfResultA is a PRIME NUMBER, then we don't want to consider multiplication or division, because *1 and /1 are boring
            if (!IsThisNumberPrime(value)) {
                ConsiderMultiplication(value, 'B');
                ConsiderDivision(value, 'B');
            } else {
                Debug.Log(value + " is a prime number, so we're skipping multiplication & division");
            }
            ConsiderExponent2(value, 'B');
            ConsiderExponent3(value, 'B');
            ConsiderSquareRoot(value, 'B');
            ConsiderCubeRoot(value, 'B');
        }

        ConsiderAllOperators_A();
        if (potentialResult2 != null) {
            ConsiderAllOperators_B();
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
                circle1Value = addition_circle1ValueToUse_1;
                circle2Value = addition_circle2ValueToUse_1;
                resultPartA = circle1Value + circle2Value;
            }
            else if (operatorWeUse == "subtraction")
            {
                partA_operator = new Operator("subtraction", 'A');
                circle1Value = subtraction_circle1ValueToUse_1;
                circle2Value = subtraction_circle2ValueToUse_1;
                resultPartA = circle1Value - circle2Value;
            }
            else if (operatorWeUse == "multiplication")
            {
                partA_operator = new Operator("multiplication", 'A');
                circle1Value = multiplication_circle1ValueToUse_1;
                circle2Value = multiplication_circle2ValueToUse_1;
                resultPartA = circle1Value * circle2Value;
            }
            else if (operatorWeUse == "division")
            {
                partA_operator = new Operator("division", 'A');
                circle1Value = division_circle1ValueToUse_1;
                circle2Value = division_circle2ValueToUse_1;
                resultPartA = circle1Value / circle2Value;
            }
            else if (operatorWeUse == "exponent2")
            {
                partA_operator = new Operator("exponent2", 'A');
                circle1Value = exponent2_circle1ValueToUse_1;
                resultPartA = Mathf.Pow(circle1Value, 2);
            }
            else if (operatorWeUse == "exponent3")
            {
                partA_operator = new Operator("exponent3", 'A');
                circle1Value = exponent3_circle1ValueToUse_1;
                resultPartA = Mathf.Pow(circle1Value, 3);
            }
            else if (operatorWeUse == "squareRoot")
            {
                partA_operator = new Operator("squareRoot", 'A');
                circle1Value = squareRoot_circle1ValueToUse_1;
                resultPartA = Mathf.Pow(circle1Value, 0.5f);
            }
            else if (operatorWeUse == "cubeRoot")
            {
                partA_operator = new Operator("cubeRoot", 'A');
                circle1Value = cubeRoot_circle1ValueToUse_1;
                resultPartA = Mathf.Pow(circle1Value, 1f / 3f);
            }

            partA_circle1 = CreateSpecificCircle(circle1Value);
            partA_circle2 = CreateSpecificCircle(circle2Value);
            partA_result = CreateSpecificCircle(resultPartA);

            Debug.Log("for PartA, circle1.value is: " + circle1Value);
            Debug.Log("for PartA, circle2.value is: " + circle2Value);
            Debug.Log("for PartA, using operator: " + operatorWeUse);
            Debug.Log("for PartA, result is: " + resultPartA);
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
                circle1Value = addition_circle1ValueToUse_2;
                circle2Value = addition_circle2ValueToUse_2;
                resultPartA = circle1Value + circle2Value;
            }
            else if (operatorWeUse == "subtraction")
            {
                partA_operator = new Operator("subtraction", 'A');
                circle1Value = subtraction_circle1ValueToUse_2;
                circle2Value = subtraction_circle2ValueToUse_2;
                resultPartA = circle1Value - circle2Value;
            }
            else if (operatorWeUse == "multiplication")
            {
                partA_operator = new Operator("multiplication", 'A');
                circle1Value = multiplication_circle1ValueToUse_2;
                circle2Value = multiplication_circle2ValueToUse_2;
                resultPartA = circle1Value * circle2Value;
            }
            else if (operatorWeUse == "division")
            {
                partA_operator = new Operator("division", 'A');
                circle1Value = division_circle1ValueToUse_2;
                circle2Value = division_circle2ValueToUse_2;
                resultPartA = circle1Value / circle2Value;
            }
            else if (operatorWeUse == "exponent2")
            {
                partA_operator = new Operator("exponent2", 'A');
                circle1Value = exponent2_circle1ValueToUse_2;
                resultPartA = Mathf.Pow(circle1Value, 2);
            }
            else if (operatorWeUse == "exponent3")
            {
                partA_operator = new Operator("exponent3", 'A');
                circle1Value = exponent3_circle1ValueToUse_2;
                resultPartA = Mathf.Pow(circle1Value, 3);
            }
            else if (operatorWeUse == "squareRoot")
            {
                partA_operator = new Operator("squareRoot", 'A');
                circle1Value = squareRoot_circle1ValueToUse_2;
                resultPartA = Mathf.Pow(circle1Value, 0.5f);
            }
            else if (operatorWeUse == "cubeRoot")
            {
                partA_operator = new Operator("cubeRoot", 'A');
                circle1Value = cubeRoot_circle1ValueToUse_2;
                resultPartA = Mathf.Pow(circle1Value, 1f / 3f);
            }

            partA_circle1 = CreateSpecificCircle(circle1Value);
            partA_circle2 = CreateSpecificCircle(circle2Value);
            partA_result = CreateSpecificCircle(resultPartA);

            Debug.Log("for PartA, circle1.value is: " + circle1Value);
            Debug.Log("for PartA, circle2.value is: " + circle2Value);
            Debug.Log("for PartA, using operator: " + operatorWeUse);
            Debug.Log("for PartA, result is: " + resultPartA);
        }

        Debug.Log("operatorList_A contains " + operatorList_1.Count + " possible operators");
        Debug.Log("operatorList_B contains " + operatorList_2.Count + " possible operators");

        // compare the two operatorLists and see which one has the most options

        if (operatorList_1.Count > operatorList_2.Count) {
            Debug.Log("going with ResultA");
            GoWithResult_1();
        } else if (operatorList_1.Count < operatorList_2.Count) {
            Debug.Log("going with ResultB");
            GoWithResult_2();
        } else {
            int rando = Random.Range(1, 3);
            if (rando == 1) {
                Debug.Log("going with ResultA");
                GoWithResult_1();
            } else {
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

    public void CreateProblem() 
    {
        int ABC = Random.Range(1, 3);   // this will determine whether we're starting by creating PartA, or starting by creating PartB
        char A_B_C = 'x';
        if (ABC == 1) {
            A_B_C = 'A';
            Debug.Log("we start by creating PartA");
        } else if (ABC == 2) {
            A_B_C = 'B';
            Debug.Log("we start by creating PartB");
        } else if (ABC == 3) {
            A_B_C = 'C';
            Debug.Log("we start by creating PartC");        // this isn't currently supported
        }
        Operator oppy = PickRandomOperator(8, A_B_C);
        //Operator operator1 = new Operator("addition", 'A');
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
        Debug.Log("first value: " + inputCircleA.value);
        Debug.Log("operator: " + oppy.type);
        if (inputCircleB != null) {
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
        Circle circle4 = null;
        Circle circle5 = null;
        Operator operator1 = null;
        Operator operator2 = null;
        Operator operator3 = null;
        Circle result = null;

        // depending on what A_B_C is, will affect which of the following we choose:
        if (A_B_C == 'A') {
            // we already created PartA, so now we create PartB
            List<OpsAndCircles> PartBStuff = CreatePartB_GivenInitial(outputCircle);
            circle1 = inputCircleA;
            circle2 = inputCircleB; // might be null
            circle3 = (Circle)PartBStuff[1]; // might be null
            operator1 = oppy;
            operator2 = (Operator)PartBStuff[0];
            result = (Circle)PartBStuff[2];
        } else if (A_B_C == 'B') {
            // we already created PartB, so now we create PartA
            List<OpsAndCircles> PartAStuff = CreatePartA_GivenInitial(inputCircleA, inputCircleB);
            circle1 = (Circle)PartAStuff[1];
            circle2 = (Circle)PartAStuff[2]; // might be null
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

        Debug.Log("circle1: " + circle1.value);
        if (circle2 != null) {
            Debug.Log("circle2: " + circle2.value);
        }
        if (circle3 != null) {
            Debug.Log("circle3: " + circle3.value);
        }
        Debug.Log("operator1: " + operator1.type);
        Debug.Log("operator2: " + operator2.type);
        Debug.Log("final result: " + result.value);





    }
    
    

}
