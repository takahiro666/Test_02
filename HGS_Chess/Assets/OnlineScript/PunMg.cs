using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PunMg : MonoBehaviourPunCallbacks
{
    OnlinePice onlinePice;
    void Start()
    {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        PhotonNetwork.ConnectUsingSettings();
        //設定
       // onlinePice = GameObject.Find("gamelot").GetComponent<OnlinePice>();
    }
    void Update()
    {
        //スペースキーでルーム作成
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //"room"という名前のルームに参加する(なかったら作って参加)
            PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        }
    }
    //ルーム入室前に呼び出される
    public override void OnConnectedToMaster()
    {

    }

    // マッチングが成功した時に呼ばれるコールバック
    //public override void OnJoinedRoom()
    //{
    //    //ルームに入ったらonlinepiceのSpawnAllChessを起動
    //        onlinePice.SpawnAllChess();
    //}
}

