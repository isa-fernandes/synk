using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameManager gameManager;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] Animator playerAnim;

    [SerializeField] float speed;

    public bool gameOver;

    [SerializeField] Vector3 moveVector;
    [SerializeField] float roundLane;
    Vector3 directionVector;
    Vector3 finalPosVector;
    Vector3 initialPosition;
    int playerPosition = 1;
    bool isMovingLane;
    
    [SerializeField] float jumpSpeed = 5.5f;
    bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            SetDirection();
            Jump();
            MoveHorizontally();

            if (transform.position.x > initialPosition.x + moveVector.x)
            {
                transform.position = initialPosition + moveVector;
            }

            if (transform.position.x < initialPosition.x - moveVector.x)
            {
                transform.position = initialPosition - moveVector;
            }
        }
    }

    void SetDirection ()
    {
        if (!isMovingLane)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (playerPosition < 2)
                {
                    isMovingLane = true;
                    directionVector = Vector3.right;
                    finalPosVector = transform.position + moveVector;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (playerPosition > 0)
                {
                    isMovingLane = true;
                    directionVector = Vector3.left;
                    finalPosVector = transform.position - moveVector;
                }
            }
        }
        
    }

    void MoveHorizontally ()
    {
        if (isMovingLane)
        {
            // Move the necessary distance to requested direction over time
            transform.Translate(moveVector * directionVector.x * Time.deltaTime * speed);
            
            // If current position and destination position are close, move what's missing and stop
            if (Mathf.Abs(transform.position.x - finalPosVector.x) < roundLane)
            {
                isMovingLane = false;
                finalPosVector.y = transform.position.y;
                transform.position = finalPosVector;
                if (directionVector.Equals(Vector3.right))
                {
                    playerPosition++;
                } else
                {
                    playerPosition--;
                }

                directionVector = Vector3.zero;

            }
        }
    }

    void Jump ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_t");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // If collides with obstacle, game over
            gameOver = true;
            playerAnim.SetBool("GameOver_b", true);
        }

        // Picked coin
        //if (other.gameObject.CompareTag("Coin"))
        //{
        //    Destroy(other.gameObject);
        //    gameManager.updateScore(10);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            
        }
    }
}
