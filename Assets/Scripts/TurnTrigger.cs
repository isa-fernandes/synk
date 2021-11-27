using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTrigger : MonoBehaviour
{
    [SerializeField] float turnSpeed;

    public Vector3 turningDirection;
    Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (turningDirection != Vector3.zero)
        {
            transform.Rotate(turningDirection * Time.deltaTime * turnSpeed);
            if (turningDirection.y == 1)
            {
                transform.Translate(Vector3.left * Time.deltaTime * turnSpeed);
            }
            
            if (transform.rotation == initialRotation)
            {
                turningDirection = Vector3.zero;
                turnSpeed /= 3;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("TurnTriggerLeft"))
        {
            turningDirection = Vector3.up;
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("TurnTriggerRight"))
        {
            turningDirection = Vector3.down;
            turnSpeed *= 3;
            Destroy(other.gameObject);
        }
        
    }
}
