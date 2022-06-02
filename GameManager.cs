using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject MainMenuUI;
    public GameObject LevelUI;
    
    public GameObject CirclesParent;
    public GameObject MathInProgress;
    public GameObject OperatorsParent;
    public GameObject GoalParent;

    // performing math operations
    int clickTally = 0;
    float value_1;
    float value_2;


    public GameObject GlobalTimer;
    public GameObject PuzzleTimer;
    TimerGlobal timerGlobal;
    TimerPuzzle timerPuzzle;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        timerGlobal = GlobalTimer.GetComponent<TimerGlobal>();
        timerPuzzle = PuzzleTimer.GetComponent<TimerPuzzle>();
        DisplayMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayMainMenu() {
        MainMenuUI.SetActive(true);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
    }
    public void DisplayLevel() {
        MainMenuUI.SetActive(false);
        LevelUI.SetActive(true);
        CirclesParent.SetActive(true);
        MathInProgress.SetActive(true);
        OperatorsParent.SetActive(true);
        GoalParent.SetActive(true);
    }
    public void StartGame() {
        PuzzleManager.instance.CreateNewPuzzle();
        DisplayLevel();
        timerPuzzle.UnpausePuzzleTimer();
        timerGlobal.UnpauseGlobalTimer();
    }
    public void EndGameEarly() {        // tie this to the "Main Menu" button in the level UI
        DisplayMainMenu();
        timerPuzzle.PausePuzzleTimer();
        timerGlobal.PauseGlobalTimer();
        timerGlobal.ResetTimeRemaining();

    }



    // need to enforce(?) certain click order

    // IF THE OPERATOR IS ADD/SUBT/MULT/DIV, then the click order is: circle, operator, circle

    // IF THE OPERATOR IS ROOT/EXPONENT, then the click order is: circle, operator

    // the first thing clicked must be a Circle
    // the second thing clicked must be an Operator
    //      depending on the operator, the next thing clicked is either a circle to complete the math, or a circle to start a new operator

    public void SendCircleToGameManager(float value) { 

    }
    public void SendOperatorToGameManager(string type) { 

    }



//        if (clickTally == 0) { 
//            if (gameObject.CompareTag("circle")) {
//                value_1 = valueOfThisCircle;
//            }
//clickTally += 1;
//        }

    public void ResetClickTally() {
        clickTally = 0;        
    }


}
