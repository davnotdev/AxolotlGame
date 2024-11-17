using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    
    Rigidbody2D rb;

    [SerializeField]
    Sprite[] substanceSprites;

    [SerializeField]
    float x;

    [SerializeField]
    float y_min, y_max;

    [SerializeField]
    float x_bound;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        int randomIndex = Random.Range(0, substanceSprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = substanceSprites[randomIndex];
        transform.position = new Vector2(x, Random.Range(y_min, y_max));

        switch (randomIndex)
        {
            case 0:
                this.tag = "Baguette";
                break;
            case 1:
                this.tag = "Toolbox";
                break;
        }

    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.left * 5f;
        if (transform.position.x < x_bound)
        {
            Destroy(gameObject);
        }
    }
}
