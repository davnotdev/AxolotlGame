using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float scrollSpeed = 0.8f;
    public bool useAlt = false;
    private Renderer rendererComponent;

    void Start() {
        rendererComponent = GetComponent<Renderer>();
    }

    void Update() {
        float d = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset;
        if (useAlt) 
        {
            offset = Vector2.right;
        }
        else 
        {
            offset = Vector2.down;
        }
        offset *= (d + Mathf.Sin(Time.time * 3.0f) * 0.01f);
        rendererComponent.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
