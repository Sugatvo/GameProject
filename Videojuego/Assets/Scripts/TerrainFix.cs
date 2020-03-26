using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainFix : MonoBehaviour
{
    //Fill this in the inspector with the prefabs to use in place of the tree prototypes defined in the Terrain object.
    public GameObject[] TreePrototypes;

    void Awake()
    {
        var data = GetComponent<Terrain>().terrainData;

        foreach (var instance in data.treeInstances)
        {
            var tree = Instantiate(TreePrototypes[instance.prototypeIndex], instance.position, Quaternion.Euler(0, Mathf.Rad2Deg * instance.rotation, 0), transform);
            tree.transform.localScale = new Vector3(instance.widthScale, instance.heightScale, instance.widthScale);
        }
        data.treeInstances = new TreeInstance[0];
    }
}