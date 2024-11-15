using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    //fields
    public Vector2 target;
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            target = Player.Instance.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(transform.position);
        }
    }

//     public void SpawnEnemy()
//     {
//         Vector2 SpawnPos = new Vector2(0, 0);
        
//     }
}
