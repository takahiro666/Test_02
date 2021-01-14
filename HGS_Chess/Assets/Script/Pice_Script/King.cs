using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class King : Move
{
    public int hp = 3;
    public int at = 1;
    OnlinePice Pos;
    private PhotonView photonView = null;

     void Awake()
    {
        photonView = GetComponent<PhotonView>(); 
    }
    public override bool[,] PossibleMove()
    {
        Pos = GameObject.Find("gamelot").GetComponent<OnlinePice>();
       // Pos = GameObject.Find("gamelot").GetComponent<Pice>();

        bool[,] r = new bool[Pos.X, Pos.Y];
        Move c;
        int i, j;

        // Top side
        i = CurrentX - 1;
        j = CurrentY + 1;
        if(CurrentY != 6)
        {
            c = OnlinePice.Instance.moves[CurrentX, CurrentY + 1];
            //c = Pice.Instance.moves[CurrentX, CurrentY + 1];
            if (c == null)
                r[CurrentX , CurrentY + 1] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX , CurrentY + 1] = true;
            if (CurrentX!=0)//左上表示
            {
                c = OnlinePice.Instance.moves[CurrentX-1, CurrentY + 1];
                //c = Pice.Instance.moves[CurrentX-1, CurrentY + 1];
                if (c == null)
                    r[CurrentX-1, CurrentY + 1] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX-1, CurrentY + 1] = true;
            }
            if(CurrentX!=Pos.X-1)
            {
                c = OnlinePice.Instance.moves[CurrentX+1, CurrentY + 1];
                //c = Pice.Instance.moves[CurrentX+1, CurrentY + 1];
                if (c == null)
                    r[CurrentX+1, CurrentY + 1] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX+1, CurrentY + 1] = true;
            }
               
               
            
        }

        //後ろ
        i = CurrentX - 1;
        j = CurrentY - 1;
        if (CurrentY != 0)
        {
            c = OnlinePice.Instance.moves[CurrentX, CurrentY - 1];
            //c = Pice.Instance.moves[CurrentX, CurrentY - 1];
            if (c == null)
                r[CurrentX, CurrentY - 1] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX, CurrentY - 1] = true;
            if (CurrentX != 0)//左上表示
            {
                c = OnlinePice.Instance.moves[CurrentX - 1, CurrentY - 1];
               // c = Pice.Instance.moves[CurrentX - 1, CurrentY - 1];
                if (c == null)
                    r[CurrentX - 1, CurrentY - 1] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }
            if (CurrentX != Pos.X - 1)
            {
                c = OnlinePice.Instance.moves[CurrentX + 1, CurrentY - 1];
                //c = Pice.Instance.moves[CurrentX + 1, CurrentY - 1];
                if (c == null)
                    r[CurrentX + 1, CurrentY - 1] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }
        }

        //左(真ん中)
        if(CurrentX != 0)
        {
            c = OnlinePice.Instance.moves[CurrentX -1, CurrentY];
            //c = Pice.Instance.moves[CurrentX -1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;
        }

        //右(真ん中)
        if (CurrentX != 6)
        {
            c = OnlinePice.Instance.moves[CurrentX +1, CurrentY];
            //c = Pice.Instance.moves[CurrentX +1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;
        }

        return r;
    }
    void Update()
    {
       // if (!photonView.IsMine) return; 
    }
}
