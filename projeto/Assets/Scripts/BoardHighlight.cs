using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlight : MonoBehaviour
{
   public static BoardHighlight Instance { set; get; }

    public GameObject highlightPrefabTerra;
    public GameObject highlightPrefabGrama;
    private List<GameObject> highlightsTerra;
    private List<GameObject> highlightsGrama;

    private void Start()
    {
        Instance = this;
        highlightsGrama = new List<GameObject>();
        highlightsTerra = new List<GameObject>();
    }

    private GameObject GetHighlightObjectGrama()
    {
        GameObject go = highlightsGrama.Find (g => !g.activeSelf);

        if(go == null || go == highlightPrefabTerra)
        {
            go = Instantiate(highlightPrefabGrama);
            highlightsGrama.Add(go);
        }

        return go;
    }

    private GameObject GetHighlightObjectTerra()
    {
        GameObject go = highlightsTerra.Find(g => !g.activeSelf);

        if (go == null || go == highlightPrefabGrama)
        {
            go = Instantiate(highlightPrefabTerra);
            highlightsTerra.Add(go);
        }

        return go;
    }


    public void HighlightAllowedMoves(bool[,] moves)
    {
        for(int i = 0; i < 8; i++)
        {
           for(int j =0; j <8; j++)
            {
                if (moves[i, j])
                {
                    GameObject go;
                    if(i%2 == 0 || i == 0)
                    {
                        if(j%2 == 0 || j == 0)
                        {
                            go = GetHighlightObjectTerra();
                            go.SetActive(true);
                            go.transform.position = new Vector3(i + 0.5f, 0 + 0.1f, j + 0.5f);
                        }
                        else
                        {
                            go = GetHighlightObjectGrama();
                            go.SetActive(true);
                            go.transform.position = new Vector3(i + 0.5f, 0 + 0.1f, j + 0.5f);
                        }
                    }
                    else
                    {
                        if (j % 2 == 0 || j == 0)
                        {
                            go = GetHighlightObjectGrama();
                            go.SetActive(true);
                            go.transform.position = new Vector3(i + 0.5f, 0 + 0.1f, j + 0.5f);
                        }
                        else
                        {
                            go = GetHighlightObjectTerra();
                            go.SetActive(true);
                            go.transform.position = new Vector3(i + 0.5f, 0 + 0.1f, j + 0.5f);
                        }
                    }
                    //GameObject go = GetHighlightObject();
                    //go.SetActive(true);
                    //go.transform.position = new Vector3(i+0.5f, 0+0.1f, j+0.5f);
                }
            }
        }
    }

    public void HidehighLights()
    {
        foreach (GameObject go in highlightsGrama)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in highlightsTerra)
        {
            go.SetActive(false);
        }
    }
}
