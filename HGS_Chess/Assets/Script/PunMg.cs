using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PunMg : MonoBehaviourPunCallbacks
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
    public override void OnJoinedRoom()
    {
        GetComponent<OnlinePice>();
    }
    // マッチングが成功した時に呼ばれるコールバック
    //public override void OnJoinedRoom()
    //{
    //    // マッチング後、ランダムな位置に自分自身のネットワークオブジェクトを生成する
    //    var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
    //    PhotonNetwork.Instantiate("GamePlayer", v, Quaternion.identity);
    //}
}
