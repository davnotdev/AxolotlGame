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

    [SerializeField]
    float x_bound;

    [SerializeField]
    float lifespan;

    float t = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        path = (MovePaths)Random.Range(0, 3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!collided)
        {
            if (path == MovePaths.Straight)
            {
                rb.velocity = Vector2.left * 5f;
                                t += 0.1f;
            }
            else if (path == MovePaths.SideToSide)
            {
                rb.velocity = new Vector2(0.0f, 10.0f * Cos(t));
                rb.velocity += new Vector2(-0.8f, 0.0f);
                t += 0.1f;
            }
            else if (path == MovePaths.LooptyLoop)
            {
                rb.velocity = new Vector2(7.0f * Cos(t), 7.0f * Sin(t));
                rb.velocity += new Vector2(-0.8f, 0.0f);
                t += 0.1f;
            }
        }

        if (transform.position.x < x_bound || t > lifespan)
        {
            Destroy(gameObject);
        }
    }

    public void DisableMovement() {
        collided = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>() == null)
        {
            collided = true;
            rb.velocity = Vector2.Reflect(rb.velocity, Vector2.right);
        }

    }
}
