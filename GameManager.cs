using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string gameType;

    public GameObject MainMenuUI;
    public GameObject StatsUI;
    public GameObject OptionsUI;
    public GameObject LevelUI;
    public GameObject GameOverUI;

    public TextMeshProUGUI MainMenu_BestScore_Timed;
    public TextMeshProUGUI MainMenu_BestScore_Endless;
    public TextMeshProUGUI MainMenu_BestScore_Kiddy;

    public GameObject CirclesParent;
    public GameObject MathInProgress;
    public GameObject OperatorsParent;
    public GameObject GoalParent;

    public GameObject GlobalTimer;
    public GameObject PuzzleTimer;
    public GameObject PuzzleTimerThatMoves;
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

    // for endless & kiddy gameTypes, there is no score. Rather, keep track of number completed, number skipped, number failed
    public int numberCompleted;
    public int numberSkipped;
    public int numberFailed;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        timerGlobal = GlobalTimer.GetComponent<TimerGlobal>();
        timerPuzzle = PuzzleTimerThatMoves.GetComponent<TimerPuzzle>();
        DisplayMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayMainMenu() {
        if (PlayerPrefs.HasKey("HighScore_Timed")) {
            int highScore = PlayerPrefs.GetInt("HighScore_Timed");
            MainMenu_BestScore_Timed.text = "High Score: " + highScore;
        } else {
            MainMenu_BestScore_Timed.text = "";
        }

        if (PlayerPrefs.HasKey("Endless_Tally")) {
            int tally = PlayerPrefs.GetInt("Endless_Tally");
            MainMenu_BestScore_Endless.text = "Total Completed: " + tally;
        } else {
            MainMenu_BestScore_Endless.text = "";
        }

        if (PlayerPrefs.HasKey("Kiddy_Tally")) {
            int tally = PlayerPrefs.GetInt("Kiddy_Tally");
            MainMenu_BestScore_Kiddy.text = "Total Completed: " + tally;
        } else {
            MainMenu_BestScore_Kiddy.text = "";
        }

        MainMenuUI.SetActive(true);
        StatsUI.SetActive(false);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
        GameOverUI.SetActive(false);
    }
    public void DisplayStats() {













        MainMenuUI.SetActive(false);
        StatsUI.SetActive(true);
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
        StatsUI.SetActive(false);
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
        StatsUI.SetActive(false);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(true);
        CirclesParent.SetActive(true);
        MathInProgress.SetActive(true);
        OperatorsParent.SetActive(true);
        GoalParent.SetActive(true);
        GameOverUI.SetActive(false);
    }
    public void StartTimedGame() {
        PuzzleManager.instance.gameType = "timed";
        gameType = "timed";
        ResetScore();
        PuzzleManager.instance.CreateNewPuzzle();
        DisplayLevel();
        timerGlobal.ResetTimeRemaining();
        timerPuzzle.UnpausePuzzleTimer();
        timerGlobal.UnpauseGlobalTimer();
        GlobalTimer.SetActive(true);
        PuzzleTimer.SetActive(true);
        PuzzleTimerThatMoves.SetActive(true);
        SetNumberOfPuzzlesRemaining(20);     // change to 20 later
    }
    public void StartEndlessGame() {
        PuzzleManager.instance.gameType = "endless";
        gameType = "endless";
        ResetScore();
        PuzzleManager.instance.CreateNewPuzzle();
        DisplayLevel();
        //timerGlobal.ResetTimeRemaining();
        //timerPuzzle.UnpausePuzzleTimer();
        //timerGlobal.UnpauseGlobalTimer();
        GlobalTimer.SetActive(false);
        PuzzleTimer.SetActive(false);
        PuzzleTimerThatMoves.SetActive(false);
        //SetNumberOfPuzzlesRemaining(20);     // change to 20 later

        PuzzlesRemainingDisplay.text = "";

    }
    public void StartKiddyGame() {
        PuzzleManager.instance.gameType = "kiddy";
        gameType = "kiddy";
        ResetScore();
        PuzzleManager.instance.CreateNewPuzzle();
        DisplayLevel();
        //timerGlobal.ResetTimeRemaining();
        //timerPuzzle.UnpausePuzzleTimer();
        //timerGlobal.UnpauseGlobalTimer();
        GlobalTimer.SetActive(false);
        PuzzleTimer.SetActive(false);
        PuzzleTimerThatMoves.SetActive(false);
        //SetNumberOfPuzzlesRemaining(20);     // change to 20 later

        PuzzlesRemainingDisplay.text = "";
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

        if (gameType == "timed") {
            if (puzzlesRemaining <= 1)
            {
                DisplayGameOver(false, true);
            }
            else
            {
                float timeRemaining = timerGlobal.GetTimeRemaining();
                float timeToTakeAway = timeRemaining / puzzlesRemaining;
                timerGlobal.SubtractFromGlobalTimer(timeToTakeAway);
                PuzzleManager.instance.CreateNewPuzzle();
            }
        } else if (gameType == "endless" || gameType == "kiddy") {
            IncreaseNumberOfSkipped(1);
            PuzzleManager.instance.CreateNewPuzzle();
        } 



    }
    public void DecreaseNumberOfPuzzlesRemaining(int amount) {
        puzzlesRemaining -= amount;
        PuzzlesRemainingDisplay.text = "Remaining: " + puzzlesRemaining;
        if (puzzlesRemaining == 0) {
            DisplayGameOver(false, false);
        }
    }
    public void SetNumberOfPuzzlesRemaining(int amount) {
        puzzlesRemaining = amount;
        PuzzlesRemainingDisplay.text = "Remaining: " + puzzlesRemaining;
    }
    //  ************************************************************************************************************************************
    public void IncreaseScore(int amount) {
        score += amount;
        ScoreDisplay.text = "Score: " + score;
    }
    public void ResetScore() {
        score = 0;
        if (gameType == "timed") {
            ScoreDisplay.text = "Score: " + score;
        } else {
            numberCompleted = 0;
            numberSkipped = 0;
            numberFailed = 0;
            UpdateCompletedSkippedFailed();
        }

    }
    //  ************************************************************************************************************************************
    public void IncreaseNumberOfCompleted(int amount) {
        numberCompleted += amount;
        if (gameType == "endless") {
            int temp = PlayerPrefs.GetInt("Endless_Tally");
            temp += amount;
            PlayerPrefs.SetInt("Endless_Tally", temp);
        } else if (gameType == "kiddy") {
            int temp = PlayerPrefs.GetInt("Kiddy_Tally");
            temp += amount;
            PlayerPrefs.SetInt("Kiddy_Tally", temp);
        }
        UpdateCompletedSkippedFailed();
    }
    public void IncreaseNumberOfSkipped(int amount) {
        numberSkipped += amount;
        UpdateCompletedSkippedFailed();
    }
    public void IncreaseNumberOfFailed(int amount) {
        numberFailed += amount;
        UpdateCompletedSkippedFailed();
    }
    public void UpdateCompletedSkippedFailed() {
        string type = "wtf";
        if (gameType == "endless") {
            type = "ENDLESS MODE\n";
        } else if (gameType == "kiddy") {
            type = "EASY MODE\n";
        }
        ScoreDisplay.text = type + "Completed: " + numberCompleted + "\nSkipped: " + numberSkipped + "\nFailed: " + numberFailed;
    }
    public void ResetCompletedSkippedFailed() {
        numberCompleted = 0;
        numberSkipped = 0;
        numberFailed = 0;
        UpdateCompletedSkippedFailed();
    }

    //  ************************************************************************************************************************************
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

        if (gameType == "timed")
        {
            if (timeRanOut)
            {
                GameOverText.text = "TIME RAN OUT";
            }
            else
            {
                GameOverText.text = "GAME OVER";
            }

            // do the score calculating here
            float timeRemain;
            if (removeAllSpeedPoints)
            {
                timeRemain = 0.1f;
            }
            else
            {
                timeRemain = timerGlobal.GetTimeRemaining();
            }
            int scoreFromTimeRemaining = (int)(timeRemain / 10);
            TimeRemainingExplanation.text = "Time Remaining: " + (int)timeRemain + " seconds = " + scoreFromTimeRemaining + " speed points";

            int total = score + scoreFromTimeRemaining;

            if (PlayerPrefs.HasKey("HighScore_Timed"))
            {
                if (total > PlayerPrefs.GetInt("HighScore_Timed"))
                {
                    PlayerPrefs.SetInt("HighScore_Timed", total);
                }
                int bestScore = PlayerPrefs.GetInt("HighScore_Timed");
                GameOverScreen_BestScore.text = "All-time Best Score (Timed mode): " + bestScore;
            }
            else
            {
                PlayerPrefs.SetInt("HighScore_Timed", total);
                GameOverScreen_BestScore.text = "All-time Best Score (Timed mode): " + total;
            }

            GameOverScore.text = "Score: " + score + " points + " + scoreFromTimeRemaining + " speed points = <b><color=#b80b0b>" + total + " POINTS";

        }
        else if (gameType == "endless" || gameType == "kiddy")
        {
            GameOverText.text = "GAME OVER";

            TimeRemainingExplanation.text = "Puzzles Completed: " + numberCompleted;

            GameOverScore.text = "Puzzles Skipped: " + numberSkipped;

            GameOverScreen_BestScore.text = "Puzzles Failed: " + numberFailed;

        }

        GameOverUI.SetActive(true);
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void ResetPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
    


}
