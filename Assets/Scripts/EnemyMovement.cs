using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;

    bool collided = false;

    public enum MovePaths { 
        Crackhead,
        RandomDirection,
        SideToSide, 
        LooptyLoop,

        _Count,
    }

    [SerializeField]
    MovePaths path;

    [SerializeField]
    float x_bound;

    [SerializeField]
    float lifespan;

    [SerializeField]
    float x;

    [SerializeField]
    float y_min, y_max;

    float t = 0;

    private float randomVariation;
    private Vector2 maybeInit;

    void Awake()
    {
        transform.position = new Vector2(x, Random.Range(y_min, y_max));
        rb = GetComponent<Rigidbody2D>();
        path = (MovePaths)Random.Range(0, (int)MovePaths._Count);

        switch (path) 
        {
            case MovePaths.Crackhead:
                randomVariation = Random.Range(8.0f, 10.0f);
                break;
            case MovePaths.RandomDirection:
                randomVariation = Random.Range(2.0f, 5.0f);
                float x = Random.value;
                float y = Random.value;
                rb.velocity = (new Vector2(x, y)).normalized * randomVariation;
                break;
            case MovePaths.SideToSide:
                randomVariation = Random.Range(1.0f, 4.0f);
                break;
            case MovePaths.LooptyLoop:
                randomVariation = Random.Range(3.0f, 5.0f);
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!collided)
        {
            switch (path) 
            {
                case MovePaths.Crackhead:
                    rb.velocity = Random.onUnitSphere * 5.0f + Vector3.left * randomVariation;
                    break;
                case MovePaths.RandomDirection:
                    rb.velocity *= 1.001f;
                    break;
                case MovePaths.SideToSide:
                    rb.velocity = new Vector2(-randomVariation, 10.0f * Cos(t + Random.value));
                    t += 0.1f;
                    break;
                case MovePaths.LooptyLoop:
                    rb.velocity = new Vector2(7.0f * Cos(t + Random.value), 7.0f * Sin(t + Random.value));
                    rb.velocity += new Vector2(-randomVariation, 0.0f);
                    t += 0.1f;
                break;
            }
        } else {
            rb.constraints = 0;
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
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<EnemyMovement>() == null)
        {
            collided = true;
            rb.velocity = Vector2.Reflect(rb.velocity, Vector2.right) * 8.0f;
            GetComponent<CircleCollider2D>().enabled = false;
        }

    }
}
