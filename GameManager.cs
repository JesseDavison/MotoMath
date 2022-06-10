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

    // STATS SCREEN stuff & PlayerPrefs strings
    public TextMeshProUGUI stats_TimedMode;
    public TextMeshProUGUI stats_EndlessMode;
    public TextMeshProUGUI stats_EasyMode;
    string stat_endless_addition = "stat_endless_addition";
    string stat_endless_subtraction = "stat_endless_subtraction";
    string stat_endless_multiplication = "stat_endless_multiplication";
    string stat_endless_division = "stat_endless_division";
    string stat_endless_exponent2 = "stat_endless_exponent2";
    string stat_endless_exponent3 = "stat_endless_exponent3";
    string stat_endless_squareRoot = "stat_endless_squareRoot";
    string stat_endless_cubeRoot = "stat_endless_cubeRoot";
    string stat_endless_solved = "stat_endless_solved";
    string stat_endless_skipped = "stat_endless_skipped";
    string stat_endless_failed = "stat_endless_failed";

    string stat_easy_addition = "stat_easy_addition";
    string stat_easy_subtraction = "stat_easy_subtraction";
    string stat_easy_multiplication = "stat_easy_multiplication";
    string stat_easy_division = "stat_easy_division";
    string stat_easy_exponent2 = "stat_easy_exponent2";
    string stat_easy_exponent3 = "stat_easy_exponent3";
    string stat_easy_squareRoot = "stat_easy_squareRoot";
    string stat_easy_cubeRoot = "stat_easy_cubeRoot";
    string stat_easy_solved = "stat_easy_solved";
    string stat_easy_skipped = "stat_easy_skipped";
    string stat_easy_failed = "stat_easy_failed";


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

        if (PlayerPrefs.HasKey(stat_endless_solved)) {
            int tally = PlayerPrefs.GetInt(stat_endless_solved);
            MainMenu_BestScore_Endless.text = "Total Completed: " + tally;
        } else {
            MainMenu_BestScore_Endless.text = "";
        }

        if (PlayerPrefs.HasKey(stat_easy_solved)) {
            int tally = PlayerPrefs.GetInt(stat_easy_solved);
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

        if (PlayerPrefs.HasKey("HighScore_Timed"))
        {
            int highScore = PlayerPrefs.GetInt("HighScore_Timed");
            stats_TimedMode.text = "Timed Mode High Score: " + highScore;
        }
        else
        {
            stats_TimedMode.text = "";
        }

        int endless_addition_count = 0;
        int endless_subtraction_count = 0;
        int endless_multiplication_count = 0;
        int endless_division_count = 0;
        int endless_exponent2_count = 0;
        int endless_exponent3_count = 0;
        int endless_squareRoot_count = 0;
        int endless_cubeRoot_count = 0;
        int endless_solved = 0;
        int endless_skipped = 0;
        int endless_failed = 0;

        if (PlayerPrefs.HasKey(stat_endless_addition)) 
        {
            endless_addition_count = PlayerPrefs.GetInt(stat_endless_addition, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_subtraction)) {
            endless_subtraction_count = PlayerPrefs.GetInt(stat_endless_subtraction, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_multiplication))
        {
            endless_multiplication_count = PlayerPrefs.GetInt(stat_endless_multiplication, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_division))
        {
            endless_division_count = PlayerPrefs.GetInt(stat_endless_division, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_exponent2))
        {
            endless_exponent2_count = PlayerPrefs.GetInt(stat_endless_exponent2, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_exponent3))
        {
            endless_exponent3_count = PlayerPrefs.GetInt(stat_endless_exponent3, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_squareRoot))
        {
            endless_squareRoot_count = PlayerPrefs.GetInt(stat_endless_squareRoot, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_cubeRoot))
        {
            endless_cubeRoot_count = PlayerPrefs.GetInt(stat_endless_cubeRoot, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_solved))
        {
            endless_solved = PlayerPrefs.GetInt(stat_endless_solved, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_skipped))
        {
            endless_skipped = PlayerPrefs.GetInt(stat_endless_skipped, 0);
        }
        if (PlayerPrefs.HasKey(stat_endless_failed))
        {
            endless_failed = PlayerPrefs.GetInt(stat_endless_failed, 0);
        }


        stats_EndlessMode.text =
            "Addition:   " + endless_addition_count + "\n" +
            "Subtraction:   " + endless_subtraction_count + "\n" +
            "Multiplication:   " + endless_multiplication_count + "\n" +
            "Division:   " + endless_division_count + "\n" +
            "Exponent = 2:   " + endless_exponent2_count + "\n" +
            "Exponent = 3:   " + endless_exponent3_count + "\n" +
            "Square Root:   " + endless_squareRoot_count + "\n" +
            "Cube Root:   " + endless_cubeRoot_count + "\n" +
            "\n" +
            "Problems Solved:   " + endless_solved + "\n" +
            "Problems Skipped:   " + endless_skipped + "\n" +
            "Problems Failed:   " + endless_failed + "\n";



        int easy_addition_count = 0;
        int easy_subtraction_count = 0;
        int easy_multiplication_count = 0;
        int easy_division_count = 0;
        int easy_exponent2_count = 0;
        int easy_exponent3_count = 0;
        int easy_squareRoot_count = 0;
        int easy_cubeRoot_count = 0;
        int easy_solved = 0;
        int easy_skipped = 0;
        int easy_failed = 0;

        if (PlayerPrefs.HasKey(stat_easy_addition))
        {
            easy_addition_count = PlayerPrefs.GetInt(stat_easy_addition, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_subtraction))
        {
            easy_subtraction_count = PlayerPrefs.GetInt(stat_easy_subtraction, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_multiplication))
        {
            easy_multiplication_count = PlayerPrefs.GetInt(stat_easy_multiplication, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_division))
        {
            easy_division_count = PlayerPrefs.GetInt(stat_easy_division, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_exponent2))
        {
            easy_exponent2_count = PlayerPrefs.GetInt(stat_easy_exponent2, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_exponent3))
        {
            easy_exponent3_count = PlayerPrefs.GetInt(stat_easy_exponent3, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_squareRoot))
        {
            easy_squareRoot_count = PlayerPrefs.GetInt(stat_easy_squareRoot, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_cubeRoot))
        {
            easy_cubeRoot_count = PlayerPrefs.GetInt(stat_easy_cubeRoot, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_solved))
        {
            easy_solved = PlayerPrefs.GetInt(stat_easy_solved, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_skipped))
        {
            easy_skipped = PlayerPrefs.GetInt(stat_easy_skipped, 0);
        }
        if (PlayerPrefs.HasKey(stat_easy_failed))
        {
            easy_failed = PlayerPrefs.GetInt(stat_easy_failed, 0);
        }


        stats_EasyMode.text =
            "Addition:   " + easy_addition_count + "\n" +
            "Subtraction:   " + easy_subtraction_count + "\n" +
            "Multiplication:   " + easy_multiplication_count + "\n" +
            "Division:   " + easy_division_count + "\n" +
            "Exponent = 2:   " + easy_exponent2_count + "\n" +
            "Exponent = 3:   " + easy_exponent3_count + "\n" +
            "Square Root:   " + easy_squareRoot_count + "\n" +
            "Cube Root:   " + easy_cubeRoot_count + "\n" +
            "\n" +
            "Problems Solved:   " + easy_solved + "\n" +
            "Problems Skipped:   " + easy_skipped + "\n" +
            "Problems Failed:   " + easy_failed + "\n";





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
        } else if (gameType == "endless") {
            IncreaseNumberOfSkipped(1);
            ChangeStat_Endless("skipped", 1);
            PuzzleManager.instance.CreateNewPuzzle();
        } else if (gameType == "kiddy") {
            IncreaseNumberOfSkipped(1);
            ChangeStat_Easy("skipped", 1);
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
    //  ************************************************************************************************************************************
    public void ChangeStat_Endless(string typeOfOperator_orCategory, int amount) {
        string keyName = "";
        if (typeOfOperator_orCategory == "addition") {
            keyName = stat_endless_addition;
        } else if (typeOfOperator_orCategory == "subtraction") {
            keyName = stat_endless_subtraction;
        } else if (typeOfOperator_orCategory == "multiplication") {
            keyName = stat_endless_multiplication;
        } else if (typeOfOperator_orCategory == "division") {
            keyName = stat_endless_division;
        } else if (typeOfOperator_orCategory == "exponent2") {
            keyName = stat_endless_exponent2;
        } else if (typeOfOperator_orCategory == "exponent3") {
            keyName = stat_endless_exponent3;
        } else if (typeOfOperator_orCategory == "squareRoot") {
            keyName = stat_endless_squareRoot;
        } else if (typeOfOperator_orCategory == "cubeRoot") {
            keyName = stat_endless_cubeRoot;
        } else if (typeOfOperator_orCategory == "solved") {
            keyName = stat_endless_solved;
        } else if (typeOfOperator_orCategory == "skipped") {
            keyName = stat_endless_skipped;
        } else if (typeOfOperator_orCategory == "failed") {
            keyName = stat_endless_failed;
        }

        if (PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.SetInt(keyName, amount + PlayerPrefs.GetInt(keyName));
        } else {
            PlayerPrefs.SetInt(keyName, amount);
        }
    }
    public void ChangeStat_Easy(string typeOfOperator_orCategory, int amount)
    {
        string keyName = "";
        if (typeOfOperator_orCategory == "addition") {
            keyName = stat_easy_addition;
        } else if (typeOfOperator_orCategory == "subtraction") {
            keyName = stat_easy_subtraction;
        } else if (typeOfOperator_orCategory == "multiplication") {
            keyName = stat_easy_multiplication;
        } else if (typeOfOperator_orCategory == "division") {
            keyName = stat_easy_division;
        } else if (typeOfOperator_orCategory == "exponent2") {
            keyName = stat_easy_exponent2;
        } else if (typeOfOperator_orCategory == "exponent3") {
            keyName = stat_easy_exponent3; 
        } else if (typeOfOperator_orCategory == "squareRoot") {
            keyName = stat_easy_squareRoot; 
        } else if (typeOfOperator_orCategory == "cubeRoot") {
            keyName = stat_easy_cubeRoot;
        } else if (typeOfOperator_orCategory == "solved") {
            keyName = stat_easy_solved;
        } else if (typeOfOperator_orCategory == "skipped") {
            keyName = stat_easy_skipped;
        } else if (typeOfOperator_orCategory == "failed") {
            keyName = stat_easy_failed;
        }

        if (PlayerPrefs.HasKey(keyName)) {
            PlayerPrefs.SetInt(keyName, amount + PlayerPrefs.GetInt(keyName));
        } else {
            PlayerPrefs.SetInt(keyName, amount);
        }
    }




}

