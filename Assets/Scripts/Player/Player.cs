using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // fields
    public static Player Instance { get; private set; } //singleton
    PlayerMovement playerMovement; //field untuk menyimpan komponen PlayerMovement
    Animator animator; //field untuk menyimpan komponen engineEffect dari GameObject EngineEffect
    void Awake() // method untuk menyatakan singleton
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
    // method yang dijalankan sekali saat game dimulai
    void Start() 
    {
        playerMovement = GetComponent<PlayerMovement>();
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if(engineEffect != null) //Jika ditemukan engineEffect
        {
            animator = engineEffect.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("EngineEffect not found");
        }
    }

    // method yang dijalankan setiap frame
    void FixedUpdate()
    {
        playerMovement.Move(); //memanggil method Move() dari playerMovement
    }
    void LateUpdate()
    {
        animator.SetBool("IsMoving", playerMovement.isMoving()); //mengatur status IsMoving dari method isMoving() pada playerMovement
    }
}
