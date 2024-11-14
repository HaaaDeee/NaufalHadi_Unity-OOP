using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    //fields
    public EnemyForward prefab;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, -5f);
        if(transform.position.y < -5)
        {
            transform.position = new Vector2(transform.position.x, 5);
        }
    }

//     public void SpawnEnemy()
//     {
//         Vector2 SpawnPos = new Vector2(0, 0);
        
//     }
}
