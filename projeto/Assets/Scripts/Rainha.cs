using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainha : PeçaDefault
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        PeçaDefault c;

        //MOVIMENTOS BISPO
        ///Diagonal Cima direita
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

        ///Diagonal Cima esquerda
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

        ///Diagonal baixo direita
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

        ///Diagonal Cima esquerda
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

        //MOVIMENTOS TORRE
        ///Vertical cima
        for (int i = 1; CurrentY + i <= 7; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + i];
            if (c == null)
            {
                r[CurrentX, CurrentY + i] = true;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX, CurrentY + i] = true;
                }
                break;
            }
        }

        ///Vertical baixo
        for (int i = 1; CurrentY - i >= 0; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - i];
            if (c == null)
            {
                r[CurrentX, CurrentY - i] = true;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX, CurrentY - i] = true;
                }
                break;
            }
        }

        ///Horizontal Direita
        for (int i = 1; CurrentX + i <= 7; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + i, CurrentY];
            if (c == null)
            {
                r[CurrentX + i, CurrentY] = true;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX + i, CurrentY] = true;
                }
                break;
            }
        }

        ///Horizontal Esquerda
        for (int i = 1; CurrentX - i >= 0; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - i, CurrentY];
            if (c == null)
            {
                r[CurrentX - i, CurrentY] = true;
            }
            if (c != null)
            {
                if (c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX - i, CurrentY] = true;
                }
                break;
            }
        }



        return r;
    }
}
