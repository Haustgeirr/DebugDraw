using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawTest : MonoBehaviour
{
    [Header("Arrow")]
    public Vector3 startPoint;
    public Vector3 direction;
    public bool arrowPoint;
    public float scale;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DebugDraw.Arrow(startPoint, direction.normalized, scale, Color.magenta, arrowPoint, duration);
    }
}
