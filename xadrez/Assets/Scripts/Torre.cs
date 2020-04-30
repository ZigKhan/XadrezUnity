using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : PeçaDefault
{
    
    public static Torre Instance { set; get; }

    private void Update()
    {
        Instance = this;
    }
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        PeçaDefault c;

        //Vertical cima
        for(int i=1; CurrentY + i <=7; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + i];
            if(c == null)
            {
                r[CurrentX, CurrentY + i] = true;
                neverMoved = false;
            }
            if(c != null)
            {
                if(c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX, CurrentY + i] = true;
                    neverMoved = false;
                }
                break;
            }
        }

        //Vertical baixo
        for (int i = 1; CurrentY - i >= 0; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - i];
            if (c == null)
            {
                r[CurrentX, CurrentY - i] = true;
                neverMoved = false;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX, CurrentY - i] = true;
                    neverMoved = false;
                }
                break;
            }
        }

        //Horizontal Direita
        for (int i = 1; CurrentX + i <= 7; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + i, CurrentY];
            if (c == null)
            {
                r[CurrentX + i, CurrentY] = true;
                neverMoved = false;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX + i, CurrentY] = true;
                    neverMoved = false;
                }
                break;
            }
        }

        //Horizontal Esquerda
        for (int i = 1; CurrentX - i >= 0; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - i, CurrentY];
            if (c == null)
            {
                r[CurrentX - i, CurrentY] = true;
                neverMoved = false;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX - i, CurrentY] = true;
                    neverMoved = false;
                }
                break;
            }
        }




        return r;
    }
}
