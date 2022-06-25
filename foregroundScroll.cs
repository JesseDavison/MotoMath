using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foregroundScroll : MonoBehaviour
{
    // gonna try https://www.youtube.com/watch?v=P3hcopOkpa8

    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    public float width;
    Vector2 vector;

    public float zoomMultiplier;

    public float speed = -3;
    public float startSpot;
    public float resetSpot;

    public float actualXpos;

    Vector3 defaultPosition;

    public bool randomized = false;

    float defaultZposition;
    //float defaultYposition;

    // Start is called before the first frame update
    void Start()
    {
        defaultZposition = transform.position.z;
        //defaultYposition = transform.position.y;
        defaultPosition = new Vector3(startSpot, transform.position.y, transform.position.z);
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        width = boxCollider.size.x * zoomMultiplier;
        rb.velocity = new Vector2(speed, 0);

        vector = new Vector2(width * 2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -width)
        {
            Reposition();
        }

        rb.velocity = new Vector2(speed, 0);

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

    void Reposition()
    {
        //Debug.Log("reposition called");
        transform.position = (Vector2)transform.position + vector;
        transform.position = new Vector3(transform.position.x, transform.position.y, defaultZposition);
    }
}
