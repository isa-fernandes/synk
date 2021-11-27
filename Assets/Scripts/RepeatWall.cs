using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatWall : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatPos = GetComponent<BoxCollider>().size.z + 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < startPos.z - repeatPos)
        {
            transform.position = startPos;
        }
    }
}
