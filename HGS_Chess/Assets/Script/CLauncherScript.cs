using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CLauncherScript : MonoBehaviourPunCallbacks
{
    public void Connect()
    {
        //Photonに接続できていなければ
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Photonに接続しました。");
            if (string.IsNullOrEmpty(PhotonNetwork.NickName))
            {
                PhotonNetwork.NickName = "Player" + Random.Range(1, 9999);
            }
            SceneManager.LoadScene("");
        }
    }
    void OnGUI()
    {
        //ログイン状態を画面に表示
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}
