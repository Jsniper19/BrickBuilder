using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomeSet : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject Enemy;
    [SerializeField] EnemyController ECon;
#pragma warning restore 0649

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Enemy)
        {
            ECon.isHome = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Enemy)
        {
            ECon.isHome = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Enemy)
        {
            ECon.isHome = false;
        }
    }
}
