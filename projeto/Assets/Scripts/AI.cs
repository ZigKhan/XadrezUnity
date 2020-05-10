using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI : MonoBehaviour
{
    public List<Moves> beta = new List<Moves>();
    public static AI Instance { set; get; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        BoardManager.Instance.temAI = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void guardaMoves( List<Moves> y, PeçaDefault z)
    {
        bool[,] r = new bool[8, 8];
        r = z.PossibleMove();
        for (int i = 0; i< 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (r[i,j] == true)
                {
                    y.Add(new Moves(i, j, z));
                }
                else { }
            }
        }
    }

    public Moves evaluateMove(List<Moves> Bot, List<Moves> Jogador)
    {
        List<Moves> Melhores = new List<Moves>();

        int index = 0;
        int maiorPoint = 0;
        int point = 0;
        PeçaDefault lastPlayed;
        lastPlayed = null;

        for (int i = 0; i < Bot.Count; i++)
        {
            point = 0;
            //vejo se o destino do move na posição 0 está ocupado por uma peça branca
            if (BoardManager.Instance.Chessmans[Bot[i].x, Bot[i].y] != null)
            {
                if(BoardManager.Instance.Chessmans[Bot[i].x, Bot[i].y].isWhite)
                {
                    if (BoardManager.Instance.Chessmans[Bot[i].x, Bot[i].y].GetType() == typeof(Rei))
                    {
                        point += 1000;
                    }
                    else if (BoardManager.Instance.Chessmans[Bot[i].x, Bot[i].y].GetType() == typeof(Rainha))
                    {
                        point += 100;
                        
                    }
                    else if (BoardManager.Instance.Chessmans[Bot[i].x, Bot[i].y].GetType() == typeof(Bispo) || BoardManager.Instance.Chessmans[Bot[i].x, Bot[i].y].GetType() == typeof(Torre) || BoardManager.Instance.Chessmans[Bot[i].x, Bot[i].y].GetType() == typeof(Cavalo))
                    {
                        point += 50;
                    }
                    else if (BoardManager.Instance.Chessmans[Bot[i].x, Bot[i].y].GetType() == typeof(peão))
                    {
                        point += 10;
                        
                    }
                    
                }
            }
            for (int j = 0; j< Jogador.Count; j++)
            {
                if(Bot[i].x == Jogador[j].x && Bot[i].y == Jogador[j].y)
                {
                    if(Bot[i].peçaDoMove.GetType() == typeof(Rei))
                    {
                        point -= 1000;
                        
                        break;
                    }
                    else if (Bot[i].peçaDoMove.GetType() == typeof(Rainha))
                    {
                        point -= 99;
                        
                        break;
                    }
                    else if (Bot[i].peçaDoMove.GetType() == typeof(Torre) || Bot[i].peçaDoMove.GetType() == typeof(Bispo) || Bot[i].peçaDoMove.GetType() == typeof(Cavalo))
                    {
                        point -= 49;
                        break;
                    }
                    else if (Bot[i].peçaDoMove.GetType() == typeof(peão))
                    {
                        point -= 9;
                      
                        break;
                    }
                }
                
            }
            
            if (point > maiorPoint)
            {
                Melhores.Clear();
                Melhores.Add(Bot[i]);
                maiorPoint = point;
                index = i;

            }
            else if (point == maiorPoint)
            {
                Melhores.Add(Bot[i]);
            }
            

        }
        if(Melhores.Count > 1)
        {
            int geraNumero = 0;
            
            geraNumero = UnityEngine.Random.Range(0, Melhores.Count);
            
            while(lastPlayed == Bot[geraNumero].peçaDoMove)
            {
                geraNumero = UnityEngine.Random.Range(0, Melhores.Count);
                Debug.Log("igual");
            }
            lastPlayed = Bot[geraNumero].peçaDoMove;
            return Bot[geraNumero];
        }
        else
        {
            return Bot[index];
        }



      
    }
}

