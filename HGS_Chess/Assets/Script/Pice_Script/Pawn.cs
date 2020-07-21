using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Move
{
   public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Move c, c2;　

        //白駒の動き
        if(isWhite)
        {
            //斜め左
            if(CurrentX != 0 && CurrentY !=7)
            {
                c = Bord.Instance.moves[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX - 1, CurrentY + 1] = true;
            }
            //斜め右
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = Bord.Instance.moves[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX + 1, CurrentY + 1] = true;
            }
            //真ん中
            if(CurrentY != 7)
            {
                c = Bord.Instance.moves[CurrentX, CurrentY + 1];
                if (c == null)
                    r[CurrentX, CurrentY + 1] = true;
            }
            //真ん中(最初の動き)
            if(CurrentY ==1)
            {
                c = Bord.Instance.moves[CurrentX, CurrentY + 1];
                c2 = Bord.Instance.moves[CurrentX, CurrentY + 2];
                if(c == null & c2 == null)
                    r[CurrentX, CurrentY + 2] = true;

            }
        }
        else 
        {
            //黒駒の動き
            if (CurrentX != 0 && CurrentY != 0)
            {

                c = Bord.Instance.moves[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }
            //斜め右
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = Bord.Instance.moves[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }
            //真ん中
            if (CurrentY != 0)
            {
                c = Bord.Instance.moves[CurrentX, CurrentY - 1];
                if (c == null)
                    r[CurrentX, CurrentY - 1] = true;
            }
            //真ん中(最初の動き)
            if (CurrentY == 6)
            {
                c = Bord.Instance.moves[CurrentX, CurrentY - 1];
                c2 = Bord.Instance.moves[CurrentX, CurrentY - 2];
                if (c == null & c2 == null)
                    r[CurrentX, CurrentY - 2] = true;
            }
        }     
        return r;
    }
}
