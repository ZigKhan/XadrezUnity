using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bispo : PeçaDefault
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        PeçaDefault c;

        //Diagonal Cima direita
        for (int i = 1; CurrentY + i <= 7 && CurrentX + i <= 7; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + i, CurrentY + i];
            if (c == null)
            {
                r[CurrentX + i, CurrentY + i] = true;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX + i, CurrentY + i] = true;
                }
                break;
            }
        }

        //Diagonal Cima esquerda
        for (int i = 1; CurrentY + i <= 7 && CurrentX - i >= 0; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - i, CurrentY + i];
            if (c == null)
            {
                r[CurrentX - i, CurrentY + i] = true;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX - i, CurrentY + i] = true;
                }
                break;
            }
        }

        //Diagonal baixo direita
        for (int i = 1; CurrentY - i >= 0 && CurrentX + i <= 7; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + i, CurrentY - i];
            if (c == null)
            {
                r[CurrentX + i, CurrentY - i] = true;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX + i, CurrentY - i] = true;
                }
                break;
            }
        }

        //Diagonal Cima esquerda
        for (int i = 1; CurrentY - i >= 0 && CurrentX - i >= 0; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - i, CurrentY - i];
            if (c == null)
            {
                r[CurrentX - i, CurrentY - i] = true;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX - i, CurrentY - i] = true;
                }
                break;
            }
        }

        return r;
    }
}
