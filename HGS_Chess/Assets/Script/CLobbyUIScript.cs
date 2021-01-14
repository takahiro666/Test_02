using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CLobbyUIScript : MonoBehaviourPunCallbacks
{
    //部屋作成ウィンドウ表示用ボタン
    public Button OpenRoomPanelButton;
    //部屋作成ウィンドウ
    public GameObject CreateRoomPanel;//部屋作成ウィンドウ
    public Text RoomNameText;//作成する部屋名
    public Slider PlayerNumberSlider;//最大入室可能人数用Slider
    public Text PlayerNumberText;//最大入室可能人数表示用Text
    public Button CreateRoomButton;//部屋作成ボタン

    void Start()
    {
        OpenRoomPanelButton = GameObject.Find("OpenRoomPanelButton").GetComponent<Button>();
    }

    void Update()
    {
        //部屋人数sliderの値をTextに代入
        PlayerNumberText.text = PlayerNumberSlider.value.ToString();
    }
    //部屋作成ウィンドウ表示湯尾ボタンを押したときの処理
    public void OnClick_OpenRoomPanelButton()
    {
        //部屋作成ウィンドウを表示していれば
        if (CreateRoomPanel.activeSelf)
        {
            //部屋作成ウィンドウを非表示に
            CreateRoomPanel.SetActive(false);
        }
        else //そうでなければ
        {
            //部屋作成ウィンドウを表示
            CreateRoomPanel.SetActive(true);
        }
    }
    //部屋作成ボタンを押したときの処理
    public void OnClick_CreateRoomButton()
    {
        //部屋を作成するときの設定
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true; //ロビーで見える部屋にする
        roomOptions.IsOpen = true;//他のプレイヤーの入室を許可する
        roomOptions.MaxPlayers = (byte)PlayerNumberSlider.value;//入室可能人数を設定
        //ルームカスタムプロパティで部屋作成者を表示させるため、作成者の名前を格納
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            { "RoomCreator",PhotonNetwork.NickName }
        };
        //ロビーにカスタムプロパティの情報を表示させる
        roomOptions.CustomRoomPropertiesForLobby = new string[]
        {
            "RoomCreator",
        };
        //部屋名がなければデフォルトの部屋名を設定
        if (string.IsNullOrEmpty(RoomNameText.text))
        {
            RoomNameText.text = "MyRoom";
        }
        //部屋作成
        PhotonNetwork.CreateRoom(RoomNameText.text, roomOptions, null);
    }
}
