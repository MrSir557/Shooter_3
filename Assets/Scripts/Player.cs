using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // its access level: public or private
    // its type: int (5, 8, 36, etc.), float (2.5f, 3.7f, etc.)
    // its name: speed, playerSpeed --- Speed, PlayerSpeed
    // optional: give it an initial value
    private float speed;
    private float horizontalInput;
    private float verticalInput;

    public int lives;

    public GameManager gameManager;
    public GameObject thruster;

    private int shooting;
    public GameObject bullet;
    public GameObject explosion;

    public bool hasShield;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        shooting = 1;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        // if (condition) { //do this }
        // else if (other condition { //do that }
        // else { //do this final }
        if (transform.position.x > 11.5f || transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        //vertical screenwrap
        //if (transform.position.y > 8.5f || transform.position.y <= -8.5f)
        //{
            //transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        //}

        if (transform.position.y >= 0f)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0);
        }
    }

    void Shooting()
    {
        //if I press SPACE
        //Create a bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(shooting)
            {
                case 1:
                    //Create a bullet
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    //Create two bullets
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
                    break;
                case 3:
                    //Create three bullets
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 30f));
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -30f));
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Powerup")
        {
            Destroy(whatIHit.gameObject);
            int powerupType = Random.Range(0, 4);
            gameManager.PlayPowerUp();
            switch(powerupType)
            {
                case 0:
                    speed = 9f;
                    gameManager.UpdatePowerupText("SPEED UP!");
                    thruster.gameObject.SetActive(true);
                    StartCoroutine(PowerDown(0));
                    break;
                case 1:
                    //double shot
                    shooting = 2;
                    gameManager.UpdatePowerupText("DOUBLE SHOT!");
                    StartCoroutine(PowerDown(1));
                    break;
                case 2:
                    //triple shot
                    shooting = 3;
                    gameManager.UpdatePowerupText("TRIPLE SHOT!");
                    StartCoroutine(PowerDown(2));
                    break;
                case 3:
                    //shield
                    hasShield = true;
                    gameManager.UpdatePowerupText("SHIELD!");
                    shield.gameObject.SetActive(true);
                    StartCoroutine(PowerDown(3));
                    break;
            }
        }
    }

    IEnumerator PowerDown(int type)
    {
        switch(type)
        {
            case 0:
                //speed up
                yield return new WaitForSeconds(10f);
                speed = 5f;
                thruster.gameObject.SetActive(false);
                break;
            case 1:
                //double shot
                yield return new WaitForSeconds(10f);
                shooting = 1;
                break;
            case 2:
                //triple shot
                yield return new WaitForSeconds(5f);
                shooting = 1;
                break;
            case 3:
                //shield
                yield return new WaitForSeconds(30f);
                if(hasShield)
                {    
                    hasShield = false;
                    gameManager.UpdatePowerupText("");
                    shield.gameObject.SetActive(false);
                }    
                break;   
        }
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    public void LoseALife()
    {
        if(!hasShield)
        {
            lives--;
        }
        else
        {
            hasShield = false;
            gameManager.UpdatePowerupText("");
            gameManager.PlayPowerDown();
            shield.gameObject.SetActive(false);
        }

        gameManager.livesText.text = "Lives: " + lives;

        if(lives == 0)
        {   
            gameManager.GameOver();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void GetALife()
    {
        if(lives < 3)
        {
            lives++;
            gameManager.PlayHealth();
            gameManager.livesText.text = "Lives: " + lives;
        }

        else
        {
            gameManager.PlayCoin();
            gameManager.EarnScore(1);
        }
    }
}