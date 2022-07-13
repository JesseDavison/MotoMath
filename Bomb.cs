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

    public float zAxisRotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (launched) {
            time += Time.deltaTime * speed;

            Vector2 pos = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * horizontalSpeed);

            float extraYbump = curve.Evaluate(time) * curveMagnifier;
            pos.y = endPosition.y + extraYbump + verticalOffset;

            transform.position = pos;
            //Debug.Log("bomb at: " + transform.position.x + ", " + transform.position.y);

            if (target == "player") {


                if (transform.position.x <= player.transform.position.x + 0.4f && transform.position.y <= 1.3f)
                {
                    // play explosion
                    //Debug.Log("about to explode bomb, y pos at: " + transform.position.y);
                    gameObject.SetActive(false);
                    launched = false;

                    GameManager.instance.PlayerExplodes();

                    //GameManager.instance.BombExplodes();

                    //// turn player into destroyed version
                    //if (playerVehicle == "bike") {
                    //    player.SetActive(false);

                    //    asdf

                    //    player.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false); // turn off normal version
                    //    player.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true); // turn on destroyed version
                    //}



                    // activate end of game summary screen
                    GameManager.instance.DisplayGameOver(false, true);

                }
                else if (transform.position.y <= 0.51f)
                {
                    // start rolling 
                    transform.GetChild(0).transform.Rotate(0, 0, 50);
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
    public void LaunchBomb_EnemyToPlayer(string playerVehicleType) {
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        target = "player";
        gameObject.SetActive(true);
        //Debug.Log("bomb launnnnnn, and y pos is " + transform.position.y);
        launched = true;
        endPosition = new Vector2(-2, 0.5f);
        playerVehicle = playerVehicleType;
        time = 0;

    }
    public void LaunchBomb_PlayerToEnemy(Vector3 enemyPosition) {
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        target = "enemy";
        launched = true;
        endPosition = new Vector3(enemyPosition.x, 0.5f, 0);
        time = 0;
    }
}
