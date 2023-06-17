using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFarming : MonoBehaviour
{
    [SerializeField] GameObject tilled_Row_End_A;
    [SerializeField] GameObject tilled_Row_End_B;
    [SerializeField] GameObject tilled_Row_Middle;

    Vector3 startTillPos;
    PlayerMovement PlayerMovement;

    bool isTilling;

    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>(); 

        isTilling = false;
    }

    
    void Update()
    {        
        TillRows();
    }


    void TillRows()
    {
        if (!isTilling) { return; }
        
        if(Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("Start Tilling");
            
        }
        else if(!Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("Stop Tilling");
            Instantiate(tilled_Row_End_B, transform.position, Quaternion.LookRotation(transform.forward, transform.up));
            isTilling = false;
        }
    }


    void OnTill()
    {
        Debug.Log("here");
        if(isTilling) { return; }
        startTillPos = transform.position;
        Instantiate(tilled_Row_End_A, startTillPos, Quaternion.LookRotation(transform.forward, transform.up));
        isTilling = true;
    }
}
