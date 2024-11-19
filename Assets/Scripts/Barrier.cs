using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Vector2 normal;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb)
        {
            float mul = 1.0f;
            if (collision.gameObject.tag == "Enemy")
            {
                mul = 3.0f;
            }
            rb.AddForce(Vector2.Reflect(rb.velocity, normal) * mul, ForceMode2D.Impulse);
        }
    }
}
