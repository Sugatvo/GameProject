using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Rango : UnitBehaviour
{
    public Vector3 targetPosition;
    public float wanderRadius = 40f;
    public GameObject indicador;
    public GameObject modelo;
    private int flujo;

    protected override void OnStart()
    {
        base.OnStart();
        ChangePosition();
        flujo = 0;
        
    }

    protected override void OnUpdate() {
        base.OnUpdate();


        // No esta haciendo anda
        if (!IsMoving() && Target == null)
        {
            ChangePosition();
            SetDestination(targetPosition);

            flujo = 0;
            indicador.SetActive(false);
            modelo.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Entity")
        {
            indicador.SetActive(true);
            modelo.SetActive(true);
            flujo += 1;
        }
        

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Entity" && Target == null)
        {
            targetPosition = other.transform.position;
            SetTarget(other.GetComponent<Entity>());
            SetDestination(targetPosition);

            Debug.Log(other.transform.position);

            //Debug.Log("Entidad entra en el rango");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Entity")
        {
            flujo -= 1;
            if(flujo == 0)
            {
                indicador.SetActive(false);
                modelo.SetActive(false);
            }
        }
        if (other.tag == "Entity" && Target)
        {
            if(other.GetComponent<Entity>() == Target)
            {
                SetTarget(null);
            }
            
        }
    }

    void ChangePosition ()
    {
        targetPosition = transform.position + Random.insideUnitSphere * wanderRadius;
        targetPosition.y = 0;
    }

    protected override void OnAnimation()
    {
        getAnimator().SetBool("running", IsMoving());
    }

}
