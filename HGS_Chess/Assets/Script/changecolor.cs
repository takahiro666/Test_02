//materialの色を変化させる
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changecolor : MonoBehaviour
{
    public bool selectflg;      //マウスカーソルが触れたら色を変える
    

    //選択時に色変更
    private Color default_color;
    private Color select_color;

    //色変更するオブジェクトのmaterial格納
    protected Material mat;
    // Start is called before the first frame update
    void Start()
    {
        //フラグ、色、マテリアルの初期化
        selectflg = false;
        
        
       
        select_color = Color.yellow;
        mat = gameObject.GetComponent<Renderer>().material;
        default_color = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
       
        mat.color = default_color;
        //フラグがtrueの時
        if (selectflg)
        {
            selectflg = false;
            mat.color = select_color;   

        }    
        
    }
}
