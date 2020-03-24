using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeInfo : MonoBehaviour
{
    Text txt;

    // Use this for initialization
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = Tree.Recolectado.ToString() + "/" + "\u221E";
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = Tree.Recolectado.ToString() + "/" + "\u221E";
    }
}
