using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Rango : MonoBehaviour
{
    public bool cantAtack;
    public Vector3 targetPosition;
    public Vector3 towardsTarget;
    public float movementSpeed = 7f;
    public float wanderRadius = 12f;

    private void Start()
    {
        cantAtack = false;
        ChangePosition();
    }

    private void Update() {
        towardsTarget = targetPosition - transform.position;
        if(towardsTarget.magnitude < 0.25f)
        {
            ChangePosition();
        }
        transform.position += towardsTarget.normalized * movementSpeed * Time.deltaTime;
       /* if (this.GetComponent<Collider>())
        {
                Debug.Log("algo entre");
            
        }*/ 
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Entity")
        {
            targetPosition = other.transform.position;
            Debug.Log(other.transform.position);
            cantAtack = true;
            
            //Debug.Log("Entidad entra en el rango");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Entity")
        {
            
            Debug.Log("Sale del rango");
        }
    }

    void ChangePosition ()
    {
        targetPosition = transform.position + Random.insideUnitSphere * wanderRadius;
        targetPosition.y = 0;
    }
}
