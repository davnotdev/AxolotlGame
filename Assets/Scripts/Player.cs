using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject offRoadMarkerLeft;
    public GameObject offRoadMarkerRight;
    public float moveBackSpeedX = 0.5f;

    private float horizontalSpeed = 0.2f;
    private float startingY;

    void Start()
    {
        startingY = transform.position.y;
    }

    void Update() 
    {
        if (
            transform.position.x <= offRoadMarkerLeft.transform.position.x ||
            transform.position.x >= offRoadMarkerRight.transform.position.x
        )
        {
            Debug.Log("owie");
        }

        if (transform.position.y != startingY)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                new Vector2(transform.position.x, startingY),
                moveBackSpeedX * Time.deltaTime
            );
        }
    }

    void FixedUpdate()
    {
        float inputAxisX = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(
            inputAxisX * horizontalSpeed,
            0.0f
        ));
    }

    void OnCollisionEnter2D() {
        Debug.Log("owie");
    }
}
