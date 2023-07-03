using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerPlanting : MonoBehaviour
{
    [SerializeField] Canvas ContainerCanvas;
    [SerializeField] Canvas buttonCanvas;

    GameObject highlightedGO;
    RectTransform buttonRect;
    CanvasScaler canvasScaler;
    PlayerMovement playerMovement;

    Vector2 canvasOffset;
    Vector2 canvasToClickPos;

    
    void Start()
    {
        buttonCanvas.enabled = false;

        playerMovement = GetComponent<PlayerMovement>();
        canvasScaler = ContainerCanvas.GetComponent<CanvasScaler>();
        buttonRect = buttonCanvas.GetComponent<RectTransform>();
    }


    void OnRightClickObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit))
        {
            if(highlightedGO != null)
            {
                highlightedGO.GetComponent<Outline>().enabled = false;
                buttonCanvas.enabled = false;
            }

            if(!hit.transform.CompareTag("TilledDirt")) { return; }

            highlightedGO = hit.transform.gameObject;
            highlightedGO.GetComponent<Outline>().enabled = true;

            PlantCanvas();
        }
    }


    void PlantCanvas()
    {
        //Mult. by the Right Click Canvas(containerCanvas) scaleFactor to account for Screen Resolution
        canvasOffset = new Vector2(
            (buttonRect.rect.width * ContainerCanvas.scaleFactor) * .5f, 
            (buttonRect.rect.height * ContainerCanvas.scaleFactor) * .5f);
        canvasToClickPos = (Mouse.current.position.value / canvasScaler.scaleFactor);

        //Moves Button Background Canvas(buttonRect) to have the top left corner at the mouse click position;
        canvasToClickPos = new Vector2(
            canvasToClickPos.x + canvasOffset.x, 
            canvasToClickPos.y - canvasOffset.y);

        buttonCanvas.enabled = true;
        buttonCanvas.transform.position = canvasToClickPos;
    }


    public void CancelRightClickCanvas()
    {
        buttonCanvas.enabled = false;
        highlightedGO.GetComponent<Outline>().enabled = false;
    }


    public void WalkToPlantingDest()
    {
        buttonCanvas.enabled = false;
        playerMovement.isAutoWalking = true;
        playerMovement.SetAutoWalkDest(highlightedGO.transform);
    }

    //void CanvasToMousePosition()
    //{
    //    Vector2 referenceResolution = canvasScaler.referenceResolution;
    //    Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

    //    float widthRatio = currentResolution.x / referenceResolution.x;
    //    float heightRatio = currentResolution.y / referenceResolution.y;

    //    float ratio = Mathf.Lerp(heightRatio, widthRatio, canvasScaler.matchWidthOrHeight);
    //}

}
