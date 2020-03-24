using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Rango : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Entity")
        {
            Debug.Log("Entidad entra en el rango");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Entity")
        {
            Debug.Log("Sale del rango");
        }
    }
}
