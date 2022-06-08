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

    bool readyToFadeColorFromGreenToBlack = false;
    public float fadeSpeed = 1;
    Color originalColor;
    Color greenColor;

    float t = 0;

    private void Start()
    {
        originalColor = Color.black;
        greenColor = Color.green;
    }

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
        if (timeValue <= 0) {
            GameManager.instance.DisplayGameOver(true, false);
        }


    }
    private void FixedUpdate()
    {
        if (readyToFadeColorFromGreenToBlack) {
            //Color currentColor = timerText.color;
            //float fadeAmount = currentColor.a - (fadeSpeed * Time.deltaTime);
            t += Time.deltaTime / 1.5f; // Divided by 5 to make it 5 seconds.
            timerText.color = Color.Lerp(greenColor, originalColor, t);
            
            if (timerText.color == originalColor) {
                readyToFadeColorFromGreenToBlack = false;
                t = 0;
            }
        }

    }
    void DisplayTime(float timeToDisplay) { 
        if (timeToDisplay < 0) {
            timeToDisplay = 0;
        }



        float milliseconds = timeToDisplay % 1 * 1000;

        if (timeToDisplay > 10) {
            timerText.text = "" + string.Format("{0:00}", timeValue);
        } else {
            timerText.text = "" + string.Format("{0:00}.{1:000}", timeValue, milliseconds);
        }
    }

    public void AddToGlobalTimer(float amount) {
        readyToFadeColorFromGreenToBlack = false;   // if the color is already changing, stop it
        timerText.color = greenColor;
        timeValue += amount;

        // now fade the text color back to black
        readyToFadeColorFromGreenToBlack = true;

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

