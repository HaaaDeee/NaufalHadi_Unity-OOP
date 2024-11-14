using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
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

    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGet, OnReturn, OnDestroyBullet, collectionCheck, defaultCapacity, maxSize);
    }

    private void FixedUpdate()
    {
        
        // Getting pooled object
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
                shootIntervalInSeconds = Time.time + 0.1f;
            }
        }

    }
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
