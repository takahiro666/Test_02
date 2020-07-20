using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Move
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Move c;
        int i, j;

        //Top side
        i = CurrentX - 1;
        j = CurrentY + 1;
        if(CurrentY != 7)
        {
            for(int k = 0;k < 3; k++)
            {
                if(i >= 0 || i < 8 )
                {
                    c = Bord.Instance.moves[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }

        //後ろ
        i = CurrentX - 1;
        j = CurrentY - 1;
        if (CurrentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = Bord.Instance.moves[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }

        //左(真ん中)
        if(CurrentX !=0)
        {
            c = Bord.Instance.moves[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;
        }

        //右(真ん中)
        if (CurrentX != 7)
        {
            c = Bord.Instance.moves[CurrentX + 1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;
        }

        return r;
    }
}
