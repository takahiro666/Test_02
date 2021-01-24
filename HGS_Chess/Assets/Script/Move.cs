using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{

    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    public bool isWhite;//白駒と黒駒を見るためのフラグ(白駒を手動でオンにする)時間があれば自動でできるように

    //OnlinePice Pos;
    Pice Pos;

    private void Start()
    {
        //Pos = GameObject.Find("gamelot").GetComponent<OnlinePice>();
        Pos = GameObject.Find("gamelot").GetComponent<Pice>();
        //Debug.Log("動く");
    }

    public void SetPosition(int x,int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[Pos.X, Pos.Y];
    }

}
