using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_trun : MonoBehaviour
{
    public Text Qtext;
    float a_color;
    public bool flag_G;
    Bord tr;
    // Use this for initialization
    void Start()
    {
        tr = GameObject.Find("gamelot").GetComponentInChildren<Bord>();
        a_color = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
        


    }
    public void Change()
    {
        //テキストの透明度を変更する
        if (tr.trun % 2 != 0)
        {
            Qtext.color = new Color(1, 0, 0, a_color);
        }

        else
        {
            Qtext.color = new Color(0, 0, 1, a_color);
        }     
        if (flag_G)
            a_color -= Time.deltaTime;
        else
            a_color += Time.deltaTime;
        if (a_color < 0)
        {
            a_color = 0;
            flag_G = false;
            Qtext.enabled = false;
        }
        else if (a_color > 1)
        {
            a_color = 1;
            flag_G = true;
        }
    }
    
}
