using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    //fields yang dapat diatur dari Unity
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    [SerializeField] Vector2 timeToFullSpeed;

    //fields yang diatur pada program
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;
    void Start() // method yang dijalankan sekali saat game dimulai
    {
        rb = GetComponent<Rigidbody2D>();
        // menghitung moveVelocity, moveFriction, dan stopFriction menggunakan rumus yang telah ditentukan
        moveVelocity = 2 * (maxSpeed/timeToFullSpeed);
        moveFriction = -2 * new Vector2(maxSpeed.x / Mathf.Pow(timeToFullSpeed.x, 2), maxSpeed.y / Mathf.Pow(timeToFullSpeed.y, 2));
        stopFriction = -2 * new Vector2(maxSpeed.x / Mathf.Pow(timeToStop.x, 2), maxSpeed.y / Mathf.Pow(timeToStop.y, 2));
    }

    public void Move()
    {
        //mengambil input dari player
        Vector2 mov_Input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        // mengatur kecepatan player
        moveDirection = mov_Input * moveVelocity;
        // mengatur kecepatan player agar tidak melebihi maxSpeed
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + moveDirection.x + GetFriction().x, -maxSpeed.x, maxSpeed.x), Mathf.Clamp(rb.velocity.y + moveDirection.y + GetFriction().y, -maxSpeed.y, maxSpeed.y)) * 2;
        Debug.Log(GetFriction());
        Debug.Log(rb.velocity);
        Debug.Log(moveDirection);
        // mengatur agar player berhenti saat kecepatan sudah mendekati 0
        if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(stopClamp.x))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Mathf.Abs(rb.velocity.y) < Mathf.Abs(stopClamp.y))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    public Vector2 GetFriction()
    {
        // menghitung gaya gesekan yang diperlukan agar player berhenti
        Vector2 ret_val = Vector2.zero;
        // menghitung gaya gesekan berdasarkan arah gerakan player
        if (moveDirection.x == 0)
        {
            ret_val.x = stopFriction.x * Mathf.Sign(rb.velocity.x);
        }
        else
        {
            ret_val.x = moveFriction.x * Mathf.Sign(rb.velocity.x);
        }
        if (moveDirection.y == 0)
        {
            ret_val.y = stopFriction.y * Mathf.Sign(rb.velocity.y);
        }
        else
        {
            ret_val.y = moveFriction.y * Mathf.Sign(rb.velocity.y);
        }
        // mengatur kapan nilai gaya gesekan harus 0, yaitu ketika player sedang diam, atau hasil pengurangan kecepatan player dengan gaya gesekan bernilai kurang atau lebih dari 0 tergantung keadaan player
        if (rb.velocity.x == 0 || (moveDirection.x > 0 && rb.velocity.x - ret_val.x < 0) || (moveDirection.x < 0 && rb.velocity.x - ret_val.x > 0))
        {
            ret_val.x = -rb.velocity.x;
        }
        if (rb.velocity.y == 0 || (moveDirection.y > 0 && rb.velocity.y - ret_val.y < 0) || (moveDirection.y < 0 && rb.velocity.y - ret_val.y > 0))
        {
            ret_val.y = -rb.velocity.y;
        }
        return ret_val;
    }

    public void MoveBound() 
    {
        float charWidth = GetComponent<PolygonCollider2D>().bounds.size.x / 2;
        float charHeight = GetComponent<PolygonCollider2D>().bounds.size.y / 2;
        float maxHeight = Camera.main.orthographicSize;
        float maxWidth = Camera.main.orthographicSize * Camera.main.aspect;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -maxWidth + charWidth, maxWidth - charWidth), Mathf.Clamp(transform.position.y, -maxHeight + charHeight * 0.5f, maxHeight - charHeight * 2.5f));
    }

    public bool isMoving() // method yang mengembalikan nilai boolean apakah player sedang bergerak atau tidak
    {
        if(rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
