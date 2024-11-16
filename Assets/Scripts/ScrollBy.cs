using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBy : MonoBehaviour
{
    public GameObject thisPrefab;
    private float movementSpeed = 0.5f;

    private GameObject partner;
    private Vector2 originalPosition;

    private float minY;

    void Start()
    {
        partner = Instantiate(thisPrefab);

        SpriteRenderer prefabRenderer = GetComponent<SpriteRenderer>();

        minY = prefabRenderer.bounds.size.y;
        originalPosition = transform.position;

        partner.transform.position = transform.position;

        partner.transform.localScale = transform.localScale;
        partner.transform.Translate(new Vector2(0.0f, minY));
    }

    void FixedUpdate()
    {
        MoveObject(this.gameObject);
        if (MoveObject(partner)) {
            transform.position = originalPosition - Vector2.up * movementSpeed;
        };
    }

    bool MoveObject(GameObject obj) {
        if (obj.transform.position.y <= -minY)
        {
            obj.transform.position = originalPosition + Vector2.up * minY;
            return true;
        } 
        else 
        {
            obj.transform.Translate(new Vector2(0.0f, -movementSpeed));
            return false;
        }
    }
}

