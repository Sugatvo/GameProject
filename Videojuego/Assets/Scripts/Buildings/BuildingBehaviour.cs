using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BuildingBehaviour : Entity
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
        if (this.GetType().ToString().Equals("CentroUrbano"))
        {
            if (ResourceManager.player1_Hierro >= 500 && ResourceManager.player1_Madera >= 500)
            {
                ResourceManager.player1_Hierro = ResourceManager.player1_Hierro - 500;
                ResourceManager.player1_Madera = ResourceManager.player1_Madera - 500;
                buildingCollider = gameObject.GetComponent<Collider>();

            }
        }
        else if (this.GetType().ToString().Equals("Barracks"))
        {
            if (ResourceManager.player1_Hierro >= 250 && ResourceManager.player1_Madera >= 300)
            {
                ResourceManager.player1_Hierro = ResourceManager.player1_Hierro - 250;
                ResourceManager.player1_Madera = ResourceManager.player1_Madera - 300;
                buildingCollider = gameObject.GetComponent<Collider>();

            }
        }
        else if (this.GetType().ToString().Equals("OilStorage"))
        {
            if (ResourceManager.player1_Hierro >= 150 && ResourceManager.player1_Madera >= 200)
            {
                ResourceManager.player1_Hierro = ResourceManager.player1_Hierro - 150;
                ResourceManager.player1_Madera = ResourceManager.player1_Madera - 200;
                buildingCollider = gameObject.GetComponent<Collider>();

            }
        }

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

    public void CanBuild()
    {
        OnCanBuild();
    }

    // API???
    protected virtual void OnPlaced() { }
    protected virtual void OnBuilt() { }
    protected virtual void OnCanBuild() { }

}
