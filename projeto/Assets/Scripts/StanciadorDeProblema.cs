using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanciadorDeProblema : MonoBehaviour
{
    //variaveis de start de mapa do problema
    public bool QuadrantePreto;
    public int[] IndexPeca;
    public int[] xSpawn;
    public int[] ySpawn;



    //variaveis de movimento
    public int[] xStart;
    public int[] yStart;

    public int[] xFinal;
    public int[] yFinal;



    public static StanciadorDeProblema Instance { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Instance = this;
        BoardManager.Instance.isProblema = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
