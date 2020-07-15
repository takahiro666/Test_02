using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayA : MonoBehaviour
{
    RaycastHit hit = new RaycastHit();
    RaycastHit crntHit = new RaycastHit();
    Ray ray;
    GameObject ClickedGameobject;
    GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        Ray();
        Rayhitobj();
        //if (hit.collider.gameObject != crntHit.collider.gameObject)
        //{
        //    hit = crntHit;
        //}
    }
    //=================================================================
    void Ray()
    {
        if (!Camera.main)//メインカメラがあるかどうか
            return;

        //カメラ上のマウスの位置にレイを作成
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //例がコライダーにあった場合情報を格納する
        Physics.Raycast(ray, out crntHit, 100f);
    }
    //===============================================================================================
    void Rayhitobj()
    {
          
        if (Physics.Raycast(ray, out hit, 100f))
        {
            ClickedGameobject = null;
            if (hit.collider.gameObject.GetComponent<changecolor>())
            {
                //作成したレイがコライダーに当たった場合true
                hit.collider.gameObject.GetComponent<changecolor>().selectflg = true;
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 ClickedGameobject = hit.collider.gameObject.transform.position;
                    Debug.Log(ClickedGameobject);
                   

                }
            }
        }
    }  
   //==================================================================================================
}
