using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    const string isIdle = "IsIdle";
    const string isWalking = "IsWalking";
    const string isWalkingBackward = "IsWalkingBackward";
    const string isTurningLeft = "IsTurningLeft";
    const string isTurningRight = "IsTurningRight";

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 5f;

    Vector2 moveInput;
    Vector3 newPosition;
    Vector3 movement;
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
            SetBool(isWalking, false);
            SetBool(isWalkingBackward, false);
            SetBool(isTurningLeft, false);
            SetBool(isTurningRight, false);
            SetBool(isIdle, true);
            return; 
        }

        moveXPos = moveInput.x * turnSpeed * Mathf.Rad2Deg * Time.deltaTime;
        moveZPos = moveInput.y * moveSpeed * Time.deltaTime;

        movement = moveZPos * transform.forward;
        newPosition = transform.position + movement;
        
        transform.position = newPosition;
        transform.Rotate(0f, moveXPos, 0f, Space.Self);
        
        SetBool(isIdle, false);
        SetBool(isWalking, moveInput.y > 0);
        SetBool(isWalkingBackward, moveInput.y < 0f);
        SetBool(isTurningLeft, moveInput.x < 0);
        SetBool(isTurningRight, moveInput.x > 0);
    }


    void SetBool(string animation, bool state)
    {
        animator.SetBool(animation, state);
    }
    
}




//newPosition = transform.position + new Vector3(0f, 0f, moveZPos);