using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portalcheck : MonoBehaviour
{
    public bool IsTouchingPlayer = false;


    void OnTriggerEnter (Collider other)
    {
        IsTouchingPlayer = true;
    }

    void OnTriggerExit (Collider other)
    {
        IsTouchingPlayer = false;
    }

}