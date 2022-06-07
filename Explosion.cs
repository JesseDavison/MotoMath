using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool explosionInMotion = false;
    public Vector3 defaultScale = new Vector3(0.1f, 0.1f, 0.1f);
    //public float maxScale = 12;
    public Vector3 maxScale = new Vector3(12, 12, 12);
    public float speed = 5;
    public float defaultSpeed = 5;
    public float speedMultiplier = 1.1f;
    public float speedCap = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (explosionInMotion) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, maxScale, Time.deltaTime * speed);
            speed *= speedMultiplier;
            if (speed > speedCap) {
                speed = speedCap;
            }

            if (transform.localScale.x > 11.9f) {
                explosionInMotion = false;
                PuzzleManager.instance.AnimatePuzzleSolved(null, true, true);
                gameObject.SetActive(false);

            }
        }
    }


    public void StartExplosion() {
        transform.localScale = defaultScale;
        speed = defaultSpeed;
        gameObject.SetActive(true);
        explosionInMotion = true;
    }

}
