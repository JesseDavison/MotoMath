using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;      // allows us to do advanced queries on selections

public class RadioButtonsDebugMode : MonoBehaviour
{
    ToggleGroup toggleGroup;
    Toggle on;
    Toggle off;


    


    // Start is called before the first frame update
    void Start()
    {
        toggleGroup.GetComponent<ToggleGroup>();
        on.GetComponent<Toggle>();
        off.GetComponent<Toggle>();
        //off.onValueChanged.AddListener(DebugModeOFF);
        //off.OnSelect(off)
    }

    

    public void ToggleClicked() {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
    }

    void DebugModeOFF() { 

    }
}
