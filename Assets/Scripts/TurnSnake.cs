using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSnake : MonoBehaviour
{
    [SerializeField] public float turnSpeed = 3;
    [SerializeField] public float turnLocal = 1;

    Vector3 turningDirection;
    Quaternion initialRotation;

    [HideInInspector] public bool isRight = true;
    [HideInInspector] public int index = 1;
    float[] x = new float[3];
    float[] x2 = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;

        if (isRight)
        {
            turningDirection = Vector3.up;
        } else
        {
            turningDirection = Vector3.down;
        }
        

        if (isRight)
        {
            x[0] = -5.1f * turnLocal;
            x[1] = -2 * turnLocal;
            
            x2[0] = 2f * turnLocal;
            x2[1] = 1f * turnLocal;
        } else
        {
            x[0] = -2 * turnLocal;
            x[1] = -5.1f * turnLocal;
            x[2] = -8.5f * turnLocal;

            x2[0] = 2.5f * turnLocal;
            x2[1] = 2f *turnLocal;
            x2[2] = 1f *turnLocal;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turningDirection != Vector3.zero)
        {
            
            if (turningDirection == Vector3.up)
            {
                transform.Rotate(turningDirection * Time.deltaTime * turnSpeed * 5);
                transform.Translate(Vector3.left * Time.deltaTime * turnSpeed * 2);
            } else if (turningDirection == Vector3.down && !isRight)
            {
                transform.Rotate(turningDirection * Time.deltaTime * turnSpeed * 3);
                transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * 2);
            }

            if (transform.position.x <= x[index] + x2[index] && transform.position.x > x[index])
            {
                transform.Rotate(turningDirection * Time.deltaTime * turnSpeed);
                turningDirection = Vector3.down;
            }
        }

        if (transform.position.x <= x[index])
        {
            turningDirection = Vector3.zero;
            Vector3 fixPos = new Vector3(x[index], transform.position.y, transform.position.z);
            transform.position = fixPos;

            if (transform.rotation != initialRotation)
            {
                transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed * 3);
            }
            
        }
    }
}
