using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject offRoadMarkerLower;
    public GameObject offRoadMarkerHigher;
    public float moveBackSpeedX = 1.0f;

    private Rigidbody2D rb;
    private GameManager gameManager;

    private float verticalSpeed = 0.2f;
    private float startingX;
    /* private bool bouncing = false; */
    /* private Vector2 additionVelocity; */

    void Start()
    {
        gameManager = GameManager.Get();
        rb = GetComponent<Rigidbody2D>();
        startingX = transform.position.x;
    }

    void Update() 
    {
        if (
            (transform.position.y <= offRoadMarkerLower.transform.position.y ||
            transform.position.y >= offRoadMarkerHigher.transform.position.y) &&
            gameManager.GetHealth() != 0
        )
        {
            Debug.Log("owie");
            gameManager.SetHealth(0);
        }
    }

    void FixedUpdate()
    {
        float inputAxisY = Input.GetAxisRaw("Vertical");
        rb.MovePosition(new Vector2(
            transform.position.x,
            transform.position.y + inputAxisY * verticalSpeed
        ));

        if (Mathf.Abs(transform.position.x - startingX) >= 0.1f) 
        {
            Vector2 targetPosition = new Vector2(startingX, transform.position.y);
            Vector2 sourcePosition = new Vector2(transform.position.x, transform.position.y);
            float direction = (startingX - transform.position.x) / Mathf.Abs(startingX - transform.position.x);
            rb.MovePosition(sourcePosition - targetPosition + Vector2.right * moveBackSpeedX * direction);
        }

        /* if (Mathf.Abs(transform.position.x - startingX) >= 0.1f && !bouncing) */
        /* { */
            /* StartCoroutine(BounceBack()); */
        /* } */
        /* if (bouncing) */
        /* { */
        /*     rb.MovePosition(additionVelocity); */
        /* } */
    }

    void OnCollisionEnter2D() {
        Debug.Log("owie");
    }

    /* IEnumerator BounceBack() */
    /* { */
    /*     bouncing = true; */
    /*     yield return new WaitForSeconds(0.5f); */
    /*     while (Mathf.Abs(transform.position.x - startingX) >= 0.1f) */ 
    /*     { */
    /*         Vector2 targetPosition = new Vector2(startingX, transform.position.y); */
    /*         Vector2 sourcePosition = new Vector2(transform.position.x, transform.position.y); */
    /*         float direction = (startingX - transform.position.x) / Mathf.Abs(startingX - transform.position.x); */

    /*         additionVelocity = sourcePosition - targetPosition + Vector2.right * moveBackSpeedX * direction; */
    /*         yield return new WaitForEndOfFrame(); */
    /*     } */
    /*     additionVelocity = new Vector2(); */
    /*     bouncing = false; */
    /* } */
}
