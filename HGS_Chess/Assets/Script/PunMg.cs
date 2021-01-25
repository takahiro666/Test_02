using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Punmg : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        PhotonNetwork.ConnectUsingSettings();
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
        GetComponent<OnlinePice>();
        GetComponent<King>();
        Debug.Log("ピース");
    }
}
