using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Buildings : BuildingBehaviour
{
    public GameObject indicador;
    public MeshRenderer modelo;

    private int flujo;

    protected override void OnStart()
    {
        base.OnStart();
        indicador.SetActive(false);
        modelo.enabled = false;
        flujo = 0;

    }

    protected override void OnUpdate()
    {
        base.OnUpdate();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Entity")
        {
            indicador.SetActive(true);
            modelo.enabled = true;
            flujo += 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Si se muere dentro del rango
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Entity")
        {
            flujo -= 1;
            if (flujo == 0)
            {
                indicador.SetActive(false);
                modelo.enabled = false;
            }
        }
    }

    protected override void OnDestroyEntity()
    {
        Debug.Log("entro");
        Debug.Log(name);
        Debug.Log(gameObject.name);
        Debug.Log(this.name);
        if (gameObject.name == "Centro Urbano CPU")
        {
            Debug.Log(name);
            Debug.Log("Sirve el if ");
        }
    }

}
