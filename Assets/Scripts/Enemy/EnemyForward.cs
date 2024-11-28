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
    private void Awake()
    {
        PickRandomPositions();
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

    private void PickRandomPositions()
    {
        Vector2 randPos = new(Random.Range(0.1f, 0.99f), 1.1f);

        transform.position = Camera.main.ViewportToWorldPoint(randPos) + new Vector3(0, 0, 10);
    }
}
