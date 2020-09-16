using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_cost : MonoBehaviour
{
    public Text Player1_cos;
    public Text Player2_cos;
    public  int P1_cos;//プレイヤー1のコスト
    public int P2_cos;  //プレイヤー2のコスト
    private int max = 5;
    Bord trn;
    // Start is called before the first frame update
    void Start()
    {
        trn = GameObject.Find("gamelot").GetComponentInChildren<Bord>();
        Player1_cos=GameObject.Find("Player1_cost").GetComponent<Text>();
        Player2_cos= GameObject.Find("Player2_cost").GetComponent<Text>();
    }

    // Update is called once per frame
   public void PCost()
    {
        if (trn.trun % 2 != 0)//プレイヤー２のコスト加算処理
        {
            if(P2_cos<max)
            {
                P2_cos++;
                Player2_cos.text = P2_cos.ToString();
            }
            else
                Player2_cos.text = P2_cos.ToString();
        }
        else//プレイヤー1のコスト加算処理
        {
            if(P1_cos <max)
            {
                P1_cos++;
                Player1_cos.text = P1_cos.ToString();
            }
            else
                Player1_cos.text = P1_cos.ToString();
        }
            
    }
}
