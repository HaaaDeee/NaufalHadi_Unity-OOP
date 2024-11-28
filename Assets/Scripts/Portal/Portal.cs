using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed = 0.15f;
    [SerializeField] float rotateSpeed = 5.0f;
    Vector2 newPosition;
    Weapon weapon;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        ChangePosition();
    }

    void Update()
    {
        weapon = Player.Instance.GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            spriteRenderer.enabled = true;
            circleCollider2D.enabled = true;
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
        else
        {
            spriteRenderer.enabled = false;
            circleCollider2D.enabled = false;
        }
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
        {
            return;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            LevelManager levelManager = GameManager.LevelManager;
            if (levelManager != null)
            {
                levelManager.LoadScene("Main");
            }
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
