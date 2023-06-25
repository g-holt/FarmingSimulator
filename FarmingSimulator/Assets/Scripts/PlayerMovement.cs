using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    const string vertical = "Vertical"; //Controls blend tree vertical float value
    const string horizontal = "Horizontal"; //Controls blend tree horizontal float value

    [SerializeField] float turnSpeed = 2f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float currThreshold = .5f;
    [SerializeField] float dampTime = .1f;

    Animator animator;
    
    Vector3 movement; //Holds value of the player input * player forward 
    Vector2 moveInput; //Input given from player
    Vector3 newPosition; //Current Position + movement to get the next position for the player
    
    float moveXPos;
    float moveZPos;
    float moveSpeed; //Set to either walk or run speed
    float walkThreshold = .5f; //Threshold for blend tree float
    float runThreshold = 1f; //Threshold for blend tree float
    public bool isTilling;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        moveSpeed = walkSpeed;
        currThreshold = walkThreshold;
    }

    
    void Update()
    {
        Move();
    }


    void OnMove(InputValue moveValue)
    {
        moveInput = moveValue.Get<Vector2>();
    }

    
    void Move()
    {
        moveXPos = moveInput.x * turnSpeed * Mathf.Rad2Deg * Time.deltaTime;
        moveZPos = moveInput.y * moveSpeed * Time.deltaTime;

        movement = moveZPos * transform.forward;
        newPosition = transform.position + movement;

        transform.position = newPosition;
        animator.SetFloat(vertical, moveInput.y * currThreshold, dampTime, Time.deltaTime);

        if(isTilling) {  return; }
        transform.Rotate(0f, moveXPos, 0f, Space.Self);
        animator.SetFloat(horizontal, moveInput.x * currThreshold, dampTime, Time.deltaTime);

        HandleRun();
    }


    void HandleRun()
    {
        if (Keyboard.current.shiftKey.isPressed && moveInput.y > 0f)
        {
            moveSpeed = runSpeed;
            currThreshold = runThreshold;
        }
        else if (Keyboard.current.shiftKey.wasReleasedThisFrame || moveInput.y <= 0f)
        {
            moveSpeed = walkSpeed;
            currThreshold = walkThreshold;
        }
    }


    public void SetBool(string animation, bool state)
    {
        animator.SetBool(animation, state);
    }


    public void FarmingMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    
    public void ResetMoveSpeed()
    {
        moveSpeed = walkSpeed;
    }
    
}







/*

Before Blend Tree Movement
==================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    const string isIdle = "IsIdle";
    const string isTurningRight = "IsTurningRight";
    const string isTurningLeft = "IsTurningLeft";
    const string isWalking = "IsWalking";
    const string isWalkingBackward = "IsWalkingBackward";
    const string isWalkingLeft = "IsWalkingLeft";
    const string isWalkingRight = "IsWalkingRight";
    const string isRunning = "IsRunning";
    const string isRunningRight = "IsRunningRight";
    const string isRunningLeft = "IsRunningLeft";

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float turnSpeed = 2f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 5f;

    Vector2 moveInput;
    Vector3 newPosition;
    Vector3 movement;
    Vector2 mousePosition;
    Vector3 mouseWorldPosition;
    Camera mainCamera;
    Animator animator;
    //AnimatorControllerParameter[] parameters;

    float moveXPos;
    float moveZPos;
    


    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        //parameters = animator.parameters;
        mainCamera = Camera.main;

        SetBool(isIdle, true);
        SetBool(isWalking, false);
    }

    
    void Update()
    {
        Move();
    }


    void OnMove(InputValue moveValue)
    {
        moveInput = moveValue.Get<Vector2>();
    }


    void OnMousePosition(InputValue mouseValue)
    {
        mousePosition = mouseValue.Get<Vector2>();
    }

    
    void Move()
    {
        if (moveInput.x == 0f && moveInput.y == 0f)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                Debug.Log(parameter.name);
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameter.name, false);
                }
            }

            SetBool(isIdle, true);
            return;
        }

        moveXPos = moveInput.x * turnSpeed * Mathf.Rad2Deg * Time.deltaTime;
        moveZPos = moveInput.y * moveSpeed * Time.deltaTime;

        movement = moveZPos * transform.forward;
        newPosition = transform.position + movement; 

        transform.position = newPosition;
        transform.Rotate(0f, moveXPos, 0f, Space.Self);

        HandleAnimations();
        HandleRun();
    }


    //TODO: Lag Camera on turn - more when running
    private void HandleAnimations()
    {
        //Idle
        SetBool(isIdle, false);

        //Turn In Place
        SetBool(isTurningRight, moveInput.x > 0f && moveInput.y == 0f);
        SetBool(isTurningLeft, moveInput.x < 0f && moveInput.y == 0f);

        //Walking
        SetBool(isWalking, moveInput.y > 0f);
        SetBool(isWalkingBackward, moveInput.y < 0f);

        //Walking Turn
        SetBool(isWalkingLeft, moveInput.x < 0f && moveInput.y > 0f && moveSpeed < runSpeed);
        SetBool(isWalkingRight, moveInput.x > 0f && moveInput.y > 0f && moveSpeed < runSpeed);

        //Running Turn
        SetBool(isRunningRight, moveInput.x > 0f && moveSpeed >= runSpeed);
        SetBool(isRunningLeft, moveInput.x < 0f && moveSpeed >= runSpeed);
    }


    void OnRun()
    {
        HandleRun();
    }


    void HandleRun()
    {
        if (Keyboard.current.shiftKey.isPressed)
        {
            moveSpeed = runSpeed;

            SetBool(isIdle, false);
            SetBool(isRunning, true);
        }
        else if (Keyboard.current.shiftKey.wasReleasedThisFrame)
        {
            moveSpeed = walkSpeed;
            SetBool(isRunning, false);
        }
    }


    void SetBool(string animation, bool state)
    {
        animator.SetBool(animation, state);
    }
    
}


*/