using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBehaviour : Entity
{
    private Animator mAnimator;
    private NavMeshAgent mNavMeshAgent;
    private bool moving = false;
    private bool pending_request = false;
    private bool mining = false;
    public LayerMask groundLayer;
    public LayerMask resourceLayer;

    protected override void OnStart()
    {
        SelectionManager.unitList.Add(this);
        mAnimator = GetComponent<Animator>();
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (selected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                    mNavMeshAgent.destination = hit.point;

                if(Physics.Raycast(ray, out hit, Mathf.Infinity, resourceLayer))
                {
                    Debug.Log(hit.transform.name);
                    if(hit.transform.name == "GoldOre")
                    {
                        pending_request = true;
                    }
                    

                }
            }
        }

        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {
            moving = false;
            if(pending_request)
            {
                mining = true;
                pending_request = false;
            }
        }
        else
        {
            moving = true;
        }

        OnAnimation();
    }

    public bool isMoving()
    {
        return moving;
    }

    public bool isMining()
    {
        return mining;
    }

    public Animator getAnimator()
    {
        return mAnimator;
    }

    protected virtual void OnAnimation() { }
}
