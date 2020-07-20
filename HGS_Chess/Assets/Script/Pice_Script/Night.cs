using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : Move
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        //左前
        KingMove(CurrentX - 1, CurrentY + 2, ref r);

        //右前
        KingMove(CurrentX + 1, CurrentY + 2, ref r);

        //RogjtiUp
        KingMove(CurrentX + 2, CurrentY + 1, ref r);

        //RightDown
        KingMove(CurrentX + 2, CurrentY - 1, ref r);

        //左後
        KingMove(CurrentX - 1, CurrentY - 2, ref r);

        //右後
        KingMove(CurrentX + 1, CurrentY - 2, ref r);

        //LeftUp
        KingMove(CurrentX - 2, CurrentY + 1, ref r);

        //LeftDown
        KingMove(CurrentX - 2, CurrentY - 1, ref r);

        return r;
    }

    public void KingMove(int x, int y, ref bool[,] r)
    {
        Move c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = Bord.Instance.moves[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
                r[x, y] = true;
        }
    }
}
