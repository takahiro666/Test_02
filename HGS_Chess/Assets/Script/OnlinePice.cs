using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
public class OnlinePice : MonoBehaviourPunCallbacks
{

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

    private Quaternion wite_A = Quaternion.Euler(270, 0, 0);
    private Quaternion orientation = Quaternion.Euler(270, 180, 0);//黒駒角度調整
    bool hasAtleastOneMove = false;
    public bool isWiteTurn = true;  //白駒のターンならture黒駒ならfalse
    public int X;   //チェス盤の横軸長さ
    public int Y;   //チェス盤の縦軸の長さ

    //UI
    public Text tex;
    public Text Player1_cos;    //プレイヤー1コストのテキスト
    public Text Player2_cos;    //プレイヤー2コストのテキスト
    public Text syohaitex;
    private int P1_cos;//プレイヤー1のコスト
    private int P2_cos;//プレイヤー2のコスト
    private int maxcos = 5;//コストの最大値
    public int trun = 1;//ターン数
    Fade_trun fade; //自分のターンと相手のターンのフェード

    public GameObject but;     //進化ボタン
    public GameObject evopice;  //進化先のピースのボタン
    public GameObject instanpice;//生成のボタン
    public GameObject pawnbutton;//ポーンのボタン
    public GameObject syouhai;

    int xpos, ypos;//進化元のオブジェクトの座標
    GameObject destryobj;//進化元のオブジェクト
    //GameObject tag;//進化元のオブジェクトのタグを取得するためのもの
    //=====================================================

    //※青木追加===========================================

    //public GameObject MagicCircle;

    //=====================================================

