using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class EnemyMovement : MonoBehaviour
{

    public enum MovePaths { Straight, SideToSide, LooptyLoop }

    [SerializeField]
    MovePaths path;

    float t = 0;
    float downSpeed = 0.1f; // larger # = moves down the screen faster
    float amplitude = 5f; // how wide move; larger # = wider
    float period = 10f; //how fast move side-to-side; larger # = faster

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (path == MovePaths.SideToSide)
        {
            transform.position = new Vector3(amplitude * Sin(t * period), -t * downSpeed, 0.0f);
            t += 0.01f;
        }
        else if (path == MovePaths.LooptyLoop)
        {
            transform.position = new Vector3(Cos(t), Sin(t), 0.0f);
            transform.position += new Vector3(0.0f, -t * downSpeed, 0.0f);
            t += 0.05f;
        }
    }
}
