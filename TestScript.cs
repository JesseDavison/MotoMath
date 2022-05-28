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

    public void CreatePartB_GivenInitial(Circle newCircle1) {

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
                if (result > -225 && result < 225 && TestIfIsInteger(result)) {
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
                if (result > -225 && result < 225 && TestIfIsInteger(result)) {
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
                if (result > -125 && result < 125 && TestIfIsInteger(result))
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
                if (result > -125 && result < 125 && TestIfIsInteger(result))
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

        Circle newCircle2 = null;
        if (operatorToUse == "addition" || operatorToUse == "subtraction" || operatorToUse == "multiplication" || operatorToUse == "division") {
            newCircle2 = CreateSpecificCircle(value);
            Debug.Log("PartB newCircle2 value: " + newCircle2.value);
        }

        Debug.Log("PartB operatorToUse: " + newOperator.type);

        // obtain result
        Circle partBresult = CreateResultCircle(newCircle1, newCircle2, newOperator);
        Debug.Log("PartB result: " + partBresult.value);





        //return toReturn;
    }

    public void CreatePartA_GivenInitial(Circle potentialResultA, Circle potentialResultB)
    {
        Debug.Log("about to start building PartA, given PartB");

        // which of these two circles will we choose as the Result of PartA, which is what we're about to create

        // we want to pick the one that gives us the most options

        // let's look at potentialResultA
        //      which operators can give us this as a result?

        // GIVEN THAT WE KNOW the operator and the Result:
        //      go thru all possible circle1 values, and see if any of them can get us to Result


        List<string> operatorList = new List<string>();

        float addition_circle1ValueToUse = 0;
        float addition_circle2ValueToUse = 0;
        float subtraction_circle1ValueToUse = 0;
        float subtraction_circle2ValueToUse = 0;
        float multiplication_circle1ValueToUse = 0;
        float multiplication_circle2ValueToUse = 0;
        float division_circle1ValueToUse = 0;
        float division_circle2ValueToUse = 0;
        float exponent2_circle1ValueToUse = 0;
        float exponent3_circle1ValueToUse = 0;
        float squareRoot_circle1ValueToUse = 0;
        float cubeRoot_circle1ValueToUse = 0;

        List<float> circle2ValuesToConsider_Add_Subtract = CreateListOfPossibleCircleValues_forArithmetic(20f, true, true, true);
        List<float> circle2ValuesToConsider_Mult_Divide = CreateListOfPossibleCircleValues_forArithmetic(20f, false, true, true);

        void ConsiderAddition(float resultValue)
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
                    operatorList.Add("addition");
                    addition_circle1ValueToUse = potentialCircle1Value;
                    addition_circle2ValueToUse = potentialCircle2Value;
                    // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                    doneWithLoop = true;
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
        void ConsiderSubtraction(float resultValue)
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
                    operatorList.Add("subtraction");
                    subtraction_circle1ValueToUse = potentialCircle1Value;
                    subtraction_circle2ValueToUse = potentialCircle2Value;
                    // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                    doneWithLoop = true;
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
        void ConsiderMultiplication(float resultValue)
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
                    operatorList.Add("multiplication");
                    multiplication_circle1ValueToUse = potentialCircle1Value;
                    multiplication_circle2ValueToUse = potentialCircle2Value;
                    // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                    doneWithLoop = true;
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
        void ConsiderDivision(float resultValue)
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
                    operatorList.Add("division");
                    division_circle1ValueToUse = potentialCircle1Value;
                    division_circle2ValueToUse = potentialCircle2Value;
                    // so... if we do end up using addition, now we know the circle1 & circle2 values we will use
                    doneWithLoop = true;
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
        void ConsiderExponent2(float resultValue)
        {
            List<float> usableResults = new List<float> { 0.25f, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121 };

            if (usableResults.Contains(resultValue))
            {
                // randomly assign circle1 to be either negative or positive
                int rando = Random.Range(1, 3);
                if (rando == 1) {
                    exponent2_circle1ValueToUse = Mathf.Pow(resultValue, 0.5f);
                } else {
                    exponent2_circle1ValueToUse = -Mathf.Pow(resultValue, 0.5f);
                }
                Debug.Log("exponent2 is viable: " + exponent2_circle1ValueToUse + " as circle1 gives us result of " + resultValue);
                operatorList.Add("exponent2");
            }
            else
            {
                Debug.Log("the resultValue isn't on the pre-approved list, so exponent2 can be ignored");
            }
        }
        void ConsiderExponent3(float resultValue)
        {
            List<float> usableResults = new List<float> { -125, -64, -27, -8, 8, 27, 64, 125 };

            if (usableResults.Contains(resultValue))
            {
                if (resultValue < 0) {
                    exponent3_circle1ValueToUse = -Mathf.Pow(-resultValue, 1f / 3f);
                } else {
                    exponent3_circle1ValueToUse = Mathf.Pow(resultValue, 1f / 3f);
                }
                Debug.Log("exponent3 is viable: " + exponent3_circle1ValueToUse + " as circle1 gives us result of " + resultValue);
                operatorList.Add("exponent3");
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
        void ConsiderSquareRoot(float resultValue) {
            List<float> usableResults = new List<float> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            if (usableResults.Contains(resultValue))
            {
                squareRoot_circle1ValueToUse = Mathf.Pow(resultValue, 2);
                Debug.Log("squareRoot is viable: " + squareRoot_circle1ValueToUse + " as circle1 gives us result of " + resultValue);
                operatorList.Add("squareRoot");

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
        void ConsiderCubeRoot(float resultValue)
        {
            List<float> usableResults = new List<float> { -5, -4, -3, -2, 2, 3, 4, 5 };

            if (usableResults.Contains(resultValue))
            {
                cubeRoot_circle1ValueToUse = Mathf.Pow(resultValue, 3);
                Debug.Log("cubeRoot is viable: " + cubeRoot_circle1ValueToUse + " as circle1 gives us result of " + resultValue);
                operatorList.Add("cubeRoot");
            }
            else
            {
                Debug.Log("the resultValue isn't on the pre-approved list, so cubeRoot can be ignored");
            }
        }
        void ConsiderAllOperators() {

            float valueOfResultA = potentialResultA.value;

            if (Mathf.Abs(valueOfResultA) <= 40) {
                ConsiderAddition(valueOfResultA);
                ConsiderSubtraction(valueOfResultA);
            } else {
                Debug.Log(valueOfResultA + " is too big, so we're skipping addition & subtraction");
            }
            // if valueOfResultA is a PRIME NUMBER, then we don't want to consider multiplication or division, because *1 and /1 are boring
            if (!IsThisNumberPrime(valueOfResultA)) {
                ConsiderMultiplication(valueOfResultA);
                ConsiderDivision(valueOfResultA);
            } else {
                Debug.Log(valueOfResultA + " is a prime number, so we're skipping multiplication & division");
            }
            ConsiderExponent2(valueOfResultA);
            ConsiderExponent3(valueOfResultA);
            ConsiderSquareRoot(valueOfResultA);
            ConsiderCubeRoot(valueOfResultA);
        }

        ConsiderAllOperators();

        Debug.Log("operatorList contains " + operatorList.Count + " possible operators");

        // now we randomly select one of the operators, and bingo we're done
        int rando = Random.Range(0, operatorList.Count);
        string operatorWeUse = operatorList[rando];

        float circle1Value = 0;
        float circle2Value = 0;
        float resultPartA = 0;
        if (operatorWeUse == "addition") {
            circle1Value = addition_circle1ValueToUse;
            circle2Value = addition_circle2ValueToUse;
            resultPartA = circle1Value + circle2Value;
        } else if (operatorWeUse == "subtraction") {
            circle1Value = subtraction_circle1ValueToUse;
            circle2Value = subtraction_circle2ValueToUse;
            resultPartA = circle1Value - circle2Value;
        } else if (operatorWeUse == "multiplication") {
            circle1Value = multiplication_circle1ValueToUse;
            circle2Value = multiplication_circle2ValueToUse;
            resultPartA = circle1Value * circle2Value;
        } else if (operatorWeUse == "division") {
            circle1Value = division_circle1ValueToUse;
            circle2Value = division_circle2ValueToUse;
            resultPartA = circle1Value / circle2Value;
        } else if (operatorWeUse == "exponent2") {
            circle1Value = exponent2_circle1ValueToUse;
            resultPartA = Mathf.Pow(circle1Value, 2);
        } else if (operatorWeUse == "exponent3") {
            circle1Value = exponent3_circle1ValueToUse;
            resultPartA = Mathf.Pow(circle1Value, 3);
        } else if (operatorWeUse == "squareRoot") {
            circle1Value = squareRoot_circle1ValueToUse;
            resultPartA = Mathf.Pow(circle1Value, 0.5f);
        } else if (operatorWeUse == "cubeRoot") {
            circle1Value = cubeRoot_circle1ValueToUse;
            resultPartA = Mathf.Pow(circle1Value, 1f / 3f);
        }

        Debug.Log("for PartA, circle1.value is: " + circle1Value);
        Debug.Log("for PartA, circle2.value is: " + circle2Value);
        Debug.Log("for PartA, using operator: " + operatorWeUse);
        Debug.Log("for PartA, result is: " + resultPartA);


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
        Operator operator1 = PickRandomOperator(8, A_B_C);
        //Operator operator1 = new Operator("addition", 'A');
        Circle inputCircleA = null;
        Circle inputCircleB = null;
        Circle outputCircle = null;



        if (operator1.type == "addition" || operator1.type == "subtraction" || operator1.type == "multiplication" || operator1.type == "division") {
            List<float> stuff = CreateListOfPossibleCircleValues_forArithmetic(20.0f, true, true, true);
            inputCircleA = CreateRandomCircle(stuff);
            //Circle inputCircleA = new Circle(1, 3);

            //Debug.Log("for circle 1: possible values: ");
            //for (int i = 0; i < stuff.Count; i++) {
            //    Debug.Log(stuff[i]);
            //}
            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            inputCircleB = CreateRandomSecondCircleThatResultsInInt(operator1, inputCircleA, 121, 20.0f, true, true, true);
            outputCircle = CreateResultCircle(inputCircleA, inputCircleB, operator1);

            Debug.Log("second value: " + inputCircleB.value);
            Debug.Log("result: " + outputCircle.value);

        } else if (operator1.type == "exponent2") {
            List<float> stuff = CreateListOfPossibleCircleValues_forExponent2(121, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);

            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, operator1);

            Debug.Log("result: " + outputCircle.value);

        } else if (operator1.type == "exponent3") {
            List<float> stuff = CreateListOfPossibleCircleValues_forExponent3(125, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);

            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, operator1);

            Debug.Log("result: " + outputCircle.value);

        } else if (operator1.type == "squareRoot") {
            List<float> stuff = CreateListOfPossibleCircleValues_forSquareRoot(11, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);

            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, operator1);

            Debug.Log("result: " + outputCircle.value);

        } else if (operator1.type == "cubeRoot") {
            List<float> stuff = CreateListOfPossibleCircleValues_forCubeRoot(5, false, true, true);
            inputCircleA = CreateRandomCircle(stuff);

            Debug.Log("first value: " + inputCircleA.value);
            Debug.Log("operator: " + operator1.type);

            outputCircle = CreateResultCircle(inputCircleA, inputCircleA, operator1);

            Debug.Log("result: " + outputCircle.value);

        }


        // CREATE THE OTHER PART OF THE PROBLEM
        //      unlike above, where we started with an operator, this time we will be starting with a circle
        //      so we need to determine which operators will work with this circle
        //          for example, if circle.value == 0.25, then we know we can't use exponent2 (as it will result in 0.25^2 = 0.125)
        //          for example, if circle.value == 100, then we know we can't use cubeRoot (as it will result in 100^(1/3) = 4.641588....


        // depending on what A_B_C is, will affect which of the following we choose:
        if (A_B_C == 'A') {
            // we already created PartA, so now we create PartB
            CreatePartB_GivenInitial(outputCircle);
        } else if (A_B_C == 'B') {
            // we already created PartB, so now we create PartA
            CreatePartA_GivenInitial(inputCircleA, inputCircleB);
        }




    }
    
    

}
