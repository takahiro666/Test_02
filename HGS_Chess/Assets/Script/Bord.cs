using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bord : MonoBehaviour
{
    public GameObject Bord_W;
    public GameObject Bord_B;
    public GameObject Wall_a;
    public GameObject Pown;
    public GameObject Night;
    public GameObject Bishop;
    public GameObject Luke;
    public GameObject Queen;
    public GameObject King;
    //public GameObject Wall_b;
    public int[,] Chessbord = new int[10, 10]   //基盤
    {
        {2,2,2,2,2,2,2,2,2,2},
        {2,0,1,0,1,0,1,0,1,2},
        {2,1,0,1,0,1,0,1,0,2},
        {2,0,1,0,1,0,1,0,1,2},
        {2,1,0,1,0,1,0,1,0,2},
        {2,0,1,0,1,0,1,0,1,2},
        {2,1,0,1,0,1,0,1,0,2},
        {2,0,1,0,1,0,1,0,1,2},
        {2,1,0,1,0,1,0,1,0,2},
        {2,2,2,2,2,2,2,2,2,2},
    };
    public int[,] Picecre = new int[10, 10] //駒
    {
        {1,1,1,1,1,1,1,1,1,1 },
        {1,5,3,4,6,7,4,3,5,1 },
        {1,2,2,2,2,2,2,2,2,1 },
        {1,0,0,0,0,0,0,0,0,1 },
        {1,0,0,0,0,0,0,0,0,1 },
        {1,0,0,0,0,0,0,0,0,1 },
        {1,0,0,0,0,0,0,0,0,1 },
        {1,2,2,2,2,2,2,2,2,1 },
        {1,5,3,4,6,7,4,3,5,1 },
        {1,1,1,1,1,1,1,1,1,1 }
    };
    void Start()
    {
        MapCreate();
        PiceCreat();
        
    }
   
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Quit();
    }

    //マップ生成
    void MapCreate()  //盤面生成
    {
        for (int i = 0; i < Chessbord.GetLength(0); i++)
        {
            for (int j = 0; j < Chessbord.GetLength(1); j++)
            {
                if (Chessbord[i, j] == 0)
                {
                    Instantiate(Bord_B, new Vector3(i, 0, j), Quaternion.identity);
                    Bord_B.name = i + "-" + j.ToString();
                }
                else if (Chessbord[i, j] == 1)
                {
                    Instantiate(Bord_W, new Vector3(i, 0, j), Quaternion.identity);
                    Bord_W.name = i + "-" + j.ToString();
                }
                else
                {
                    Instantiate(Wall_a, new Vector3(i, 0, j), Quaternion.identity);
                    //Instantiate(Wall_b, new Vector3(i, 0, j), Quaternion.identity);
                    Wall_a.name = Wall_a.ToString();
                }
            }
        }
    }
    void PiceCreat() //駒の生成
    {
        for (int i = 0; i < Picecre.GetLength(0); i++)
        {
            for (int j = 0; j < Picecre.GetLength(1); j++)
            {         
                if (Picecre[i, j] == 2)     //ポーン
                {
                    Instantiate(Pown, new Vector3(i, 1, j), Quaternion.identity);
                    Pown.name = "ポーン"+i.ToString();
                }
                else if (Picecre[i, j] == 3)    //ナイト
                {
                    Instantiate(Night, new Vector3(i, 1, j), Quaternion.identity);
                    Night.name = "ナイト" + i.ToString();
                }
                else if (Picecre[i, j] == 4)    //ビショップ
                {
                    Instantiate(Bishop, new Vector3(i, 1, j), Quaternion.identity);
                    Bishop.name = "ビショップ" + i.ToString();
                }
                else if (Picecre[i, j] == 5)        //ルーク
                {
                    Instantiate(Luke, new Vector3(i, 1, j), Quaternion.identity);
                    Luke.name = "ルーク" + i.ToString();
                }
                else if (Picecre[i, j] == 6)    //クイーン
                {
                    Instantiate(Queen, new Vector3(i, 1, j), Quaternion.identity);
                    Queen.name = "クイーン" + i.ToString();
                }
                 else if (Picecre[i, j] == 7)    //キング
                {
                    Instantiate(King, new Vector3(i, 1, j), Quaternion.identity);
                    King.name = "キング" + i.ToString();
                }
                else if(Picecre[i,j] ==1)
                {
                    Instantiate(Wall_a, new Vector3(i, 1, j), Quaternion.identity);
                    Wall_a.name = Wall_a.ToString();
                }

            }
        }
    }

    //ゲーム終了
    void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Application.Quit();
    }
}
