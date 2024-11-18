using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameManager gameManager;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-startTime >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Player")
        {
            //The player picked up a coin!
            gameManager.EarnScore(1);
            gameManager.PlayCoin();
            Destroy(this.gameObject);
        }
    }
}
