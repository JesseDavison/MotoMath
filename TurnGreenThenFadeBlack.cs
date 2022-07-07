using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnGreenThenFadeBlack : MonoBehaviour
{
    //TextMeshPro textReference;
    public TextMeshProUGUI textReference;

    bool readyToFadeColorFromGreenToBlack = false;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (readyToFadeColorFromGreenToBlack)
        {
            //Color currentColor = timerText.color;
            //float fadeAmount = currentColor.a - (fadeSpeed * Time.deltaTime);
            t += Time.deltaTime / 1.5f; // Divided by 5 to make it 5 seconds.
            textReference.color = Color.Lerp(Color.green, Color.black, t);

            if (textReference.color == Color.black)
            {
                readyToFadeColorFromGreenToBlack = false;
                t = 0;
            }
        }
    }

    public void TurnGreenThenFade() {

        textReference.color = Color.green;
        readyToFadeColorFromGreenToBlack = true;
    }

}
