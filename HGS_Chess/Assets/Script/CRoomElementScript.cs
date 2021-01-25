﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CRoomElementScript : MonoBehaviour
{
    //Room情報UI表示用
    public Text RoomName; //部屋名
    public Text PlayerNumber;//人数
    public Text RoomCreater;//部屋作成者名
    //入室ボタンroomname格納用
    private string roomname;
    //GetRoomListからRoomElementにセットしていく為の関数
    public void SetRoomInfo(string _RoomName,int _PlayerNumber,int _MaxPlayer,string _RomCreater)
    {
        //入室ボタン用roomName取得
        roomname = _RoomName;
        RoomName.text = "部屋名 :" + _RoomName;
        PlayerNumber.text = "人数 :" + _PlayerNumber + "/" + _MaxPlayer;
        RoomCreater.text = "作成者 :" + RoomCreater;
    }
    //入室ボタン処理
    public void OnjoinRoomButton()
    {
        //roomnameの部屋に入室
        PhotonNetwork.JoinRoom(roomname);
    }
}
