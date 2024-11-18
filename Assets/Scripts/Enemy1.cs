using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float randomX = 0;
    public float randomY = 0;

    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(-2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(randomX, 2f, 0) * Time.deltaTime * 3f);
  
        if (transform.position.y < -8.5f || transform.position.x > 11.5f || transform.position.x < -11.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Player")
        {
            //I hit the player!
            whatIHit.GetComponent<Player>().LoseALife();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(whatIHit.tag == "Weapon")
        {
            //I got shot!
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(15);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }
}