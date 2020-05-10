using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui : MonoBehaviour
{
    public GameObject BotaoRainha;
    public GameObject BotaoBispo;
    public GameObject BotaoTorre;
    public static Ui Instance { set; get; }
    public List<GameObject> PeçasPretasCapturadasPeloBranco;
    public List<GameObject> PeçasBrancasCapturadasPeloPreto;
    // Start is called before the first frame update







    private void CapturadoPreto()
    {
        if (BoardManager.Instance.RainhaB)
        {
            PeçasBrancasCapturadasPeloPreto[0].SetActive(true);
}


        if (BoardManager.Instance.BispoB == 1)
        {
            PeçasBrancasCapturadasPeloPreto[1].SetActive(true);
        }
        if (BoardManager.Instance.BispoB == 2)
        {
            PeçasBrancasCapturadasPeloPreto[2].SetActive(true);
        }



        if (BoardManager.Instance.TorreB == 1)
        {
            PeçasBrancasCapturadasPeloPreto[3].SetActive(true);
        }
        if (BoardManager.Instance.TorreB == 2)
        {
            PeçasBrancasCapturadasPeloPreto[4].SetActive(true);
        }



        if (BoardManager.Instance.CavaloB == 1)
        {
            PeçasBrancasCapturadasPeloPreto[5].SetActive(true);
        }
        if (BoardManager.Instance.CavaloB == 2)
        {
            PeçasBrancasCapturadasPeloPreto[6].SetActive(true);
        }




        if (BoardManager.Instance.PeãoB > 0)
        {
            for(int i = 1; i<= BoardManager.Instance.PeãoB; i++)
            {
                PeçasBrancasCapturadasPeloPreto[i+6].SetActive(true);
            }
        }
    }


    private void CapturadoBranco()
    {
        if (BoardManager.Instance.RainhaP)
        {
            PeçasPretasCapturadasPeloBranco[0].SetActive(true);
        }


        if (BoardManager.Instance.BispoP == 1)
        {
            PeçasPretasCapturadasPeloBranco[1].SetActive(true);
        }
        if (BoardManager.Instance.BispoP == 2)
        {
            PeçasPretasCapturadasPeloBranco[2].SetActive(true);
        }



        if (BoardManager.Instance.TorreP == 1)
        {
            PeçasPretasCapturadasPeloBranco[3].SetActive(true);
        }
        if (BoardManager.Instance.TorreP == 2)
        {
            PeçasPretasCapturadasPeloBranco[4].SetActive(true);
        }



        if (BoardManager.Instance.CavaloP == 1)
        {
            PeçasPretasCapturadasPeloBranco[5].SetActive(true);
        }
        if (BoardManager.Instance.CavaloP == 2)
        {
            PeçasPretasCapturadasPeloBranco[6].SetActive(true);
        }




        if (BoardManager.Instance.PeãoP > 0)
        {
            for (int i = 1; i <= BoardManager.Instance.PeãoP; i++)
            {
                PeçasPretasCapturadasPeloBranco[i + 6].SetActive(true);
            }
        }
    }



    public void PromoteRainha()
    {
        BoardManager.Instance.isPromote = false;
        BotaoRainha.SetActive(false);
        BotaoBispo.SetActive(false);
        BotaoTorre.SetActive(false);
        if (BoardManager.Instance.Py == 7)
        {
            BoardManager.Instance.SpawnChessman(0, BoardManager.Instance.Px, BoardManager.Instance.Py);
        }
        else
        {
            BoardManager.Instance.SpawnChessman(6, BoardManager.Instance.Px, BoardManager.Instance.Py);
        }
    }

    public void PromoteBispo()
    {
        BoardManager.Instance.isPromote = false;
        BotaoRainha.SetActive(false);
        BotaoBispo.SetActive(false);
        BotaoTorre.SetActive(false);
        if (BoardManager.Instance.Py == 7)
        {
            BoardManager.Instance.SpawnChessman(3, BoardManager.Instance.Px, BoardManager.Instance.Py);
        }
        else
        {
            BoardManager.Instance.SpawnChessman(9, BoardManager.Instance.Px, BoardManager.Instance.Py);
        }
    }

    public void PromoteTorre()
    {
        BoardManager.Instance.isPromote = false;
        BotaoRainha.SetActive(false);
        BotaoBispo.SetActive(false);
        BotaoTorre.SetActive(false);
        if (BoardManager.Instance.Py == 7)
        {
            BoardManager.Instance.SpawnChessman(2, BoardManager.Instance.Px, BoardManager.Instance.Py);
        }
        else
        {
            BoardManager.Instance.SpawnChessman(8, BoardManager.Instance.Px, BoardManager.Instance.Py);
        }
    }





    void Start()
    {
        Instance = this;
        BotaoRainha.SetActive(false);
        BotaoBispo.SetActive(false);
        BotaoTorre.SetActive(false);

        for (int i = 0; i < 15; i++)
        {
            GameObject c = PeçasPretasCapturadasPeloBranco[i];
            GameObject d = PeçasBrancasCapturadasPeloPreto[i];
            c.SetActive(false);
            d.SetActive(false);
        }
    }



    public void returnMenu()
    {
        BoardManager.Instance.capturedKing = true;
    }
    // Update is called once per frame
    void Update()
    {
        CapturadoPreto();
        CapturadoBranco();
    }
}
