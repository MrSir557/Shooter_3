using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-12f, 12f), Random.Range(-8f, 8f), 0);
        float tempValue = Random.Range(0.1f, 1f);
        transform.localScale = new Vector3(tempValue, tempValue, tempValue);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Random.Range(0.1f, 0.6f));

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Random.Range(3f, 6f)* gameManager.cloudSpeed);

        if(transform.position.y <= -9f)
        {
            transform.position = new Vector3(Random.Range(-12f, 12f), 9f, 0);
        }
    }
}
