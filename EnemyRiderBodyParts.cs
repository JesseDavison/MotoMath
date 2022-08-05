using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRiderBodyParts : MonoBehaviour
{

    public bool launched = false;
    float time = 0;
    public float speed;
    public float horizontalSpeed;
    public Vector3 endPosition;
    public AnimationCurve curve;
    public float curveMagnifier;
    public float verticalOffset;


    //public float zAxisRotationSpeed;

    //public GameObject BodyPart;

    //public GameObject bombImage_GameObject;
    //public float defaultMidairRotationSpeed = 5;
    public float midairRotationSpeed = 5;

    public Vector3 speedVector;

    public float yPosToBeginRolling = -1.3f;

    public bool thisShouldRoll_andNotBounce = false;





    void FixedUpdate()
    {
        if (launched)
        {
            time += Time.deltaTime * speed;

            Vector3 pos = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * horizontalSpeed);

            float extraYbump = curve.Evaluate(time) * curveMagnifier;
            pos.y = endPosition.y + extraYbump + verticalOffset;

            transform.position = pos;
            //Debug.Log("bomb at: " + transform.position.x + ", " + transform.position.y);

            // rotate slowly in mid-air
            transform.Rotate(0, 0, midairRotationSpeed);




            if (time >= 1) { 
                if (thisShouldRoll_andNotBounce) 
                {
                    launched = false;
                    horizontalSpeed = Random.Range(20, 31);
                    endPosition = new Vector3(-11, -4.01f, 0);
                }
                else
                {
                    time = 0;
                    //float randomX = transform.position.x + Random.Range(-6, -2f);
                    //endPosition = new Vector3(randomX, -4.01f, 0);
                    curveMagnifier = Random.Range(2, 6f);
                    horizontalSpeed = Random.Range(20, 31);
                    endPosition = new Vector3(-11, -4.01f, 0);
                    midairRotationSpeed = Random.Range(25, 60);
                }
            }



            //if (transform.position.y <= yPosToBeginRolling) { 
            //    // decide whether to bounce again, or roll on the ground, based on the boolean

            //    if (thisShouldRoll_andNotBounce) 
            //    {
            //        launched = false;
            //    }
            //    else
            //    {
            //        transform.position = new Vector3(transform.position.x, -3.99f, 0);
            //        time = 0;
            //        float randomX = transform.position.x + Random.Range(-6, -2f);
            //        endPosition = new Vector3(randomX, -4.01f, 0);
            //        curveMagnifier = Random.Range(2, 6f);
            //    }


            //}


            

        }
        else
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * horizontalSpeed);
            transform.position = pos;



            //transform.position = transform.position + speedVector;
            if (thisShouldRoll_andNotBounce) {
                transform.Rotate(0, 0, 55);
            }

        }


        if (transform.position.x < -10)
        {
            gameObject.SetActive(false);
        }





    }


    public void LaunchBodyPart()
    {
        // set sprite rotation to position zero
        transform.rotation = Quaternion.Euler(0, 0, 0);

        float randomX = Random.Range(-6f, 7);
        endPosition = new Vector3(randomX, -4.01f, 0);
        curveMagnifier = Random.Range(5, 12f);
        midairRotationSpeed = Random.Range(2, 10);
        horizontalSpeed = Random.Range(7, 10f);


        gameObject.SetActive(true);

        time = 0;

        launched = true;
        //Debug.Log("body part position: " + transform.position.x + ", " + transform.position.y + " (" + gameObject.name + ")");
    }


}
