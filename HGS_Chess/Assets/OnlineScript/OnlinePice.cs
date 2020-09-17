using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class OnlinePice: MonoBehaviourPunCallbacks
{

    
    public int id;
    private PhotonView photonView;
    //※園城追加=================================================
    public static OnlinePice Instance { set; get; }
    private bool[,] allowedMoves { set; get; }

    public Move[,] moves { set; get; }
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
    public int X;
    public int Y;
    public Text tex;
    public int trun = 1;//ターン数
    Fade_trun fade; //自分のターンと相手のターンのフェード
    public Player_cost P1cos;//プレイヤーのコスト
    //=====================================================
  

    private void Start()
    {
        fade = GameObject.Find("Canvas").GetComponent<Fade_trun>();
        tex = GameObject.Find("trun").GetComponentInChildren<Text>();
        P1cos = GameObject.Find("Canvas").GetComponent<Player_cost>();
        //P1cos.PCost();
        ChangeTurn();
      
        //==============
        Instance = this;
        //==============
       
        SpawnAllChess();
        // Debug.Log(trun + "ターン目");
    }

    void Update()
    {
        UpdateSlection();
        DrawChess();
        fade.Change();
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChess == null)
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit, 100f, LayerMask.GetMask("ChessPlane")))
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

   
    //※駒の位置==========================================================================
    private void SpawnAllChess()
    {
        activeChessm = new List<GameObject>();
        moves = new Move[7, 7];
        //白駒生成位置=================================================================
        for (int i = 1; i < X - 1; i++)
        {
            if (i != 0 && i < X)
            {
                if (i != 3)
                    PiceCreat(1, i, 0);
                else
                    PiceCreat(0, i, 0);
            }
        }
        //黒駒生成位置================================================================
        for (int i = 1; i < X - 1; i++)
        {
            if (i != 0 && i < X)
            {
                if (i != 3)
                    PiceCreat(3, i, Y - 1);
                else
                    PiceCreat(2, i, Y - 1);
            }
        }
    }

    //駒の選択==================================================================
    private void SelectChess(int x, int y)
    {
        if (moves[x, y] == null)
            return;

        if (moves[x, y].isWhite != isWiteTurn)
            return;

        allowedMoves = moves[x, y].PossibleMove();
        for (int i = 0; i < X; i++)
            for (int j = 0; j < Y; j++)
                if (allowedMoves[i, j])
                    hasAtleastOneMove = true;

        selectedChess = moves[x, y];
        BoarHi.Instance.HighlightAllowedMoves(allowedMoves);

    }

    //選択した駒を動かす=================================================================
    private void MoveChess(int x, int y)
    {

        if (allowedMoves[x, y])
        {
            Move c = moves[x, y];
            if (c != null && c.isWhite != isWiteTurn)
            {
                //駒を捕まえたとき
                //キングかどうか
                if (c.GetType() == typeof(King))
                {//ゲーム終了
                    SceneManager.LoadScene("TitleScene");
                    //EndGame();
                    //return;
                }
                activeChessm.Remove(c.gameObject);
                Destroy(c.gameObject);  //消滅させる
            }

            moves[selectedChess.CurrentX, selectedChess.CurrentY] = null;
            selectedChess.transform.position = GetTileCenter(x, y);
            selectedChess.SetPosition(x, y);
            moves[x, y] = selectedChess;
            P1cos.PCost();
            trun++;
            isWiteTurn = !isWiteTurn; //白と黒のターン入れ替え
            ChangeTurn();
            fade.Qtext.enabled = true;//表示
            //Debug.Log(trun + "ターン目");
            //Debug.Log("黒のターン");

        }
        BoarHi.Instance.Hidehighlights();
        selectedChess = null;
    }
    //=========================================================================================
    //※駒の生成===============================================================================
    private void PiceCreat(int index, int x, int y)
    {
        if (index < 2)   //このif文を入れないと黒駒がすべて180度回転し反対方向を向く
        {
            GameObject go = Instantiate(chessmPrefabs[index], GetTileCenter(x, y), Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            moves[x, y] = go.GetComponent<Move>();
            moves[x, y].SetPosition(x, y);
            activeChessm.Add(go);
        }
        else
        {
            GameObject go = Instantiate(chessmPrefabs[index], GetTileCenter(x, y), orientation) as GameObject;
            go.transform.SetParent(transform);
            moves[x, y] = go.GetComponent<Move>();
            moves[x, y].SetPosition(x, y);
            activeChessm.Add(go);
            moves[x, y] = go.GetComponent<Move>();
            moves[x, y].SetPosition(x, y);

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

    //自分の真進カーソルの位置を描画==========================================================
    private void DrawChess()
    {
        Vector3 widthLine = Vector3.right * X;
        Vector3 heigthLine = Vector3.forward * Y;

        for (int i = 0; i <= X; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= X; j++)
            {
                start = Vector3.right * i;
                Debug.DrawLine(start, start + heigthLine);
            }
        }

        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
        }
    }
    //キング取られたときのゲーム終了処理
    private void EndGame()
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
    //プレイヤー1とプレイヤー2のターンを表示する
    private void ChangeTurn()
    {
        if (trun % 2 != 0)
        {

            tex.color = new Color(1, 0, 0, 1);
            tex.text = "Player1のターン";

        }
        else
        {
            tex.color = new Color(0, 0, 1, 1);
            tex.text = "Player2のターン";
        }

    }
}
