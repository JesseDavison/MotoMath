using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOpacityToZero : MonoBehaviour
{
    bool fading = false;

    SpriteRenderer thisSpriteRenderer;
    public float amountToReduceByPerTick;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fading == true) {
            Color currentColor = thisSpriteRenderer.color;
            currentColor.a -= amountToReduceByPerTick;

            if (currentColor.a <= 0) {
                fading = false;
            }

            thisSpriteRenderer.color = currentColor;

        }
        
    }



    public void BeginFadingOpacityToZero() {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        fading = true;
    }
}
