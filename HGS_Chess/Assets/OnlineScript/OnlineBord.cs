using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineBord : MonoBehaviour
{
    public GameObject Bord_W;
    public GameObject Bord_B;
    public int X;
    public int Y;
    public int[,] Chessbord; //基盤
    // Start is called before the first frame update
    void Start()
    {
        Chessbord = new int[X, Y];
        BordArray();
        MapCreate();
    }

    // Update is called once per frame
    
    // ボードの配列を作成======================================================
    void BordArray()
    {
        for (int i = 0; i < Y; i++)
        {
            for (int j = 0; j < X; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                        Chessbord[i, j] = 0;
                    else
                        Chessbord[i, j] = 1;
                }
                else
                {
                    if (j % 2 == 0)
                        Chessbord[i, j] = 1;
                    else
                        Chessbord[i, j] = 0;
                }
            }
        }
    }

    //マップ生成======================================================================================================
    void MapCreate()  //盤面生成
    {
        for (int i = 0; i < Chessbord.GetLength(0); i++)
        {
            for (int j = 0; j < Chessbord.GetLength(1); j++)
            {
                if (Chessbord[i, j] == 0)
                {
                    Instantiate(Bord_B, new Vector3(i + 0.5f, -0.5f, j + 0.5f), Quaternion.identity);
                    Bord_B.name = i + "-" + j.ToString();
                }
                else /*if (Chessbord[i, j] == 1)*/
                {
                    Instantiate(Bord_W, new Vector3(i + 0.5f, -0.5f, j + 0.5f), Quaternion.identity);
                    Bord_W.name = i + "-" + j.ToString();
                }
            }
        }
    }

}
