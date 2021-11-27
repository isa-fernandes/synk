using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject snakePrefab;
    [SerializeField] float snakePrefabSize = 1;
    [SerializeField] public float snakeSpeed = 15;
    [SerializeField] float snakeY = 1.5f;
    [SerializeField] float snakeZ = 0;
    [SerializeField] float snakeTurnLocal = 1;
    [SerializeField] float turnSpeed = 3;
    //public GameObject coinPrefab;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject subway;

    [SerializeField] float minSubway = 57;
    [SerializeField] float maxSubway = 25;

    private int distance = 5;
    private int forwardBound = 90;
    //private float coinPosY = 4;

    private int startObstacles = 4;
    private bool isAtStart = true;
    private int minZOffset = 3;

    private float startDelay = 0;
    [SerializeField] float repeatRate = 4;

    private PlayerController playerControllerScript;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        isAtStart = false;
        InvokeRepeating("SpawnIfNotDead", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int GenerateRandomLine ()
    {
        int randomPosX = Random.Range(-1, 2);
        return randomPosX;
    }

    int GenerateRandomZ ()
    {
        int randomPosZ;

        // Only spawn obstacles at random Z during start
        if (isAtStart)
        {
            randomPosZ = Random.Range(minZOffset, forwardBound / distance) * distance;
 
        }
        else
        {
            randomPosZ = forwardBound;
        }

        return randomPosZ;
    }

    public void updateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.SetText("Score: " + score);
    }

    /*void SpawnCoins ()
    {
        int randomPosX = GenerateRandomLine();
        int randomPosZ = GenerateRandomZ();
        Vector3 coinPosition = new Vector3(randomPosX * distance, coinPosY, randomPosZ);

        Instantiate(coinPrefab, coinPosition, coinPrefab.gameObject.transform.rotation);
    }*/


    void SpawnObstacles()
    {
        int randomSide = Random.Range(0, 2);
        Vector3 pos;
        float z;

        if (snakeZ == 0)
        {
            z = subway.transform.position.z - 12;
        } else
        {
            z = snakeZ;
        }

        if (randomSide == 0)
        {
            pos = new Vector3(25f, snakeY, z);
        } else
        {
            pos = new Vector3(-25f, snakeY, z);
        }

        GameObject generatedTrap = Instantiate(snakePrefab, pos, snakePrefab.gameObject.transform.rotation);
        generatedTrap.transform.localScale -= new Vector3(2*randomSide, 0, 0);
        generatedTrap.transform.localScale *= snakePrefabSize;

        generatedTrap.GetComponent<MoveBackward>().speed = snakeSpeed;
        generatedTrap.GetComponent<TurnSnake>().turnLocal = snakeTurnLocal;
        generatedTrap.GetComponent<TurnSnake>().turnSpeed = turnSpeed;

        int randomLane = Random.Range(0, 2);
        generatedTrap.GetComponent<TurnSnake>().index = randomLane;
        /*foreach (Transform child in generatedTrap.transform)
            if (child.gameObject.name == "turn right " + randomLane)
            {
                child.gameObject.SetActive(true);
            }*/
    }

    // General controller for only spawning while is not dead
    void SpawnIfNotDead ()
    {
        if (!playerControllerScript.gameOver && subway.transform.position.z <= minSubway && subway.transform.position.z >= maxSubway)
        {
            //SpawnCoins();
            SpawnObstacles();
            
        }
    }
}
