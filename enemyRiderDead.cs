using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRiderDead : MonoBehaviour
{

    public bool launched = false;
    float time = 0;
    public float speed;
    public float horizontalSpeed;
    public Vector3 endPosition;
    public AnimationCurve curve;
    public float curveMagnifier;
    public float verticalOffset;

    string playerVehicle;

    string target;

    //public GameObject player;
    bool isThePlayerMoving;

    //public float zAxisRotationSpeed;

    public GameObject enemyRiderSprite;

    //public GameObject bombImage_GameObject;
    public float defaultMidairRotationSpeed = 5;
    float midairRotationSpeed = 5;

    public Vector3 speedVector;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            enemyRiderSprite.transform.Rotate(0, 0, midairRotationSpeed);

        


            if (transform.position.y <= -1.3f)
            {

                launched = false;
                transform.position = new Vector3(transform.position.x, transform.position.y, 2);

                // should land face-down on ground and "stick" to the ground, i.e. have speed of -40


            }

        }
        else
        {
            transform.position = transform.position + speedVector;
        }


        if (transform.position.x < -5) {
            gameObject.SetActive(false);
        }





    }

    public void LaunchRider() {
        // set sprite rotation to position zero
        enemyRiderSprite.transform.rotation = Quaternion.Euler(0, 0, 0);

        gameObject.SetActive(true);

        time = 0;

        launched = true;
    }


}
