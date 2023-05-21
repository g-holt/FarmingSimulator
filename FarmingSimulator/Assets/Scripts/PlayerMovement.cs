using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    const string isIdle = "IsIdle";
    const string isWalking = "IsWalking";
    const string isWalkingBackward = "IsWalkingBackward";

    [SerializeField] float moveSpeed = 5f;

    Vector2 moveInput;
    Vector3 newPosition;
    Animator animator;

    float moveXPos;
    float moveZPos;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        SetBool(isIdle, true);
        SetBool(isWalking, false);
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
        if(moveInput.x == 0f && moveInput.y == 0f) 
        {
            SetBool(isIdle, true);
            SetBool(isWalking, false);
            SetBool(isWalkingBackward, false);
            return; 
        }

        moveXPos = moveInput.x * moveSpeed * Time.deltaTime;
        moveZPos = moveInput.y * moveSpeed * Time.deltaTime;

        newPosition = transform.position + new Vector3(moveXPos, 0f, moveZPos);
        transform.position = newPosition;

        SetBool(isIdle, false);
        
        if(moveInput.y < 0f)
        {
            SetBool(isWalkingBackward, true);
        }
        else
        {
            SetBool(isWalking, true);
        }
    }


    void SetBool(string animation, bool state)
    {
        animator.SetBool(animation, state);
    }
    
}
