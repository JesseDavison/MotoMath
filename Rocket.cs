using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public float speed = 1;
    public float defaultSpeed = 1;
    public float speedMultiplier = 1;

    public float targetXPOS;
    //public Vector2 targetPosition;

    public GameObject target;
    public GameObject player;

    public float hoverDelay;
    //public Vector2 hovering_positionToHoverTo;
    public float hovering_XpositionToHoverTo;
    public float horizontalHoverDistance;
    public bool hoverIsFinished = false;

    public float verticalHoverDistance;
    public float originalYPosition;
    public float verticalHoverYPosition;
    public float verticalHoverSpeed;
    public bool hoverUpward = false;
    public bool hoverIsComplete = false;

    public int whichEdgeOfScreen;   // either 21 or -6
    public bool shootingAtEnemy = true;


    // Start is called before the first frame update
    void Start()
    {
        //targetPosition = new Vector2(30, transform.position.y);     // the rocket will overshoot, but disappear in the explosion
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hoverIsComplete == false) 
        {
            // move the hover up/down
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, verticalHoverYPosition), verticalHoverSpeed);

            if (Mathf.Abs(transform.position.y - verticalHoverYPosition) < 0.05f)
            {
                hoverIsComplete = true;

            }
            // hover backwards
            transform.position = Vector2.Lerp(transform.position, new Vector2(hovering_XpositionToHoverTo, transform.position.y), Time.deltaTime * speed);


        } 
        else 
        {

            transform.position = Vector2.Lerp((Vector2)transform.position, new Vector2(whichEdgeOfScreen, originalYPosition), Time.deltaTime * speed);
            //transform.position = Vector2.Lerp((Vector2)transform.position, new Vector2(30, transform.position.y), Time.deltaTime * speed);
            speed *= speedMultiplier;


            if (shootingAtEnemy == true) {
                targetXPOS = target.transform.position.x;       // this allows the enemy to move around and still have the missile "hit" at the right spot/time

                if (transform.position.x > targetXPOS)
                {
                    GameManager.instance.EnemyExplodes();
                    gameObject.SetActive(false);

                }

            } else {
                targetXPOS = player.transform.position.x;
                if (transform.position.x < targetXPOS) {
                    GameManager.instance.PlayerExplodes(true);
                    gameObject.SetActive(false);
                }
            }




        }





    }


    public void LaunchRocket() {
        speed = defaultSpeed;
        //hovering_positionToHoverTo = new Vector2(transform.position.x - hoverDistance, transform.position.y);
        hovering_XpositionToHoverTo = transform.position.x - horizontalHoverDistance;
        originalYPosition = transform.position.y;

        hoverIsComplete = false;
        int rando = Random.Range(1, 3);
        verticalHoverDistance = Random.Range(0.2f, 1f);
        if (rando == 1) {
            verticalHoverYPosition = originalYPosition - verticalHoverDistance;
        } else {
            verticalHoverYPosition = originalYPosition + verticalHoverDistance;
        }
        whichEdgeOfScreen = 21;
        shootingAtEnemy = true;
        //transform.GetChild(0).transform.rotation = new Vector3(0, 0, 0);
        //var rotationVector = transform.rotation.eulerAngles;
        //rotationVector.y = 0;
        //transform.rotation = Quaternion.Euler(rotationVector);
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;

    }

    public void LaunchRocketBackward() {
        speed = defaultSpeed;
        hovering_XpositionToHoverTo = transform.position.x + horizontalHoverDistance;
        originalYPosition = transform.position.y;

        hoverIsComplete = false;
        int rando = Random.Range(1, 3);
        verticalHoverDistance = Random.Range(0.2f, 1f);
        if (rando == 1) {
            verticalHoverYPosition = originalYPosition - verticalHoverDistance;
        } else {
            verticalHoverYPosition = originalYPosition + verticalHoverDistance;
        }
        whichEdgeOfScreen = -6;
        shootingAtEnemy = false;
        //var rotationVector = transform.rotation.eulerAngles;
        //rotationVector.y = 180;
        //transform.rotation = Quaternion.Euler(rotationVector);
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
    }



    //IEnumerator HoverBeforeMoving()
    //{
    //    yield return new WaitForSeconds(hoverDelay);
    //    //Debug.Log("waiting for " + delay + " seconds");
    //    hoverIsComplete = true;
    //    //Debug.Log("launching NOW");

    //}



}
