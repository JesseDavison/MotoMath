using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerPuzzle : MonoBehaviour
{
    public float defaultTimeValue = 20;
    float timeValue = 20;
    //public Text timerText;
    //public GameObject globalTimer;
    public TextMeshProUGUI puzzleTimerText;

    bool timerPaused = true;


    // Update is called once per frame
    void Update()
    {
        if (timerPaused == false) {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
            }

            DisplayTime(timeValue);
        }


    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float milliseconds = timeToDisplay % 1 * 1000;

        if (timeToDisplay > 3) {
            puzzleTimerText.text = "Bonus time: " + string.Format("{0:00}", timeValue);
        } else {
            puzzleTimerText.text = "Bonus time: " + string.Format("{0:00}.{1:000}", timeValue, milliseconds);
        }

    }

    public float GetPuzzleTimeRemaining() {
        return timeValue;
    }
    public void ResetPuzzleTimer() {
        timeValue = defaultTimeValue;
    }
    public void PausePuzzleTimer() {
        timerPaused = true;
    }
    public void UnpausePuzzleTimer() {
        timerPaused = false;
    }


}

