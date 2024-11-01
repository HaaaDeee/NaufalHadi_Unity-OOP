using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static Player Instance { get; private set; }
    PlayerMovement playerMovement;
    Animator animator;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if(engineEffect != null)
        {
            animator = engineEffect.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("EngineEffect not found");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.Move();
    }
    void LateUpdate()
    {
        animator.SetBool("IsMoving", playerMovement.isMoving());
    }
}
