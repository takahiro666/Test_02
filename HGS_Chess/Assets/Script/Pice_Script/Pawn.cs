using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Move
{
    public int hp = 3;
    public int at = 1;
    Pice Pos;
    public override bool[,] PossibleMove()
    {
        Pos = GameObject.Find("gamelot").GetComponent<Pice>();
        bool[,] r = new bool[Pos.X, Pos.Y];
        Move c, c2;　

        //白駒の動き
        if(isWhite)
        {
            //斜め左
            if(CurrentX != 0 && CurrentY !=6)
            {
                c = Pice.Instance.moves[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX - 1, CurrentY + 1] = true;
                
            }
            //斜め右
            if (CurrentX != 6 && CurrentY != 6)
            {
                c = Pice.Instance.moves[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX + 1, CurrentY + 1] = true;

            }
            //真ん中
            if(CurrentY != 6)
            {
                c = Pice.Instance.moves[CurrentX, CurrentY + 1];
                if (c == null || !c.isWhite)
                    r[CurrentX, CurrentY + 1] = true;
            }
            //真ん中(最初の動き)
            if(CurrentY ==0)
            {
                c = Pice.Instance.moves[CurrentX, CurrentY + 1];
                c2 = Pice.Instance.moves[CurrentX, CurrentY + 2];
                if(c == null & c2 == null)
                    r[CurrentX, CurrentY + 2] = true;

            }
            
        }
        else 
        {
            //黒駒の動き
            if (CurrentX != 0 && CurrentY != 0)
            {

                c = Pice.Instance.moves[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }
            //斜め右
            if (CurrentX != 6 && CurrentY != 0)
            {
                c = Pice.Instance.moves[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }
            //真ん中
            if (CurrentY != 0)
            {
                c = Pice.Instance.moves[CurrentX, CurrentY - 1];
                if (c == null || c.isWhite)
                if (c == null ||c.isWhite)
                    r[CurrentX, CurrentY - 1] = true;
            }
            //真ん中(最初の動き)
            if (CurrentY == 6)
            {
                c = Pice.Instance.moves[CurrentX, CurrentY - 1];
                c2 = Pice.Instance.moves[CurrentX, CurrentY - 2];
                if (c == null & c2 == null)
                    r[CurrentX, CurrentY - 2] = true;
            }
        }     
        return r;
    }
}
