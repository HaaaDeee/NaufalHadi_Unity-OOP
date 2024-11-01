using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    [SerializeField] Vector2 timeToFullSpeed;

    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * (maxSpeed/timeToFullSpeed);
        moveFriction = -2 * new Vector2(maxSpeed.x / Mathf.Pow(timeToFullSpeed.x, 2), maxSpeed.y / Mathf.Pow(timeToFullSpeed.y, 2));
        stopFriction = -2 * new Vector2(maxSpeed.x / Mathf.Pow(timeToStop.x, 2), maxSpeed.y / Mathf.Pow(timeToStop.y, 2));
    }

    public void Move()
    {
        Vector2 mov_Input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        moveDirection = mov_Input * moveVelocity;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + moveDirection.x + GetFriction().x, -maxSpeed.x, maxSpeed.x), Mathf.Clamp(rb.velocity.y + moveDirection.y + GetFriction().y, -maxSpeed.y, maxSpeed.y)) * 2;
        Debug.Log(GetFriction());
        Debug.Log(rb.velocity);
        Debug.Log(moveDirection);
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
        Vector2 ret_val = Vector2.zero;
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
        if (rb.velocity.x == 0 || (moveDirection.x > 0 && rb.velocity.x - ret_val.x < 0) || (moveDirection.x < 0 && rb.velocity.x - ret_val.x > 0))
        {
            ret_val.x = 0;
        }
        if (rb.velocity.y == 0 || (moveDirection.y > 0 && rb.velocity.y - ret_val.y < 0) || (moveDirection.y < 0 && rb.velocity.y - ret_val.y > 0))
        {
            ret_val.y = 0;
        }
        return ret_val;
    }

    public void MoveBound()
    {

    }

    public bool isMoving()
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
