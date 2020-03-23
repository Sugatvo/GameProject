using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBehaviour : Entity
{
    private Animator mAnimator;
    private NavMeshAgent navMeshAgent;
    private bool moving = false;
    private bool mining = false;

    public UnitData unitData;

    public Entity Target { get; private set; }
    public bool inRange { get; private set; }

    private float nextFire;


    protected override void OnStart()
    {
        SelectionManager.unitList.Add(this);
        mAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        PauseMovement();
    }

    protected override void OnUpdate()
    {
        UpdateInput();
        UpdateMovement();
        UpdateCombat();
    }
    protected override void OnLateUpdate()
    {
        if (Target)
        {
            Vector3 position = Target.transform.position;
            navMeshAgent.SetDestination(position);
            if(!IsMoving())
                transform.LookAt(position);
        }
    }

    private void UpdateInput()
    {
        if (selected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (Input.GetMouseButton(1) && hit.collider)
                {
                    if (hit.collider.tag == "Entity") {
                        Entity entity = hit.collider.GetComponent<Entity>();
                        if (entity != this)
                        {
                            SetTarget(entity);
                            SetDestination(Target.transform.position);
                        }
                        else
                        {
                            SetTarget(null);
                            SetDestination(hit.point);
                        }
                    }
                    else
                    {
                        SetTarget(null);
                        SetDestination(hit.point);
                    }
                }       
            }
        }
    }
    private void UpdateMovement()
    {
        float distance = navMeshAgent.remainingDistance;
        if (Target)
        {
            bool newRange = false;
            if (distance <= unitData.Range)
                newRange = true;

            if (inRange != newRange)
            {
                inRange = newRange;
                OnRangeChange();
            }
        }
        else
        {
            if (inRange)
            {
                inRange = false;
                OnRangeChange();
            }

            bool targetReach = distance <= navMeshAgent.stoppingDistance;
            if (IsMoving())
            {
                if (targetReach)
                {
                    PauseMovement();
                    OnArrived();
                }
            }
            else
            {
                if (!targetReach)
                {
                    ResumeMovement();
                }
            }
        }
    }
    protected virtual void OnArrived() { }
    protected virtual void OnRangeChange()
    {
        if (inRange)
            PauseMovement();
        else
            ResumeMovement();
    }
    protected virtual void UpdateCombat()
    {
        if(Target && inRange && Time.time > nextFire)
        {
            nextFire = Time.time + unitData.AttackCooldown;
            Target.Damage(unitData.AttackDamage);
        }
    }

    protected void PauseMovement()
    {
        navMeshAgent.isStopped = true;
        moving = false;
        OnAnimation();
    }
    protected void ResumeMovement()
    {
        navMeshAgent.isStopped = false;
        moving = true;
        OnAnimation();

    }
    protected void SetDestination(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
        ResumeMovement();
    }
    protected bool IsMoving()
    {
        return !navMeshAgent.isStopped && navMeshAgent.velocity.sqrMagnitude > 0.01f;
    }

    protected void SetTarget(Entity target)
    {
        if (Target != target && target != this)
        {
            Target = target;
            OnTargetSet();
        }
    }
    protected virtual void OnTargetSet() { }

    protected override void OnDestroyEntity()
    {
        base.OnDestroyEntity();
        SelectionManager.unitList.Remove(this);
    }

    protected virtual void OnAnimation() { }

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


}

[System.Serializable]
public class UnitData
{
    [Header("Combat Info")]
    public float AttackDamage;
    public float AttackCooldown;
    public float Range;
}