    private void Start()
    {
        fade = GameObject.Find("Canvas").GetComponent<Fade_trun>();
        P1_cos = 1;   //プレイヤー１のターン数
        Player1_cos.text = P1_cos.ToString();//プレイヤー１のターン数をtextに表示
        but.SetActive(false);//進化ボタンを非表示にする
        evopice.SetActive(false);
        instanpice.SetActive(false);
        pawnbutton.SetActive(false);
        syouhai.SetActive(false);
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
        //進化フェーズの処理
        if (Input.GetMouseButtonDown(0))
        {
            RayHItObj();
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
                    if (trun >= 3)
                    {
                        but.SetActive(false);
                        instanpice.SetActive(false);
                    }

                }
            }
            but.SetActive(false);
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
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
            //レイを作成しコライダーに当たったら色を変える
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
        moves = new Move[X, Y];
        //白駒生成位置=================================================================
        for (int i = 1; i < X - 1; i++)
        {
            if (i != 0 && i < X)
            {
                if (i != 3)
                    PiceCreat(5, i, 0);     //PiceCreat(リストの番号,x座標,y座標)
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
                    PiceCreat(11, i, Y - 1);
                else
                    PiceCreat(6, i, Y - 1);
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
        BoarHi.Instance.HighlightAllowedMoves(allowedMoves);    //駒の行動範囲にPlaneを生成
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
                {
                    EndGame();
                    return;
                }
                if (isWiteTurn) //P1がP2の駒を取った時にP1のコストを+1
                {
                    if (P1_cos < maxcos)
                    {
                        P1_cos++;
                        Player1_cos.text = P1_cos.ToString();
                    }

                }
                else //P2がP1の駒を取った時に21のコストを+1
                {
                    if (P2_cos < maxcos)
                    {
                        P2_cos++;
                        Player2_cos.text = P2_cos.ToString();
                    }
                }
                activeChessm.Remove(c.gameObject);
                Destroy(c.gameObject);  //消滅させる
            }
            moves[selectedChess.CurrentX, selectedChess.CurrentY] = null;
            selectedChess.transform.position = GetTileCenter(x, y);
            selectedChess.SetPosition(x, y);
            moves[x, y] = selectedChess;
            PCost();
            trun++;
            isWiteTurn = !isWiteTurn; //白と黒のターン入れ替え
            ChangeTurn();
            fade.Qtext.enabled = true;//表示
            //Debug.Log(trun + "ターン目");
            //Debug.Log("黒のターン"+isWiteTurn);

        }
        BoarHi.Instance.Hidehighlights();
        selectedChess = null;
    }
    //=========================================================================================
    //※駒の生成===============================================================================
    private void PiceCreat(int index, int x, int y)
    {
        if (index > 6)   //このif文を入れないと黒駒がすべて180度回転し反対方向を向く
        {
            GameObject go = Instantiate(chessmPrefabs[index], GetTileCenter(x, y), wite_A) as GameObject;
            go.transform.SetParent(transform);
            moves[x, y] = go.GetComponent<Move>();
            moves[x, y].SetPosition(x, y);
            activeChessm.Add(go);
        }
        else //白駒の
        {
            GameObject go = Instantiate(chessmPrefabs[index], GetTileCenter(x, y), orientation) as GameObject;
            go.transform.SetParent(transform);
            moves[x, y] = go.GetComponent<Move>();
            moves[x, y].SetPosition(x, y);
            activeChessm.Add(go);
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

    //自分の真進カーソルの位置を描画チェス盤をギズモで見れるようにする================================-
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
    //キング取られたときのゲーム終了処理=========================================================
    private void EndGame()
    {
        tex.enabled = false;
        syouhai.SetActive(true);
        if (isWiteTurn)
        {
            syohaitex.color = new Color(1, 0, 0, 1);
            syohaitex.text = "Player1の勝利";
            Invoke("EndSern", 2);
            //Debug.Log("白チームの勝ち");
        }
        else
        {
            syohaitex.color = new Color(0, 0, 1, 1);
            syohaitex.text = "Player2の勝利";
            Invoke("EndSern", 2);
            //Debug.Log("黒チームの勝ち");
        }
        foreach (GameObject go in activeChessm)//残ってる駒をデストロイ
            Destroy(go);

        ////初期化
        //isWiteTurn = true;
        //BoarHi.Instance.Hidehighlights();
        //SpawnAllChess();
        //but.SetActive(false);//進化ボタンを非表示にする

    }
    //プレイヤー1とプレイヤー2のターンを表示する============================================================
    private void ChangeTurn()
    {
        if (isWiteTurn)
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
    //コスト処理======================================
    private void PCost()
    {
        if (trun % 2 != 0)//プレイヤー２のコスト加算処理
        {
            if (P2_cos < maxcos)
            {
                P2_cos++;
                Player2_cos.text = P2_cos.ToString();
            }
            else
                Player2_cos.text = P2_cos.ToString();
        }
        else//プレイヤー1のコスト加算処理
        {
            if (P1_cos < maxcos)
            {
                P1_cos++;
                Player1_cos.text = P1_cos.ToString();
            }
            else
                Player1_cos.text = P1_cos.ToString();
        }
    }

    //進化ボタンを押したときの処理
    public void Evolution_Button()
    {
        but.SetActive(false);
        evopice.SetActive(true);
        instanpice.SetActive(false);
        pawnbutton.SetActive(false);

    }
    public void InstanPice()
    {
        instanpice.SetActive(false);
        pawnbutton.SetActive(true);
        but.SetActive(false);
    }
    private void RayHItObj()//進化元のオブジェクトの位置取得
    {
        if (!Camera.main)
            return;

        //進化フェーズが終わり次のフェーズの呼び出し
        Vector3 pos = Input.mousePosition;
        pos.z = 10.0f;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit, 100f, LayerMask.GetMask("Pice")))//ここに進化元を削除する
        {
            Debug.Log(hit.collider.gameObject == isWiteTurn);
            if (trun >= 3)
            {
                but.SetActive(true);//進化ボタンを表示する
                evopice.SetActive(false);
                instanpice.SetActive(false);
                pawnbutton.SetActive(false);
            }

            xpos = (int)hit.point.x;
            ypos = (int)hit.point.z;
            destryobj = hit.collider.gameObject;//進化元のオブジェクト取得
            Debug.Log(destryobj.tag);
        }
        else if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit, 100f, LayerMask.GetMask("ChessPlane")))//ここに進化元を削除する
        {
            xpos = (int)hit.point.x;
            ypos = (int)hit.point.z;
            if (trun >= 3 && ypos < 1 || ypos > 5)
            {
                instanpice.SetActive(true);
                evopice.SetActive(false);
                but.SetActive(false);
            }
            else
                instanpice.SetActive(false);

        }
        //but.SetActive(false);
    }

    //駒の進化===================================================================================-
    public void PiceEvolution_Knight()//ナイトの進化処理
    {
        if (isWiteTurn && destryobj.tag == "Wite" && P1_cos >= 2)
        {
            Destroy(destryobj);
            PiceCreat(4, xpos, ypos);   //knight生成
            P1_cos = P1_cos - 2;
            Player1_cos.text = P1_cos.ToString();
        }

        if (!isWiteTurn && destryobj.tag == "Black" && P2_cos >= 2)
        {
            Destroy(destryobj);
            PiceCreat(10, xpos, ypos);   //knight生成
            P2_cos = P2_cos - 2;
            Player2_cos.text = P2_cos.ToString();
        }
    }
    //ルークの進化処理
    public void PiceEvolution_Rook()
    {
        if (isWiteTurn && P1_cos >= 3 && destryobj.tag == "Wite")
        {
            Destroy(destryobj);
            PiceCreat(3, xpos, ypos);   //rook生成
            P1_cos = P1_cos - 3;
            Player1_cos.text = P1_cos.ToString();
        }
        else if (!isWiteTurn && P2_cos >= 3 && destryobj.tag == "Black")
        {
            Destroy(destryobj);
            PiceCreat(9, xpos, ypos);   //rook生成
            P2_cos = P2_cos - 3;
            Player2_cos.text = P2_cos.ToString();
        }
    }
    //ビショップの進化処理
    public void PiceEvolution_Bishop()
    {
        if (isWiteTurn && P1_cos >= 3 && destryobj.tag == "Wite")
        {
            Destroy(destryobj);
            PiceCreat(2, xpos, ypos);   //rook生成
            P1_cos = P1_cos - 3;
            Player1_cos.text = P1_cos.ToString();
        }
        else if (!isWiteTurn && P2_cos >= 3 && destryobj.tag == "Black")
        {
            Destroy(destryobj);
            PiceCreat(8, xpos, ypos);   //rook生成
            P2_cos = P2_cos - 3;
            Player2_cos.text = P2_cos.ToString();
        }
    }
    //クイーン進化処理
    public void PiceEvolution_Queen()
    {
        if (isWiteTurn && P1_cos >= 4 && destryobj.tag == "Wite")
        {
            Destroy(destryobj);
            PiceCreat(1, xpos, ypos);   //rook生成
            P1_cos = P1_cos - 4;
            Player1_cos.text = P1_cos.ToString();
        }
        else if (!isWiteTurn && P2_cos >= 4 && destryobj.tag == "Black")
        {
            Destroy(destryobj);
            PiceCreat(7, xpos, ypos);   //rook生成
            P2_cos = P2_cos - 4;
            Player2_cos.text = P2_cos.ToString();
        }
    }
    //ポーン生成処理
    public void Pawninstant()
    {
        if (isWiteTurn && P1_cos >= 1 && trun % 2 != 0 && ypos < 1)
        {
            PiceCreat(5, xpos, ypos);
            P1_cos = P1_cos - 1;
            Player1_cos.text = P1_cos.ToString();
        }
        else if (!isWiteTurn && P2_cos >= 1 && trun % 2 == 0 && ypos > 5)
        {
            PiceCreat(11, xpos, ypos);
            P2_cos = P2_cos - 1;
            Player2_cos.text = P2_cos.ToString();
        }
    }
    private void EndSern()//強制的にシーンを移動させる
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    Debug.Log("A");
        //ゲーム終了
        SceneManager.LoadScene("TitleScene");
        //}
    }
}
