using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bord : MonoBehaviour
{
    public GameObject Bord_W;
    public GameObject Bord_B;
    public GameObject Wall_a;
    //public GameObject Wall_b;
    public bool select_flg;
    public int[,] Chessbord = new int[10, 10]
    {
        {2,2,2,2,2,2,2,2,2,2},
        {2,0,1,0,1,0,1,0,1,2},
        {2,1,0,1,0,1,0,1,0,2},
        {2,0,1,0,1,0,1,0,1,2},
        {2,1,0,1,0,1,0,1,0,2},
        {2,0,1,0,1,0,1,0,1,2},
        {2,1,0,1,0,1,0,1,0,2},
        {2,0,1,0,1,0,1,0,1,2},
        {2,1,0,1,0,1,0,1,0,2},
        {2,2,2,2,2,2,2,2,2,2},

        //{0,1,0,1,0,1,0,1},
        //{1,0,1,0,1,0,1,0},
        //{0,1,0,1,0,1,0,1},
        //{1,0,1,0,1,0,1,0},
        //{0,1,0,1,0,1,0,1},
        //{1,0,1,0,1,0,1,0},
        //{0,1,0,1,0,1,0,1},
        //{1,0,1,0,1,0,1,0},
    };
    void Start()
    {
        //Bordの生成
        for(int i = 0; i <Chessbord.GetLength(0); i++)
        {
            for (int j = 0; j < Chessbord.GetLength(1); j++)
            {
                if (Chessbord[i, j] == 0)
                {
                    Instantiate(Bord_B, new Vector3(i, 0, j), Quaternion.identity);
                    Bord_B.name = i + "-" + j.ToString();
                }
                else if (Chessbord[i, j] == 1)
                {
                    Instantiate(Bord_W, new Vector3(i, 0, j), Quaternion.identity);
                    Bord_W.name = i + "-" + j.ToString();
                }
                else
                {
                    Instantiate(Wall_a, new Vector3(i, 0, j), Quaternion.identity);
                    //Instantiate(Wall_b, new Vector3(i, 0, j), Quaternion.identity);
                    Wall_a.name = Wall_a.ToString();
                }
            }
        }
    }
    //ゲーム終了
    void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Application.Quit();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Quit();
    }
}
