using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    private Renderer rendererComponent;

    void Start() {
        rendererComponent = GetComponent<Renderer>();
    }

    void Update() {
        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(0.0f, -y + Mathf.Sin(Time.time * 3.0f) * 0.01f);
        rendererComponent.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
