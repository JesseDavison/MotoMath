using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clickable : MonoBehaviour
{


    // this script is used for both circles & operators, so we have the following:
    public float valueOfThisCircle_orGoal;
    public int IDnumberOfCircleDataAttachedToThis;



    public string typeOfThisOperator;
    public int IDnumberOfOperatorDataAttachedToThis;


    public bool readyToBeUsed = false;




    private void OnMouseDown()
    {
        //if (gameObject.CompareTag("circle")) {
        //    // send value to PuzzleManager            
        //    PuzzleManager.instance.AcceptClickedCircle(gameObject);
        //}

        //if (gameObject.CompareTag("operator")) {
        //    // send type to GameManager
        //    PuzzleManager.instance.AcceptClickedOperator(gameObject);
        //}
        PuzzleManager.instance.AcceptClickedCircleOrOperator(gameObject);






    }



}




// DOUBLE CLICK
//private void OnMouseOver()
//{
//    if (Input.GetMouseButtonDown(0))
//    {
//        float timeSinceLastClick = Time.time - lastClickTime;       // https://www.youtube.com/watch?v=9pKXXNgCgq8

//        if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
//        {
//            // do double click actions here
//            GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
//            foreach (GameObject goal in goals)
//            {
//                if (goal.GetComponent<Goal>().goalNumber == valueOfThisThing && goal.GetComponent<Goal>().goalFulfilled == false)
//                {
//                    //transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, Time.deltaTime * 2);

//                    // destroy the circle and change the color of the goal to reflect the fact that it's been fulfilled
//                    goal.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.1f, 0.1f, 1f);
//                    // also, make this goal unable to interact with anything
//                    goal.GetComponent<Goal>().goalFulfilled = true;
//                    //GameManager.instance.AddPoints(1);
//                    //Destroy(gameObject);
//                    gameObject.SetActive(false);
//                    GameManager.instance.backButton.SetActive(true);
//                    GameManager.instance.ReduceScore(1);
//                    GameManager.instance.ContributeToKillFeed("goal", goal.GetComponent<Goal>().goalNumber, 0, 0);
//                    gameManager.checkIfLevelIsOver();
//                    GameManager.instance.TakeSnapshot();



//                    break;
//                }
//            }
//        }
//        else
//        {
//            // do normal click actions here
//        }
//        lastClickTime = Time.time;
//    }
//}