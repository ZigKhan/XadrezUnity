using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : PeçaDefault
{
    
    
   public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        PeçaDefault c;
        

       



        //MOVIMENTOS Vertical e horizontal
        //movimento Horizontal pra Direita
        if(CurrentX + 1 <= 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
            {
                r[CurrentX + 1, CurrentY] = true;

            }
        }

        //movimento Horizontal pra esquerda
        if (CurrentX - 1 >= 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
            {
                r[CurrentX - 1, CurrentY] = true;

            }
        }

        //movimento Vertical pra baixo
        if (CurrentY - 1 >= 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
            if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
            {
                r[CurrentX, CurrentY - 1] = true;
                
            }
        }

        //movimento Vertical pra Cima
        if (CurrentY + 1 <= 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
            if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
            {
                r[CurrentX, CurrentY + 1] = true;
            }
        }



        //Rock movimento torre e Rei
        if (neverMoved)
        {
            if (isWhite)
            {
                if (BoardManager.Instance.Chessmans[0, 0] != null && BoardManager.Instance.Chessmans[0, 0].neverMoved)
                {
                    PeçaDefault a;
                    PeçaDefault b;
                    PeçaDefault l;
                    if (!BoardManager.Instance.attackedSquares[1] && !BoardManager.Instance.attackedSquares[2] && !BoardManager.Instance.attackedSquares[3] && !BoardManager.Instance.attackedSquares[4])
                    {
                        a = BoardManager.Instance.Chessmans[3, 0];
                        b = BoardManager.Instance.Chessmans[2, 0];
                        l = BoardManager.Instance.Chessmans[1, 0];
                        if (a == null && b == null && l == null)
                        {
                            r[CurrentX - 2, CurrentY] = true;
                        }

                    }
                }
                if (BoardManager.Instance.Chessmans[7, 0] != null && BoardManager.Instance.Chessmans[7, 0].neverMoved)
                {
                    PeçaDefault a;
                    PeçaDefault b;
                    if (!BoardManager.Instance.attackedSquares[4] && !BoardManager.Instance.attackedSquares[5] && !BoardManager.Instance.attackedSquares[6])
                    {
                        a = BoardManager.Instance.Chessmans[5, 0];
                        b = BoardManager.Instance.Chessmans[6, 0];
                        if (a == null && b == null)
                        {
                            r[CurrentX + 2, CurrentY] = true;
                        }

                    }
                }
            }
            else if (!isWhite)
            {
                if (BoardManager.Instance.Chessmans[0, 7] != null && BoardManager.Instance.Chessmans[0, 7].neverMoved)
                {
                    PeçaDefault a;
                    PeçaDefault b;
                    PeçaDefault l;
                    if (!BoardManager.Instance.attackedSquaresBlack[1] && !BoardManager.Instance.attackedSquaresBlack[2] && !BoardManager.Instance.attackedSquaresBlack[3] && !BoardManager.Instance.attackedSquaresBlack[4])
                    {
                        a = BoardManager.Instance.Chessmans[3, 7];
                        b = BoardManager.Instance.Chessmans[2, 7];
                        l = BoardManager.Instance.Chessmans[1, 7];
                        if (a == null && b == null && l == null)
                        {
                            r[CurrentX - 2, CurrentY] = true;
                        }

                    }
                }
                if (BoardManager.Instance.Chessmans[7, 7] != null && BoardManager.Instance.Chessmans[7, 7].neverMoved)
                {
                    PeçaDefault a;
                    PeçaDefault b;
                    if (!BoardManager.Instance.attackedSquaresBlack[4] && !BoardManager.Instance.attackedSquaresBlack[5] && !BoardManager.Instance.attackedSquaresBlack[6])
                    {
                        a = BoardManager.Instance.Chessmans[5, 7];
                        b = BoardManager.Instance.Chessmans[6, 7];
                        if (a == null && b == null)
                        {
                            r[CurrentX + 2, CurrentY] = true;
                        }

                    }
                }
            }
            
        }




        //movimento diagonal cima direita
        if (CurrentX + 1 <= 7 && CurrentY + 1 <= 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
            if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
            {
                r[CurrentX + 1, CurrentY + 1] = true;
            }
        }

        //movimento diagonal cima esquerda
        if (CurrentX - 1 >= 0 && CurrentY + 1 <= 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
            if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
            {
                r[CurrentX - 1, CurrentY + 1] = true;
            }
        }

        //movimento diagonal baixo esquerda
        if (CurrentX - 1 >= 0 && CurrentY - 1 >= 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
            if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
            {
                r[CurrentX - 1, CurrentY - 1] = true;
            }
        }

        //movimento diagonal baixo direita
        if (CurrentX + 1 >= 0 && CurrentY - 1 >= 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
            if (c == null || c.isWhite != BoardManager.Instance.isWhiteTurn)
            {
                r[CurrentX + 1, CurrentY - 1] = true;
            }
        }



        return r;
    }
}
