using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBehaviour : Entity
{
    private Animator mAnimator;
    private NavMeshAgent mNavMeshAgent;
    private bool mRunning = false;


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
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    mNavMeshAgent.destination = hit.point;
            }
        }

        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
            mRunning = false;
        else
            mRunning = true;
        mAnimator.SetBool("running", mRunning);
    }
}
