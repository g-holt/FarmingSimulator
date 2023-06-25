using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTilling : MonoBehaviour
{
    private const string animIsTilling = "IsTilling"; 

    [SerializeField] GameObject tilled_Row_End_A;
    [SerializeField] GameObject tilled_Row_End_B;
    [SerializeField] GameObject tilled_Row_Middle;
    [SerializeField] GameObject tool_Hoe;
    [SerializeField] float tillingSpeed = 1f;

    GameObject rowParent;
    GameObject lastTilledPos;
    PlayerMovement PlayerMovement;
    List<GameObject> currList = new List<GameObject>();
    Dictionary<String, List<GameObject>> rowsDict = new Dictionary<String, List<GameObject>>(); 
  
    Vector3 newTile;

    int rowListIndex = 0;
    string rowListName;
    float tilledDist;
    bool isTilling;


    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();     

        isTilling = false;
        tool_Hoe.SetActive(false);
    }


    //Function Called By Player Button Press
    void OnTill()
    {
        if(isTilling) { return; }
                
        CreateRowAndParent();
        
        lastTilledPos = Instantiate(tilled_Row_End_A, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.LookRotation(-transform.forward, transform.up), rowParent.transform);
        PlayerMovement.FarmingMoveSpeed(tillingSpeed);               
        
        isTilling = true;
        PlayerMovement.isTilling = true;

        tool_Hoe.SetActive(true);
        PlayerMovement.SetBool(animIsTilling, true);

        StartCoroutine(CheckTilledDist());
    }


    IEnumerator CheckTilledDist()
    {
        while(Keyboard.current.spaceKey.isPressed)
        {
            tilledDist = Vector3.Distance(lastTilledPos.transform.position, transform.position);
            
            if(tilledDist >= 1)
            {            
                InstantiateAndAddToList(tilled_Row_Middle);
            }

            /* Reduces the amount of times we have to run Vector3.Distance() */
            yield return new WaitForSeconds(1);
        }

        StopTilling();
    }


    void StopTilling()
    {
        InstantiateAndAddToList(tilled_Row_End_B);
        
        tool_Hoe.SetActive(false);
        PlayerMovement.ResetMoveSpeed();
        PlayerMovement.SetBool(animIsTilling, false);

        rowListIndex++;
        isTilling = false;
        lastTilledPos = null;
        PlayerMovement.isTilling = false;
    }


    void CreateRowAndParent()
    {
        rowListName = "Row_" + rowListIndex.ToString();
        rowsDict.Add(rowListName, new List<GameObject>());
        currList = rowsDict[rowListName];
        rowParent = new GameObject(rowListName);
    }


    void InstantiateAndAddToList(GameObject tilledObj)
    {
        /* 
        -transform.forward is used in LookRotation to make sure the object is facing the correct(opposite) direction because the player is moving backward.
        Because of this the lastTilledPos.transform.forward is now the direction of the player moving backward allowing the objects to be correctly placed
        in line as the player is moving back. To have the objects placed in the forward direction of the player remove the '-' of the transform.forward in LookRotation().      
        */
        newTile = lastTilledPos.transform.position + (lastTilledPos.transform.forward * 1f);
        
        lastTilledPos = Instantiate(tilledObj, newTile, Quaternion.LookRotation(-transform.forward, transform.up), rowParent.transform);
        currList.Add(lastTilledPos);
    }


    void PrintDictionaryLists()
    {
        foreach(string key in rowsDict.Keys)
        {
            Debug.Log(key);
            currList = rowsDict[key];
            foreach(GameObject obj in currList)
            {
                Debug.Log(obj.name);
            }
        }
    }

    
}