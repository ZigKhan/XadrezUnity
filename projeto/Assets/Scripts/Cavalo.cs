using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalo : PeçaDefault
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        PeçaDefault c;

        //movimento L pra baixo 2
        if (CurrentY - 2 >= 0)
        {
            ///movimento pra direita 1
            if (CurrentX < 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 2];
                if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX + 1, CurrentY - 2] = true;
                }
            }

            ///movimento pra esquerda 1
            if (CurrentX > 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 2];
                //checa se C é branco e o turno é preto e o contrário tb, se for o move é possivel
                if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX - 1, CurrentY - 2] = true;
                }
            }
        }
        //Movimento L pra cima 2
        if (CurrentY + 2 <= 7)
        {
            ///movimento pra direita 1
            if (CurrentX < 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 2];
                if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX + 1, CurrentY + 2] = true;
                }
            }

            ///movimento pra esquerda 1
            if (CurrentX > 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 2];
                //checa se C é branco e o turno é preto e o contrário tb, se for o move é possivel
                if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX - 1, CurrentY + 2] = true;
                }
            }
        }

        //Movimento L pra cima 1
        if (CurrentY + 1 <= 7)
        {
            ///movimento pra direita 1
            if (CurrentX <= 5)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentY + 1];
                if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX + 2, CurrentY + 1] = true;
                }
            }

            ///movimento pra esquerda 1
            if (CurrentX >= 2)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentY + 1];
                //checa se C é branco e o turno é preto e o contrário tb, se for o move é possivel
                if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX - 2, CurrentY + 1] = true;
                }
            }
        }

        //Movimento L pra baixo 1
        if (CurrentY - 1 >= 0)
        {
            ///movimento pra direita 2
            if (CurrentX <= 5)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentY - 1];
                if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX + 2, CurrentY - 1] = true;
                }
            }

            ///movimento pra esquerda 2
            if (CurrentX >= 2)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentY - 1];
                //checa se C é branco e o turno é preto e o contrário tb, se for o move é possivel
                if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
                {
                    r[CurrentX - 2, CurrentY - 1] = true;
                }
            }
        }

        return r;
    }
}