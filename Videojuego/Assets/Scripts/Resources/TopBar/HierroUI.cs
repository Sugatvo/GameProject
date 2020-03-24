using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HierroUI : MonoBehaviour
{
    Text txt;

    // Use this for initialization
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = ResourceManager.player1_Hierro.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = ResourceManager.player1_Hierro.ToString();
    }
}