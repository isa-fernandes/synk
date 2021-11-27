using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZ : MonoBehaviour
{
    [SerializeField] float outOfBoundsZ;
    [SerializeField] float speed;

    Vector3 initialPosition;
    Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.z > outOfBoundsZ)
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        else
        {
            Destroy(gameObject);
        }
            
    }
}
