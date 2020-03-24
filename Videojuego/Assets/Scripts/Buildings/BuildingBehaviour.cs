using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingBehaviour : Entity
{
    public bool placed { get; private set; }
    public bool built { get; private set; }

    public bool validPosition { get; private set; }
    private int collisions;

    private MeshRenderer meshRenderer;
    private Collider buildingCollider;


    protected override void OnStart()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        buildingCollider = gameObject.GetComponent<Collider>();

        
    }

    private void Update()
    {
        if (!placed && started)
        {
            validPosition = collisions > 0 ? false : true;
            if (validPosition)
                meshRenderer.material.color = Color.white;
            else
                meshRenderer.material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!placed)
            collisions++;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!placed)
            collisions--;
    }

    public void Place()
    {
        meshRenderer.material.color = Color.white;
        buildingCollider.isTrigger = false;
        NavMeshObstacle navMeshObstacle = gameObject.AddComponent<NavMeshObstacle>();
        navMeshObstacle.carving = true;

        placed = true;
        OnPlaced();

        Build();
    }

    public void Build()
    {
        SelectionManager.buildList.Add(this);
        built = true;
        OnBuilt();
    }

    // API???
    protected virtual void OnPlaced() { }
    protected virtual void OnBuilt() { }
}
