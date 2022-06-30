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
    public GameObject InventoryUI;

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

    public TextMeshProUGUI xpGainText;
    string XP_amount = "XP_amount";
    public TextMeshProUGUI xpDisplay;
    public TextMeshProUGUI xpDisplayForInventoryScreen;
    public bool readyToFadeColorFromGreenToBlack = false;
    float t = 0;

    public TextMeshProUGUI weaponList;
    public TextMeshProUGUI partsList;
    string bulletsInInventory = "bulletsInInventory";
    string rocketsInInventory = "rocketsInInventory";
    string scrapmetalInInventory = "scrapmetalInInventory";
    string gunpowderInInventory = "gunpowderInInventory";
    string wiringInInventory = "wiringInInventory";


    public Animator explodey;
    public float durationOfExplosion;
    public GameObject playerVehicle;
    public GameObject basicEnemy;
    public Animator basicEnemyDriving;
    public Animator basicEnemyExploding;
    public GameObject basicEnemyHull;
    public int puzzleSolvesSinceLastEnemy = 0;
    public GameObject rocket;
    public bool enemyInRange = false;
    public float enemyAppearSpeed;

    public TextMeshProUGUI LevelUI_ammoDisplay;







    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
        #else
            Debug.unityLogger.logEnabled=false;
        #endif
        // from https://answers.unity.com/questions/1301347/how-to-disable-all-logging-on-release-build.html


        instance = this;
        timerGlobal = GlobalTimer.GetComponent<TimerGlobal>();
        timerPuzzle = PuzzleTimerThatMoves.GetComponent<TimerPuzzle>();
        DisplayMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (readyToFadeColorFromGreenToBlack)
        {
            //Color currentColor = timerText.color;
            //float fadeAmount = currentColor.a - (fadeSpeed * Time.deltaTime);
            t += Time.deltaTime / 1.5f; // Divided by 5 to make it 5 seconds.
            xpDisplay.color = Color.Lerp(Color.green, Color.black, t);

            if (xpDisplay.color == Color.black)
            {
                readyToFadeColorFromGreenToBlack = false;
                t = 0;
            }
        }
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
        InventoryUI.SetActive(false);
    }
    public void DisplayStats() {

        int numSpaces = 10;

        if (PlayerPrefs.HasKey("HighScore_Timed"))
        {
            int highScore = PlayerPrefs.GetInt("HighScore_Timed");
            stats_TimedMode.text = "Timed Mode High Score: " + highScore;
        }
        else
        {
            stats_TimedMode.text = "";
        }
        
        string NumWithSpaces(int totalSpacesWanted, int theNumber) {

            int temp = totalSpacesWanted - theNumber.ToString().Length;
            string toReturn = "";
            for (int i = 0; i < temp; i++) {
                toReturn += "_";
            }
            toReturn += theNumber.ToString();
            return toReturn;
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
        if (PlayerPrefs.HasKey(stat_endless_subtraction)) 
        {
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
            "Addition" + NumWithSpaces(numSpaces, endless_addition_count) + "\n" +
            "Subtraction" + NumWithSpaces(numSpaces, endless_subtraction_count) + "\n" +
            "Multiplication" + NumWithSpaces(numSpaces, endless_multiplication_count) + "\n" +
            "Division" + NumWithSpaces(numSpaces, endless_division_count) + "\n" +
            "Exponent = 2" + NumWithSpaces(numSpaces, endless_exponent2_count) + "\n" +
            "Exponent = 3" + NumWithSpaces(numSpaces, endless_exponent3_count) + "\n" +
            "Square Root" + NumWithSpaces(numSpaces, endless_squareRoot_count) + "\n" +
            "Cube Root" + NumWithSpaces(numSpaces, endless_cubeRoot_count) + "\n" +
            "\n" +
            "Problems Solved" + NumWithSpaces(numSpaces, endless_solved) + "\n" +
            "Problems Skipped" + NumWithSpaces(numSpaces, endless_skipped) + "\n" +
            "Problems Failed" + NumWithSpaces(numSpaces, endless_failed) + "\n";



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
            "Addition" + NumWithSpaces(numSpaces, easy_addition_count) + "\n" +
            "Subtraction" + NumWithSpaces(numSpaces, easy_subtraction_count) + "\n" +
            "Multiplication" + NumWithSpaces(numSpaces, easy_multiplication_count) + "\n" +
            "Division" + NumWithSpaces(numSpaces, easy_division_count) + "\n" +
            "Exponent = 2" + NumWithSpaces(numSpaces, easy_exponent2_count) + "\n" +
            "Exponent = 3" + NumWithSpaces(numSpaces, easy_exponent3_count) + "\n" +
            "Square Root" + NumWithSpaces(numSpaces, easy_squareRoot_count) + "\n" +
            "Cube Root" + NumWithSpaces(numSpaces, easy_cubeRoot_count) + "\n" +
            "\n" +
            "Problems Solved" + NumWithSpaces(numSpaces, easy_solved) + "\n" +
            "Problems Skipped" + NumWithSpaces(numSpaces, easy_skipped) + "\n" +
            "Problems Failed" + NumWithSpaces(numSpaces, easy_failed) + "\n";


        MainMenuUI.SetActive(false);
        StatsUI.SetActive(true);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
        GameOverUI.SetActive(false);
        InventoryUI.SetActive(false);
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
        InventoryUI.SetActive(false);
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
        InventoryUI.SetActive(false);
        ShowXP();

        // update & display ammo levels
        ShowLevelUI_ammoDisplay();


    }
    public void DisplayInventory() {

        string NumWithSpaces(int totalSpacesWanted, int theNumber) {
            int temp = totalSpacesWanted - theNumber.ToString().Length;
            string toReturn = "";
            for (int i = 0; i < temp; i++) {
                toReturn += "_";
            }
            toReturn += theNumber.ToString();
            return toReturn;
        }

        weaponList.text =
            "Bullets" + NumWithSpaces(10, PlayerPrefs.GetInt("bulletsInInventory", 0)) + "\n" +
            "Rockets" + NumWithSpaces(10, PlayerPrefs.GetInt("rocketsInInventory", 0)) + "\n";

        partsList.text =
            "Scrap Metal" + NumWithSpaces(10, PlayerPrefs.GetInt("scrapmetalInInventory", 0)) + "\n" +
            "Gun Powder" + NumWithSpaces(10, PlayerPrefs.GetInt("gunpowderInInventory", 0)) + "\n" +
            "Wiring" + NumWithSpaces(10, PlayerPrefs.GetInt("wiringInInventory", 0)) + "\n";

        MainMenuUI.SetActive(false);
        StatsUI.SetActive(false);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
        GameOverUI.SetActive(false);
        InventoryUI.SetActive(true);
        xpDisplayForInventoryScreen.text = "XP: " + PlayerPrefs.GetInt(XP_amount);
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
    public void IncreaseXP() {
        // check which toggles are active
        int XPtoGrant = 1;
        if (PuzzleManager.instance.ReturnNegativeNumbersON_OFF()) { XPtoGrant += 1; }
        if (PuzzleManager.instance.ReturnFractionsON_OFF()) { XPtoGrant += 1; }
        if (PuzzleManager.instance.ReturnExponentsON_OFF()) { XPtoGrant += 1; }
        if (PuzzleManager.instance.ReturnMultDivideON_OFF()) { XPtoGrant += 1; }
        
        if (gameType == "endless") {
            // multiplier of 2 because this is "normal mode"
            XPtoGrant *= 2;
        } else if (gameType == "kiddy") { 
            // do nothing
        } else if (gameType == "timed") { 
            // do nothing for now
        } else if (gameType == "hard") { 
            // do nothing for now, but probably a multiplier of 3 or 4
        }

        //ShowXPgain(XPtoGrant);
        xpGainText.GetComponent<XPnotify>().BeginMove(XPtoGrant);


        if (PlayerPrefs.HasKey(XP_amount))
        {
            int currentXP = PlayerPrefs.GetInt(XP_amount);
            currentXP += XPtoGrant;
            PlayerPrefs.SetInt(XP_amount, currentXP);
            Debug.Log("xp just set to " + currentXP);
            //xpDisplay.text = "Total XP: " + currentXP;
        }
        else
        {
            // then we create the playerPrefs key
            PlayerPrefs.SetInt(XP_amount, XPtoGrant);
            //xpDisplay.text = "";
        }
    }
    public void ShowXP() { 
        if (PlayerPrefs.HasKey(XP_amount)) {
            xpDisplay.text = "Total XP: " + PlayerPrefs.GetInt(XP_amount);
        } else {
            xpDisplay.text = "";
        }
    }
    public void ShowXPgainWithGreenFade()
    {
        //xpGainText.gameObject.SetActive(true);
        //xpGainText.text = "+ " + xpAmount + " XP";
        int currentXP = PlayerPrefs.GetInt(XP_amount);
        xpDisplay.text = "Total XP: " + currentXP;
        Debug.Log("inside ShowXPWithGreenFade: xp is " + currentXP);
        xpDisplay.color = Color.green;
        readyToFadeColorFromGreenToBlack = true;

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

    //  ************************************************************************************************************************************
    public void BuyBullets() {
        // take away XP
        int tempy = PlayerPrefs.GetInt(XP_amount);
        if (tempy >= 10) {
            PlayerPrefs.SetInt(XP_amount, tempy - 10);

            // add bullets to inventory
            PlayerPrefs.SetInt(bulletsInInventory, PlayerPrefs.GetInt(bulletsInInventory) + 10);

            DisplayInventory();
        }
        else {
            // do nothing cuz you are POOR
            Debug.Log("omg so poor");
        }

    }
    public void BuyRocket()
    {
        // take away XP
        int tempy = PlayerPrefs.GetInt(XP_amount);
        if (tempy >= 10)
        {
            PlayerPrefs.SetInt(XP_amount, tempy - 10);

            // add bullets to inventory
            PlayerPrefs.SetInt(rocketsInInventory, PlayerPrefs.GetInt(rocketsInInventory) + 1);

            DisplayInventory();
        }
        else
        {
            // do nothing cuz you are POOR
            Debug.Log("omg so poor");
        }

    }
    public void SpendRocket() {
        int tempy = PlayerPrefs.GetInt(rocketsInInventory);
        if (tempy >= 1) {
            PlayerPrefs.SetInt(rocketsInInventory, tempy - 1);
            FireRocket();
            //DisplayInventory();
            // update the levelUI inventory thingy
            ShowLevelUI_ammoDisplay();
        } else {
            Debug.Log("no rockets to shoot!");
        }
    }


    public void ShowLevelUI_ammoDisplay() {
        int numBullets = PlayerPrefs.GetInt(bulletsInInventory, 0);
        int numRockets = PlayerPrefs.GetInt(rocketsInInventory, 0);
        LevelUI_ammoDisplay.text =
            "Bullets: " + numBullets + "\n" +
            "Rockets: " + numRockets;
    }



    //  ************************************************************************************************************************************

    public void PlayExplosion() {
        explodey.gameObject.SetActive(true);
        explodey.Play("explodeyAnim", -1, 0f);
        //explodey.gameObject.SetActive(false);
        StartCoroutine(ExplosionDisableAfterAnimation());
    }
    IEnumerator ExplosionDisableAfterAnimation() {
        yield return new WaitForSeconds(durationOfExplosion);
        explodey.gameObject.SetActive(false);
    }


    public void EnemyAppears() {


        //basicEnemy.GetComponent<VehicleBounce>().SetGoalPosForRocket(21, 50, 0);
        basicEnemy.transform.position = new Vector3(21, 0, 1);

        basicEnemy.SetActive(true);
        basicEnemyDriving.gameObject.SetActive(true);
        basicEnemyExploding.gameObject.SetActive(false);
        puzzleSolvesSinceLastEnemy = 0;
        basicEnemy.GetComponent<VehicleBounce>().driftForwardBackward(enemyAppearSpeed);
        basicEnemy.GetComponent<VehicleBounce>().midBounce = true;

        enemyInRange = true;


    }
    
    public void FireRocket() {
        rocket.transform.position = playerVehicle.transform.position + new Vector3(0, 0.5f, 0);
        // begin rocket slowly launching, & hitting enemy
        rocket.SetActive(true);
        //rocket.GetComponent<Rocket>().SetGoalPosForRocket(30, rocketSpeed, targetXPos);
        rocket.GetComponent<Rocket>().LaunchRocket();

    }
    public void FireGuns() { 

    }
    public void EnemyFiresRocket() {
        rocket.transform.position = basicEnemy.transform.position + new Vector3(0, 0.5f, 0);
        rocket.SetActive(true);
        rocket.GetComponent<Rocket>().LaunchRocketBackward();

    }
    public void EnemyFiresGuns() { 

    }
    
    
    public void EnemyExplodes() {

        explodey.transform.position = basicEnemy.transform.position + new Vector3(-6.6f, -3.95f, 0);
        PlayExplosion();
        //basicEnemy.SetActive(false);
        //basicEnemy.gameObject.transform.GetChild<
        //basicEnemyExploding.
        //basicEnemyDriving.gameObject.SetActive(false);
        //basicEnemyExploding.gameObject.SetActive(true);
        //basicEnemy.GetComponent<VehicleBounce>().SetGoalPosForRocket(-55, 33, 0);


        // turn off the normal enemy
        basicEnemy.SetActive(false);
        // turn on the hull
        basicEnemyHull.transform.position = basicEnemy.transform.position - new Vector3(6.5f, 0, 0);
        basicEnemyHull.SetActive(true);
        // kick off the hull-bouncing script
        basicEnemyHull.GetComponent<HullWrecking>().BeginBouncing();

        enemyInRange = false;
      
    }
    public void PlayerExplodes() {
        explodey.transform.position = playerVehicle.transform.position + new Vector3(-6.6f, -3.95f, 0);
        PlayExplosion();
        //playerVehicle.SetActive(false);
    }
    public void ResolveConflictFavorably() { 
        if (enemyInRange == false) {
            puzzleSolvesSinceLastEnemy += 1;

            int rando = Random.Range(1, 2);

            if (rando == 1) {
                EnemyAppears();
            }

        } else {
            // the enemy is here, so kill it and make explosion
            SpendRocket();

            // or shoot guns

            // or enemy shoots and player dodges

        }
    }
    public void ResolveConflictUNFAVORABLY() { 
        if (enemyInRange == false) { 

        } else {
            Debug.Log("resolving unfavorably");
            EnemyFiresRocket();
        }
    }

}

