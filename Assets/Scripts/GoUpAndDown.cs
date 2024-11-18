using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUpAndDown : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(Random.value / 2.0f);
        while (true)
        {
            transform.position += new Vector3(0.0f, 0.005f * Mathf.Cos(Time.time * 10.0f), 0.0f);
            yield return new WaitForEndOfFrame();
        }
    }
}
