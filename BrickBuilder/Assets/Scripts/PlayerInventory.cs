using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int BB_brickBasic = 0;
    [SerializeField] private float BB_basicBrick_RegenTime = 1;
    private float BB_basicBrick_Regen = 0;

    private void Update()
    {
        if (BB_brickBasic < 100)
        {
            if (BB_basicBrick_Regen >= BB_basicBrick_RegenTime)
            {
                GenerateBrick();
                BB_basicBrick_Regen = 0;
            }
            else
            {
                BB_basicBrick_Regen += Time.deltaTime;
            }
        }
    }

    void GenerateBrick()
    {
        BB_brickBasic++;
        Debug.Log("got brick");
    }
}
