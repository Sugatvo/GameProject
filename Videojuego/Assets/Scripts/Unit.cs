using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Unit : MonoBehaviour
{
    public GameObject selector;
    private bool State;
    private Animator mAnimator;
    private NavMeshAgent mNavMeshAgent;
    private bool mRunning = false;

    void Start()
    {
        State = false;
        SetSelector(false);
        SelectionManager.unitList.Add(this);
        mAnimator = GetComponent<Animator>();
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetSelector(bool statement)
    {
        selector.SetActive(statement);
        State = statement;
    }


    // Update is called once per frame
    void Update()
    {
        if(State == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    mNavMeshAgent.destination = hit.point;
                }

            }

            if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
            {
                mRunning = false;
            }
            else
            {
                mRunning = true;
            }

            mAnimator.SetBool("running", mRunning);

        }

    }


}
