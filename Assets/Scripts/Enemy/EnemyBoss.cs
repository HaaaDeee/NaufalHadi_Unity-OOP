using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    //fields of boss shooting bullets
    [Header("Shooting Boss Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;


    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;


    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    public Transform parentTransform;
    // Boss Can shoot bullets
    public EnemyBoss prefab;
    private int direction;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGet, OnReturn, OnDestroyBullet, collectionCheck, defaultCapacity, maxSize);
        rb = GetComponent<Rigidbody2D>();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(3 * direction, 0);
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
        // Code to shoot bullets
        if (Time.time > shootIntervalInSeconds && objectPool != null)
        {
            Bullet bullet = objectPool.Get();
            if (bullet == null)
            {
                return;
            }
            else
            {
                bullet.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bullet.bulletSpeed, ForceMode2D.Impulse);
                bullet.Deactivate();
                shootIntervalInSeconds = Time.time + 0.3f;
            }
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

    //function to shoot player
    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet);
        newBullet.ObjectPool = objectPool;
        return newBullet;
    }

    private void OnGet(Bullet pooledBullet)
    {
        pooledBullet.gameObject.SetActive(true);
    }

    private void OnReturn(Bullet pooledBullet)
    {
        pooledBullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet pooledBullet)
    {
        Destroy(pooledBullet.gameObject);
    }
}
