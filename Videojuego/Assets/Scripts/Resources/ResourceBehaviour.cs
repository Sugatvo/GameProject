using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResourceBehaviour : Entity
{
    protected override void OnStart()
    {
        SelectionManager.resourceList.Add(this);
    }

}