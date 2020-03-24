using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinaInfo : MonoBehaviour
{
    Text txt;

    // Use this for initialization
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = Mina.Recolectado.ToString() + "/" + "\u221E";
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = Mina.Recolectado.ToString() + "/" + "\u221E";
    }
}
