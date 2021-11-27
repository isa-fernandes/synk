using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountNextScene : MonoBehaviour
{
    [SerializeField] float timeNextScene;
    [SerializeField] GameObject transitionPrefab;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] AudioSource audio;

    [SerializeField] float timeStartChangeLights;
    [SerializeField] float timeEndChangeLights;
    [SerializeField] Light[] spotLights;
    [SerializeField] Light[] pointLights;
    [SerializeField] Color spotPrimaryColor;
    [SerializeField] Color pointPrimaryColor;
    [SerializeField] Color spotSecondaryColor;
    [SerializeField] Color pointSecondaryColor;
    [SerializeField] GameManager gameManager;
    [SerializeField] MoveBackward moveBackward;

    float currentTime;
    bool isPrimary = true;
    bool startedChangingLights = false;

    float speedMultiplySubway = 1.2f;
    float speedMultiplySnake = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        textMeshPro.SetText(Mathf.Round(currentTime).ToString());

        if (currentTime >= timeStartChangeLights && !startedChangingLights)
        {
            startedChangingLights = true;
            gameManager.snakeSpeed *= speedMultiplySnake;
            moveBackward.speed *= speedMultiplySubway;
            StartCoroutine(ChangeColor());
        }

        if (currentTime >= timeNextScene)
        {
            StartCoroutine(CallNextScene());   
        }
    }

    IEnumerator CallNextScene ()
    {
        transitionPrefab.SetActive(true);

        GameObject[] snakes = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject snake in snakes)
        {
            Destroy(snake.gameObject);
        }

        yield return new WaitForSeconds(2.4f);

        audio.Pause();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ChangeColor ()
    {
        Color newSpotColor;
        Color newPointColor;

        newSpotColor = isPrimary ? spotSecondaryColor : spotPrimaryColor;
        newPointColor = isPrimary ? pointSecondaryColor : pointPrimaryColor;

        foreach (Light light in spotLights)
        {
            light.color = newSpotColor;
        }

        foreach (Light light in pointLights)
        {
            light.color = newPointColor;
        }

        isPrimary = !isPrimary;

        yield return new WaitForSeconds(0.5f);

        if (currentTime <= timeEndChangeLights)
        {
            StartCoroutine(ChangeColor());
        } else
        {
            foreach (Light light in spotLights)
            {
                light.color = spotPrimaryColor;
            }

            foreach (Light light in pointLights)
            {
                light.color = pointPrimaryColor;
            }

            isPrimary = true;

            gameManager.snakeSpeed /= speedMultiplySnake;
            moveBackward.speed /= speedMultiplySubway;
        }

    }
}
