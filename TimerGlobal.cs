using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerGlobal : MonoBehaviour
{
    public float timeValue = 90;
    public float defaultTimeValue = 90;
    //public Text timerText;
    public TextMeshProUGUI timerText;
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

    void DisplayTime(float timeToDisplay) { 
        if (timeToDisplay < 0) {
            timeToDisplay = 0;
        }



        float milliseconds = timeToDisplay % 1 * 1000;

        timerText.text = "" + string.Format("{0:00}.{1:000}", timeValue, milliseconds);
    }

    public void AddToGlobalTimer(float amount) {

        timeValue += amount;
    }
    public void SubtractFromGlobalTimer(float amount) {

        timeValue -= amount;
    }
    public void PauseGlobalTimer() {

        timerPaused = true;
    }
    public void UnpauseGlobalTimer() {

        timerPaused = false;
    }
    public float GetTimeRemaining() {

        return timeValue;
    }
    public void ResetTimeRemaining() {
        timeValue = defaultTimeValue;
    }

}

