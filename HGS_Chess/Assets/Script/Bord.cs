﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bord : MonoBehaviour
{
    public GameObject Bord_W;
    public GameObject Bord_B;
    public GameObject Wall_a;
    public GameObject Wall_b;
    //※園城追加=================================================
    public static Bord Instance { set; get; }
    private bool[,] allowedMoves { set; get; }

    public Move[,] moves { set; get;}
    private Move selectedChess;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;
    public List<GameObject> chessmPrefabs;
    private List<GameObject> activeChessm = new List<GameObject>();

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);//角度調整

    bool hasAtleastOneMove = false;

    public bool isWiteTurn = true;  //白駒のターンならture黒駒ならfalse
    //=====================================================
    // public int[,] Chessbord = new int[10, 10]   //基盤
    //{
    //    {2,2,2,2,2,2,2,2,2,2},
    //    {2,0,1,0,1,0,1,0,1,2},
    //    {2,1,0,1,0,1,0,1,0,2},
    //    {2,0,1,0,1,0,1,0,1,2},
    //    {2,1,0,1,0,1,0,1,0,2},
    //    {2,0,1,0,1,0,1,0,1,2},
    //    {2,1,0,1,0,1,0,1,0,2},
    //    {2,0,1,0,1,0,1,0,1,2},
    //    {2,1,0,1,0,1,0,1,0,2},
    //    {2,2,2,2,2,2,2,2,2,2},
    //};
    //public int[,] Chessbord = new int[8, 8]  //基盤8*8
    //{
    //    {0,1,0,1,0,1,0,1},
    //    {1,0,1,0,1,0,1,0},
    //    {0,1,0,1,0,1,0,1},
    //    {1,0,1,0,1,0,1,0},
    //    {0,1,0,1,0,1,0,1},
    //    {1,0,1,0,1,0,1,0},
    //    {0,1,0,1,0,1,0,1},
    //    {1,0,1,0,1,0,1,0},
    //};
    ////public int[,] Picecre = new int[10, 10] //駒
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
    private void Start()
    {
        //==============
        Instance = this;
        //==============
        //MapCreate();
        SpawnAllChess();
       
    }
   
    void Update()
    {
        UpdateSlection();
        DrawChess();
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(gameObject.name);
            if(selectionX >= 0 && selectionY >= 0)
            {
                if(selectedChess == null)
                {
                    //チェスが選択された
                    SelectChess(selectionX, selectionY);
                }
                else
                {
                    //駒が動かせる状態か
                    MoveChess(selectionX, selectionY);
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape)) Quit();
    }

    //レイを作成しコライダーに当たったら色を変える==============================================================================
    private void UpdateSlection()  
    {
        if (!Camera.main)
            return;

        Vector3 pos = Input.mousePosition;
        pos.z = 10.0f;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        //Debug.DrawRay(ray.origin, ray.direction * 20.0f);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit,100f,LayerMask.GetMask("ChessPlane")))
        {
            //Debug.Log(hit.point);
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
            if (hit.collider.gameObject.GetComponent<changecolor>())
            {     
                hit.collider.gameObject.GetComponent<changecolor>().selectflg = true;                                
            }          
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }
    //================================================================================================================
    //マップ生成======================================================================================================
    //void MapCreate()  //盤面生成
    //{
    //    for (int i = 0; i < Chessbord.GetLength(0); i++)
    //    {
    //        for (int j = 0; j < Chessbord.GetLength(1); j++)
    //        {
    //            if (Chessbord[i, j] == 0)
    //            {
    //                Instantiate(Bord_B, new Vector3(i, 0, j), Quaternion.identity);
    //                Bord_B.name = i + "-" + j.ToString();
    //            }
    //            else if (Chessbord[i, j] == 1)
    //            {
    //                Instantiate(Bord_W, new Vector3(i, 0, j), Quaternion.identity);
    //                Bord_W.name = i + "-" + j.ToString();
    //            }
    //            else
    //            {
    //                Instantiate(Wall_a, new Vector3(i, 0, j), Quaternion.identity);
    //                //Instantiate(Wall_b, new Vector3(i, 0, j), Quaternion.identity);
    //                Wall_a.name = Wall_a.ToString();
    //            }
    //        }
    //    }
    //}
    //駒の選択==================================================================
    private void SelectChess(int x,int y)
    {
        if (moves[x, y] == null)
            return;

        if(moves[x,y].isWhite != isWiteTurn)
            return;

        allowedMoves = moves[x, y].PossibleMove();
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (allowedMoves[i, j])
                    hasAtleastOneMove = true;
            
        selectedChess = moves[x, y];
        BoarHi.Instance.HighlightAllowedMoves(allowedMoves);
        
    }

    //選択した駒を動かす=================================================================
    private void MoveChess(int x,int y)
    {
        
        if(allowedMoves[x,y] )
        {
            Move c = moves[x, y];         
            if (c != null && c.isWhite != isWiteTurn)
            {
                //駒を捕まえたとき
                //キングかどうか
                if (c.GetType() == typeof(King))
                {//ゲーム終了
                    EndGmae();
                    return;
                }
                activeChessm.Remove(c.gameObject);
                Destroy(c.gameObject);  //消滅させる
            }

            moves[selectedChess.CurrentX, selectedChess.CurrentY] = null;
            selectedChess.transform.position = GetTileCenter(x, y);
            selectedChess.SetPosition(x, y);
            moves[x, y] = selectedChess;
            //isWiteTurn = !isWiteTurn; //白と黒のターン入れ替え
            Debug.Log("黒のターン");
            
        }
        BoarHi.Instance.Hidehighlights();
        selectedChess = null;
    }
    //=========================================================================================
    //※駒の生成===============================================================================
    private void PiceCreat(int index,int x,int y)
    {
        if(index < 6)   //このif文を入れないと黒駒がすべて180度回転し反対方向を向く
        {
            GameObject go = Instantiate(chessmPrefabs[index], GetTileCenter(x,y), Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            moves[x,y] = go.GetComponent<Move>();
            moves[x, y].SetPosition(x, y);
            activeChessm.Add(go);
        }
        else　　　
        {
            GameObject go = Instantiate(chessmPrefabs[index], GetTileCenter(x, y), orientation) as GameObject;
            go.transform.SetParent(transform);
            activeChessm.Add(go);
        }
    }

    //※駒の位置==========================================================================
    private void SpawnAllChess()
    {
        activeChessm = new List<GameObject>();
        moves = new Move[8, 8];

        //白駒生成位置=================================================================
        //king
        PiceCreat(0, 3, 0);

        //Queen
        PiceCreat(1, 4, 0);

        //Rooks
        PiceCreat(2, 0, 0);
        PiceCreat(2, 7, 0);

        //Bishops
        PiceCreat(3, 2, 0);
        PiceCreat(3, 5, 0);

        //Night
        PiceCreat(4, 1, 0);
        PiceCreat(4, 6, 0);

        //Pawns
        for(int i = 0;i<8;i++)
        {
            PiceCreat(5, i, 1);
        }

        //黒駒生成位置======================================================
        //king
        PiceCreat(6, 3, 7);

        //Queen
        PiceCreat(7, 4, 7);

        //Rooks
        PiceCreat(8, 0, 7);
        PiceCreat(8, 7, 7);

        //Bishops
        PiceCreat(9, 2, 7);
        PiceCreat(9, 5, 7);

        //Night
        PiceCreat(10, 1, 7);
        PiceCreat(10, 6, 7);

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            PiceCreat(11, i, 6);
        }
    }
    //※駒を中心に乗せる======================================================================
    private Vector3 GetTileCenter(int x, int y)
    { 
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET ;
        origin.y = 0.5f;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET ;
        return origin;
    }

    //自分の真進カーソルの位置を描画==========================================================
    private void DrawChess()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heigthLine = Vector3.forward * 8;

        for(int i =0; i<=8;i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start,start+widthLine);
            for (int j =0; j<=8;j++)
            {
                start = Vector3.right * i;
                Debug.DrawLine(start, start + heigthLine);
            }
        }

        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY+1 ) + Vector3.right * (selectionX+1 ));
        }
    }
    //ゲーム終了=============================================================================
    private void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Application.Quit();
    }

    //キング取られたときのゲーム終了処理
    private void EndGmae()
    {
        if (isWiteTurn)
            Debug.Log("白チームの勝ち");
        else
            Debug.Log("黒チームの勝ち");

        foreach (GameObject go in activeChessm)
            Destroy(go);

        //初期化
        isWiteTurn = true;
        BoarHi.Instance.Hidehighlights();
        SpawnAllChess();
    }
}