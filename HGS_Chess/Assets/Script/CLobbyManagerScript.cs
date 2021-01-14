using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HashTable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

namespace Com.MyCompany.MyGame
{
    public class CLobbyManagerScript : MonoBehaviourPunCallbacks
    {
        #region Public Variables
        //部屋一覧表示用オブジェクト
        public GameObject RoomParent;
        public GameObject RoomElementPrefab;
        //ルーム接続情報表示用Text
        public Text InfoText;
        #endregion
        #region Monobehaviour CallBAcks
        void Awake()
        {
            //ルーム内クライアントがMasterClientと同じシーンをロードするように設定
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        #endregion
        #region Publick Methods
        public void GetRooms(List<RoomInfo> roomInfo)
        {
            //ルームがなければreturn
            if (roomInfo == null || roomInfo.Count == 0) return;
            //ルームがあればRoomElementでそれぞれのルーム情報を表示
            for (int i = 0; i < roomInfo.Count; i++)
            {
                Debug.LogError(roomInfo[i].Name + ":" + roomInfo[i].Name + "-" + roomInfo[i].PlayerCount + "/" + roomInfo[i].MaxPlayers);
                //ルーム情報表示用RoomElementを生成
                GameObject RoomElement = GameObject.Instantiate(RoomElementPrefab);
                //RoomElementをcontentの子オブジェクトとしてセット
                RoomElement.transform.SetParent(RoomParent.transform);
                //RoomElementにルーム情報をセット
                RoomElement.GetComponent<CRoomElementScript>().SetRoomInfo(roomInfo[i].Name, roomInfo[i].PlayerCount, roomInfo[i].MaxPlayers, roomInfo[i].CustomProperties["RoomCreator"].ToString());
            }
        }
        //RoomElementを一括削除
        public static void DestroyChildObject(Transform parent_trans)
        {
            for (int i = 0; i < parent_trans.childCount; i++)
            {
                GameObject.Destroy(parent_trans.GetChild(i).gameObject);
            }
        }
        #endregion
        #region MonobihaviourPunCallbacks
        //GetRoomListは一定時間ごとに更新され、その更新のタイミングで実行する処理
        //ルームリストの更新があった時
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("OnRoomListUpdate");
            DestroyChildObject(RoomParent.transform);
            GetRooms(roomList);
        }
        //マスターサーバーへの接続が成功した時に呼ばれるコールバック
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }
        //ロビーに入ったときの処理
        public override void OnJoinedLobby()
        {
            Debug.Log("OnjoinedLobby");
        }
        //ルーム入室時の処理
        public override void OnJoinedRoom()
        {

        }
        //ルーム作成時の処理
        public override void OnCreatedRoom()
        {

        }
        #endregion
    }

}