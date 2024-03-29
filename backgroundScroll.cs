using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroll : MonoBehaviour
{
    // gonna try https://www.youtube.com/watch?v=P3hcopOkpa8

    // also used https://forum.unity.com/threads/changing-boxcollider2d-size-to-match-sprite-bounds-at-runtime.267964/

    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    public float width;
    Vector2 vector;

    public float zoomMultiplier;

    public float speed = -3;
    public float defaultSpeed;
    public float startSpot;
    public float resetSpot;

    public float actualXpos;

    Vector3 defaultPosition;

    public bool foregroundObject = false;
    public bool randomized = false;
    public float randomizedMultiplier = 1;

    float defaultZposition;
    //float defaultYposition;

    bool readyToAdjust = false;
    public GameObject companionImage;

    public float tweakThis;


    // Start is called before the first frame update
    void Start()
    {
        defaultZposition = transform.position.z;
        //defaultYposition = transform.position.y;
        defaultPosition = new Vector3(startSpot, transform.position.y, transform.position.z);

        Vector2 sizeOfSprite = GetComponent<SpriteRenderer>().sprite.bounds.size;

        boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.size = sizeOfSprite;


        rb = GetComponent<Rigidbody2D>();

        if (!foregroundObject) {
            width = boxCollider.size.x * zoomMultiplier;
            //Debug.Log("the width is " + width);
        }

        rb.velocity = new Vector2(speed, 0);

        //width -= 0.06024f / 2f;      // put this in because i'm seeing a tiny gap between images

        vector = new Vector2(width * 2f - tweakThis, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -width) {
            Reposition();
            readyToAdjust = true;
        }

        rb.velocity = new Vector2(speed, 0);

        if (readyToAdjust) {
            float gap = Vector2.Distance(transform.position, companionImage.transform.position);
            if (gap > width) {
                //Debug.Log("before change, gap: " + gap + ", name: " + gameObject.name);
                Vector3 adjustmentVector = new Vector3(gap - width, 0, 0);
                transform.position = transform.position - adjustmentVector;
                //Debug.Log("adjustment made. Gap: " + gap + " width: " + width + ", difference: " + (gap - width));
            }
            //gap = Vector2.Distance(transform.position, companionImage.transform.position);
            //Debug.Log("after change, gap: " + gap + ", name: " + gameObject.name);

            readyToAdjust = false;
        }

        // occasionally make sure the distance between the images is the same as at the beginning









        //actualXpos = transform.position.x;

        //transform.position += new Vector3(-1 * speed, 0, 0);

        //if (transform.position.x <= resetSpot) {
        //    transform.position = defaultPosition;
        //    if (randomized) {
        //        int rando = Random.Range(1, 400);
        //        transform.position = new Vector3(20 + rando, transform.position.y, transform.position.z);
        //    }
        //}
    }

    void Reposition() {
        //Debug.Log("reposition called");
        if (randomized)
        {
            randomizedMultiplier = Random.Range(0.5f, 4f);
        }
        else
        {
            randomizedMultiplier = 1;
        }
        //Debug.Log("position before Reposition: " + transform.position.x);
        transform.position = (Vector2)transform.position + (vector) * randomizedMultiplier;
        transform.position = new Vector3(transform.position.x, transform.position.y, defaultZposition);
        //Debug.Log("position after Reposition: " + transform.position.x);
    }

    public void SetSpeedToZero() {
        speed = 0;
    }
    public void RestoreSpeed() {
        speed = defaultSpeed;
    }

}
