using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBounce : MonoBehaviour
{

    public float time = 0;
    public float angleModifier = 2;
    public float bumpSpeed = 1f;

    [Header("Tweak the following:")]

    //public float durationOfBump_1 = 1.6f;
    //public float durationOfBump_2 = 1.6f;

    public float delayBeforeStartingBumps = 0.4f;

    public float angleModifier_bump1 = 5;
    public float angleModifier_bump2 = 3;
    public float speed_bump1 = 0.1f;
    public float speed_bump2 = 0.06f;
    //public float timeIncrementer_bump2 = 0.06f;


    //float default_angleModifier = 1f;
    //float default_timeIncrementer = 0.05f;

    public bool doingBump_1 = false;
    public bool doingBump_2 = false;
    //public bool movingToOriginalPosition = false;


    // Start is called before the first frame update
    void Start()
    {
        //default_angleModifier = angleModifier;
        //default_timeIncrementer = timeIncrementer;
    }

    
    void FixedUpdate()
    {
        if (doingBump_1) {
            float angleNudge = angleModifier * Mathf.Abs(Mathf.Sin(time));
            transform.rotation = Quaternion.Euler(0, 0, 6.78f + angleNudge);

            time += bumpSpeed;
            if (time >= 3.14159f)
            {
                doingBump_1 = false;
                angleModifier = angleModifier_bump2;
                bumpSpeed = speed_bump2;
                doingBump_2 = true;

            }
        } 
        else if (doingBump_2)
        {
            float angleNudge = angleModifier * Mathf.Abs(Mathf.Sin(time));
            transform.rotation = Quaternion.Euler(0, 0, 6.78f + angleNudge);

            time += bumpSpeed;
            if (time >= 6.28318f) {
                doingBump_2 = false;

            }
        }
        
        
        
        
        
        //else if (movingToOriginalPosition) {
        //    //float currentRotationZ = transform.rotation.z;
        //    //Debug.Log("currentRotationZ: " + currentRotationZ);
        //    transform.rotation = Quaternion.Euler(0, 0, 6.78f - 0.0001f);
        //    Debug.Log("Rot rot rotation: " + transform.rotation.z);

        //    if (transform.rotation.z <= 0.05913217f) {
        //        Debug.Log("done rotating");
        //        movingToOriginalPosition = false;
        //        transform.rotation = Quaternion.Euler(0, 0, 6.78f);
        //        //doingBumps = false;
        //    }
        //}


    }
    public void MakeItBump()
    {
        
        StartCoroutine(StartBump());
    }

    IEnumerator StartBump() {
        time = 0;
        //transform.rotation = Quaternion.Euler(0, 0, 6.78f);
        angleModifier = angleModifier_bump1;
        bumpSpeed = speed_bump1;

        yield return new WaitForSeconds(delayBeforeStartingBumps);
        doingBump_1 = true;


        //yield return new WaitForSeconds(durationOfBump_1);
        //angleModifier = angleModifier_bump2;
        //bumpSpeed = speed_bump2;



        //yield return new WaitForSeconds(durationOfBump_2);
        //EndTheBumps();

    }

    public void EndTheBumps() {
        //angleModifier = default_angleModifier;
        //timeIncrementer = default_timeIncrementer;
        doingBump_1 = false;
        //transform.rotation = Quaternion.Euler(0, 0, 6.78f);
        //movingToOriginalPosition = true;
    }

    public void EnableUseOfNOS() {
        GameManager.instance.EnableUseOfNOS();
    }

}


//var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
//transform.position = defaultPosition + offset;