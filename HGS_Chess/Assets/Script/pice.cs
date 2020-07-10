using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pice : MonoBehaviour
{
   
    public enum PiceType
    {
        Pown = 2,    //ぽーん
        Night = 3,   //ナイト
        Bishop = 4,  //ビショップ
        Luke = 5,    //ルーク
        Queen = 6,   //クイーン
        King = 7,    //キング
        None =-1
    }

    public PiceType PICE;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PiceSelect()
    {
        switch(PICE)
        {
            case PiceType.Pown:
                //  挙動の処理
                break;
            case PiceType.Night:
                break;
            case PiceType.Luke:
                break;
            case PiceType.Bishop:
                break;
            case PiceType.Queen:
                break;
            case PiceType.King:
                break;

        }
    }

   
}
