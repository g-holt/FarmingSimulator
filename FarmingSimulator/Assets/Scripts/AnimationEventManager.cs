using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    PlayerMovement playerMovement;


    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }


    public void AnimEventEnablePlayerInput()
    {
        Debug.Log("Animation Event");
        playerMovement.EnablePlayerInput();
    }

}
