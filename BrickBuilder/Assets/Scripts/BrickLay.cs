using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickLay : MonoBehaviour
{
    [SerializeField] GameObject NextStage = null;
    [SerializeField] PlayerInventory playerInventory = null;


    [SerializeField] int req_BB_brickBasic = 1;
    bool built = false;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Build");
            if (!built)
            {
                if (playerInventory.BB_brickBasic >= req_BB_brickBasic)
                {
                    NextStage.SetActive(true);
                    playerInventory.BB_brickBasic -= req_BB_brickBasic;
                    built = true;
                }
            }
        }
    }
}
