using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject offRoadMarkerLeft;
    public GameObject offRoadMarkerRight;
    public float moveBackSpeedX = 0.5f;

    private float verticalSpeed = 0.2f;
    private float startingX;

    void Start()
    {
        startingX = transform.position.x;
    }

    void Update() 
    {
        if (
            transform.position.y <= offRoadMarkerLeft.transform.position.y ||
            transform.position.y >= offRoadMarkerRight.transform.position.y
        )
        {
            Debug.Log("owie");
        }

        if (transform.position.x != startingX)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                new Vector2(startingX, transform.position.y),
                moveBackSpeedX * Time.deltaTime
            );
        }
    }

    void FixedUpdate()
    {
        float inputAxisY = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(
            0.0f,
            inputAxisY * verticalSpeed
        ));
    }

    void OnCollisionEnter2D() {
        Debug.Log("owie");
    }
}
