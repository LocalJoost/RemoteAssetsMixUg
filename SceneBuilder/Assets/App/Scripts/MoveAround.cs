using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var points = new List<Vector3>();
        points.Add(gameObject.transform.position);
        points.Add(gameObject.transform.position);
        points.Add(gameObject.transform.position + new Vector3(0,0,1));
        points.Add(gameObject.transform.position + new Vector3(1,0,1));
        points.Add(gameObject.transform.position + new Vector3(-1,0,1));
        points.Add(gameObject.transform.position + new Vector3(-1,1,1));
        points.Add(gameObject.transform.position);
        points.Add(gameObject.transform.position);

        LeanTween.moveSpline(gameObject, points.ToArray(), 3).setLoopClamp();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
