using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject enemy1;
    public GameObject cloud;
    public GameObject powerUp;

    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public AudioClip coinSound;
    public AudioClip healthSound;

    public GameObject coin;
    public GameObject health;

    private bool isPlayerAlive;

    private int score;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI livesText;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;

    public TextMeshProUGUI powerupText;

    public GameObject explosion;
    public int cloudSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("CreateEnemy1", 10f, 5f);
        
        StartCoroutine(CreateCoin());
        StartCoroutine(CreateHealth());
        StartCoroutine(CreatePowerUp());

        CreateSky();
        cloudSpeed = 1;
        isPlayerAlive = true;
        
        score = 0;
        scoreText.text = "Score: " + score;

        livesText.text = "Lives: 3";
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    void CreateEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(-9f, 9f), 9f, 0), Quaternion.Euler(0, 0, 180));
    }

    void CreateEnemy1()
    {
        Instantiate(enemy1, new Vector3(Random.Range(-9f, 9f), 9f, 0), Quaternion.Euler(0, 0, 180));
    }

    IEnumerator CreateCoin()
    {
        yield return new WaitForSeconds(Random.Range(5f, 15f));
        Instantiate(coin, new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 0f), 0), Quaternion.identity);
        StartCoroutine(CreateCoin());
    }

    IEnumerator CreateHealth()
    {
        yield return new WaitForSeconds(Random.Range(15f, 25f));
        Instantiate(health, new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 0f), 0), Quaternion.identity);
        StartCoroutine(CreateHealth());
    }

    IEnumerator CreatePowerUp()
    {
        yield return new WaitForSeconds(Random.Range(20f, 30f));
        Instantiate(powerUp, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        StartCoroutine(CreatePowerUp());
    }

    void CreateSky()
    {   
        for(int i = 0; i < 20; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }    
    }

    public void EarnScore(int newScore)
    {
        score = score + newScore;
        scoreText.text = "Score: " + score;
    }

    public void UpdatePowerupText(string text)
    {
        powerupText.text = text;
    }

    public void GameOver()
    {
        isPlayerAlive = false;
        CancelInvoke();
        cloudSpeed = 0;

        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }

    void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void PlayPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUpSound, Camera.main.transform.position);
    }

    public void PlayPowerDown()
    {
        AudioSource.PlayClipAtPoint(powerDownSound, Camera.main.transform.position);
    }

    public void PlayCoin()
    {
        AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
    }

    public void PlayHealth()
    {
        AudioSource.PlayClipAtPoint(healthSound, Camera.main.transform.position);
    }
}
