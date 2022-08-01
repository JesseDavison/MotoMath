using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    bool launched = false;
    float time = 0;
    public float speed;
    public float horizontalSpeed;
    public Vector3 endPosition;
    public AnimationCurve curve;
    public float curveMagnifier;
    public float verticalOffset;

    string playerVehicle;

    string target;

    public GameObject player;
    bool isThePlayerMoving;

    public float zAxisRotationSpeed;

    public GameObject bombImage_GameObject;
    public float defaultMidairRotationSpeed = 5;
    float midairRotationSpeed = 5;


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
        if (launched) {
            time += Time.deltaTime * speed;

            Vector3 pos = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * horizontalSpeed);

            float extraYbump = curve.Evaluate(time) * curveMagnifier;
            pos.y = endPosition.y + extraYbump + verticalOffset;

            transform.position = pos;
            //Debug.Log("bomb at: " + transform.position.x + ", " + transform.position.y);

            // rotate slowly in mid-air
            bombImage_GameObject.transform.Rotate(0, 0, midairRotationSpeed);

            if (target == "player") {


                if (transform.position.x <= player.transform.position.x + 0.4f && transform.position.y <= 1.3f)
                {
                    // play explosion
                    //Debug.Log("about to explode bomb, y pos at: " + transform.position.y);
                    gameObject.SetActive(false);
                    launched = false;

                    if (isThePlayerMoving == true) {
                        GameManager.instance.PlayerExplodes(true);
                    } else {
                        GameManager.instance.PlayerExplodes(false);
                    }


                    // activate end of game summary screen
                    GameManager.instance.DisplayGameOver(false, true);


                }
                else if (transform.position.y <= 0.51f)
                {
                    // start rolling 
                    bombImage_GameObject.transform.Rotate(0, 0, 55);
                    //transform.GetChild(0).transform.Rotate(0, 0, 50);
                }
            }
            else if (target == "enemy") 
            {
                if (transform.position.x >= endPosition.x - 1.5f && transform.position.y <= 1.3f)
                {
                    gameObject.SetActive(false);
                    launched = false;
                    GameManager.instance.EnemyExplodes();

                }
                else if (transform.position.y <= 0.51f) {
                    // begin rolling forward
                    transform.GetChild(0).transform.Rotate(0, 0, zAxisRotationSpeed);
                }
            }
                
                



        }






    }
    public void LaunchBomb_EnemyToPlayer(string playerVehicleType, bool playerIsMoving) {
        isThePlayerMoving = playerIsMoving;
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        midairRotationSpeed = -defaultMidairRotationSpeed;
        target = "player";
        gameObject.SetActive(true);
        //Debug.Log("bomb launnnnnn, and y pos is " + transform.position.y);
        launched = true;
        endPosition = new Vector3(-2, 0.5f, 2);
        playerVehicle = playerVehicleType;
        time = 0;

    }
    public void LaunchBomb_PlayerToEnemy(Vector3 enemyPosition) {
        bombImage_GameObject.transform.rotation = Quaternion.Euler(0, 0, -40);
        midairRotationSpeed = -defaultMidairRotationSpeed;
        //transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, -40);
        target = "enemy";
        launched = true;
        endPosition = new Vector3(enemyPosition.x, 0.5f, 2);
        time = 0;
    }
}
