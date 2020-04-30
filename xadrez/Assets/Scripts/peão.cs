using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peão : PeçaDefault
{
    

    // Start is called before the first frame update
    public override bool[,] PossibleMove()
    {
        PeçaDefault c, c2;
        bool[,] r = new bool[8, 8];
        // time branco

        if (isWhite)
        {
            //diagonal left
            if(CurrentX != 0 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if (c!=null && !c.isWhite)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            //diagonal right
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            //Middle
            if(CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                if(c == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

            //middle on first move
            if(CurrentY == 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];
                if(c == null & c2 == null)
                {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }

            if(CurrentY == 4)
            {
                
                if(CurrentX < 7)
                {
                    c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
                    if (c == BoardManager.Instance.lastBlack && BoardManager.Instance.lastBlack.GetType() == typeof(peão) && BoardManager.Instance.lastBlack.movedTwice)
                    {
                        r[CurrentX + 1, CurrentY + 1] = true;
                    }
                }
                if(CurrentX > 0)
                {
                    c2 = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
                    if (c2 == BoardManager.Instance.lastBlack && BoardManager.Instance.lastBlack.GetType() == typeof(peão) && BoardManager.Instance.lastBlack.movedTwice)
                    {
                        r[CurrentX - 1, CurrentY + 1] = true;
                    }
                }
                
            }

        }
        else
        {
            //diagonal left
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }

            //diagonal right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            //Middle
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }

            //middle on first move
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];
                if (c == null & c2 == null)
                {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }
            if (CurrentY == 3)
            {
                
                if(CurrentX > 0)
                {
                    c2 = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
                    if (c2 == BoardManager.Instance.lastWhite && BoardManager.Instance.lastWhite.GetType() == typeof(peão) && BoardManager.Instance.lastWhite.movedTwice)
                    {
                        r[CurrentX - 1, CurrentY - 1] = true;
                    }
                }
                if(CurrentX < 7)
                {
                    c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
                    if (c == BoardManager.Instance.lastWhite && BoardManager.Instance.lastWhite.GetType() == typeof(peão) && BoardManager.Instance.lastWhite.movedTwice)
                    {
                        r[CurrentX + 1, CurrentY - 1] = true;
                    }
                }
                
                
            }
        }

        return r;
    }
   
}
