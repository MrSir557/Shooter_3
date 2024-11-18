using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(2, 0, 0) * Time.deltaTime * 3f);

        if (transform.position.x > 11.5f || transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if(Time.time-startTime >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Player")
        {
            //The player picked up health!
            whatIHit.GetComponent<Player>().GetALife();
            Destroy(this.gameObject);
        }
    }
}
