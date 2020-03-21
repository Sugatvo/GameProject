using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Building")]
public class BuildingObject : ScriptableObject
{
    public string Name;
    public string Description;

    public Vector3 Offset;

    public GameObject Prefab;
}
