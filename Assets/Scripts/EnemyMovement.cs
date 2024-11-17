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

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        path = (MovePaths)Random.Range(0, 3);
        Debug.Log(path);
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            if (path == MovePaths.Straight)
            {
                rb.velocity = Vector2.left * 5f;
            }
            else if (path == MovePaths.SideToSide)
            {
                rb.velocity = new Vector2(0.0f, 10.0f * Cos(t * 0.5f));
                rb.velocity += new Vector2(-0.8f, 0.0f);
                t += 0.1f;
            }
            else if (path == MovePaths.LooptyLoop)
            {
                rb.velocity = new Vector2(10.0f * Cos(t * 0.5f), 10.0f * Sin(t * 0.5f));
                rb.velocity += new Vector2(-0.8f, 0.0f);
                t += 0.1f;
            }
        }
        
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
