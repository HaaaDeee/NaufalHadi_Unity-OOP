using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    // Bullet Fields
    public float bulletSpeed = 20;
    private IObjectPool<Bullet> objectPool;

    public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }
    public int damage = 10;
    private Rigidbody2D rb;

    public void Deactivate()
    {
        StartCoroutine(BulletRetturn(2f));
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject)
    //     {
    //         StartCoroutine(BulletRetturn(0));
    //     }
    // }

    IEnumerator BulletRetturn(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        objectPool.Release(this);
    }

}
