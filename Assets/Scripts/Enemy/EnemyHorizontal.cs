using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    //fields
    public EnemyHorizontal prefab;
    private int direction;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(5 * direction, 0);
        if(Mathf.Abs(transform.position.x) > 9)
        {
            if (direction == 1)
            {
                transform.position = new Vector2(8.8f, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(-8.8f, transform.position.y);
            }
            direction *= -1;
        }
    }

    public void SpawnEnemy()
    {
        int randSpawn = Random.Range(0, 1);
        Vector2 SpawnPos = new Vector2(0, 0);
        

        if (randSpawn == 0)
        {
            SpawnPos = new Vector2(-10, Random.Range(-4, 4));
            direction = 1;
        }
        else
        {
            SpawnPos = new Vector2(10, Random.Range(-4, 4));
            direction = -1;
        }
    }
}
