using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_trun : MonoBehaviour
{
    public Text Qtext;
    float a_color;
    public bool flag_G;
    Pice tr;
    public float time = 0.01f;
    // Use this for initialization
    void Start()
    {
        //no_clic.SetActive(false);
        tr = GameObject.Find("gamelot").GetComponentInChildren<Pice>();
        a_color = 0;
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
            a_color -= time;
        else
            a_color += time;

        if (a_color < 0)
        {
            a_color = 0;
            flag_G = false;
            Qtext.enabled = false;//非表示
        }
        else if (a_color > 1)
        {
            a_color = 1;
            flag_G = true;
        }
        
    }
   
}
