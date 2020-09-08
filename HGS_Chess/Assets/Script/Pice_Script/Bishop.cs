using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Move
{
    public int hp = 3;
    public int at = 1;

    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Move c;
        int i, j;

        //左前
        i = CurrentX;
        j = CurrentY;
        while(true)
        {
            i--;
            j++;
            if(i < 0 || j >= 8)
                break;

            c = Bord.Instance.moves[i, j];
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
            if (i >= 8 || j >= 8)
                break;

            c = Bord.Instance.moves[i, j];
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

            c = Bord.Instance.moves[i, j];
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
            if (i >= 8 || j<0)
                break;

            c = Bord.Instance.moves[i, j];
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
