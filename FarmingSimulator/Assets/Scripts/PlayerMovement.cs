using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Vector2 moveInput;

    float moveXPos;
    float moveZPos;


    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        Move();
    }


    void OnMove(InputValue moveValue)
    {
        moveInput = moveValue.Get<Vector2>();
    }


    void Move()
    {
        if(moveInput.x == 0f && moveInput.y == 0f) { return; }

        moveXPos = moveInput.x * moveSpeed * Time.deltaTime;
        moveZPos = moveInput.y * moveSpeed * Time.deltaTime;
    }




}
