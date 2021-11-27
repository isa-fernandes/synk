using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackward : MonoBehaviour
{
    PlayerController playerControllerScript;

    [SerializeField] public float speed = 15.0f;
    [SerializeField] bool slowWall = true;
    float backLimit = -170;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (gameObject.CompareTag("Wall") && slowWall)
        {
            speed /= 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            
        }

        if (transform.position.z < backLimit && !gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
