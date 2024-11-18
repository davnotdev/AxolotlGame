using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substance : MonoBehaviour
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
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = substanceSprites[Random.Range(0, substanceSprites.Length)];
        transform.position = new Vector2(x, Random.Range(y_min, y_max));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.left * 10f;
        if (transform.position.x < x_bound)
        {
            Destroy(gameObject);
        }
    }
}
