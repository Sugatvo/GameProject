using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComidaUI : MonoBehaviour
{
    Text txt;

    // Use this for initialization
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = ResourceManager.player1_Comida.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = ResourceManager.player1_Comida.ToString();
    }
}
