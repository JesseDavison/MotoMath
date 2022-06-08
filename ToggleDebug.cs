using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDebug : MonoBehaviour
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


    void ToggleValueChangeOccured(Toggle tglValue) {
        //Debug.Log("current value: " + tglValue.isOn + "   " + thisToggle.name);

        if (tglValue.isOn) { // this will be attached to the ON toggle button, so in this function put all the stuff when DEBUG IS ON = no random ordering of circles/operators
            PuzzleManager.instance.TurnDebugModeON();
        } else if (tglValue.isOn == false) {
            PuzzleManager.instance.TurnDebugModeOFF();
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

}
