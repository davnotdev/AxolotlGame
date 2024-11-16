using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;

    bool collided = false;

    public enum MovePaths { Straight, SideToSide, LooptyLoop }

    [SerializeField]
    MovePaths path;

    float t = 0;
    float amplitude = 10f; // how wide move; larger # = wider
    float period = 1.0f; //how fast move side-to-side; larger # = faster

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            if (path == MovePaths.Straight)
            {
                rb.velocity = Vector2.down * 5f;
            }
            else if (path == MovePaths.SideToSide)
            {
                rb.velocity = new Vector2(amplitude * Sin(t * period), -1f);
                t += 0.01f;
            }
            else if (path == MovePaths.LooptyLoop)
            {
                rb.velocity = new Vector2(10 * Cos(t * 5f), 10 * Sin(t * 5f));
                rb.velocity += new Vector2(0.0f, -0.8f);
                t += 0.01f;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        rb.velocity = Vector2.Reflect(rb.velocity, Vector2.up);
    }
}