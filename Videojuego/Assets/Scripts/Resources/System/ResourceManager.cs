using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static int player1_Hierro;
    public static int player1_Madera;
    public static int player1_Comida;
    public static int player1_Unidad_Max;
    public static int player1_Unidad_Actual;

    public static int player2_Hierro;
    public static int player2_Madera;
    public static int player2_Comida;
    public static int player2_Unidad_Max;
    public static int player2_Unidad_Actual;

    // Start is called before the first frame update
    void Start()
    {
        player1_Hierro = 500;
        player1_Comida = 500;
        player1_Madera = 500;
        player1_Unidad_Max = 10;
        player1_Unidad_Actual = SelectionManager.unitList.Count;

        player2_Hierro = 500;
        player2_Comida = 500;
        player2_Madera = 500;
        player2_Unidad_Max = 10;
        player2_Unidad_Actual = SelectionManager.unitList.Count;

    }

    // Update is called once per frame
    void Update()
    {
        player1_Unidad_Actual = SelectionManager.unitList.Count;
    }
}