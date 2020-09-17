using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Move
{
    public int hp = 3;
    public int at = 1;
    Pice Pos;
    public override bool[,] PossibleMove()
    {
        Pos = GameObject.Find("gamelot").GetComponent<Pice>();
        bool[,] r = new bool[Pos.X, Pos.Y];

        Move c;
        int i, j;

        //左前
        i = CurrentX;
        j = CurrentY;
        while(true)
        {
            i--;
            j++;
            if(i < 0 || j >= Pos.X)
                break;

            c = Pice.Instance.moves[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        //右前
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i >= Pos.X || j >= Pos.X)
                break;

            c = Pice.Instance.moves[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        //左後
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j <0)
                break;

            c = Pice.Instance.moves[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        //右後
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i >= Pos.X || j<0)
                break;

            c = Pice.Instance.moves[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }


        return r;
    }
}
