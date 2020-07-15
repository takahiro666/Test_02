using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bord : MonoBehaviour
{
    public GameObject Bord_W;
    public GameObject Bord_B;
    public GameObject Wall_a;
    //※園城追加=================================================
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 1f;
    public List<GameObject> chessmPrefabs;
    private List<GameObject> activeChessm = new List<GameObject>();
    private Quaternion orientation = Quaternion.Euler(0, 180, 0);//角度調整
    //=====================================================
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
    //public int[,] Picecre = new int[10, 10] //駒
    //{
    //    {1,1,1,1,1,1,1,1,1,1 },
    //    {1,5,3,4,6,7,4,3,5,1 },
    //    {1,2,2,2,2,2,2,2,2,1 },
    //    {1,0,0,0,0,0,0,0,0,1 },
    //    {1,0,0,0,0,0,0,0,0,1 },
    //    {1,0,0,0,0,0,0,0,0,1 },
    //    {1,0,0,0,0,0,0,0,0,1 },
    //    {1,2,2,2,2,2,2,2,2,1 },
    //    {1,5,3,4,6,7,4,3,5,1 },
    //    {1,1,1,1,1,1,1,1,1,1 }
    //};
    void Start()
    {
        MapCreate();
        SkpawnAllChess();
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
    //※駒の生成===============================================================================
    private void PiceCreat(int index,Vector3 positione)
    {
        if(index < 6)   //このif文を入れないと黒駒がすべて180度回転し反対方向を向く
        {
            GameObject go = Instantiate(chessmPrefabs[index], positione, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            activeChessm.Add(go);
        }
        else　　　
        {
            GameObject go = Instantiate(chessmPrefabs[index], positione, orientation) as GameObject;
            go.transform.SetParent(transform);
            activeChessm.Add(go);
        }
    }

    //※駒の位置==========================================================================
    private void SkpawnAllChess()
    {
        activeChessm = new List<GameObject>();

        //白駒生成位置=================================================================
        //king
        PiceCreat(0, GetTileCenter(3, 0));

        //Queen
        PiceCreat(1, GetTileCenter(4, 0));

        //Rooks
        PiceCreat(2, GetTileCenter(0, 0));
        PiceCreat(2, GetTileCenter(7, 0));

        //Bishops
        PiceCreat(3, GetTileCenter(2, 0));
        PiceCreat(3, GetTileCenter(5, 0));

        //Night
        PiceCreat(4, GetTileCenter(1, 0));
        PiceCreat(4, GetTileCenter(6, 0));

        //Pawns
        for(int i = 0;i<8;i++)
        {
            PiceCreat(5, GetTileCenter(i, 1));
        }


        //黒駒生成位置======================================================
        //king
        PiceCreat(6, GetTileCenter(3, 7));

        //Queen
        PiceCreat(7, GetTileCenter(4, 7));

        //Rooks
        PiceCreat(8, GetTileCenter(0, 7));
        PiceCreat(8, GetTileCenter(7, 7));

        //Bishops
        PiceCreat(9, GetTileCenter(2, 7));
        PiceCreat(9, GetTileCenter(5, 7));
   
        //Night
        PiceCreat(10, GetTileCenter(1, 7));
        PiceCreat(10, GetTileCenter(6, 7));

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            PiceCreat(11, GetTileCenter(i, 6));
        }
    }
    //※駒を中心に乗せる======================================================================
    private Vector3 GetTileCenter(int x, int y)
    { 
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.y = 0.5f;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }
    //ゲーム終了=============================================================================
    void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Application.Quit();
    }
}
