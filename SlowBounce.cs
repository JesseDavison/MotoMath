using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBounce : MonoBehaviour
{

    float time = 0;
    public float angleModifier = 2;
    public float timeIncrementer = 1f;
    public float durationOfSmallBump = 1.6f;
    public float delayBeforeBump = 0.4f;
    public float angleModifier_bump = 5;
    public float timeIncrementer_bump = 0.1f;

    float default_angleModifier = 1f;
    float default_timeIncrementer = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        //default_angleModifier = angleModifier;
        //default_timeIncrementer = timeIncrementer;
    }

    
    void FixedUpdate()
    {
        float angleNudge = angleModifier * Mathf.Abs(Mathf.Sin(time));
        transform.rotation = Quaternion.Euler(0, 0, 6.78f + angleNudge);

        time += timeIncrementer;
        if (time >= 6.283185f) {
            time = 0;
        }

    }
    public void MakeItBump()
    {
        
        StartCoroutine(StartBump());
    }

    IEnumerator StartBump() {
        yield return new WaitForSeconds(delayBeforeBump);
        transform.rotation = Quaternion.Euler(0, 0, 6.78f);
        angleModifier = angleModifier_bump;
        timeIncrementer = timeIncrementer_bump;

        yield return new WaitForSeconds(durationOfSmallBump);
        EndTheBump();

    }

    public void EndTheBump() {
        angleModifier = default_angleModifier;
        timeIncrementer = default_timeIncrementer;
    }

}


//var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
//transform.position = defaultPosition + offset;