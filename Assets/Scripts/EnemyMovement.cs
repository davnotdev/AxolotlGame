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

    private float randomVariation;
    private Vector2 maybeInit;
    private float secsToDespawn = 15.0f;

    void Awake()
    {
        StartCoroutine(Despawn());

        transform.position = new Vector2(x, Random.Range(y_min, y_max));
        rb = GetComponent<Rigidbody2D>();
        path = (MovePaths)GameManager.RandomWithWeights(
                new List<uint> { 10, 10, 1, 1 });

        switch (path) 
        {
            case MovePaths.Crackhead:
                randomVariation = Random.Range(8.0f, 10.0f);
                break;
            case MovePaths.RandomDirection:
                randomVariation = Random.Range(8.0f, 10.0f);
                float x = Random.value;
                float y = Random.value;
                rb.velocity = (new Vector2(x, y)).normalized * randomVariation;
                break;
            case MovePaths.SideToSide:
                randomVariation = Random.Range(5.0f, 10.0f);
                break;
            case MovePaths.LooptyLoop:
                randomVariation = Random.Range(8.0f, 14.0f);
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
                    rb.velocity *= 1.02f;
                    break;
                case MovePaths.SideToSide:
                    rb.velocity = new Vector2(-randomVariation, 10.0f * Cos(Time.time + Random.value));
                    break;
                case MovePaths.LooptyLoop:
                    rb.velocity = new Vector2(7.0f * Cos(Time.time + Random.value), 7.0f * Sin(Time.time + Random.value));
                    rb.velocity += new Vector2(-randomVariation, 0.0f);
                break;
            }
        } else {
            rb.constraints = 0;
        }

        if (transform.position.x < x_bound)
        {
            Destroy(gameObject);
        }
    }

    public void DisableMovement()
    {
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

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(secsToDespawn);
        Destroy(gameObject);
    }
}
