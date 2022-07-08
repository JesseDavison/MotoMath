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
    //public GameObject MathInProgress;
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


    string moneyInInventory = "moneyInInventory";
    string fuelInInventory = "fuelInInventory";         // this will be a float
    string nitrousInInventory = "nitrousInInventory";   // this will be a float
    string scrapmetalInInventory = "scrapmetalInInventory";
    string electronicsInInventory = "electronicsInInventory";

    string bulletsInInventory = "bulletsInInventory";
    string rocketsInInventory = "rocketsInInventory";
    string bombsInInventory = "bombsInInventory";
    string caltropsInInventory = "caltropsInInventory";
    string flamethrowerInInventory = "flamethrowerInInventory";


    public Animator explodey;
    public float durationOfExplosion;
    public GameObject playerVehicle;
    public GameObject basicEnemy;
    public Animator basicEnemyDriving;
    public Animator basicEnemyExploding;
    public GameObject basicEnemyHull;
    public int puzzleSolvesSinceLastEnemy = 0;
    public GameObject rocket;
    public GameObject caltrops_1;
    public GameObject caltrops_2;
    public GameObject caltrops_3;
    public GameObject caltrops_4;
    public bool enemyInRange = false;
    public float enemyAppearSpeed;


    public TextMeshProUGUI LevelUI_inventoryDisplay_money;
    public TextMeshProUGUI LevelUI_inventoryDisplay_fuel;
    public TextMeshProUGUI LevelULI_inventoryDisplay_nitrous;
    public TextMeshProUGUI LevelUI_inventoryDisplay_scrapMetal;
    public TextMeshProUGUI LevelUI_inventoryDisplay_electronics;
    public TextMeshProUGUI LevelUI_inventoryDisplay_bullets;
    public TextMeshProUGUI LevelUI_inventoryDisplay_rockets;
    public TextMeshProUGUI LevelUI_inventoryDisplay_bombs;
    public TextMeshProUGUI LevelUI_inventoryDisplay_caltrops;
    public TextMeshProUGUI LevelUI_inventoryDisplay_flamethrower;

    public GameObject FuelGauge;
    FuelGaugeScript fuelGaugeScript;

    public GameObject NitrousBottle;
    Animator nitrousBottle_animator;
    bool usingNitrous = false;
    public float usingNitrousMultiplier = 1.01f;
    bool fullSpeedAchieved = false;
    bool readyToSlowDown = false;

    public GameObject LOOT_money_icon;
    LootAnimationScript LOOT_money_script;
    int LOOT_money_quantity;

    public GameObject LOOT_fuel_icon;
    LootAnimationScript LOOT_fuel_script;
    int LOOT_fuel_quantity;

    public GameObject LOOT_nitrous_icon;
    LootAnimationScript LOOT_nitrous_script;
    int LOOT_nitrous_quantity;

    public GameObject LOOT_scrapMetal_icon;
    LootAnimationScript LOOT_scrapMetal_script;
    int LOOT_scrapMetal_quantity;

    public GameObject LOOT_electronics_icon;
    LootAnimationScript LOOT_electronics_script;
    int LOOT_electronics_quantity;

    // ammo
    public GameObject LOOT_bullets_icon;
    LootAnimationScript LOOT_bullets_script;
    int LOOT_bullets_quantity;

    public GameObject LOOT_rocket_icon;
    LootAnimationScript LOOT_rocket_script;
    int LOOT_rocket_quantity;

    public GameObject LOOT_bombs_icon;
    LootAnimationScript LOOT_bombs_script;
    int LOOT_bombs_quantity;

    public GameObject LOOT_caltrops_icon;
    LootAnimationScript LOOT_caltrops_script;
    int LOOT_caltrops_quantity;

    public GameObject LOOT_flamethrower_icon;
    LootAnimationScript LOOT_flamethrower_script;
    int LOOT_flamethrower_quantity;







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

        fuelGaugeScript = FuelGauge.GetComponent<FuelGaugeScript>();

        nitrousBottle_animator = NitrousBottle.GetComponent<Animator>();

        LOOT_money_script = LOOT_money_icon.GetComponent<LootAnimationScript>();
        LOOT_fuel_script = LOOT_fuel_icon.GetComponent<LootAnimationScript>();
        LOOT_nitrous_script = LOOT_nitrous_icon.GetComponent<LootAnimationScript>();
        LOOT_scrapMetal_script = LOOT_scrapMetal_icon.GetComponent<LootAnimationScript>();
        LOOT_electronics_script = LOOT_electronics_icon.GetComponent<LootAnimationScript>();

        LOOT_bullets_script = LOOT_bullets_icon.GetComponent<LootAnimationScript>();
        LOOT_rocket_script = LOOT_rocket_icon.GetComponent<LootAnimationScript>();
        LOOT_bombs_script = LOOT_bombs_icon.GetComponent<LootAnimationScript>();
        LOOT_caltrops_script = LOOT_caltrops_icon.GetComponent<LootAnimationScript>();
        LOOT_flamethrower_script = LOOT_flamethrower_icon.GetComponent<LootAnimationScript>();
        

        DisplayMainMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (usingNitrous == true) {
            if (fullSpeedAchieved == false) 
            {
                fuelGaugeScript.StopFuelConsumption();
                Time.timeScale = Time.timeScale * usingNitrousMultiplier;
                if (Time.timeScale >= 5.01f) {
                    Time.timeScale = 5;

                    fullSpeedAchieved = true;
                    StartCoroutine(Nitrous_waitForMomentBeforeReturningNormalSpeed());
                    Debug.Log("max speed achieved");
                }
            } 
            else if (readyToSlowDown == true)
            {
                Time.timeScale *= 0.99f;
                if (Time.timeScale < 1) {
                    Time.timeScale = 1;
                    readyToSlowDown = false;
                    usingNitrous = false;
                    fullSpeedAchieved = false;
                    Debug.Log("back to normal speed");
                    fuelGaugeScript.StartFuelConsumption();
                    playerVehicle.GetComponent<VehicleBounce>().EndNitrousBoost();
                    basicEnemy.GetComponent<VehicleBounce>().BringVehicleBackOnScreen();
                    LoadNewPuzzleAfterSKIP();
                    Debug.Log("LoadNewPuzzleAfterSKIP");

                }
            }
            
            
            

        }
    }

    public void DisplayMainMenu()
    {
        if (PlayerPrefs.HasKey("HighScore_Timed"))
        {
            int highScore = PlayerPrefs.GetInt("HighScore_Timed");
            MainMenu_BestScore_Timed.text = "High Score: " + highScore;
        }
        else
        {
            MainMenu_BestScore_Timed.text = "";
        }

        if (PlayerPrefs.HasKey(stat_endless_solved))
        {
            int tally = PlayerPrefs.GetInt(stat_endless_solved);
            MainMenu_BestScore_Endless.text = "Total Completed: " + tally;
        }
        else
        {
            MainMenu_BestScore_Endless.text = "";
        }

        if (PlayerPrefs.HasKey(stat_easy_solved))
        {
            int tally = PlayerPrefs.GetInt(stat_easy_solved);
            MainMenu_BestScore_Kiddy.text = "Total Completed: " + tally;
        }
        else
        {
            MainMenu_BestScore_Kiddy.text = "";
        }

        MainMenuUI.SetActive(true);
        StatsUI.SetActive(false);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        //MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
        GameOverUI.SetActive(false);
        InventoryUI.SetActive(false);
    }
    public void DisplayStats()
    {

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

        string NumWithSpaces(int totalSpacesWanted, int theNumber)
        {

            int temp = totalSpacesWanted - theNumber.ToString().Length;
            string toReturn = "";
            for (int i = 0; i < temp; i++)
            {
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
        //MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
        GameOverUI.SetActive(false);
        InventoryUI.SetActive(false);
    }
    public void DisplayOptions()
    {
        MainMenuUI.SetActive(false);
        StatsUI.SetActive(false);
        OptionsUI.SetActive(true);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        //MathInProgress.SetActive(false);
        OperatorsParent.SetActive(false);
        GoalParent.SetActive(false);
        GameOverUI.SetActive(false);
        InventoryUI.SetActive(false);
    }
    public void DisplayLevel()
    {
        MainMenuUI.SetActive(false);
        StatsUI.SetActive(false);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(true);
        CirclesParent.SetActive(true);
        //MathInProgress.SetActive(true);
        OperatorsParent.SetActive(true);
        GoalParent.SetActive(true);
        GameOverUI.SetActive(false);
        InventoryUI.SetActive(false);

        // update & display ammo levels
        ShowLevelUI_ammo_and_inventory_Display();



    }

    public void StartTimedGame()
    {
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
    public void StartEndlessGame()
    {
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
    public void StartKiddyGame()
    {
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
    public void EndGameEarly()
    {        // tie this to the "Main Menu" button in the level UI
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

        Debug.Log("SkipPuzzle() invoked... nitrous level is: " + PlayerPrefs.GetFloat(nitrousInInventory));

        if (PlayerPrefs.GetFloat(nitrousInInventory) >= 100) {

            PlayerPrefs.SetFloat(nitrousInInventory, 0);
            ShowLevelUI_ammo_and_inventory_Display();

            // make the player speed up
            usingNitrous = true;
            playerVehicle.GetComponent<VehicleBounce>().DriveToMiddle_forNitrousBoost();
            // if an enemy is there, it should fall back and go away


            // make circles, operators, & goal disappear left-ward
            PuzzleManager.instance.MakeCirclesOperatorsGoal_moveLeftForNitrous();
            // IF ENEMY IS ON SCREEN:
            basicEnemy.GetComponent<VehicleBounce>().DriveAwayBackward();

        }



    }
    IEnumerator Nitrous_waitForMomentBeforeReturningNormalSpeed() {
        yield return new WaitForSeconds(20);
        Debug.Log("About to change readyToSlowDown to true");
        readyToSlowDown = true;

    }
    public void LoadNewPuzzleAfterSKIP() {


        if (gameType == "timed")
        {
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
        }
        else if (gameType == "endless")
        {
            IncreaseNumberOfSkipped(1);
            ChangeStat_Endless("skipped", 1);
            PuzzleManager.instance.CreateNewPuzzle();
        }
        else if (gameType == "kiddy")
        {
            IncreaseNumberOfSkipped(1);
            ChangeStat_Easy("skipped", 1);
            PuzzleManager.instance.CreateNewPuzzle();
        }
    }
    //IEnumerator ShowNextPuzzleAfterNitrousEnds() { 
    //    //yield return new WaitForSeconds()
    //}
    public void DecreaseNumberOfPuzzlesRemaining(int amount)
    {
        puzzlesRemaining -= amount;
        PuzzlesRemainingDisplay.text = "Remaining: " + puzzlesRemaining;
        if (puzzlesRemaining == 0)
        {
            DisplayGameOver(false, false);
        }
    }
    public void SetNumberOfPuzzlesRemaining(int amount)
    {
        puzzlesRemaining = amount;
        PuzzlesRemainingDisplay.text = "Remaining: " + puzzlesRemaining;
    }
    //  ************************************************************************************************************************************
    public void IncreaseScore(int amount)
    {
        score += amount;
        ScoreDisplay.text = "Score: " + score;

        //// temporary:
        //BuyRocket();

    }
    public void ResetScore()
    {
        score = 0;
        if (gameType == "timed")
        {
            ScoreDisplay.text = "Score: " + score;
        }
        else
        {
            numberCompleted = 0;
            numberSkipped = 0;
            numberFailed = 0;
            UpdateCompletedSkippedFailed();
        }

    }



    //  ************************************************************************************************************************************
    public void IncreaseNumberOfCompleted(int amount)
    {
        numberCompleted += amount;
        if (gameType == "endless")
        {
            int temp = PlayerPrefs.GetInt("Endless_Tally");
            temp += amount;
            PlayerPrefs.SetInt("Endless_Tally", temp);
        }
        else if (gameType == "kiddy")
        {
            int temp = PlayerPrefs.GetInt("Kiddy_Tally");
            temp += amount;
            PlayerPrefs.SetInt("Kiddy_Tally", temp);
        }
        UpdateCompletedSkippedFailed();
    }
    public void IncreaseNumberOfSkipped(int amount)
    {
        numberSkipped += amount;
        UpdateCompletedSkippedFailed();
    }
    public void IncreaseNumberOfFailed(int amount)
    {
        numberFailed += amount;
        UpdateCompletedSkippedFailed();
    }
    public void UpdateCompletedSkippedFailed()
    {
        string type = "wtf";
        if (gameType == "endless")
        {
            type = "ENDLESS MODE\n";
        }
        else if (gameType == "kiddy")
        {
            type = "EASY MODE\n";
        }
        ScoreDisplay.text = type + "Completed: " + numberCompleted + "\nSkipped: " + numberSkipped + "\nFailed: " + numberFailed;
    }
    public void ResetCompletedSkippedFailed()
    {
        numberCompleted = 0;
        numberSkipped = 0;
        numberFailed = 0;
        UpdateCompletedSkippedFailed();
    }

    //  ************************************************************************************************************************************
    public void DisplayGameOverForButtonPress()
    {  // this exists so i can press the "quit" button on the levelUI
        DisplayGameOver(false, true);
    }
    public void DisplayGameOver(bool timeRanOut, bool removeAllSpeedPoints)
    {
        MainMenuUI.SetActive(false);
        OptionsUI.SetActive(false);
        LevelUI.SetActive(false);
        CirclesParent.SetActive(false);
        //MathInProgress.SetActive(false);
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
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    //  ************************************************************************************************************************************
    public void ChangeStat_Endless(string typeOfOperator_orCategory, int amount)
    {
        string keyName = "";
        if (typeOfOperator_orCategory == "addition")
        {
            keyName = stat_endless_addition;
        }
        else if (typeOfOperator_orCategory == "subtraction")
        {
            keyName = stat_endless_subtraction;
        }
        else if (typeOfOperator_orCategory == "multiplication")
        {
            keyName = stat_endless_multiplication;
        }
        else if (typeOfOperator_orCategory == "division")
        {
            keyName = stat_endless_division;
        }
        else if (typeOfOperator_orCategory == "exponent2")
        {
            keyName = stat_endless_exponent2;
        }
        else if (typeOfOperator_orCategory == "exponent3")
        {
            keyName = stat_endless_exponent3;
        }
        else if (typeOfOperator_orCategory == "squareRoot")
        {
            keyName = stat_endless_squareRoot;
        }
        else if (typeOfOperator_orCategory == "cubeRoot")
        {
            keyName = stat_endless_cubeRoot;
        }
        else if (typeOfOperator_orCategory == "solved")
        {
            keyName = stat_endless_solved;
        }
        else if (typeOfOperator_orCategory == "skipped")
        {
            keyName = stat_endless_skipped;
        }
        else if (typeOfOperator_orCategory == "failed")
        {
            keyName = stat_endless_failed;
        }

        if (PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.SetInt(keyName, amount + PlayerPrefs.GetInt(keyName));
        }
        else
        {
            PlayerPrefs.SetInt(keyName, amount);
        }
    }
    public void ChangeStat_Easy(string typeOfOperator_orCategory, int amount)
    {
        string keyName = "";
        if (typeOfOperator_orCategory == "addition")
        {
            keyName = stat_easy_addition;
        }
        else if (typeOfOperator_orCategory == "subtraction")
        {
            keyName = stat_easy_subtraction;
        }
        else if (typeOfOperator_orCategory == "multiplication")
        {
            keyName = stat_easy_multiplication;
        }
        else if (typeOfOperator_orCategory == "division")
        {
            keyName = stat_easy_division;
        }
        else if (typeOfOperator_orCategory == "exponent2")
        {
            keyName = stat_easy_exponent2;
        }
        else if (typeOfOperator_orCategory == "exponent3")
        {
            keyName = stat_easy_exponent3;
        }
        else if (typeOfOperator_orCategory == "squareRoot")
        {
            keyName = stat_easy_squareRoot;
        }
        else if (typeOfOperator_orCategory == "cubeRoot")
        {
            keyName = stat_easy_cubeRoot;
        }
        else if (typeOfOperator_orCategory == "solved")
        {
            keyName = stat_easy_solved;
        }
        else if (typeOfOperator_orCategory == "skipped")
        {
            keyName = stat_easy_skipped;
        }
        else if (typeOfOperator_orCategory == "failed")
        {
            keyName = stat_easy_failed;
        }

        if (PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.SetInt(keyName, amount + PlayerPrefs.GetInt(keyName));
        }
        else
        {
            PlayerPrefs.SetInt(keyName, amount);
        }
    }

    //  ************************************************************************************************************************************
    public void BuyBullets()
    {
        //// take away XP
        //int tempy = PlayerPrefs.GetInt(XP_amount);
        //if (tempy >= 10)
        //{
        //    PlayerPrefs.SetInt(XP_amount, tempy - 10);

        //    // add bullets to inventory
        //    PlayerPrefs.SetInt(bulletsInInventory, PlayerPrefs.GetInt(bulletsInInventory) + 10);

        //}
        //else
        //{
        //    // do nothing cuz you are POOR
        //    Debug.Log("omg so poor");
        //}

    }
    public void BuyRocket()
    {
        //// take away XP
        //int tempy = PlayerPrefs.GetInt(XP_amount);
        //if (tempy >= 10)
        //{
        //    PlayerPrefs.SetInt(XP_amount, tempy - 10);

        //    // add bullets to inventory
        PlayerPrefs.SetInt(rocketsInInventory, PlayerPrefs.GetInt(rocketsInInventory) + 2);

        //if (Time.timeScale == 1) {
        //    Time.timeScale = 2;
        //    Debug.Log("timescale is 2");
        //} else if (Time.timeScale == 2) {
        //    Time.timeScale = 3;
        //    Debug.Log("timescale is 3");
        //} else if (Time.timeScale == 3) {
        //    Time.timeScale = 4;
        //    Debug.Log("timescale is 4");
        //} else {
        //    Time.timeScale = 1;
        //    Debug.Log("timescale is 1");
        //}


        PlayerPrefs.SetFloat(nitrousInInventory, 100);
        ShowLevelUI_ammo_and_inventory_Display();






        //    Debug.Log("bought a rocket");

        //    //DisplayInventory();
        //    ShowLevelUI_ammo_and_inventory_Display();
        //}
        //else
        //{
        //    // do nothing cuz you are POOR
        //    Debug.Log("omg so poor");
        //}

    }

    public void SpendRocket()
    {
        int tempy = PlayerPrefs.GetInt(rocketsInInventory);
        if (tempy >= 1)
        {
            PlayerPrefs.SetInt(rocketsInInventory, tempy - 1);
            FireRocket();
            //DisplayInventory();
            // update the levelUI inventory thingy
            ShowLevelUI_ammo_and_inventory_Display();
        }
        else
        {
            Debug.Log("no rockets to shoot!");
        }
    }


    public void ShowLevelUI_ammo_and_inventory_Display()
    {

        // money, fuel, nitrous
        // Scrap metal, electronics
        // bullets, rockets, bombs, caltrops, flamethrower

        int numMoney = PlayerPrefs.GetInt(moneyInInventory, 0);
        float numFuel = PlayerPrefs.GetFloat(fuelInInventory, 0);
        float numNitrous = PlayerPrefs.GetFloat(nitrousInInventory, 0);
        int numScrapMetal = PlayerPrefs.GetInt(scrapmetalInInventory, 0);
        int numElectronics = PlayerPrefs.GetInt(electronicsInInventory, 0);

        int numBullets = PlayerPrefs.GetInt(bulletsInInventory, 0);
        int numRockets = PlayerPrefs.GetInt(rocketsInInventory, 0);
        int numBombs = PlayerPrefs.GetInt(bombsInInventory, 0);
        int numCaltrops = PlayerPrefs.GetInt(caltropsInInventory, 0);
        int numFlamethrower = PlayerPrefs.GetInt(flamethrowerInInventory, 0);

        LevelUI_inventoryDisplay_money.text = "$" + numMoney;
        LevelUI_inventoryDisplay_fuel.text = numFuel.ToString("F2");
        LevelULI_inventoryDisplay_nitrous.text = numNitrous.ToString("F2");
        AnimateNitrous(numNitrous);
        LevelUI_inventoryDisplay_scrapMetal.text = numScrapMetal.ToString();
        LevelUI_inventoryDisplay_electronics.text = numElectronics.ToString();

        LevelUI_inventoryDisplay_bullets.text = numBullets.ToString();
        LevelUI_inventoryDisplay_rockets.text = numRockets.ToString();
        LevelUI_inventoryDisplay_bombs.text = numBombs.ToString();
        LevelUI_inventoryDisplay_caltrops.text = numCaltrops.ToString();
        LevelUI_inventoryDisplay_flamethrower.text = numFlamethrower.ToString();

    }



    //  ************************************************************************************************************************************

    public void PlayExplosion()
    {
        explodey.gameObject.SetActive(true);
        explodey.Play("explodeyAnim", -1, 0f);
        //explodey.gameObject.SetActive(false);
        StartCoroutine(ExplosionDisableAfterAnimation());
    }
    IEnumerator ExplosionDisableAfterAnimation()
    {
        yield return new WaitForSeconds(durationOfExplosion);
        explodey.gameObject.SetActive(false);
    }


    public void EnemyAppears()
    {


        //basicEnemy.GetComponent<VehicleBounce>().SetGoalPosForRocket(21, 50, 0);
        basicEnemy.transform.position = new Vector3(21, 0, 1);

        basicEnemy.SetActive(true);
        basicEnemyDriving.gameObject.SetActive(true);
        basicEnemyExploding.gameObject.SetActive(false);
        puzzleSolvesSinceLastEnemy = 0;
        basicEnemy.GetComponent<VehicleBounce>().driftForwardBackward(enemyAppearSpeed);
        basicEnemy.GetComponent<VehicleBounce>().midBounce = true;

        enemyInRange = true;


        // enemies can appear behind player... the player moves forward



    }

    public void FireRocket()
    {
        rocket.transform.position = playerVehicle.transform.position + new Vector3(0, 0.5f, 0);
        // begin rocket slowly launching, & hitting enemy
        rocket.SetActive(true);
        //rocket.GetComponent<Rocket>().SetGoalPosForRocket(30, rocketSpeed, targetXPos);
        rocket.GetComponent<Rocket>().LaunchRocket();

    }
    public void FireGuns()
    {

    }
    public void EnemyFires_Rocket()
    {
        rocket.transform.position = basicEnemy.transform.position + new Vector3(0, 0.5f, 0);
        rocket.SetActive(true);
        rocket.GetComponent<Rocket>().LaunchRocketBackward();

    }
    public void EnemyFires_Caltrops(bool favorable) {
        // if enemy is in middle of screen, should make the enemy move forward before the caltrops land
        basicEnemy.GetComponent<VehicleBounce>().DriveToForwardPosition_forDroppingCaltrops();

        caltrops_1.transform.position = basicEnemy.transform.position;
        caltrops_2.transform.position = basicEnemy.transform.position;
        caltrops_3.transform.position = basicEnemy.transform.position;
        caltrops_4.transform.position = basicEnemy.transform.position;
        caltrops_1.SetActive(true);
        caltrops_2.SetActive(true);
        caltrops_3.SetActive(true);
        caltrops_4.SetActive(true);
        caltrops_1.GetComponent<CaltropsBounce>().LaunchCaltrops(favorable);
        caltrops_2.GetComponent<CaltropsBounce>().LaunchCaltrops(favorable);
        caltrops_3.GetComponent<CaltropsBounce>().LaunchCaltrops(favorable);
        caltrops_4.GetComponent<CaltropsBounce>().LaunchCaltrops(favorable);

    }
    public void EnemyFiresGuns()
    {

    }


    public void EnemyExplodes()
    {

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

    public void SpawnLoot(Vector2 startPosition, float zAxisRotationSpeed) {

        // money, fuel, nitrous
        // Scrap metal, electronics
        // bullets, rockets, bombs, caltrops, flamethrower




        // There is always: 
        //      money, fuel, scrapMetal

        // The other 2 loot slots are randomly selected from:
        //      nitrous, electronics
        //      ammo, 5 types
        //              .... therefore need to choose which 2 we will use, there are 7 things to choose from, so:
        List<int> options = new List<int> {1, 2, 3, 4, 5, 6, 7};
        //Debug.Log("just created List<int> options: " + options[0] + " " + options[1] + " " + options[2]);
        // create a random INDEX
        int randomIndex1 = Random.Range(0, options.Count);
        int rando1 = options[randomIndex1];
        options.Remove(rando1);
        // create a random INDEX
        int randomIndex2 = Random.Range(0, options.Count);
        int rando2 = options[randomIndex2];
        options.Remove(rando2);

        // 1: nitrous, 2: electronics, 3: bullets, 4: rockets, 5: bombs, 6: caltrops, 7: flamethrower
        List<int> results = new List<int> { rando1, rando2 };
        Debug.Log("results list: " + results[0] + " " + results[1]);

        Vector2 spot1 = new Vector2(6, 5.5f);
        Vector2 spot2 = new Vector2(6, 4.5f);
        Vector2 spot3 = new Vector2(6, 3.5f);
        Vector2 spot4 = new Vector2(6, 2.5f);
        Vector2 spot5 = new Vector2(6, 1.5f);
        List<Vector2> remainingSpots = new List<Vector2> { spot4, spot5 };


        // always money
        LOOT_money_quantity = Random.Range(1, 10);
        LOOT_money_icon.SetActive(true);
        LOOT_money_icon.transform.position = startPosition + new Vector2(0, 0.5f);
        LOOT_money_script.StartMovement(LOOT_money_quantity, spot1);

        // always fuel
        LOOT_fuel_quantity = Random.Range(3, 10);
        LOOT_fuel_icon.SetActive(true);
        LOOT_fuel_icon.transform.position = startPosition + new Vector2(0, 0.5f);
        LOOT_fuel_script.StartMovement(LOOT_fuel_quantity, spot2);


        // 1: nitrous, 2: electronics, 3: bullets, 4: rockets, 5: bombs, 6: caltrops, 7: flamethrower
        if (results.Contains(1))
        {
            LOOT_nitrous_quantity = Random.Range(25, 25);
            LOOT_nitrous_icon.SetActive(true);
            LOOT_nitrous_icon.transform.position = startPosition + new Vector2(0, 0.5f);
            LOOT_nitrous_script.StartMovement(LOOT_nitrous_quantity, remainingSpots[0]);
            remainingSpots.Remove(remainingSpots[0]);
        }

        // always scrapMetal
        LOOT_scrapMetal_quantity = Random.Range(5, 20);
        LOOT_scrapMetal_icon.SetActive(true);
        LOOT_scrapMetal_icon.transform.position = startPosition + new Vector2(0, 0.5f);
        LOOT_scrapMetal_script.StartMovement(LOOT_scrapMetal_quantity, spot3);

        if (results.Contains(2))
        {
            LOOT_electronics_quantity = Random.Range(5, 10);
            LOOT_electronics_icon.SetActive(true);
            LOOT_electronics_icon.transform.position = startPosition + new Vector2(0, 0.5f);
            // assign the first spot on the list of spots (remainingSpots), then remove that spot from the list of spots so it won't be used twice
            LOOT_electronics_script.StartMovement(LOOT_electronics_quantity, remainingSpots[0]);
            remainingSpots.Remove(remainingSpots[0]);
        }

        // ammo
        // 1: nitrous, 2: electronics, 3: bullets, 4: rockets, 5: bombs, 6: caltrops, 7: flamethrower
        if (results.Contains(3))
        {
            LOOT_bullets_quantity = Random.Range(1, 18);
            LOOT_bullets_icon.SetActive(true);
            LOOT_bullets_icon.transform.position = startPosition + new Vector2(0, 0.5f);
            LOOT_bullets_script.StartMovement(LOOT_bullets_quantity, remainingSpots[0]);
            remainingSpots.Remove(remainingSpots[0]);
        }
        if (results.Contains(4))
        {
            LOOT_rocket_quantity = Random.Range(1, 4);
            LOOT_rocket_icon.SetActive(true);
            LOOT_rocket_icon.transform.position = startPosition + new Vector2(0, 0.5f);
            LOOT_rocket_script.StartMovement(LOOT_rocket_quantity, remainingSpots[0]);
            remainingSpots.Remove(remainingSpots[0]);
        }
        if (results.Contains(5))
        {
            LOOT_bombs_quantity = Random.Range(1, 4);
            LOOT_bombs_icon.SetActive(true);
            LOOT_bombs_icon.transform.position = startPosition + new Vector2(0, 0.5f);
            LOOT_bombs_script.StartMovement(LOOT_bombs_quantity, remainingSpots[0]);
            remainingSpots.Remove(remainingSpots[0]);
        }
        if (results.Contains(6))
        {
            LOOT_caltrops_quantity = Random.Range(1, 4);
            LOOT_caltrops_icon.SetActive(true);
            LOOT_caltrops_icon.transform.position = startPosition + new Vector2(0, 0.5f);
            LOOT_caltrops_script.StartMovement(LOOT_caltrops_quantity, remainingSpots[0]);
            remainingSpots.Remove(remainingSpots[0]);
        }
        if (results.Contains(7))
        {
            LOOT_flamethrower_quantity = Random.Range(1, 4);
            LOOT_flamethrower_icon.SetActive(true);
            LOOT_flamethrower_icon.transform.position = startPosition + new Vector2(0, 0.5f);
            LOOT_flamethrower_script.StartMovement(LOOT_flamethrower_quantity, remainingSpots[0]);
            remainingSpots.Remove(remainingSpots[0]);
        }







    }
    public void LootMoney() {
        PlayerPrefs.SetInt(moneyInInventory, PlayerPrefs.GetInt(moneyInInventory, 0) + LOOT_money_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void LootFuel() {
        // can't go over 100% full
        float temp = PlayerPrefs.GetFloat(fuelInInventory, 0) + LOOT_fuel_quantity;
        if (temp >= 100) {
            temp = 100;
        }
        PlayerPrefs.SetFloat(fuelInInventory, temp);
        fuelGaugeScript.AddFuelToGauge(LOOT_fuel_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void LootNitrous() {
        // can't go over 100% full
        float temp = PlayerPrefs.GetFloat(nitrousInInventory, 0) + LOOT_nitrous_quantity;
        if (temp >= 100) {
            temp = 100;
        }
        PlayerPrefs.SetFloat(nitrousInInventory, temp);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void AnimateNitrous(float amount) {
        //float amount = PlayerPrefs.GetFloat(nitrousInInventory);
        // animate here, just like with fuel                *************************************

        Debug.Log("inside AnimateNitrous(), and amount = " + amount);
        
        if (amount <= 0)
        {
            nitrousBottle_animator.SetBool("nitrous_level0", true);
            nitrousBottle_animator.SetBool("nitrous_level1", false);
            nitrousBottle_animator.SetBool("nitrous_level2", false);
            nitrousBottle_animator.SetBool("nitrous_level3", false);
            nitrousBottle_animator.SetBool("nitrous_level4", false);
        }
        else if (amount > 0 && amount < 30)
        {
            nitrousBottle_animator.SetBool("nitrous_level0", false);
            nitrousBottle_animator.SetBool("nitrous_level1", true);
            nitrousBottle_animator.SetBool("nitrous_level2", false);
            nitrousBottle_animator.SetBool("nitrous_level3", false);
            nitrousBottle_animator.SetBool("nitrous_level4", false);
        }
        else if (amount >= 30 && amount < 70)
        {
            nitrousBottle_animator.SetBool("nitrous_level0", false);
            nitrousBottle_animator.SetBool("nitrous_level1", false);
            nitrousBottle_animator.SetBool("nitrous_level2", true);
            nitrousBottle_animator.SetBool("nitrous_level3", false);
            nitrousBottle_animator.SetBool("nitrous_level4", false);
        }
        else if (amount >= 70 && amount < 100)
        {
            nitrousBottle_animator.SetBool("nitrous_level0", false);
            nitrousBottle_animator.SetBool("nitrous_level1", false);
            nitrousBottle_animator.SetBool("nitrous_level2", false);
            nitrousBottle_animator.SetBool("nitrous_level3", true);
            nitrousBottle_animator.SetBool("nitrous_level4", false);
        }
        else if (amount >= 100)
        {
            nitrousBottle_animator.SetBool("nitrous_level0", false);
            nitrousBottle_animator.SetBool("nitrous_level1", false);
            nitrousBottle_animator.SetBool("nitrous_level2", false);
            nitrousBottle_animator.SetBool("nitrous_level3", false);
            nitrousBottle_animator.SetBool("nitrous_level4", true);
        }
    }
    public void LootScrapMetal() {
        PlayerPrefs.SetInt(scrapmetalInInventory, PlayerPrefs.GetInt(scrapmetalInInventory, 0) + LOOT_scrapMetal_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void LootElectronics() {
        PlayerPrefs.SetInt(electronicsInInventory, PlayerPrefs.GetInt(electronicsInInventory, 0) + LOOT_electronics_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void LootBullets() {
        PlayerPrefs.SetInt(bulletsInInventory, PlayerPrefs.GetInt(bulletsInInventory, 0) + LOOT_bullets_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void LootRocket() {
        PlayerPrefs.SetInt(rocketsInInventory, PlayerPrefs.GetInt(rocketsInInventory, 0) + LOOT_rocket_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void LootBombs() {
        PlayerPrefs.SetInt(bombsInInventory, PlayerPrefs.GetInt(bombsInInventory, 0) + LOOT_bombs_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void LootCaltrops() {
        PlayerPrefs.SetInt(caltropsInInventory, PlayerPrefs.GetInt(caltropsInInventory, 0) + LOOT_caltrops_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }
    public void LootFlamethrower() {
        PlayerPrefs.SetInt(flamethrowerInInventory, PlayerPrefs.GetInt(flamethrowerInInventory, 0) + LOOT_flamethrower_quantity);
        ShowLevelUI_ammo_and_inventory_Display();
    }

    //  ************************************************************************************************************************************


    //  ************************************************************************************************************************************

    public void PlayerExplodes()
    {
        explodey.transform.position = playerVehicle.transform.position + new Vector3(-6.6f, -3.95f, 0);
        PlayExplosion();
        //playerVehicle.SetActive(false);
    }
    public void ResolveConflictFavorably()
    {
        // need to work out UI
        //      main menu
        //      levelUI
        //      post-game summary UI
        //      inventory: it's a roguelike so let's keep inventory simple: there's ammo, currency, scraps. When you replace a part the old one is gone.



        // intermediate step before level loads, so that longer animations can play out without obstruction/distraction


        // player randomly shown sign: "Supplies Ahead" or something
        //      button appears to allow player to buy stuff, including fuel, ammo, and convert scrap into weapons & items 
        //      tiny chance that created weapons & items will have good stats
        //      ITEMS: engine, turbo booster, supercharger, transmission, gear assembly, 
        //      what is the point of stats if the only thing that changes is how much damage your attacks do? what stats could there be???
        //      ARMOR rebuild, to allow survival of one attack (that occurs when a puzzle is failed)
        //          so, we could play with the theme, and whatever the armor thing is, it gives you some amount of failed-puzzle survival
        //          e.g., you upgraded the armor on the sides, and you now have +0.48 towards your next hit protection
        //          getting hit takes away 1.0 hit protection



        // STATS
        //      tire quality, as tires degrade over time and you have to buy new ones... better tires degrade slower 
        //      + chance to shoot extra bullets/rockets/whatever
        //      + chance to crit
        //      + 

        //  when a bonus procs, an announcement is made on-screen



        // WEAPONS
        //      flamethrower
        //      grappling hook
        //      chain saw
        //      minigun
        //      cannon
        //      caltrops, spikes of some sort, Czech hedgehogs 
        //      mortars
        //      missiles
        //      classic bombs
        //  when weapon is ineffective against enemy an announcement is made on-screen


        // ITEMS, Loot
        //      some skins unlocked via achievements, others are small-chance random drops
        //      fuel
        //      scrap metal, scrap electronics, explosive 
        //      ammo, parts to make ammo




        // VEHICLES
        //      tank treads
        //      vehicles have states of degradation




        // player gains progress toward BOSS BATTLE
        //      boss battle is a timed mode
        //      it starts when player hits the button that pops up when the bar is full
        //      bar fills when enemies die only




        // the longer a player goes without failing a puzzle, the higher their $$ multiplier gets... thus incentivizing longer play sessions
        //      their "combo" builds


        // after X number of puzzle types are solved, certain things are unlocked for purchase, and the announcement is made in-game
        //      sometimes you have to unlock during every playthrough


        // defeating an enemy rewards you with loot... small amount of scrap for normal enemies with very rare chance for new items
        // beat a boss: at least 1 new items with small chance of high quality
        //      need to work out what the stats might be


        // after a certain weapon type is purchased X number of times, it is unlocked permanently


        // the time allowed for boss battles increases slightly as total number of puzzle solves increases
        //      thus, when starting fresh, your first boss battle will have very little time available



        // MAYBE
        //      player starts with only addition & subtraction... after a certain amount of solves the option to unlock more operators appears as a button
        //      if the player hits the button the game is now harder, $ gains increase, and they get a one-time $ bonus


        // the player is present with a "WARNING: RAVINE AHEAD" sign, and a time-limit appears, and the next puzzle solve makes them accelerate
        // to be able to successfully jump the ravine
        //      failure = too slow and death


        if (enemyInRange == false)
        {
            puzzleSolvesSinceLastEnemy += 1;

            int rando = Random.Range(1, 2);

            if (rando == 1)
            {
                EnemyAppears();
            }

            // or player gathers small amount of resources


            // or enemy rocket appears out of nowhere and player successfully dodges it


        }
        else
        {
            int rando = Random.Range(1, 3);


            // the enemy is here, so kill it and make explosion
           if (rando == 1) {
                SpendRocket();
            }
           else if (rando == 2) {
                EnemyFires_Caltrops(true);
            }


            // or shoot guns

            // or enemy shoots and player dodges



            // different enemies have different most-effective ways to kill them... so the player can toggle between which weapon is active

            // no visible health bar... instead, vehicles become increasingly damaged... there is an invisible health number that dictates progression from one stage of broken-ness to the next




        }
    }
    public void ResolveConflictUNFAVORABLY()
    {
        // when a player's run ends, a summary screen appears, showing what they gained on that run


        if (enemyInRange == false)
        {


            // or rocket comes out of nowhere and player dies, and enemy appears during death sequence


            // or player hits a small object and gets a flat tire, causing vehicle to limp... this is a good outcome because most alternatives are death



            // if the player SKIPS, they don't die, but they lose a lot of money or progress or something
            //      basically, if failing a puzzle has a high chance of permadeath, then Skipping is a better alternative because you won't die (the first time)
            //      SKIP could mean using nitrous to run away, and refilling nitrous is done via purchases or loot



            // if the player QUITS... i don't know... should this be treated as a Pause button, or do we want the games to be played from start to finish in one sitting?
            //      for example, "warning, if you quit all progress will be lost"


        }
        else
        {
            int rando = Random.Range(1, 3);
            if (rando == 1) {
                EnemyFires_Rocket();
            } else if (rando == 2) {
                EnemyFires_Caltrops(false);
            }



            // or enemy fires guns



            // or.... player attacks and misses?



        }
    }

}