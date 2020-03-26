using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBehaviour : Entity
{
    private Animator mAnimator;
    private NavMeshAgent navMeshAgent;
    private bool mining = false;
    private bool farming = false;
    private bool cutting = false;

    private bool request_mining = false;
    private bool request_farming = false;
    private bool request_cutting = false;

    public UnitData unitData;

    public Entity Target { get; private set; }
    public bool inRange { get; private set; }

    private float nextFire;
    public LayerMask layer;


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
        UpdateResources();
        UpdateIA();
    }
    protected override void OnLateUpdate()
    {
        if (Target)
        {
            Vector3 position = Target.transform.position;
            navMeshAgent.SetDestination(position);
            if (!IsMoving())
                transform.LookAt(position);
        }
    }

    private void UpdateInput()
    {
        if (selected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (Input.GetMouseButton(1) && hit.collider)
                {
                    if (hit.collider.tag == "Cpu") {
                        Entity entity = hit.collider.GetComponent<Entity>();
                        if (entity != this)
                        {
                            request_farming = false;
                            request_mining = false;
                            SetTarget(entity);
                            SetDestination(Target.transform.position);
                        }
                    }
                    else if (hit.collider.tag == "Resource")
                    {
                        ResourceBehaviour r = hit.collider.GetComponent<ResourceBehaviour>();
                        Debug.Log(r.name);
                        if (r.name == "Ore")
                        {
                            request_mining = true;
                            SetTarget(null);
                            SetDestination(hit.point);
                        }

                        if (r.name == "Farm")
                        {
                            request_farming = true;
                            SetTarget(null);
                            SetDestination(hit.point);
                        }
                        if (r.name.Contains("Tree"))
                        {
                            request_cutting = true;
                            SetTarget(null);
                            SetDestination(hit.point);
                        }

                    }
                    else
                    {
                        request_farming = false;
                        request_mining = false;
                        request_cutting = false;
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
                    if (request_mining)
                    {
                        mining = true;
                        request_mining = false;
                    }
                    else if (request_farming)
                    {
                        farming = true;
                        request_farming = false;
                    }
                    else if (request_cutting)
                    {
                        cutting = true;
                        request_cutting = false;
                    }
                    else
                    {
                        farming = false;
                        mining = false;
                        cutting = false;
                    }
                    OnAnimation();
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
        OnAnimation();
    }
    protected void ResumeMovement()
    {
        navMeshAgent.isStopped = false;
        OnAnimation();

    }
    protected void SetDestination(Vector3 target)
    {
        mining = false;
        farming = false;
        cutting = false;
        OnAnimation();
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

    public bool isMining()
    {
        return mining;
    }

    public bool isFarming()
    {
        return farming;
    }

    public bool isCutting()
    {
        return cutting;
    }

    public Animator getAnimator()
    {
        return mAnimator;
    }

    protected virtual void UpdateResources() { }


    private void UpdateIA()
    {
        /*if (this.tag == "Cpu")
        {   
           
           Debug.Log("holaaa");
        }*/
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
