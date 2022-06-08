using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject MainMenuUI;
    public GameObject OptionsUI;
    public GameObject LevelUI;
    public GameObject GameOverUI;

    public TextMeshProUGUI MainMenu_BestScore;

    public GameObject CirclesParent;
    public GameObject MathInProgress;
    public GameObject OperatorsParent;
    public GameObject GoalParent;

    public GameObject GlobalTimer;
    public GameObject PuzzleTimer;
    TimerGlobal timerGlobal;
    TimerPuzzle timerPuzzle;

    public int puzzlesRemaining;
    public TextMeshProUGUI PuzzlesRemainingDisplay;

    public int score;
    public TextMeshProUGUI ScoreDisplay;

    public TextMeshProUGUI GameOverText;        // this will say "OUT OF TIME" or "GAME OVER", depending on circumstances
    public TextMeshProUGUI TimeRemainingExplanation;
    public TextMeshProUGUI GameOverScore;
    public TextMeshProUGUI GameOverScreen_BestScore;





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
        if (PlayerPrefs.HasKey("HighScore")) {
            int highScore = PlayerPrefs.GetInt("HighScore");
            MainMenu_BestScore.text = "All-time Best Score: " + highScore;
        } else {
            MainMenu_BestScore.text = "";
        }

        MainMenuUI.SetActive(true);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
        GameOverUI.SetActive(false);
    }
    public void DisplayOptions() {
        MainMenuUI.SetActive(false);
        OptionsUI.SetActive(true);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
        GameOverUI.SetActive(false);
    }
    public void DisplayLevel() {
        MainMenuUI.SetActive(false);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(true);
        CirclesParent.SetActive(true);
        MathInProgress.SetActive(true);
        OperatorsParent.SetActive(true);
        GoalParent.SetActive(true);
        GameOverUI.SetActive(false);
    }
    public void StartGame() {
        ResetScore();
        PuzzleManager.instance.CreateNewPuzzle();
        DisplayLevel();
        timerGlobal.ResetTimeRemaining();
        timerPuzzle.UnpausePuzzleTimer();
        timerGlobal.UnpauseGlobalTimer();
        SetNumberOfPuzzlesRemaining(20);     // change to 20 later
    }
    public void EndGameEarly() {        // tie this to the "Main Menu" button in the level UI
        DisplayMainMenu();
        timerPuzzle.PausePuzzleTimer();
        timerGlobal.PauseGlobalTimer();
        timerGlobal.ResetTimeRemaining();

    }
    public void SkipPuzzle()
    {
        // reduces the amount of time remaining in such a way that the player will always have zero speed points
        //e.g., if there are 10 puzzles left & 100 seconds left, then it reduces by 10 seconds
        //e.g., if there are 2 puzzles left & 50 seconds left, then it reduces by 25 seconds
        //e.g., base case: if there is 1 puzzle left, then it reduces by ALL seconds

        if (puzzlesRemaining <= 1) {
            DisplayGameOver(false, true);
        } else {
            float timeRemaining = timerGlobal.GetTimeRemaining();
            float timeToTakeAway = timeRemaining / puzzlesRemaining;
            timerGlobal.SubtractFromGlobalTimer(timeToTakeAway);
            PuzzleManager.instance.CreateNewPuzzle();
        }

    }
    public void DecreaseNumberOfPuzzlesRemaining(int amount) {
        puzzlesRemaining -= amount;
        PuzzlesRemainingDisplay.text = "Puzzles Remaining: " + puzzlesRemaining;
        if (puzzlesRemaining == 0) {
            DisplayGameOver(false, false);
        }
    }
    public void SetNumberOfPuzzlesRemaining(int amount) {
        puzzlesRemaining = amount;
        PuzzlesRemainingDisplay.text = "Puzzles Remaining: " + puzzlesRemaining;
    }
    public void IncreaseScore(int amount) {
        score += amount;
        ScoreDisplay.text = "Score: " + score;
    }
    public void ResetScore() {
        score = 0;
        ScoreDisplay.text = "Score: " + score;
    }
    public void DisplayGameOverForButtonPress() {  // this exists so i can press the "quit" button on the levelUI
        DisplayGameOver(false, true); 
    }
    public void DisplayGameOver(bool timeRanOut, bool removeAllSpeedPoints) {
        MainMenuUI.SetActive(false);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);

        if (timeRanOut) {
            GameOverText.text = "TIME RAN OUT";
        } else {
            GameOverText.text = "GAME OVER";
        }


        // do the score calculating here
        float timeRemain;
        if (removeAllSpeedPoints) {
            timeRemain = 0.1f;
        } else {
            timeRemain = timerGlobal.GetTimeRemaining();
        }
        int scoreFromTimeRemaining = (int)(timeRemain / 10);
        TimeRemainingExplanation.text = "Time Remaining: " + (int)timeRemain + " seconds = " + scoreFromTimeRemaining + " speed points";

        int total = score + scoreFromTimeRemaining;

        if (PlayerPrefs.HasKey("HighScore")) {
            if (total > PlayerPrefs.GetInt("HighScore")) {
                PlayerPrefs.SetInt("HighScore", total);
            }
            int bestScore = PlayerPrefs.GetInt("HighScore");
            GameOverScreen_BestScore.text = "All-time Best Score: " + bestScore;
        } else {
            PlayerPrefs.SetInt("HighScore", total);
            GameOverScreen_BestScore.text = "All-time Best Score: " + total;
        }

        GameOverScore.text = "Score: " + score + " points + " + scoreFromTimeRemaining + " speed points = <b><color=#b80b0b>" + total + " POINTS";
        GameOverUI.SetActive(true);
    }
    public void QuitGame() {
        Application.Quit();
    }



}
