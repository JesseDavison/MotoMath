using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootAnimationScript : MonoBehaviour
{
    public string whatIsThisThing;      // set this in the interface so that when the animation is finished the levelUI shows correct inventory levels


    public bool movingToFirstSpot = false;
    public float firstSpeed = 1;
    public float firstSpeedMultiplier = 1;
    public float firstSpeedDefault = 1;
    //public Vector2 startSpot;
    public Vector2 firstSpot;


    public float timeToWait;

    TextMeshPro quantityText;
    int quantity;

    public bool movingToSecondSpot = false;
    public float secondSpeed = 1;
    public float secondSpeedMultiplier = 1;
    public float secondSpeedDefault = 1;
    public Vector2 secondSpot;


    public bool fadingAway = false;
    SpriteRenderer spriteRenderer;
    public float fadeSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        quantityText = transform.GetChild(1).GetComponent<TextMeshPro>();
        quantityText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (movingToFirstSpot) {
            transform.position = Vector2.MoveTowards(transform.position, firstSpot, Time.deltaTime * firstSpeed);
            if (Vector2.Distance(transform.position, firstSpot) < 0.01f) {
                movingToFirstSpot = false;
                quantityText.text = "+" + quantity;
                StartCoroutine(WaitAMoment());
            }

            // now pause for a moment before depositing loot


        }
        else if (movingToSecondSpot) {
            transform.position = Vector2.MoveTowards(transform.position, secondSpot, Time.deltaTime * secondSpeed);
            if (Vector2.Distance(transform.position, secondSpot) < 0.01f)
            {
                movingToSecondSpot = false;
                fadingAway = true;
            }

        } else if (fadingAway) {
            Color currentColor = spriteRenderer.color;
            float fadeAmount = currentColor.a - (fadeSpeed * Time.deltaTime);
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, fadeAmount);

            if (currentColor.a < 0.01f) {
                fadingAway = false;
                quantityText.text = "";
                gameObject.SetActive(false);
                spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
                GameManager.instance.LootRocket();
                GameManager.instance.ShowLevelUI_ammo_and_inventory_Display();
            }

        }




    }

    IEnumerator WaitAMoment() {
        yield return new WaitForSeconds(timeToWait);
        movingToSecondSpot = true;
    }

    public void StartMovement(int quan) {
        //quantityText.text = "";       // for some reason uncommenting this produces an error
        quantity = quan;
        movingToFirstSpot = true;
    }

}
