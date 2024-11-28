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
    private void Awake()
    {
        PickRandomPositions();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void PickRandomPositions()
    {
        Vector2 randPos;
        Vector2 dir;

        if (Random.Range(-1, 1) >= 0)
        {
            dir = Vector2.right;
        }
        else
        {
            dir = Vector2.left;
        }

        if (dir == Vector2.right)
        {
            randPos = new(1.1f, Random.Range(0.1f, 0.95f));
        }
        else
        {
            randPos = new(-0.01f, Random.Range(0.1f, 0.95f));
        }

        transform.position = Camera.main.ViewportToWorldPoint(randPos) + new Vector3(0, 0, 10);
    }
}
