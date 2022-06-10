using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleExponents : MonoBehaviour
{
    // https://www.youtube.com/watch?v=pq_BOf25IF0
    public Toggle thisToggle;

    // Start is called before the first frame update
    void Start()
    {
        thisToggle = GetComponent<Toggle>();

        thisToggle.onValueChanged.AddListener(delegate
        {
            ToggleValueChangeOccured(thisToggle);
        });
    }


    void ToggleValueChangeOccured(Toggle tglValue)
    {
        //Debug.Log("current value: " + tglValue.isOn + "   " + thisToggle.name);

        if (tglValue.isOn)
        { // this will be attached to the ON toggle button,
          // so.... when negatives
            PuzzleManager.instance.TurnExponentsON();
        }
        else if (tglValue.isOn == false)
        {
            PuzzleManager.instance.TurnExponentsOFF();
        }
    }


}
