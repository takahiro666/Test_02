using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Move
{
    public int hp = 3;
    public int at = 1;
    Pice Pos;
    public override bool[,] PossibleMove()
    {
        Pos = GameObject.Find("gamelot").GetComponent<Pice>();
        bool[,] r = new bool[Pos.X,Pos.Y];
        Move c;
        int i;

        //右
        i = CurrentX;
        while(true)
        {
            i++;
            if (i >= Pos.X)
                break;

            c = Pice.Instance.moves[i, CurrentY];
            if (c == null)
                r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;

                break;
            }
        }

        //左側の動き
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = Pice.Instance.moves[i, CurrentY];
            if (c == null)
                r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;

                break;
            }
        }

        //前の動き
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= Pos.X)
                break;

            c = Pice.Instance.moves[CurrentX, i];
            if (c == null)
                r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;

                break;
            }
        }

        //後ろの動き
        i = CurrentY;
        while (true)
        {
            i--;
            if (i <0)
                break;

            c = Pice.Instance.moves[CurrentX, i];
            if (c == null)
                r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;

                break;
            }
        }
        return r;
    }
}
