using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PunMg : MonoBehaviourPunCallbacks
{
    //playerナンバー
    int Player_Num;
    OnlinePice onlinePice;
    void Start()
    {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        PhotonNetwork.ConnectUsingSettings();
        //設定
        onlinePice = GameObject.Find("gamelot").GetComponent<OnlinePice>();
    }
    void Update()
    {

    }
    //ルーム入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        //"room"という名前のルームに参加する(なかったら作って参加)
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    // マッチングが成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        //playerナンバー設定
        if (PhotonNetwork.PlayerList.Length != 1)
        {
            Player_Num = 2;
            Debug.Log("プレイヤー1");
        }
        else Player_Num = 1;
        Debug.Log("プレイヤー2");
        Invoke("On", 1.0f);
        //OnlinePice onlinePice = GetComponent<OnlinePice>();
        // マッチング後、ランダムな位置に自分自身のネットワークオブジェクトを生成する
        //var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        //PhotonNetwork.Instantiate("Cube", v, Quaternion.identity,0);
    }
    void On()
    {
        onlinePice.SpawnAllChess();
    }
    //playerナンバー共有
    public int Player_Number()
    {
        return Player_Num;
    }
}

