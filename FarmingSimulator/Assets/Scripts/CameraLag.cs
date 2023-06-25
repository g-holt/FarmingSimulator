using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraLag : MonoBehaviour
{
    [SerializeField] Transform playerTransform; // Reference to the character's transform
    [SerializeField] float camYOffset = 2f; //Controls Camera height;
    [SerializeField] float camXRotation = 0f;
    [SerializeField] float camDistanceFromCharacter = 5f;  //How far the camera is from the character
    [SerializeField] float camLagSpeed = 5f; // Controls the camera position lag
    [SerializeField] float camRotationLag = 10f; // Controls the camera rotation lag

    Vector3 camPosYOffset; //V3 for yOffset to Control height of camera
    Vector3 camDesiredPosition; //Position behind the player the camera is moving towards
    Quaternion camDesiredRotation; //Rotation for the camera to rotate towards based on player forward position and distance from player to camera


    //Updates after the character movement
    void LateUpdate()
    {
        camPosYOffset = new Vector3(0f, camYOffset, 0f);

        //character's rotation
        //camDesiredRotation = Quaternion.LookRotation(playerTransform.forward, Vector3.up);
        camDesiredRotation = Quaternion.LookRotation(new Vector3(playerTransform.forward.x, camXRotation, playerTransform.forward.z), Vector3.up);


        //Calculate the desired camera position
        camDesiredPosition = playerTransform.position - playerTransform.forward * camDistanceFromCharacter;

        //Vector3 trTCamDist = playerTransform.transform.forward * camDistanceFromCharacter;
        //Debug.Log(playerTransform.transform.position.ToString() + " - (" + playerTransform.transform.forward.ToString() + " * " + camDistanceFromCharacter.ToString() + ")");
        //Debug.Log(playerTransform.transform.position.ToString() + " - (" + trTCamDist.ToString() + ")");
        //Debug.Log(camDesiredPosition.ToString() + " " + playerTransform.right);

        //Interpolate the camera's position and rotation
        transform.position = Vector3.Lerp(transform.position, camDesiredPosition + camPosYOffset, camLagSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, camDesiredRotation, camRotationLag * Time.deltaTime);
    }
}
